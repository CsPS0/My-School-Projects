# School Projects Repository

My schoolwork from two technical schools: classwork, homework, tests and exam practice.

- **Website:** <https://csps0.github.io/My-School-Projects/> (searchable index of every project and file)
- **License:** MIT

## Layout

Every project lives at `<School>/Osztaly<grade>/<SUBJECT>/<date>_<Topic>`.

### NEU ([Neumann János Informatikai Technikum](https://neumann.bmszc.hu/))

| Folder | Subjects |
|---|---|
| `NEU/Osztaly11` (2024/25) | `DATABASE`, `DESKTOP`, `WEB`, `DOCKER`, `DK`, `IKT`, `OTHER` |
| `NEU/Osztaly12` (2025/26) | `BACKEND`, `DESKTOP`, `TESTING`, `WEB`, `DOCKER`, `DK`, `IKT`, `OTHER` |
| `NEU/Osztaly13` (2026/27) | `AKK/DATABASE`, `AKK/DESKTOP`, `AKK/FULLSTACK`, `AKK/TESTING`, `DevCorner`, `OTHER` |

| Subject | What is in it |
|---|---|
| `DATABASE` | MySQL / MariaDB tasks: tables, views, users and rights, ER diagrams |
| `DESKTOP` | C# console, WPF and REST API (ASP.NET Core) projects |
| `BACKEND` | PHP: basics, OOP with Composer autoloading, Faker data, REST APIs |
| `FULLSTACK` | Laravel + Vue projects on the school `fullstack2627` Docker base |
| `TESTING` | Unit tests (MSTest, NUnit, xUnit) and test planning |
| `WEB` | HTML, CSS, JavaScript |
| `DOCKER` | Docker and Linux basics |
| `DK` | Access databases and extra programming challenges |
| `IKT` | Group projects |
| `OTHER` | English, Hungarian, German and other subjects |

Study guides in `NEU/Osztaly13/AKK`:

- [`DATABASE/DOCKER_UTMUTATO.md`](NEU/Osztaly13/AKK/DATABASE/DOCKER_UTMUTATO.md): Docker + MySQL cheat sheet for tests
- [`FULLSTACK/FRONTEND-TEST-GUIDE.md`](NEU/Osztaly13/AKK/FULLSTACK/FRONTEND-TEST-GUIDE.md): Vue frontend test guide
- [`FULLSTACK/PRACTICE-TASK-KERTMOZI.md`](NEU/Osztaly13/AKK/FULLSTACK/PRACTICE-TASK-KERTMOZI.md): practice exam

### Verebay ([Verebély László Informatikai Technikum](https://verebelyszki.hu/))

Grades 9–10 (2022–2024): `WEB`, `PYTHON`, `OFFICE`, `CISCO`, `OTHER`. See [Verebay/README.md](Verebay/README.md).

### docs

Source of the GitHub Pages site (`index.html`, `css/`, `js/`). It reads a pre-built file tree (`tree-data.js`, `tree.json`), so it needs no GitHub API calls. It has search with keyboard shortcuts, sorting, a dark mode, copy-link, a random-project button and an in-page viewer for Markdown and Office files.

## Naming

- **Project folder:** `YY-MMDD-MMDD_Topic` for the school week the task was given, e.g. `26-1116-1122_Rotary` is 16–22 November 2026. Older folders use `YY-MMDD_Topic` (a single day) or `YYYY-MMDD-Topic`.
- **Solution folder inside a project:** `solticsongor-<Topic>` or `solti-csongor-<topic>`, as the task asks.
- **Project notes:** newer projects have a short `README.md` saying how to start them.

## What is not stored here

Build output and dependencies are ignored: `bin/`, `obj/`, `packages/`, `vendor/`, `node_modules/`, `.next/`, `.vs/`, `.idea/`.
Submission zips are not committed either; they are made separately for handing in.

## Working on the repo

| Task | Command |
|---|---|
| Rebuild the website's file tree | `npm run tree` |
| Rebuild it automatically on every commit | `git config core.hooksPath .githooks` |
| Sync after editing `docs/tree.json` by hand | `npm run sync-tree` |
| Preview the website locally | `npm run dev` |

GitHub Actions:

- `repo-checks.yml` runs on every push and fails if a zip, `.env`, dependency folder or build output gets committed.
- `dotnet.yml` runs when C# files change. It builds every `.sln`/`.slnx` in `NEU/Osztaly12` and `NEU/Osztaly13` and runs the unit tests. Unfinished solutions are listed in `.github/ci-skip.txt`.

### Commit messages

Short English messages in this form: `<type>(<area>): <what changed>`, for example:

- `feat(desktop): add Rotary REST API, console and GUI`
- `fix(database): drop the right table in kolcsonzesek-table.sql`
- `docs(fullstack): update frontend test guide`
- `chore(repo): untrack zips and build output`

Types: `feat` (new task or feature), `fix`, `docs`, `refactor`, `chore`. Area: the subject folder in lower case (`database`, `desktop`, `fullstack`, `testing`, `web`, `docs`, `repo`).

---

**Maintained by:** CsPS
