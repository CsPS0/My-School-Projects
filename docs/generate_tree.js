#!/usr/bin/env node

const fs = require('fs');
const path = require('path');
const { execSync } = require('child_process');

function getRepoRoot() {
  try {
    const gitRoot = execSync('git rev-parse --show-toplevel', {
      encoding: 'utf8',
      stdio: ['ignore', 'pipe', 'ignore']
    }).trim();
    if (gitRoot && fs.existsSync(gitRoot)) return gitRoot;
  } catch (e) {}
  return path.resolve(__dirname, '..');
}

const repoRoot = getRepoRoot();
const treeJsonPath = path.join(repoRoot, 'docs', 'tree.json');
const treeJsPath = path.join(repoRoot, 'docs', 'js', 'tree-data.js');
const indexPath = path.join(repoRoot, 'docs', 'index.html');
const gitHookPath = path.join(repoRoot, '.git', 'hooks', 'pre-commit');

function formatTreeJson(entries) {
  const lines = entries.map(e => '  ' + JSON.stringify(e));
  return '[\n' + lines.join(',\n') + '\n]\n';
}

function getTimestampVersion() {
  const d = new Date();
  const pad = n => String(n).padStart(2, '0');
  return '' + d.getFullYear() + pad(d.getMonth() + 1) + pad(d.getDate()) + '_' + pad(d.getHours()) + pad(d.getMinutes());
}

function updateIndexCacheVersion(targetVersion) {
  if (!fs.existsSync(indexPath)) return null;
  let html = fs.readFileSync(indexPath, 'utf8');
  const v = targetVersion || getTimestampVersion();
  const regex = /(<script\b[^>]*\bsrc="js\/tree-data\.js)(?:\?v=[^"]*)?(")/;
  if (regex.test(html)) {
    const updated = html.replace(regex, `$1?v=${v}$2`);
    if (updated !== html) {
      fs.writeFileSync(indexPath, updated, 'utf8');
      return v;
    }
  }
  return null;
}

// Last change time (unix seconds) per file: the newest commit that changed its content.
// Pure renames/moves (100% similarity) are not counted as a change: the date is taken
// from the last real edit under the old path. Files with uncommitted changes (or
// untracked ones) use their filesystem mtime instead.
function getLastChangedMap() {
  const map = new Map();
  try {
    const log = execSync('git -c core.quotepath=false log -M --format=@@%ct --name-status', {
      cwd: repoRoot,
      encoding: 'utf8',
      maxBuffer: 256 * 1024 * 1024
    });
    const alias = new Map(); // older path -> current path, from pure renames
    const resolve = p => alias.get(p) || p;
    const mark = (p, ts) => { const cur = resolve(p); if (!map.has(cur)) map.set(cur, ts); };
    let ts = 0;
    for (const line of log.split('\n')) {
      if (line.startsWith('@@')) { ts = Number(line.slice(2)) || 0; continue; }
      if (!line) continue;
      const [status, a, b] = line.split('\t');
      if (status === 'R100') {
        // History is walked newest first, so older commits on `a` belong to the renamed file.
        if (!alias.has(a)) alias.set(a, resolve(b));
      } else if (status[0] === 'R' || status[0] === 'C') {
        mark(b, ts);
      } else if (status !== 'D') {
        mark(a, ts);
      }
    }
  } catch (e) {}

  try {
    const status = execSync('git -c core.quotepath=false status --porcelain -z --untracked-files=all', {
      cwd: repoRoot,
      encoding: 'utf8',
      maxBuffer: 64 * 1024 * 1024
    });
    const parts = status.split('\0');
    for (let i = 0; i < parts.length; i++) {
      const rec = parts[i];
      if (rec.length < 4) continue;
      const code = rec.slice(0, 2);
      const file = rec.slice(3);
      if (code[0] === 'R' || code[0] === 'C') i++; // skip the original path of a rename/copy
      if (code.includes('D')) continue;
      map.set(file, null);
    }
  } catch (e) {}

  return map;
}

function scanRepoEntries() {
  console.log(`[tree-gen] Scanning: ${repoRoot}`);
  const startTime = Date.now();

  const raw = execSync('git -c core.quotepath=false ls-files -z --cached --others --exclude-standard', {
    cwd: repoRoot,
    encoding: 'utf8',
    maxBuffer: 64 * 1024 * 1024
  });

  const files = raw.split('\0').filter(Boolean);
  const lastChanged = getLastChangedMap();
  const entries = [];
  const dirMtime = new Map();
  let fileCount = 0;

  for (const filePath of files) {
    if (
      filePath === 'docs/tree.json' ||
      filePath === 'docs/js/tree-data.js' ||
      filePath === 'docs/generate_tree.js'
    ) {
      continue;
    }

    const fullPath = path.join(repoRoot, filePath);
    let size = 0;
    let mtime = 0;
    try {
      const stat = fs.statSync(fullPath);
      if (stat.isDirectory()) {
        if (!dirMtime.has(filePath)) dirMtime.set(filePath, 0);
        continue;
      }
      size = stat.size;
      const committed = lastChanged.get(filePath);
      mtime = typeof committed === 'number' ? committed : Math.floor(stat.mtimeMs / 1000);
    } catch (e) {
      continue;
    }

    entries.push([filePath, false, size, mtime]);
    fileCount++;

    // A directory's last change is the newest change of any file inside it.
    const segments = filePath.split('/');
    for (let i = 1; i < segments.length; i++) {
      const dir = segments.slice(0, i).join('/');
      if (!(dirMtime.get(dir) >= mtime)) dirMtime.set(dir, mtime);
    }
  }

  for (const [dir, mtime] of dirMtime) {
    entries.push([dir, true, 0, mtime]);
  }

  entries.sort((a, b) => a[0].localeCompare(b[0]));
  const elapsed = Date.now() - startTime;
  console.log(`[tree-gen] ${fileCount} files, ${dirMtime.size} dirs (${entries.length} items, ${elapsed}ms)`);
  return entries;
}

function generateFromGit() {
  const entries = scanRepoEntries();

  const formattedJson = formatTreeJson(entries);
  fs.writeFileSync(treeJsonPath, formattedJson, 'utf8');
  const jsonKb = Math.round(Buffer.byteLength(formattedJson) / 1024);
  console.log(`[tree-gen] Wrote ${path.relative(repoRoot, treeJsonPath)} (${jsonKb} KB)`);

  const jsContent = 'window.REPO_TREE = ' + JSON.stringify(entries) + ';\n';
  fs.writeFileSync(treeJsPath, jsContent, 'utf8');
  console.log(`[tree-gen] Wrote ${path.relative(repoRoot, treeJsPath)}`);

  const newVer = updateIndexCacheVersion();
  if (newVer) {
    console.log(`[tree-gen] Updated cache buster in ${path.relative(repoRoot, indexPath)} (?v=${newVer})`);
  }

  console.log(`[tree-gen] Done.`);
}

function syncFromJson() {
  console.log(`[tree-gen] Reading ${path.relative(repoRoot, treeJsonPath)}...`);
  if (!fs.existsSync(treeJsonPath)) {
    throw new Error(`File not found: ${treeJsonPath}`);
  }

  const raw = fs.readFileSync(treeJsonPath, 'utf8');
  const parsed = JSON.parse(raw);
  if (!Array.isArray(parsed)) {
    throw new Error('docs/tree.json must contain a JSON array');
  }

  const normalized = [];
  for (const item of parsed) {
    if (!item) continue;
    if (Array.isArray(item)) {
      normalized.push([String(item[0] || ''), Boolean(item[1]), Number(item[2] || 0), Number(item[3] || 0)]);
    } else if (typeof item === 'object') {
      normalized.push([String(item.path || ''), Boolean(item.isDir), Number(item.size || 0), Number(item.mtime || 0)]);
    }
  }

  normalized.sort((a, b) => a[0].localeCompare(b[0]));

  const formattedJson = formatTreeJson(normalized);
  fs.writeFileSync(treeJsonPath, formattedJson, 'utf8');
  console.log(`[tree-gen] Formatted ${path.relative(repoRoot, treeJsonPath)} (${normalized.length} items)`);

  const jsContent = 'window.REPO_TREE = ' + JSON.stringify(normalized) + ';\n';
  fs.writeFileSync(treeJsPath, jsContent, 'utf8');
  console.log(`[tree-gen] Synced ${path.relative(repoRoot, treeJsPath)}`);

  const newVer = updateIndexCacheVersion();
  if (newVer) {
    console.log(`[tree-gen] Updated cache buster in ${path.relative(repoRoot, indexPath)} (?v=${newVer})`);
  }

  console.log(`[tree-gen] Done.`);
}

function installGitHook() {
  const hookScript = `#!/bin/sh
node docs/generate_tree.js
git add docs/tree.json docs/js/tree-data.js docs/index.html
`;

  try {
    const hooksDir = path.join(repoRoot, '.git', 'hooks');
    if (!fs.existsSync(hooksDir)) {
      fs.mkdirSync(hooksDir, { recursive: true });
    }
    fs.writeFileSync(gitHookPath, hookScript, { encoding: 'utf8', mode: 0o755 });
    try { fs.chmodSync(gitHookPath, 0o755); } catch (e) {}

    console.log(`[tree-gen] Git pre-commit hook installed: ${gitHookPath}`);
  } catch (err) {
    console.log(`Note: Could not write directly to .git/hooks/.`);
    console.log(`To enable auto updates on commit, run: git config core.hooksPath .githooks`);
  }
}

function startWatchMode() {
  generateFromGit();
  console.log(`[tree-gen] Watching for changes (Ctrl+C to stop)...`);

  let debounceTimer = null;
  fs.watch(repoRoot, { recursive: true }, (eventType, filename) => {
    if (!filename) return;
    if (
      filename.startsWith('.git') ||
      filename.includes('node_modules') ||
      filename.startsWith('docs/tree.json') ||
      filename.startsWith('docs/js/tree-data.js') ||
      filename.startsWith('docs/index.html')
    ) {
      return;
    }

    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(() => {
      console.log(`[tree-gen] Changed: ${filename}, regenerating...`);
      try {
        generateFromGit();
      } catch (err) {
        console.error(`[tree-gen] Error: ${err.message}`);
      }
    }, 300);
  });
}

function printHelp() {
  console.log(`
Usage: node docs/generate_tree.js [option]

Options:
  (no option)       Scan repo and update tree.json, tree-data.js, and index.html
  --sync            Sync tree-data.js & index.html from edits in tree.json
  --install-hook    Install git pre-commit hook
  --watch           Watch for file changes and auto-regenerate
  --help, -h        Show help
`);
}

const arg = process.argv[2];
if (arg === '--help' || arg === '-h') {
  printHelp();
} else if (arg === '--install-hook') {
  installGitHook();
} else if (arg === '--sync' || arg === '--from-json') {
  syncFromJson();
} else if (arg === '--watch') {
  startWatchMode();
} else {
  generateFromGit();
}
