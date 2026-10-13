# Backend & Database Architecture Update

This document summarizes the backend and database architecture introduced in the `becse/database-new` merge. It explains what changed, how the new stack works, and key differences from the previous architecture.

---

## 1. Summary of Changes

The backend architecture shifted from Entity Framework Core to a lightweight, high-performance micro-ORM approach paired with pure SQL migrations:

| Component | Previous Solution | New Solution (Becse) |
| :--- | :--- | :--- |
| **Project Structure** | Single `backend/` folder | Solution `backend/backend.slnx` with two projects |
| **ORM / Data Access** | Entity Framework Core 10 | RepoDb (Hybrid Micro-ORM) with Npgsql |
| **Database Migrations** | EF Core CLI (`dotnet ef migrations`) | DbUp (`Eviduct.Migrations`) executing raw `.sql` files |
| **Database Engine** | PostgreSQL 16 | PostgreSQL 17 Alpine |
| **Active Domain Endpoints** | Placeholder endpoints (tasks, settings) | Domain endpoints for Cases and Evidence Files |
| **Storage Infrastructure** | Local volume placeholder | SeaweedFS S3-compatible object storage |
| **Proxy & Routing** | Direct port mapping | Traefik reverse proxy with local virtual hosts |

---

## 2. Solution Structure

The backend is now organized as a multi-project solution in `backend/backend.slnx`:

```
backend/
├── Eviduct.Api/            Web API application (Controllers, Models, Repositories)
│   ├── Controllers/        CasesController.cs
│   ├── Models/             Case, EvidenceFile, User, Group, Connection, EventData
│   ├── Repositories/       CaseRepository.cs, FileRepository.cs
│   ├── PostgresConnectionFactory.cs
│   └── Program.cs
├── Eviduct.Migrations/     Standalone DbUp migration console app
│   ├── Program.cs          Migration runner execution logic
│   └── Scripts/            Ordered SQL migration scripts
│       ├── 0000-schema.sql
│       └── 0001-seed-example-data.sql
└── backend.slnx            Visual Studio / .NET 10 solution file
```

---

## 3. Data Access Layer: RepoDb

### What is RepoDb?
RepoDb is a lightweight hybrid micro-ORM for .NET. Like Dapper, it sits directly on top of ADO.NET and Npgsql, avoiding the heavy change-tracking overhead of Entity Framework Core.

### Why this approach was chosen:
- **Performance & Low Latency**: Faster query execution and significantly lower memory overhead compared to full-blown ORMs.
- **Explicit Database Control**: Queries and mappings are transparent without hidden LINQ-to-SQL translation issues.
- **Micro-Repository Pattern**:
  - `PostgresConnectionFactory`: Injected as a singleton service to create new `NpgsqlConnection` instances using the connection string.
  - `CaseRepository`: Queries the `cases` table using RepoDb methods such as `conn.QueryAllAsync<Case>()` and `conn.QueryAsync<Case>()`.
  - `FileRepository`: Runs parameterized SQL queries mapping many-to-many case relationships via the `case_to_file` join table.

---

## 4. Migrations & Schema: DbUp

### What is DbUp?
DbUp is a database migration library for .NET that runs raw SQL scripts sequentially inside transactions. It tracks applied scripts inside a `schemaversions` table in PostgreSQL.

### Migration Scripts:
1. `0000-schema.sql`:
   - `groups`: Access control tiers (Administrators, Investigators, Auditors, Read Only).
   - `users`: User records with hashed passwords and foreign key links to groups.
   - `files`: Evidentiary files, storage paths, file type classifications, and timestamps.
   - `event_data`: Start and end timestamps linked to files for chronological ordering.
   - `cases`: Case metadata, description, timestamp, and required access level.
   - `connection_types`: Evidence relationship tags (e.g. `same-source`, `temporal-overlap`, `references`, `derived-from`, `contradicts`, `corroborates`, `duplicate`, `belongs-to-entity`).
   - `connections`: Many-to-many relationship graph connecting pairs of files.
   - `case_to_file`: Join table connecting cases and evidence files.

2. `0001-seed-example-data.sql`:
   - Seeds realistic demonstration cases (`Warehouse Breach 2026`, `Corporate Espionage`).
   - Seeds evidence files (incident reports, CCTV recordings, server authentication logs, latent fingerprints, witness statements).
   - Seeds connection relationships between related evidence.

---

## 5. API Endpoints

The API currently exposes the following endpoints through `CasesController.cs`:

### `GET /api/cases`
Returns a list of all recorded cases.
- **Response**: Array of `Case` objects (`id`, `accessLevel`, `name`, `description`, `timestamp`).

### `GET /api/cases/{id}`
Returns a specific case by its primary key ID.
- **Status 200**: Single `Case` object.
- **Status 404**: Returned if no case matches the ID.

### `GET /api/cases/{id}/files?type=1&type=2`
Returns evidence files attached to the specified case.
- **Query Parameter `type`**: Optional array of integer file types:
  - `1`: General (Reports, PDFs, Notes)
  - `2`: Event (CCTV, Audio, Logs, Network Traffic)
  - `3`: Person (Interviews, Statements, IDs)
  - `4`: Forensic (Disk Images, RAM dumps, Hash lists)
  - `5`: Media (Photographs, Scans, Screenshots)
- **Response**: Array of `EvidenceFile` objects (`id`, `accessLevel`, `type`, `name`, `description`, `filePath`, `timestamp`).

---

## 6. Docker & Infrastructure Integration

In `docker/docker-compose.yml`:

1. **Automated Migrations on Startup**:
   A dedicated `migrations` service runs before the backend starts:
   - It compiles and runs `Eviduct.Migrations.dll`.
   - The backend service waits for migrations to complete successfully before booting (`depends_on.condition: service_completed_successfully`).

2. **Database Engine**:
   PostgreSQL upgraded to `postgres:17-alpine`.

3. **Traefik Reverse Proxy**:
   Routes local domains without port collisions:
   - `http://frontend.localhost` -> Vue 3 frontend
   - `http://backend.localhost` -> ASP.NET Core Web API
   - `http://swagger.localhost` -> Swagger UI documentation
   - `http://localhost:8081` -> Traefik dashboard

4. **Object Storage**:
   SeaweedFS runs on port 8333 providing an S3-compatible API for binary file uploads (CCTV, audio, images).

---

## 7. How to Run Locally

### Direct .NET CLI:
```bash
# Build the entire solution
dotnet build backend/backend.slnx

# Run the Web API (listens on port 5275)
dotnet run --project backend/Eviduct.Api --launch-profile http

# Run migrations manually against a local PostgreSQL instance
set EVIDUCT_DATABASE_CONNECTION=Host=localhost;Port=5432;Database=eviduct;Username=postgres;Password=postgres
dotnet run --project backend/Eviduct.Migrations
```

### Full Stack via Docker Compose:
```bash
cd docker
cp .env.example .env    # first time only
docker compose up -d
```
All migrations apply automatically during Docker startup.
