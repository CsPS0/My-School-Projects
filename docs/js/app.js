const REPO = 'CsPS0/My-School-Projects', BRANCH = 'main', PAGE = 100;
const $ = id => document.getElementById(id);
const fold = s => s.normalize('NFD').replace(/[̀-ͯ]/g, '').toLowerCase();
const esc = s => s.replace(/[&<>"]/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;' }[c]));
const ICON = {
  sun: '<svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><circle cx="12" cy="12" r="4"/><path d="M12 2v2M12 20v2M4.9 4.9l1.4 1.4M17.7 17.7l1.4 1.4M2 12h2M20 12h2M4.9 19.1l1.4-1.4M17.7 6.3l1.4-1.4"/></svg>',
  dice: '<svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><rect x="3" y="3" width="18" height="18" rx="3"/><circle cx="8.5" cy="8.5" r=".8"/><circle cx="15.5" cy="15.5" r=".8"/><circle cx="12" cy="12" r=".8"/></svg>',
  star: '<svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><path d="m12 2 3.1 6.3 6.9 1-5 4.9 1.2 6.8-6.2-3.2-6.2 3.2L7 14.2 2 9.3l6.9-1z"/></svg>',
  link: '<svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><path d="M10 13a5 5 0 0 0 7.5.5l3-3a5 5 0 0 0-7-7l-1.7 1.7"/><path d="M14 11a5 5 0 0 0-7.5-.5l-3 3a5 5 0 0 0 7 7l1.7-1.7"/></svg>',
  check: '<svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><path d="M20 6 9 17l-5-5"/></svg>',
  folder: '<svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><path d="M3 6a2 2 0 0 1 2-2h4l2 3h8a2 2 0 0 1 2 2v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/></svg>',
  file: '<svg class="ic" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><path d="M14 2v6h6"/></svg>'
};
const MONTHS = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
let items = [], shown = PAGE, favs = new Set();
try { favs = new Set(JSON.parse(localStorage.favs || '[]')); } catch {}

async function loadTree() {
  const key = 'tree:' + REPO;
  try { const c = JSON.parse(sessionStorage.getItem(key)); if (c) return c; } catch {}
  const r = await fetch(`https://api.github.com/repos/${REPO}/git/trees/${BRANCH}?recursive=1`);
  if (!r.ok) throw new Error(r.status === 403 ? 'GitHub API rate limit hit, try again in a bit.' : 'GitHub API error ' + r.status);
  const tree = (await r.json()).tree.map(t => [t.path, t.type === 'tree']);
  try { sessionStorage.setItem(key, JSON.stringify(tree)); } catch {}
  return tree;
}

function toItem([path, isDir]) {
  const parts = path.split('/'), name = parts.at(-1);
  // both schools use <School>/OsztalyXX/<SUBJECT>/<project>
  const grade = parts[1] ? +parts[1].replace('Osztaly', '') + '. osztály' + (parts[0] === 'Verebay' ? ' (Verebay)' : '') : '';
  const subject = parts[2] || '';
  // folder names like "24-0916-0922_Topic" encode the school week
  const m = name.match(/^(\d{2})-(\d{2})(\d{2})/);
  const date = m ? `20${m[1]} ${MONTHS[+m[2] - 1] || ''} ${+m[3]}` : '';
  const project = isDir && (!!m || parts.length === 4);
  return { path, name, isDir, grade, subject: subject.toUpperCase(), date, sort: m ? m[1] + m[2] + m[3] : '',
           project, hay: fold(path.replace(/[_\-\/.]/g, ' ')) };
}

function highlight(text, words) {
  if (!words.length) return esc(text);
  const f = fold(text), marks = new Array(text.length).fill(false);
  for (const w of words) for (let i = f.indexOf(w); i !== -1; i = f.indexOf(w, i + 1))
    for (let j = i; j < i + w.length; j++) marks[j] = true;
  let out = '';
  for (let i = 0; i < text.length; i++) {
    if (marks[i] && !marks[i - 1]) out += '<mark>';
    out += esc(text[i]);
    if (marks[i] && !marks[i + 1]) out += '</mark>';
  }
  return out;
}

const urlOf = it => `https://github.com/${REPO}/${it.isDir ? 'tree' : 'blob'}/${BRANCH}/${it.path.split('/').map(encodeURIComponent).join('/')}`;

const SORTS = {
  // projects first; undated items sink in both date orders
  new: (a, b) => (b.project - a.project) || b.sort.localeCompare(a.sort) || a.path.localeCompare(b.path),
  old: (a, b) => (b.project - a.project) || (!a.sort - !b.sort) || a.sort.localeCompare(b.sort) || a.path.localeCompare(b.path),
  name: (a, b) => a.name.localeCompare(b.name, 'hu'),
};

function filtered() {
  const q = $('q').value.trim(), grade = $('grade').value, subject = $('subject').value;
  const words = fold(q.replace(/[_\-\/.]/g, ' ')).split(/\s+/).filter(Boolean);
  const withFiles = $('files').checked, favOnly = $('fav-only').checked;
  const hits = items.filter(it =>
    (favOnly ? favs.has(it.path) : withFiles || (it.isDir && (words.length || it.project))) &&
    (!grade || it.grade === grade) && (!subject || it.subject === subject) &&
    words.every(w => it.hay.includes(w)));
  return { hits: hits.sort(SORTS[$('sort').value]), words, q };
}

function render(reset = true) {
  if (reset) shown = PAGE;
  const { hits, words, q } = filtered();
  $('status').textContent = `${hits.length} result${hits.length === 1 ? '' : 's'}` + (hits.length > shown ? ` (showing ${shown})` : '');
  $('more').classList.toggle('hidden', hits.length <= shown);
  $('results').innerHTML = hits.slice(0, shown).map(it => {
    const tags = [it.grade, it.subject, it.date].filter(Boolean);
    const kind = (it.isDir ? ICON.folder + 'folder' : ICON.file + (it.name.includes('.') ? it.name.split('.').pop() : 'file'));
    const on = favs.has(it.path);
    return `<li class="relative"><a class="card pr-20" href="${urlOf(it)}" target="_blank" rel="noopener">
      <div class="font-semibold break-words">${highlight(it.name, words)}</div>
      <div class="text-sm text-zinc-500 dark:text-zinc-400 break-all">${highlight(it.path, words)}</div>
      <div class="flex flex-wrap gap-1.5 mt-1.5"><span class="tag inline-flex items-center gap-1">${kind}</span>${tags.map(t => `<span class="tag">${esc(t)}</span>`).join('')}</div></a>
      <div class="absolute top-2 right-2 flex gap-2">
        <button class="star ${on ? 'on' : ''}" data-fav="${esc(it.path)}" title="Favorite" aria-label="Favorite">${ICON.star}</button>
        <button class="star" data-copy="${esc(it.path)}" title="Copy link" aria-label="Copy link">${ICON.link}</button></div></li>`;
  }).join('');
  history.replaceState(null, '', q ? '#' + encodeURIComponent(q) : location.pathname);
}

function fillSelect(sel, values) {
  for (const v of [...new Set(values)].filter(Boolean).sort()) sel.add(new Option(v, v));
}

$('results').addEventListener('click', e => {
  const b = e.target.closest('button'); if (!b) return;
  if (b.dataset.fav) {
    favs.delete(b.dataset.fav) || favs.add(b.dataset.fav);
    try { localStorage.favs = JSON.stringify([...favs]); } catch {}
    render(false);
  } else if (b.dataset.copy) {
    navigator.clipboard?.writeText(urlOf(items.find(i => i.path === b.dataset.copy)));
    b.innerHTML = ICON.check; setTimeout(() => b.innerHTML = ICON.link, 1200);
  }
});

$('more').onclick = () => { shown += PAGE; render(false); };
$('theme').onclick = () => {
  const dark = document.documentElement.classList.toggle('dark');
  try { localStorage.theme = dark ? 'dark' : 'light'; } catch {}
};
$('clear').onclick = () => {
  for (const id of ['q', 'grade', 'subject']) $(id).value = '';
  $('sort').value = 'new'; $('files').checked = $('fav-only').checked = false; render();
};
$('random').onclick = () => {
  const p = items.filter(i => i.project);
  if (p.length) open(urlOf(p[Math.random() * p.length | 0]), '_blank', 'noopener');
};

loadTree().then(tree => {
  items = tree.filter(([p]) => /^(NEU|Verebay)\//.test(p)).map(toItem);
  fillSelect($('grade'), items.map(i => i.grade));
  fillSelect($('subject'), items.filter(i => i.project).map(i => i.subject));
  $('q').value = decodeURIComponent(location.hash.slice(1));
  render();
}).catch(e => { $('status').textContent = e.message; });

for (const id of ['q', 'grade', 'subject', 'files', 'fav-only', 'sort']) $(id).addEventListener('input', () => render());
addEventListener('keydown', e => {
  if (e.key === '/' && document.activeElement !== $('q')) { e.preventDefault(); $('q').focus(); }
});
