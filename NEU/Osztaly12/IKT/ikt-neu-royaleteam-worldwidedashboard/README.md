# World Wide Dashboard - Royale Team

## Description
The **World Wide Dashboard** is a modern, modular data aggregation platform that makes the most important global and local data available in one place. The goal of the project is to unify fragmented data sources (SteamCharts, YouTube, financial news, election data) into a cleanly transparent, customizable interface.

The defining feature of the software is its Object-Oriented (OOP) architecture, which allows for the rapid and secure integration of new data sources (modules).

## Features
- **Gaming and Personal Statistics:** Global trends in Steam player counts, alongside personal Tracker.gg (R6, LoL, RL, etc.) and Exophase (cross-platform playtime) integrations.
- **Video & Music:** YouTube channel statistics and global Top 10 music charts (with Last.fm, Spotify, SoundCloud data), plus personal Last.fm "Now Playing" tracking.
- **Politics:** Hungarian election data and global leadership statistics.
- **Finance & Crypto:** Stock exchange rates and cryptocurrency data (including the unique Hawk Tuah Coin).
- **Customizability:** Dynamic layout (Compact or Full width), and a dedicated Settings page for personal API keys.
- **Security and Legal Declarations:** A fully-fledged "Data & Privacy Policy" page for transparency of Mock and Live data, as well as a built-in Open Source License viewer.
- **Design:** Premium, card-based dark/light theme with fluid Anime.js animations and a unique visual 404 error page.

## Technology Stack
- **Frontend/Backend:** Next.js 15+ (App Router)
- **Runtime:** Bun
- **Language:** TypeScript (OOP approach)
- **Styling:** Tailwind CSS
- **Animations:** Anime.js
- **Data Management:** Prisma + SQLite (Caching)
- **Containerization:** Docker

## System Designs and Documentation
Detailed technical and visual plans for the project are contained in separate documents within the `docs/` directory:

> [!NOTE]
> The diagrams were created using **Mermaid.js**. They can be viewed graphically in VS Code with the appropriate extension (or natively on GitHub).

- **[Mindmap](./docs/UML/MINDMAP.md):** Functional overview of the project
- **[Flowchart](./docs/UML/FLOWCHART.md):** Data flow and fetching process
- **[Class Diagram](./docs/UML/CLASS.md):** Object-Oriented (OOP) architecture
- **[Style Guide](./docs/STYLE.md):** Visual rules and design templates

## How to run the project?

### 1. Local Development (Bun & Prisma)
1. Clone the repository.
2. Copy the `.env.example` file to `.env` and (mandatory) fill in the `YOUTUBE_API_KEY` and `LASTFM_API_KEY` values. *(Note: personal API keys, such as the Steam key, should not be entered here, but rather on the Settings page within the UI!)*
3. Install dependencies:
   ```bash
   bun install
   ```
4. Run the Prisma database migration (SQLite Cache):
   ```bash
   bunx prisma migrate dev
   ```
5. Start the development server:
   ```bash
   bun dev
   ```
6. Open in your browser: `http://localhost:3000`

### 2. Containerized Execution (Docker Compose)
If you use Docker, you can easily set up and run the entire environment. We recommend building the project (the service is named `wwdb` in the compose file) first without starting it automatically, and then launching it in the background:

1. Build the container without starting it automatically:
   ```bash
   docker-compose up --build --no-start
   ```
2. Start the `wwdb` service in the background:
   ```bash
   docker-compose start wwdb
   ```
   *(To stop the service later, use `docker-compose stop wwdb`, or use `docker-compose down` to remove the container completely.)*

The website will be available at `http://localhost:3000`.

## Team
- **Solti Csongor Péter** - Lead Developer / Chief Architecture - test asd
- **Páva Zalán** - Backend Developer / Testing
- **Bodolai Richárd Tamás** - Assistant Developer / Oldest Team Member