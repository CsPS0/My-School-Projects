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

function scanRepoEntries() {
  console.log(`[tree-gen] Scanning: ${repoRoot}`);
  const startTime = Date.now();

  const raw = execSync('git -c core.quotepath=false ls-files -z --cached --others --exclude-standard', {
    cwd: repoRoot,
    encoding: 'utf8',
    maxBuffer: 64 * 1024 * 1024
  });

  const files = raw.split('\0').filter(Boolean);
  const entries = [];
  const dirs = new Set();
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
    try {
      const stat = fs.statSync(fullPath);
      if (stat.isDirectory()) {
        dirs.add(filePath);
        continue;
      }
      size = stat.size;
    } catch (e) {
      continue;
    }

    entries.push([filePath, false, size]);
    fileCount++;

    // Every parent folder of a file is a directory entry.
    const segments = filePath.split('/');
    for (let i = 1; i < segments.length; i++) {
      dirs.add(segments.slice(0, i).join('/'));
    }
  }

  for (const dir of dirs) {
    entries.push([dir, true, 0]);
  }

  entries.sort((a, b) => a[0].localeCompare(b[0]));
  const elapsed = Date.now() - startTime;
  console.log(`[tree-gen] ${fileCount} files, ${dirs.size} dirs (${entries.length} items, ${elapsed}ms)`);
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
      normalized.push([String(item[0] || ''), Boolean(item[1]), Number(item[2] || 0)]);
    } else if (typeof item === 'object') {
      normalized.push([String(item.path || ''), Boolean(item.isDir), Number(item.size || 0)]);
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
