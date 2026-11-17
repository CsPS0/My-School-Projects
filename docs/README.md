# docs

GitHub Pages index for [CsPS0/My-School-Projects](https://csps0.github.io/My-School-Projects/).

## Commands

```bash
npm run tree        # Scan repo and regenerate tree data
npm run sync-tree   # Sync manual edits from tree.json to tree-data.js
npm run watch       # Auto-regenerate on file changes
npm run dev         # Start local preview server (localhost:3000)
```

## Auto-Update on Commit

Run once to auto-generate and stage tree files on every `git commit`:

```bash
git config core.hooksPath .githooks
```

## Editing tree.json

`docs/tree.json` is formatted line-by-line (`["path", isDir, size]`).
After manual edits, run `npm run sync-tree`. On `localhost`, edits reflect immediately on page refresh.
