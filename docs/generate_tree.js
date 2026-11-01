const fs = require('fs');
const { execSync } = require('child_process');

console.log('Generating tree data with git -c core.quotepath=false ls-files -s...');
const raw = execSync('git -c core.quotepath=false ls-files -s', { encoding: 'utf8' });
const lines = raw.trim().split(/\r?\n/);
const entries = [];
const dirSet = new Set();

for (const line of lines) {
  const parts = line.split('\t');
  if (parts.length < 2) continue;
  let filePath = parts[1].trim();
  if (filePath.startsWith('"') && filePath.endsWith('"')) {
    filePath = filePath.slice(1, -1);
  }

  if (filePath.startsWith('docs/tree.json') || filePath.startsWith('docs/js/tree-data.js') || filePath.startsWith('docs/generate_tree.js')) continue;

  let size = 0;
  try {
    size = fs.statSync(filePath).size;
  } catch (e) {}

  entries.push([filePath, false, size]);

  const segments = filePath.split('/');
  for (let i = 1; i < segments.length; i++) {
    dirSet.add(segments.slice(0, i).join('/'));
  }
}

for (const dir of dirSet) {
  entries.push([dir, true, 0]);
}

entries.sort((a, b) => a[0].localeCompare(b[0]));
const json = JSON.stringify(entries);
fs.writeFileSync('docs/tree.json', json);
fs.writeFileSync('docs/js/tree-data.js', 'window.REPO_TREE = ' + json + ';');
console.log(`Successfully generated docs/tree.json and docs/js/tree-data.js with ${entries.length} items (${Math.round(fs.statSync('docs/tree.json').size / 1024)} KB)`);
