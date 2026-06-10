# Developer Documentation - World Wide Dashboard

## Project Setup (For Team Members)

Since multiple people are developing the project, it is important that everyone has the same environment configured. The project uses the **Bun** runtime for speed and modern tooling.

### Prerequisites
1. **Install Bun:**
   - Windows (PowerShell): `powershell -c "irm bun.sh/install.ps1 | iex"`
   - Important: After installation, restart your terminal or add it to your PATH: `C:\Users\<username>\.bun\bin`
2. **Git Access:** Ensure you can log into the shared GitLab/GitHub repository.

### First Steps
Clone the repository, then run the following commands in the project root directory:

```bash
# 1. Download dependencies
bun install

# 2. Set up environment variables (copy the example)
# Warning: For global modules, the YOUTUBE_API_KEY and LASTFM_API_KEY are mandatory!
cp .env.example .env

# 3. Initialize the database (SQLite Cache)
bunx prisma migrate dev

# 4. Start the development server
bun dev
```
The website will be available at [http://localhost:3000](http://localhost:3000).

> [!TIP]
> **Docker execution:** If you don't want to mess with Bun, just run the `docker-compose up --build` command, which builds the project and runs it on port 3000!

## Architecture and System Designs (UML)

Per the teachers' request, the project is built using an **object-oriented** approach. The visual structure of the system and the data flow processes are detailed in the diagrams found in the `docs/UML/` directory:

> [!NOTE]
> The diagrams were created using **Mermaid.js**. They will appear graphically in VS Code with the appropriate extension (or natively on GitHub).

- **[Mindmap](./UML/MINDMAP.md):** The complete technological and functional overview of the project.
- **[Flowchart](./UML/FLOWCHART.md):** The lifecycle of data flow from the client interface to external APIs.
- **[Class Diagram](./UML/CLASS.md):** Detailed OOP plan for the Strategy Pattern implementation (with 11 registered data sources).

> [!IMPORTANT]
> Zalán has created the C#-based tests (xUnit) for the backend architecture. The test folder is located under `tests/Dashboard.Tests`, and the test execution report is available in the `docs/Tesztelesi Jegyzokonyv/tesztelési_jegyzőkönyv.md` file!

The most important code files are located in the `src/lib/core` directory:

- **`IDataSource`**: An interface describing what a data source must look like.
- **`BaseDataSource`**: An abstract class providing default error handling and features.
- **`DashboardManager`**: This class coordinates all the modules (Steam, YouTube, etc.).

### Adding a New Module (Example)
If you want to add a new data source, create a new file in the `src/lib/providers/` folder that extends the `BaseDataSource` class.

## Coding Standards
- **Language:** Code variables and classes are written in **English**.
- **Strict Rule (NO COMMENTS):** It is **STRICTLY FORBIDDEN** to place any kind of comments (// or /* */) in the codebase! To enforce this, Next.js linter rules have been modified in the `.eslintrc.json` file.
- **Documentation:** The README and the contents of the `docs/` folder are written entirely in **English**.
- **Style:** We use Tailwind CSS for a fast and consistent UI.

## Git Workflow
1. Always run a `git pull` before you start working!
2. When you finish a feature:
   ```bash
   git add .
   git commit -m "feat: add steam player count widget"
   git push
   ```
