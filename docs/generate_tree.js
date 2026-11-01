const fs = require('fs');
const { execSync } = require('child_process');

console.log('Generating tree.json with git -c core.quotepath=false ls-files -s...');
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

  if (filePath.startsWith('docs/tree.json') || filePath.startsWith('docs/generate_tree.js')) continue;

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
fs.writeFileSync('docs/tree.json', JSON.stringify(entries));
console.log(`Successfully generated docs/tree.json with ${entries.length} items (${Math.round(fs.statSync('docs/tree.json').size / 1024)} KB)`);
const sample = entries.filter(e => e[0].includes('DESKTOP')).slice(0, 3);
console.log('Sample:', sample);
