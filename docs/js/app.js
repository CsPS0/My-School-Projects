const REPO = 'CsPS0/My-School-Projects', BRANCH = 'main', PAGE = 40;
const $ = id => document.getElementById(id);
const fold = s => s ? s.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase() : '';
const esc = s => s ? String(s).replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c])) : '';

const MONTHS = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
const BINARY_EXTS = new Set(['zip', '7z', 'rar', 'tar', 'gz', 'bz2', 'iso', 'exe', 'dll', 'bin', 'pdb', 'obj']);

// Crisp, modern SVG icons (VS Code / GitHub style)
const SVG_ICONS = {
  chevron_down: `<svg class="w-3.5 h-3.5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><polyline points="6 9 12 15 18 9"/></svg>`,
  chevron_right: `<svg class="w-3.5 h-3.5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><polyline points="9 18 15 12 9 6"/></svg>`,
  folder: `<svg class="w-4 h-4 text-amber-500 fill-amber-500/20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M20 20a2 2 0 0 0 2-2V8a2 2 0 0 0-2-2h-7.9a2 2 0 0 1-1.69-.9L9.6 3.9A2 2 0 0 0 8 3H4a2 2 0 0 0-2 2v13a2 2 0 0 0 2 2Z"/></svg>`,
  folder_open: `<svg class="w-4 h-4 text-amber-500 fill-amber-500/30" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m6 14 1.5-2.9A2 2 0 0 1 9.24 10H20a2 2 0 0 1 1.94 2.5l-1.54 6a2 2 0 0 1-1.95 1.5H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h3.9a2 2 0 0 1 1.69.9l.81 1.2a2 2 0 0 0 1.67.9H18a2 2 0 0 1 2 2v2"/></svg>`,
  cs: `<svg class="w-4 h-4 text-purple-600 dark:text-purple-400" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="16 18 22 12 16 6"/><polyline points="8 6 2 12 8 18"/><line x1="12" x2="12" y1="4" y2="20"/></svg>`,
  sln: `<svg class="w-4 h-4 text-purple-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="18" height="18" rx="2"/><path d="m9 12 2 2 4-4"/></svg>`,
  csproj: `<svg class="w-4 h-4 text-blue-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/></svg>`,
  sql: `<svg class="w-4 h-4 text-amber-600 dark:text-amber-400" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><ellipse cx="12" cy="5" rx="9" ry="3"/><path d="M3 5v14c0 1.66 4 3 9 3s9-1.34 9-3V5"/><path d="M3 12c0 1.66 4 3 9 3s9-1.34 9-3"/></svg>`,
  dia: `<svg class="w-4 h-4 text-emerald-600 dark:text-emerald-400" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="7" height="7"/><rect x="14" y="3" width="7" height="7"/><rect x="14" y="14" width="7" height="7"/><rect x="3" y="14" width="7" height="7"/><path d="M10 6.5h4M6.5 10v4M17.5 10v4M10 17.5h4"/></svg>`,
  php: `<svg class="w-4 h-4 text-indigo-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><path d="m8 12 2-2 2 2-2 2-2-2Z"/><path d="m14 12 2-2 2 2-2 2-2-2Z"/></svg>`,
  js: `<svg class="w-4 h-4 text-yellow-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="18" height="18" x="3" y="3" rx="2"/><path d="M9 16.5v-3a1.5 1.5 0 0 0-3 0v.5"/><path d="M14 16.5v-7"/></svg>`,
  html: `<svg class="w-4 h-4 text-orange-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="16 18 22 12 16 6"/><polyline points="8 6 2 12 8 18"/></svg>`,
  css: `<svg class="w-4 h-4 text-sky-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m12 2 8 3-2 15-6 2-6-2-2-15 8-3Z"/></svg>`,
  json: `<svg class="w-4 h-4 text-teal-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M8 3H6a2 2 0 0 0-2 2v3a2 2 0 0 1-2 2 2 2 0 0 1 2 2v3a2 2 0 0 0 2 2h2"/><path d="M16 3h2a2 2 0 0 1 2 2v3a2 2 0 0 0 2 2 2 2 0 0 0-2 2v3a2 2 0 0 1-2 2h-2"/></svg>`,
  md: `<svg class="w-4 h-4 text-cyan-600 dark:text-cyan-400" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><path d="M14 2v6h6"/><line x1="16" x2="8" y1="13" y2="13"/><line x1="16" x2="8" y1="17" y2="17"/><line x1="10" x2="8" y1="9" y2="9"/></svg>`,
  pdf: `<svg class="w-4 h-4 text-red-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/><path d="M9 15v-4h2a1.5 1.5 0 0 1 0 3H9"/></svg>`,
  zip: `<svg class="w-4 h-4 text-amber-600 dark:text-amber-400" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="18" height="18" x="3" y="3" rx="2"/><path d="M10 3v18M14 3v18M10 7h4M10 11h4M10 15h4"/></svg>`,
  zip_large: `<svg class="w-10 h-10" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="18" height="18" x="3" y="3" rx="2"/><path d="M10 3v18M14 3v18M10 7h4M10 11h4M10 15h4"/></svg>`,
  img: `<svg class="w-4 h-4 text-emerald-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="18" height="18" x="3" y="3" rx="2" ry="2"/><circle cx="9" cy="9" r="2"/><path d="m21 15-3.086-3.086a2 2 0 0 0-2.828 0L6 21"/></svg>`,
  file: `<svg class="w-4 h-4 text-zinc-400" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/></svg>`,
  download: `<svg class="w-3.5 h-3.5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg>`,
  github: `<svg class="w-3.5 h-3.5" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M15 3h6v6"/><path d="M10 14 21 3"/><path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"/></svg>`
};

function getFileSvg(ext) {
  if (['cs'].includes(ext)) return SVG_ICONS.cs;
  if (['sln', 'slnx'].includes(ext)) return SVG_ICONS.sln;
  if (['csproj'].includes(ext)) return SVG_ICONS.csproj;
  if (['sql'].includes(ext)) return SVG_ICONS.sql;
  if (['dia'].includes(ext)) return SVG_ICONS.dia;
  if (['php'].includes(ext)) return SVG_ICONS.php;
  if (['js', 'mjs', 'ts'].includes(ext)) return SVG_ICONS.js;
  if (['html', 'xml', 'xaml', 'vue'].includes(ext)) return SVG_ICONS.html;
  if (['css'].includes(ext)) return SVG_ICONS.css;
  if (['json'].includes(ext)) return SVG_ICONS.json;
  if (['md'].includes(ext)) return SVG_ICONS.md;
  if (['pdf'].includes(ext)) return SVG_ICONS.pdf;
  if (['zip', '7z', 'rar', 'tar', 'gz', 'bz2'].includes(ext)) return SVG_ICONS.zip;
  if (['png', 'jpg', 'jpeg', 'svg', 'webp', 'gif', 'ico'].includes(ext)) return SVG_ICONS.img;
  return SVG_ICONS.file;
}

let rawTree = [];
let allItems = [];
let shownCount = PAGE;
let favs = new Set();
let currentProject = null;
let currentFilePath = null;
let currentCodeText = '';
const fileCache = new Map();
const collapsedFolders = new Set();

try { favs = new Set(JSON.parse(localStorage.favs || '[]')); } catch {}

// Update Highlight.js theme dynamically
function updateHljsTheme(isDark) {
  const themeLink = $('hljs-theme');
  if (themeLink) {
    themeLink.href = isDark
      ? 'https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.11.1/styles/github-dark.min.css'
      : 'https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.11.1/styles/github.min.css';
  }
}

// Ultra-fast tree loader: check preloaded window.REPO_TREE first (0ms load)
async function loadTree() {
  if (window.REPO_TREE && Array.isArray(window.REPO_TREE)) {
    return window.REPO_TREE;
  }

  // Strategy 2: local fetch
  try {
    const res = await fetch('tree.json');
    if (res.ok) return await res.json();
  } catch {}

  // Strategy 3: GitHub API fallback
  const key = 'tree:' + REPO;
  try {
    const cached = JSON.parse(sessionStorage.getItem(key));
    if (cached) return cached;
  } catch {}

  const r = await fetch(`https://api.github.com/repos/${REPO}/git/trees/${BRANCH}?recursive=1`);
  if (!r.ok) throw new Error(r.status === 403 ? 'GitHub API rate limit hit, try again later.' : 'GitHub API error ' + r.status);
  const json = await r.json();
  const tree = json.tree.map(t => [t.path, t.type === 'tree', t.size || 0]);
  try { sessionStorage.setItem(key, JSON.stringify(tree)); } catch {}
  return tree;
}

// Normalize metadata from path
function parseItem([path, isDir, size]) {
  const parts = path.split('/');
  const name = parts[parts.length - 1];

  let grade = '';
  let subject = '';
  let isProject = false;

  if (parts[0] === 'NEU') {
    if (parts[1] === 'Osztaly13') {
      grade = '13. osztály';
      if (parts[2] === 'AKK') {
        const sub = parts[3] || 'OTHER';
        subject = sub === 'BACKEND&FRONTEND' ? 'BACKEND' : sub.toUpperCase();
        if (isDir && parts.length === 5) isProject = true;
      } else {
        subject = (parts[2] || 'OTHER').toUpperCase();
        if (isDir && parts.length === 4) isProject = true;
      }
    } else if (parts[1] === 'Osztaly12') {
      grade = '12. osztály';
      subject = (parts[2] || 'OTHER').toUpperCase();
      if (isDir && parts.length === 4) isProject = true;
    } else if (parts[1] === 'Osztaly11') {
      grade = '11. osztály';
      subject = (parts[2] || 'OTHER').toUpperCase();
      if (isDir && parts.length === 4) isProject = true;
    }
  } else if (parts[0] === 'Verebay') {
    grade = 'Verebay';
    subject = (parts[2] || 'OTHER').toUpperCase();
    if (isDir && parts.length === 4) isProject = true;
  }

  // Detect dated project directories e.g. 26-1012-1018_... or 24-0916-0922
  const m = name.match(/^(\d{2})-(\d{2})(\d{2})/);
  const date = m ? `20${m[1]} ${MONTHS[+m[2] - 1] || ''} ${+m[3]}` : '';
  if (isDir && m) isProject = true;

  // Clean subject display
  if (subject === 'WEB') subject = 'WEB';
  if (subject === 'DESKTOP') subject = 'DESKTOP';
  if (subject === 'DATABASE') subject = 'DATABASE';
  if (subject === 'TESTING') subject = 'TESTING';
  if (subject === 'BACKEND') subject = 'BACKEND';
  if (subject === 'DOCKER') subject = 'DOCKER';

  return {
    path,
    name,
    isDir,
    size: size || 0,
    grade,
    subject,
    date,
    sort: m ? m[1] + m[2] + m[3] : '',
    project: isProject,
    hay: fold(path.replace(/[_\-\/.]/g, ' '))
  };
}

const urlOf = path => `https://github.com/${REPO}/${path.includes('.') ? 'blob' : 'tree'}/${BRANCH}/${path.split('/').map(encodeURIComponent).join('/')}`;
const rawUrlOf = path => `https://raw.githubusercontent.com/${REPO}/${BRANCH}/${path.split('/').map(encodeURIComponent).join('/')}`;

function highlight(text, words) {
  if (!words || !words.length) return esc(text);
  const f = fold(text), marks = new Array(text.length).fill(false);
  for (const w of words) {
    for (let i = f.indexOf(w); i !== -1; i = f.indexOf(w, i + 1)) {
      for (let j = i; j < i + w.length; j++) marks[j] = true;
    }
  }
  let out = '';
  for (let i = 0; i < text.length; i++) {
    if (marks[i] && !marks[i - 1]) out += '<mark>';
    out += esc(text[i]);
    if (marks[i] && !marks[i + 1]) out += '</mark>';
  }
  return out;
}

const SORTS = {
  new: (a, b) => (b.project - a.project) || b.sort.localeCompare(a.sort) || a.path.localeCompare(b.path),
  old: (a, b) => (b.project - a.project) || (!a.sort - !b.sort) || a.sort.localeCompare(b.sort) || a.path.localeCompare(b.path),
  name: (a, b) => a.name.localeCompare(b.name, 'hu'),
};

function filtered() {
  const q = $('q').value.trim();
  const grade = $('grade').value;
  const subject = $('subject').value;
  const words = fold(q.replace(/[_\-\/.]/g, ' ')).split(/\s+/).filter(Boolean);
  const withFiles = $('files').checked;
  const favOnly = $('fav-only').checked;

  const hits = allItems.filter(it => {
    if (favOnly && !favs.has(it.path)) return false;
    if (!withFiles && !it.project) return false;
    if (grade && it.grade !== grade) return false;
    if (subject && it.subject !== subject) return false;
    if (words.length && !words.every(w => it.hay.includes(w))) return false;
    return true;
  });

  return { hits: hits.sort(SORTS[$('sort').value]), words, q };
}

function getProjectFiles(projectPath) {
  const prefix = projectPath.endsWith('/') ? projectPath : projectPath + '/';
  return allItems.filter(it => !it.isDir && it.path.startsWith(prefix));
}

// Render Project / File Cards (Clean, concise, no huge root path)
function render(reset = true) {
  if (reset) shownCount = PAGE;
  const { hits, words, q } = filtered();
  
  $('status').textContent = `${hits.length} result${hits.length === 1 ? '' : 's'}` + (hits.length > shownCount ? ` (showing ${shownCount})` : '');
  $('more').classList.toggle('hidden', hits.length <= shownCount);

  $('results').innerHTML = hits.slice(0, shownCount).map(it => {
    const isFile = !it.isDir;
    const isFav = favs.has(it.path);
    const ext = it.name.includes('.') ? it.name.split('.').pop().toLowerCase() : '';
    const fileSvg = isFile ? getFileSvg(ext) : SVG_ICONS.folder;
    const subjectTagClass = `tag-${(it.subject || '').toLowerCase()}`;

    // Compute file stats for project
    let projectStatsBadge = '';
    if (!isFile) {
      const files = getProjectFiles(it.path);
      const exts = new Set(files.map(f => f.name.split('.').pop().toLowerCase()).filter(e => ['cs', 'sql', 'php', 'js', 'html', 'dia'].includes(e)));
      const extList = Array.from(exts).slice(0, 3).map(e => e.toUpperCase()).join(' · ');
      projectStatsBadge = `<span class="tag bg-zinc-100 dark:bg-zinc-800 text-zinc-600 dark:text-zinc-400">${files.length} files ${extList ? `· ${extList}` : ''}</span>`;
    }

    // Relative match path for file search only
    let matchSubline = '';
    if (isFile && words.length) {
      matchSubline = `<div class="text-xs text-zinc-400 font-mono mt-0.5 break-all">${highlight(it.name, words)}</div>`;
    }

    return `
    <li class="relative card group">
      <div class="flex items-center justify-between gap-3 pr-24">
        <div class="min-w-0 flex-1 cursor-pointer" data-action="${isFile ? 'open-file' : 'open-project'}" data-path="${esc(it.path)}">
          <div class="flex items-center gap-2 flex-wrap">
            <span class="shrink-0 flex items-center">${fileSvg}</span>
            <span class="font-bold text-[15px] text-zinc-900 dark:text-zinc-100 group-hover:text-blue-600 dark:group-hover:text-blue-400 break-words transition-colors">
              ${highlight(it.name, words)}
            </span>
            ${isFile 
              ? `<span class="tag text-[11px] text-blue-600 dark:text-blue-400">View file</span>` 
              : `<span class="tag text-[11px] bg-blue-50 text-blue-600 dark:bg-blue-950/60 dark:text-blue-300 font-medium">Browse Code</span>`}
          </div>
          ${matchSubline}
          <div class="flex flex-wrap items-center gap-1.5 mt-1.5">
            ${it.grade ? `<span class="tag font-semibold">${esc(it.grade)}</span>` : ''}
            ${it.subject ? `<span class="tag ${subjectTagClass} font-semibold">${esc(it.subject)}</span>` : ''}
            ${it.date ? `<span class="tag bg-stone-100 dark:bg-zinc-800 text-zinc-600 dark:text-zinc-400">${esc(it.date)}</span>` : ''}
            ${projectStatsBadge}
          </div>
        </div>

        <div class="absolute top-2.5 right-2.5 flex items-center gap-1 shrink-0">
          <button class="star ${isFav ? 'on' : ''}" data-fav="${esc(it.path)}" title="Add to favorites" aria-label="Favorite">
            <svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="m12 2 3.1 6.3 6.9 1-5 4.9 1.2 6.8-6.2-3.2-6.2 3.2L7 14.2 2 9.3l6.9-1z"/></svg>
          </button>
          <button class="star" data-copy="${esc(it.path)}" title="Copy link" aria-label="Copy link">
            <svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M10 13a5 5 0 0 0 7.5.5l3-3a5 5 0 0 0-7-7l-1.7 1.7"/><path d="M14 11a5 5 0 0 0-7.5-.5l-3 3a5 5 0 0 0 7 7l1.7-1.7"/></svg>
          </button>
          <a class="star inline-flex items-center" href="${urlOf(it.path)}" target="_blank" rel="noopener" title="Open on GitHub" aria-label="GitHub">
            <svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M15 3h6v6"/><path d="M10 14 21 3"/><path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"/></svg>
          </a>
        </div>
      </div>
    </li>`;
  }).join('');

  $('q-clear').classList.toggle('hidden', !$('q').value);
}

function formatBytes(bytes) {
  if (!bytes) return '0 B';
  const k = 1024, sizes = ['B', 'KB', 'MB', 'GB'];
  const i = Math.floor(Math.log(bytes) / Math.log(k));
  return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
}

async function fetchFileContent(filePath) {
  if (fileCache.has(filePath)) {
    return fileCache.get(filePath);
  }

  // Strategy 1: Local relative fetch
  try {
    const localRes = await fetch('../' + filePath);
    if (localRes.ok) {
      const text = await localRes.text();
      fileCache.set(filePath, text);
      return text;
    }
  } catch {}

  // Strategy 2: GitHub raw usercontent
  const rawUrl = rawUrlOf(filePath);
  const ghRes = await fetch(rawUrl);
  if (!ghRes.ok) throw new Error(`HTTP ${ghRes.status} (${ghRes.statusText})`);
  const text = await ghRes.text();
  fileCache.set(filePath, text);
  return text;
}

// Smart priority file picker when opening a project: NEVER pick .zip / binaries first!
function pickInitialFile(files) {
  if (!files || !files.length) return null;

  // 1. Primary code files: Program.cs, index.php, MainWindow.xaml.cs, App.xaml.cs
  const mainEntry = files.find(f => /Program\.cs$|index\.php$|MainWindow\.xaml\.cs$/i.test(f.name));
  if (mainEntry) return mainEntry;

  // 2. Readable code files (.cs, .sql, .php, .js, .html, .vue, .xaml)
  const codeFile = files.find(f => {
    const ext = f.name.split('.').pop().toLowerCase();
    return ['cs', 'sql', 'php', 'js', 'html', 'vue', 'xaml'].includes(ext);
  });
  if (codeFile) return codeFile;

  // 3. Documentation / config files (.md, .txt, .json, .yaml, .yml)
  const docFile = files.find(f => {
    const ext = f.name.split('.').pop().toLowerCase();
    return ['md', 'txt', 'json', 'yaml', 'yml'].includes(ext);
  });
  if (docFile) return docFile;

  // 4. Non-binary file
  const nonBinary = files.find(f => {
    const ext = f.name.split('.').pop().toLowerCase();
    return !BINARY_EXTS.has(ext);
  });
  if (nonBinary) return nonBinary;

  return files[0];
}

// Open and render a file in the modal viewer
async function viewFile(filePath) {
  currentFilePath = filePath;
  const item = allItems.find(i => i.path === filePath) || { path: filePath, name: filePath.split('/').pop(), size: 0 };
  const ext = item.name.includes('.') ? item.name.split('.').pop().toLowerCase() : '';

  // Clean relative breadcrumb within project (no root prefix!)
  let relBreadcrumb = item.name;
  if (currentProject && filePath.startsWith(currentProject)) {
    const pfxLen = currentProject.endsWith('/') ? currentProject.length : currentProject.length + 1;
    relBreadcrumb = filePath.substring(pfxLen).split('/').join(' / ');
  }

  // Update headers
  $('active-file-icon').innerHTML = getFileSvg(ext);
  $('active-filename').textContent = item.name;
  $('active-file-size').textContent = item.size ? `· ${formatBytes(item.size)}` : '';
  $('active-file-lines').textContent = '';
  $('active-file-lang').textContent = ext || 'text';
  $('code-dialog-breadcrumb').textContent = relBreadcrumb;
  $('code-github-link').href = urlOf(item.path);
  $('code-copy-text').textContent = 'Copy';
  $('md-toggle-btn').classList.add('hidden');

  // Highlight active item in sidebar tree
  document.querySelectorAll('#sidebar-tree .tree-row').forEach(el => {
    el.classList.toggle('active', el.dataset.filePath === filePath);
  });

  // Reset view panes
  $('code-status').classList.remove('hidden');
  $('code-status-text').textContent = `Loading ${item.name}…`;
  $('code-wrapper').classList.add('hidden');
  $('md-preview').classList.add('hidden');
  $('code-media').classList.add('hidden');
  $('code-media').innerHTML = '';
  currentCodeText = '';

  // Fast Binary / Archive Handler (Instant, NO text fetch!)
  if (BINARY_EXTS.has(ext)) {
    $('code-status').classList.add('hidden');
    $('code-media').classList.remove('hidden');
    $('code-media').innerHTML = `
      <div class="text-center p-8 space-y-4 max-w-md mx-auto">
        <div class="w-16 h-16 mx-auto flex items-center justify-center rounded-2xl bg-amber-50 dark:bg-amber-950/50 text-amber-600 dark:text-amber-400 border border-amber-200 dark:border-amber-800">
          ${SVG_ICONS.zip_large}
        </div>
        <div>
          <h3 class="font-bold text-lg text-zinc-900 dark:text-zinc-100">${esc(item.name)}</h3>
          <p class="text-xs text-zinc-400 font-mono mt-1">${formatBytes(item.size)} · Binary Archive</p>
        </div>
        <p class="text-sm text-zinc-500">Binary archives cannot be previewed directly as text.</p>
        <div class="flex items-center justify-center gap-3 pt-2">
          <a class="ctl inline-flex items-center gap-2 text-sm bg-blue-600 text-white border-blue-600 hover:bg-blue-700 shadow-sm" href="${rawUrlOf(filePath)}" download="${esc(item.name)}">
            ${SVG_ICONS.download} Download ${ext.toUpperCase()}
          </a>
          <a class="ctl inline-flex items-center gap-2 text-sm" href="${urlOf(filePath)}" target="_blank" rel="noopener">
            ${SVG_ICONS.github} View on GitHub
          </a>
        </div>
      </div>`;
    return;
  }

  // Handle Images
  if (['png', 'jpg', 'jpeg', 'gif', 'svg', 'webp', 'ico'].includes(ext)) {
    $('code-status').classList.add('hidden');
    $('code-media').classList.remove('hidden');
    $('code-media').innerHTML = `
      <div class="max-w-full max-h-full flex flex-col items-center justify-center p-4">
        <img src="${rawUrlOf(filePath)}" alt="${esc(item.name)}" class="max-h-[60vh] max-w-full rounded-lg shadow-md border border-zinc-200 dark:border-zinc-800 object-contain bg-white dark:bg-zinc-950">
        <p class="text-xs text-zinc-400 mt-3 font-mono">${esc(item.name)} (${formatBytes(item.size)})</p>
      </div>`;
    return;
  }

  // Handle PDF
  if (ext === 'pdf') {
    $('code-status').classList.add('hidden');
    $('code-media').classList.remove('hidden');
    $('code-media').innerHTML = `
      <div class="text-center p-8 space-y-4">
        <div class="w-16 h-16 mx-auto flex items-center justify-center rounded-2xl bg-red-50 dark:bg-red-950/50 text-red-600 dark:text-red-400 border border-red-200 dark:border-red-800">
          <svg class="w-8 h-8" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/></svg>
        </div>
        <h3 class="font-bold text-lg">${esc(item.name)}</h3>
        <p class="text-sm text-zinc-500 max-w-md mx-auto">PDF document preview available via direct link or download.</p>
        <div class="flex items-center justify-center gap-3">
          <a class="ctl inline-flex items-center gap-2 text-sm bg-blue-600 text-white border-blue-600 hover:bg-blue-700" href="${rawUrlOf(filePath)}" target="_blank" rel="noopener">
            Open PDF in new tab
          </a>
          <a class="ctl inline-flex items-center gap-2 text-sm" href="${urlOf(filePath)}" target="_blank" rel="noopener">
            ${SVG_ICONS.github} View on GitHub
          </a>
        </div>
      </div>`;
    return;
  }

  // Handle Dia diagrams
  if (ext === 'dia') {
    $('code-status').classList.add('hidden');
    $('code-media').classList.remove('hidden');
    $('code-media').innerHTML = `
      <div class="text-center p-8 space-y-4">
        <div class="w-16 h-16 mx-auto flex items-center justify-center rounded-2xl bg-emerald-50 dark:bg-emerald-950/50 text-emerald-600 dark:text-emerald-400 border border-emerald-200 dark:border-emerald-800">
          <svg class="w-8 h-8" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="3" width="7" height="7"/><rect x="14" y="3" width="7" height="7"/><rect x="14" y="14" width="7" height="7"/><rect x="3" y="14" width="7" height="7"/><path d="M10 6.5h4M6.5 10v4M17.5 10v4M10 17.5h4"/></svg>
        </div>
        <h3 class="font-bold text-lg">${esc(item.name)}</h3>
        <p class="text-sm text-zinc-500 max-w-md mx-auto">Dia Database ER Diagram.</p>
        <div class="flex items-center justify-center gap-3">
          <a class="ctl inline-flex items-center gap-2 text-sm bg-blue-600 text-white border-blue-600 hover:bg-blue-700" href="${rawUrlOf(filePath)}" download>
            ${SVG_ICONS.download} Download .dia
          </a>
          <button id="dia-raw-btn" class="ctl text-sm">View XML</button>
        </div>
      </div>`;
    $('dia-raw-btn')?.addEventListener('click', async () => {
      loadCodeText(filePath, 'xml');
    });
    return;
  }

  // Handle Code / Text / Markdown
  await loadCodeText(filePath, ext);
}

// Load and display code / text
async function loadCodeText(filePath, ext) {
  try {
    const text = await fetchFileContent(filePath);
    currentCodeText = text;

    $('code-status').classList.add('hidden');

    // Handle Markdown with formatted preview option
    if (ext === 'md' && window.marked) {
      $('md-toggle-btn').classList.remove('hidden');
      $('md-toggle-btn').textContent = 'View Raw Markdown';
      $('md-preview').classList.remove('hidden');
      $('md-preview').innerHTML = marked.parse(text);
      $('active-file-lines').textContent = `· ${text.split('\n').length} lines`;
      return;
    }

    // Code Editor View
    $('code-wrapper').classList.remove('hidden');
    const codeEl = $('code-content');
    codeEl.className = 'hljs block';

    const langMap = {
      cs: 'csharp',
      sql: 'sql',
      php: 'php',
      vue: 'xml',
      html: 'xml',
      xml: 'xml',
      xaml: 'xml',
      js: 'javascript',
      mjs: 'javascript',
      ts: 'typescript',
      json: 'json',
      css: 'css',
      py: 'python',
      md: 'markdown',
      yaml: 'yaml',
      yml: 'yaml',
      sh: 'bash',
      bash: 'bash',
      ps1: 'powershell',
      dockerfile: 'dockerfile'
    };

    if (langMap[ext]) {
      codeEl.classList.add(`language-${langMap[ext]}`);
    }

    codeEl.textContent = text;
    if (window.hljs) {
      hljs.highlightElement(codeEl);
    }

    // Generate Line Numbers Gutter
    const lineCount = text.split('\n').length;
    $('active-file-lines').textContent = `· ${lineCount} lines`;
    const gutterHtml = Array.from({ length: lineCount }, (_, i) => `<div>${i + 1}</div>`).join('');
    $('code-gutter').innerHTML = gutterHtml;

  } catch (err) {
    $('code-status').classList.remove('hidden');
    $('code-status-text').innerHTML = `
      <span class="text-red-500 font-semibold">Failed to load file</span><br>
      <span class="text-xs text-zinc-400">${esc(err.message)}</span><br>
      <a class="ctl inline-block mt-3 text-xs" href="${urlOf(filePath)}" target="_blank" rel="noopener">Open on GitHub</a>`;
  }
}

// Build hierarchical file tree structure from flat file list
function buildHierarchy(files, rootPath) {
  const root = { name: '', isDir: true, children: {}, fullPath: '' };
  const prefixLen = rootPath.endsWith('/') ? rootPath.length : rootPath.length + 1;

  for (const f of files) {
    const rel = f.path.substring(prefixLen);
    const segments = rel.split('/');
    let curr = root;

    for (let i = 0; i < segments.length; i++) {
      const seg = segments[i];
      const isLast = (i === segments.length - 1);

      if (isLast) {
        curr.children[seg] = {
          name: seg,
          isDir: false,
          file: f,
          fullPath: f.path,
          relPath: rel
        };
      } else {
        if (!curr.children[seg]) {
          const folderRel = segments.slice(0, i + 1).join('/');
          curr.children[seg] = {
            name: seg,
            isDir: true,
            children: {},
            relPath: folderRel,
            fullPath: rootPath + '/' + folderRel
          };
        }
        curr = curr.children[seg];
      }
    }
  }
  return root;
}

// Render Tree HTML recursively with collapsible folders and clean icons
function renderTreeHtml(node, depth = 0) {
  const childKeys = Object.keys(node.children).sort((a, b) => {
    const aNode = node.children[a];
    const bNode = node.children[b];
    // Directories first, then files
    if (aNode.isDir !== bNode.isDir) return bNode.isDir - aNode.isDir;
    return a.localeCompare(b, 'hu');
  });

  return childKeys.map(key => {
    const child = node.children[key];

    if (child.isDir) {
      const isCollapsed = collapsedFolders.has(child.fullPath);
      const folderIcon = isCollapsed ? SVG_ICONS.folder : SVG_ICONS.folder_open;

      return `
        <div class="tree-folder ${isCollapsed ? 'collapsed' : ''}" data-folder-path="${esc(child.fullPath)}">
          <div class="tree-row tree-folder-row hover:text-zinc-900 dark:hover:text-zinc-100" data-toggle-folder="${esc(child.fullPath)}">
            <span class="tree-chevron">${SVG_ICONS.chevron_down}</span>
            <span class="shrink-0 flex items-center">${folderIcon}</span>
            <span class="font-medium truncate font-mono text-[12px]">${esc(child.name)}</span>
          </div>
          <div class="tree-folder-children">
            ${renderTreeHtml(child, depth + 1)}
          </div>
        </div>`;
    } else {
      const ext = child.name.includes('.') ? child.name.split('.').pop().toLowerCase() : '';
      const icon = getFileSvg(ext);
      const isActive = child.fullPath === currentFilePath;

      return `
        <div class="tree-row tree-file ${isActive ? 'active' : ''}" data-file-path="${esc(child.fullPath)}" title="${esc(child.relPath)}">
          <span class="w-3.5 shrink-0"></span>
          <span class="shrink-0 flex items-center">${icon}</span>
          <span class="truncate flex-1 font-mono text-[12px]">${esc(child.name)}</span>
          <span class="text-[10px] text-zinc-400 font-mono shrink-0 pl-1">${formatBytes(child.file.size)}</span>
        </div>`;
    }
  }).join('');
}

// Render sidebar tree view with optional live search filter
function renderSidebarTree(files, rootPath) {
  const filterVal = $('sidebar-filter').value.toLowerCase().trim();
  const filtered = filterVal ? files.filter(f => f.path.toLowerCase().includes(filterVal)) : files;

  if (!filtered.length) {
    $('sidebar-tree').innerHTML = `<p class="p-3 text-zinc-400 text-center text-xs">No matching files</p>`;
    return;
  }

  const hierarchy = buildHierarchy(filtered, rootPath);
  $('sidebar-tree').innerHTML = renderTreeHtml(hierarchy);
}

// Open project explorer modal
function openProject(projectPath, targetFilePath = null) {
  currentProject = projectPath;
  collapsedFolders.clear();
  const dialog = $('code-dialog');
  if (!dialog) return;

  const projectItem = allItems.find(i => i.path === projectPath) || { name: projectPath.split('/').pop(), subject: 'Project', grade: '' };
  $('code-dialog-title').textContent = projectItem.name;
  $('modal-project-badge').textContent = projectItem.subject || 'PROJECT';

  const projectFiles = getProjectFiles(projectPath);
  $('sidebar-count').textContent = projectFiles.length;
  $('sidebar-filter').value = '';

  // Render Hierarchical Tree
  renderSidebarTree(projectFiles, projectPath);

  dialog.showModal();

  // If a specific file was requested, view it; otherwise smart-pick the best readable file
  if (targetFilePath) {
    viewFile(targetFilePath);
  } else if (projectFiles.length > 0) {
    const priority = pickInitialFile(projectFiles);
    if (priority) viewFile(priority.path);
  } else {
    $('active-filename').textContent = 'Empty Project';
    $('code-status').classList.remove('hidden');
    $('code-status-text').textContent = 'No files found in this project folder.';
  }
}

// Tree view interaction: toggle folder collapse or open file
$('sidebar-tree').addEventListener('click', e => {
  // Folder collapse / expand toggle
  const folderRow = e.target.closest('[data-toggle-folder]');
  if (folderRow) {
    const folderPath = folderRow.dataset.toggleFolder;
    if (collapsedFolders.has(folderPath)) {
      collapsedFolders.delete(folderPath);
    } else {
      collapsedFolders.add(folderPath);
    }
    const folderEl = folderRow.closest('.tree-folder');
    if (folderEl) {
      folderEl.classList.toggle('collapsed', collapsedFolders.has(folderPath));
    }
    return;
  }

  // File row click
  const fileRow = e.target.closest('.tree-file');
  if (fileRow && fileRow.dataset.filePath) {
    viewFile(fileRow.dataset.filePath);
  }
});

// Sidebar search filter input
$('sidebar-filter').addEventListener('input', () => {
  if (currentProject) {
    renderSidebarTree(getProjectFiles(currentProject), currentProject);
  }
});

// Markdown / Raw toggle
$('md-toggle-btn').addEventListener('click', () => {
  const isViewingFormatted = !$('md-preview').classList.contains('hidden');
  if (isViewingFormatted) {
    $('md-preview').classList.add('hidden');
    $('code-wrapper').classList.remove('hidden');
    $('md-toggle-btn').textContent = 'View Rendered Preview';

    const codeEl = $('code-content');
    codeEl.className = 'hljs language-markdown block';
    codeEl.textContent = currentCodeText;
    if (window.hljs) hljs.highlightElement(codeEl);

    const lineCount = currentCodeText.split('\n').length;
    $('code-gutter').innerHTML = Array.from({ length: lineCount }, (_, i) => `<div>${i + 1}</div>`).join('');
  } else {
    $('code-wrapper').classList.add('hidden');
    $('md-preview').classList.remove('hidden');
    $('md-toggle-btn').textContent = 'View Raw Markdown';
  }
});

// Copy code button
$('code-copy-btn').onclick = () => {
  if (!currentCodeText) return;
  navigator.clipboard?.writeText(currentCodeText);
  $('code-copy-text').textContent = 'Copied!';
  setTimeout(() => { $('code-copy-text').textContent = 'Copy'; }, 1500);
};

// Word wrap toggle
$('code-wrap-btn').onclick = () => {
  const wrapper = $('code-wrapper');
  wrapper.classList.toggle('code-wrapped');
  $('code-wrap-btn').classList.toggle('bg-blue-50', wrapper.classList.contains('code-wrapped'));
  $('code-wrap-btn').classList.toggle('text-blue-600', wrapper.classList.contains('code-wrapped'));
};

// Download current file
$('code-download-btn').onclick = () => {
  if (!currentFilePath) return;
  const a = document.createElement('a');
  a.href = rawUrlOf(currentFilePath);
  a.download = currentFilePath.split('/').pop();
  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
};

// Close modal
$('code-close-btn').onclick = () => $('code-dialog').close();

// Click delegation on results list
$('results').addEventListener('click', e => {
  const favBtn = e.target.closest('button[data-fav]');
  if (favBtn) {
    const path = favBtn.dataset.fav;
    if (favs.has(path)) favs.delete(path);
    else favs.add(path);
    try { localStorage.favs = JSON.stringify([...favs]); } catch {}
    render(false);
    return;
  }

  const copyBtn = e.target.closest('button[data-copy]');
  if (copyBtn) {
    const path = copyBtn.dataset.copy;
    navigator.clipboard?.writeText(urlOf(path));
    const orig = copyBtn.innerHTML;
    copyBtn.innerHTML = '✓';
    setTimeout(() => { copyBtn.innerHTML = orig; }, 1200);
    return;
  }

  const card = e.target.closest('[data-action]');
  if (card) {
    const action = card.dataset.action;
    const path = card.dataset.path;
    if (action === 'open-project') {
      openProject(path);
    } else if (action === 'open-file') {
      const parentDir = path.substring(0, path.lastIndexOf('/'));
      const project = allItems.find(i => i.isDir && i.project && path.startsWith(i.path + '/')) || { path: parentDir };
      openProject(project.path, path);
    }
  }
});

// Filter Pills click handler
document.querySelectorAll('.pill[data-pill-type]').forEach(pill => {
  pill.addEventListener('click', () => {
    document.querySelectorAll(`.pill[data-pill-type="${pill.dataset.pillType}"]`).forEach(p => p.classList.remove('active'));
    pill.classList.add('active');

    const type = pill.dataset.pillType;
    const val = pill.dataset.pillValue;
    $(type).value = val;
    render();
  });
});

// Synchronize dropdown selects with pills
$('subject').addEventListener('change', () => {
  const val = $('subject').value;
  document.querySelectorAll('.pill[data-pill-type="subject"]').forEach(p => {
    p.classList.toggle('active', p.dataset.pillValue === val);
  });
  render();
});

// Clear Search Input Button
$('q-clear').addEventListener('click', () => {
  $('q').value = '';
  render();
  $('q').focus();
});

// Reset all filters
$('clear').onclick = () => {
  $('q').value = '';
  $('grade').value = '';
  $('subject').value = '';
  $('files').checked = false;
  $('fav-only').checked = false;
  $('sort').value = 'new';
  document.querySelectorAll('.pill[data-pill-type="subject"]').forEach(p => {
    p.classList.toggle('active', p.dataset.pillValue === '');
  });
  render();
};

// Theme Toggle
$('theme').onclick = () => {
  const dark = document.documentElement.classList.toggle('dark');
  try { localStorage.theme = dark ? 'dark' : 'light'; } catch {}
  updateHljsTheme(dark);
};

// Show More Pagination
$('more').onclick = () => {
  shownCount += PAGE;
  render(false);
};

// Random Project Selector
$('random').onclick = () => {
  const projects = allItems.filter(i => i.project);
  if (projects.length) {
    const rand = projects[Math.floor(Math.random() * projects.length)];
    openProject(rand.path);
  }
};

function fillSelect(sel, values, defaultLabel = 'All') {
  sel.innerHTML = `<option value="">${defaultLabel}</option>`;
  for (const v of [...new Set(values)].filter(Boolean).sort()) {
    sel.add(new Option(v, v));
  }
}

// Light dismiss fallback for dialogs without closedby support
if (!('closedBy' in HTMLDialogElement.prototype)) {
  const dialog = $('code-dialog');
  const outside = (e, d) => {
    const r = d.getBoundingClientRect();
    return e.clientX < r.left || e.clientX > r.right || e.clientY < r.top || e.clientY > r.bottom;
  };
  let pressedOutside = false;
  dialog.addEventListener('pointerdown', (e) => { pressedOutside = outside(e, dialog); });
  dialog.addEventListener('pointerup', (e) => { if (pressedOutside && outside(e, dialog)) dialog.close(); });
}

// Keyboard shortcuts: '/' focuses search
addEventListener('keydown', e => {
  if (e.key === '/' && document.activeElement !== $('q') && !$('code-dialog').open) {
    e.preventDefault();
    $('q').focus();
  }
});

for (const id of ['q', 'grade', 'files', 'fav-only', 'sort']) {
  $(id).addEventListener('input', () => render());
}

// Application Initialization
loadTree().then(tree => {
  rawTree = tree;
  allItems = tree.filter(([p]) => /^(NEU|Verebay)\//.test(p)).map(parseItem);

  fillSelect($('grade'), allItems.map(i => i.grade), 'All grades');
  
  const distinctSubjects = ['DESKTOP', 'DATABASE', 'BACKEND', 'TESTING', 'WEB', 'DOCKER', 'DK', 'IKT', 'OTHER'];
  fillSelect($('subject'), distinctSubjects, 'All subjects');

  const projectCount = allItems.filter(i => i.project).length;
  const fileCount = allItems.filter(i => !i.isDir).length;
  $('quick-stats').textContent = `${projectCount} projects · ${fileCount.toLocaleString()} files`;

  if (location.hash && location.hash.length > 1) {
    const hash = decodeURIComponent(location.hash.slice(1));
    if (hash.startsWith('project=')) {
      const projPath = hash.replace('project=', '');
      openProject(projPath);
    } else {
      $('q').value = hash;
    }
  }

  render();
}).catch(err => {
  $('status').textContent = 'Error loading project list: ' + err.message;
});
