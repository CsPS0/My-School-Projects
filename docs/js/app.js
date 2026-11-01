const REPO = 'CsPS0/My-School-Projects', BRANCH = 'main', PAGE = 40;
const $ = id => document.getElementById(id);
const fold = s => s.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase();
const esc = s => s ? String(s).replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c])) : '';

const MONTHS = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
const FILE_ICONS = {
  cs: '🔷',
  sln: '⚙️',
  slnx: '⚙️',
  csproj: '📦',
  sql: '🗄️',
  dia: '📐',
  php: '🐘',
  js: '🟨',
  mjs: '🟨',
  json: '📋',
  html: '🌐',
  css: '🎨',
  md: '📝',
  txt: '📄',
  pdf: '📕',
  png: '🖼️',
  jpg: '🖼️',
  jpeg: '🖼️',
  svg: '🖼️',
  webp: '🖼️',
  gif: '🖼️',
  yaml: '⚙️',
  yml: '⚙️',
  dockerfile: '🐳',
  folder: '📁'
};

const SUBJECT_ICONS = {
  DESKTOP: '💻',
  DATABASE: '🗄️',
  BACKEND: '⚙️',
  TESTING: '🧪',
  WEB: '🌐',
  DOCKER: '🐳',
  DK: '📊',
  IKT: '📡',
  OTHER: '📁'
};

let rawTree = [];
let allItems = [];
let projectItems = [];
let shownCount = PAGE;
let favs = new Set();
let currentProject = null;
let currentFilePath = null;
let currentCodeText = '';
const fileCache = new Map();

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

// Load repository tree (prefer local static tree.json for 0ms load and no rate limits)
async function loadTree() {
  try {
    const res = await fetch('tree.json');
    if (res.ok) {
      const data = await res.json();
      return data;
    }
  } catch (e) {
    console.warn('Local tree.json fetch failed, falling back to GitHub API:', e);
  }

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

// GitHub & Raw URLs
const urlOf = path => `https://github.com/${REPO}/${path.includes('.') ? 'blob' : 'tree'}/${BRANCH}/${path.split('/').map(encodeURIComponent).join('/')}`;
const rawUrlOf = path => `https://raw.githubusercontent.com/${REPO}/${BRANCH}/${path.split('/').map(encodeURIComponent).join('/')}`;

// Highlighting matching search terms
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

// Find files belonging to a project
function getProjectFiles(projectPath) {
  const prefix = projectPath.endsWith('/') ? projectPath : projectPath + '/';
  return allItems.filter(it => !it.isDir && it.path.startsWith(prefix));
}

// Render Project / File Cards
function render(reset = true) {
  if (reset) shownCount = PAGE;
  const { hits, words, q } = filtered();
  
  $('status').textContent = `${hits.length} result${hits.length === 1 ? '' : 's'}` + (hits.length > shownCount ? ` (showing ${shownCount})` : '');
  $('more').classList.toggle('hidden', hits.length <= shownCount);

  $('results').innerHTML = hits.slice(0, shownCount).map(it => {
    const isFile = !it.isDir;
    const isFav = favs.has(it.path);
    const ext = it.name.includes('.') ? it.name.split('.').pop().toLowerCase() : '';
    const fileIcon = isFile ? (FILE_ICONS[ext] || '📄') : (SUBJECT_ICONS[it.subject] || '📁');
    const subjectTagClass = `tag-${(it.subject || '').toLowerCase()}`;

    // Compute file stats for project
    let projectStatsBadge = '';
    if (!isFile) {
      const files = getProjectFiles(it.path);
      const exts = new Set(files.map(f => f.name.split('.').pop().toLowerCase()).filter(e => ['cs', 'sql', 'php', 'js', 'html', 'dia'].includes(e)));
      const extList = Array.from(exts).slice(0, 3).map(e => e.toUpperCase()).join(' · ');
      projectStatsBadge = `<span class="tag bg-zinc-100 dark:bg-zinc-800 text-zinc-600 dark:text-zinc-400">${files.length} files ${extList ? `· ${extList}` : ''}</span>`;
    }

    return `
    <li class="relative card group">
      <div class="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-3 pr-24">
        <div class="min-w-0 flex-1 cursor-pointer" data-action="${isFile ? 'open-file' : 'open-project'}" data-path="${esc(it.path)}">
          <div class="flex items-center gap-2 flex-wrap">
            <span class="text-lg shrink-0">${fileIcon}</span>
            <span class="font-bold text-base text-zinc-900 dark:text-zinc-100 group-hover:text-blue-600 dark:group-hover:text-blue-400 break-words transition-colors">
              ${highlight(it.name, words)}
            </span>
            ${isFile ? `<span class="tag text-[11px] text-blue-600 dark:text-blue-400">view code</span>` : `<span class="tag text-[11px] bg-blue-50 text-blue-600 dark:bg-blue-950 dark:text-blue-300 font-medium">Browse Code</span>`}
          </div>
          <div class="text-xs text-zinc-500 dark:text-zinc-400 font-mono mt-1 break-all">
            ${highlight(it.path, words)}
          </div>
          <div class="flex flex-wrap items-center gap-1.5 mt-2">
            ${it.grade ? `<span class="tag font-semibold">${esc(it.grade)}</span>` : ''}
            ${it.subject ? `<span class="tag ${subjectTagClass} font-semibold">${esc(it.subject)}</span>` : ''}
            ${it.date ? `<span class="tag bg-stone-100 dark:bg-zinc-800 text-zinc-600 dark:text-zinc-400">${esc(it.date)}</span>` : ''}
            ${projectStatsBadge}
          </div>
        </div>

        <div class="absolute top-3 right-3 flex items-center gap-1 shrink-0">
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

  // Update clear search button
  $('q-clear').classList.toggle('hidden', !$('q').value);
}

// Format file size
function formatBytes(bytes) {
  if (!bytes) return '0 B';
  const k = 1024, sizes = ['B', 'KB', 'MB', 'GB'];
  const i = Math.floor(Math.log(bytes) / Math.log(k));
  return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
}

// Fetch file content with multi-strategy & memory cache
async function fetchFileContent(filePath) {
  if (fileCache.has(filePath)) {
    return fileCache.get(filePath);
  }

  // Strategy 1: Local relative fetch (for offline / local dev server)
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

// Open and render a file in the modal viewer
async function viewFile(filePath) {
  currentFilePath = filePath;
  const item = allItems.find(i => i.path === filePath) || { path: filePath, name: filePath.split('/').pop(), size: 0 };
  const ext = item.name.includes('.') ? item.name.split('.').pop().toLowerCase() : '';

  // Update headers
  $('active-filename').textContent = item.name;
  $('active-file-size').textContent = item.size ? `· ${formatBytes(item.size)}` : '';
  $('active-file-lines').textContent = '';
  $('active-file-lang').textContent = ext || 'text';
  $('code-dialog-path').textContent = item.path;
  $('code-github-link').href = urlOf(item.path);
  $('code-copy-text').textContent = 'Copy';
  $('md-toggle-btn').classList.add('hidden');

  // Highlight active item in sidebar tree
  document.querySelectorAll('#sidebar-tree .tree-item').forEach(el => {
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
        <span class="text-5xl">📕</span>
        <h3 class="font-bold text-lg">${esc(item.name)}</h3>
        <p class="text-sm text-zinc-500 max-w-md mx-auto">PDF document preview available via direct link or download.</p>
        <div class="flex items-center justify-center gap-3">
          <a class="ctl inline-flex items-center gap-2 text-sm bg-blue-600 text-white border-blue-600 hover:bg-blue-700" href="${rawUrlOf(filePath)}" target="_blank" rel="noopener">
            Open PDF in new tab
          </a>
          <a class="ctl inline-flex items-center gap-2 text-sm" href="${urlOf(filePath)}" target="_blank" rel="noopener">
            View on GitHub
          </a>
        </div>
      </div>`;
    return;
  }

  // Handle Dia files
  if (ext === 'dia') {
    $('code-status').classList.add('hidden');
    $('code-media').classList.remove('hidden');
    $('code-media').innerHTML = `
      <div class="text-center p-8 space-y-4">
        <span class="text-5xl">📐</span>
        <h3 class="font-bold text-lg">${esc(item.name)}</h3>
        <p class="text-sm text-zinc-500 max-w-md mx-auto">Dia Database Diagram. To view or edit, open in Dia or convert to PNG.</p>
        <div class="flex items-center justify-center gap-3">
          <a class="ctl inline-flex items-center gap-2 text-sm bg-blue-600 text-white border-blue-600 hover:bg-blue-700" href="${rawUrlOf(filePath)}" download>
            Download .dia file
          </a>
          <button id="dia-raw-btn" class="ctl text-sm">View raw file</button>
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

// Open project explorer modal
function openProject(projectPath, targetFilePath = null) {
  currentProject = projectPath;
  const dialog = $('code-dialog');
  if (!dialog) return;

  const projectItem = allItems.find(i => i.path === projectPath) || { name: projectPath.split('/').pop(), subject: 'Project', grade: '' };
  $('code-dialog-title').textContent = projectItem.name;
  $('modal-project-badge').textContent = projectItem.subject || 'PROJECT';
  $('code-dialog-path').textContent = projectPath;

  const projectFiles = getProjectFiles(projectPath);
  $('sidebar-count').textContent = projectFiles.length;

  // Render Sidebar Tree
  renderSidebarTree(projectFiles, projectPath);

  dialog.showModal();

  // If a specific file was requested, view it; otherwise find the best primary file
  if (targetFilePath) {
    viewFile(targetFilePath);
  } else if (projectFiles.length > 0) {
    // Pick priority file: Program.cs, .sql, README.md, or first code file
    const priority = projectFiles.find(f => /Program\.cs$|\.sql$|README\.md$/i.test(f.name)) ||
                     projectFiles.find(f => !/\.pdf|\.dia/i.test(f.name)) ||
                     projectFiles[0];
    viewFile(priority.path);
  } else {
    $('active-filename').textContent = 'Empty Project';
    $('code-status').classList.remove('hidden');
    $('code-status-text').textContent = 'No files found in this project folder.';
  }
}

// Render files list in sidebar with instant filter
function renderSidebarTree(files, rootPath) {
  const filterVal = $('sidebar-filter').value.toLowerCase().trim();
  const filtered = filterVal ? files.filter(f => f.path.toLowerCase().includes(filterVal)) : files;

  const prefixLen = rootPath.endsWith('/') ? rootPath.length : rootPath.length + 1;

  $('sidebar-tree').innerHTML = filtered.map(f => {
    const relPath = f.path.substring(prefixLen);
    const ext = f.name.includes('.') ? f.name.split('.').pop().toLowerCase() : '';
    const icon = FILE_ICONS[ext] || '📄';
    const isActive = f.path === currentFilePath;

    return `
      <div class="tree-item ${isActive ? 'active' : ''}" data-file-path="${esc(f.path)}" title="${esc(relPath)}">
        <span class="shrink-0 text-sm">${icon}</span>
        <span class="truncate flex-1">${esc(relPath)}</span>
        <span class="text-[10px] text-zinc-400 font-mono shrink-0">${formatBytes(f.size)}</span>
      </div>`;
  }).join('');
}

// Sidebar file selection
$('sidebar-tree').addEventListener('click', e => {
  const item = e.target.closest('.tree-item');
  if (item && item.dataset.filePath) {
    viewFile(item.dataset.filePath);
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
  // Favorite button
  const favBtn = e.target.closest('button[data-fav]');
  if (favBtn) {
    const path = favBtn.dataset.fav;
    if (favs.has(path)) favs.delete(path);
    else favs.add(path);
    try { localStorage.favs = JSON.stringify([...favs]); } catch {}
    render(false);
    return;
  }

  // Copy link button
  const copyBtn = e.target.closest('button[data-copy]');
  if (copyBtn) {
    const path = copyBtn.dataset.copy;
    navigator.clipboard?.writeText(urlOf(path));
    const orig = copyBtn.innerHTML;
    copyBtn.innerHTML = '✓';
    setTimeout(() => { copyBtn.innerHTML = orig; }, 1200);
    return;
  }

  // Open Project / File card click
  const card = e.target.closest('[data-action]');
  if (card) {
    const action = card.dataset.action;
    const path = card.dataset.path;
    if (action === 'open-project') {
      openProject(path);
    } else if (action === 'open-file') {
      // Find enclosing project for file
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

// Fill Select Dropdown options
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

// Keyboard shortcuts: '/' focuses search, 'Esc' handled natively by dialog
addEventListener('keydown', e => {
  if (e.key === '/' && document.activeElement !== $('q') && !$('code-dialog').open) {
    e.preventDefault();
    $('q').focus();
  }
});

// Filter events
for (const id of ['q', 'grade', 'files', 'fav-only', 'sort']) {
  $(id).addEventListener('input', () => render());
}

// Application Initialization
loadTree().then(tree => {
  rawTree = tree;
  allItems = tree.filter(([p]) => /^(NEU|Verebay)\//.test(p)).map(parseItem);

  fillSelect($('grade'), allItems.map(i => i.grade), 'All grades');
  
  // Normalized Subjects for dropdown
  const distinctSubjects = ['DESKTOP', 'DATABASE', 'BACKEND', 'TESTING', 'WEB', 'DOCKER', 'DK', 'IKT', 'OTHER'];
  fillSelect($('subject'), distinctSubjects, 'All subjects');

  // Stats badge
  const projectCount = allItems.filter(i => i.project).length;
  const fileCount = allItems.filter(i => !i.isDir).length;
  $('quick-stats').textContent = `${projectCount} projects · ${fileCount.toLocaleString()} files`;

  // Parse initial URL hash if present
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
