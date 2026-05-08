## Plan: DailyBoard Full-Stack Scaffold (Clean Architecture)

**TL;DR:**  
Scaffold a full-stack, clean-architecture solution for DailyBoard: ASP.NET Core 10 (minimal API, MediatR, EF Core/Postgres, Identity/JWT), React 18 + Vite + Tailwind, Dockerized, with xUnit test projects and all required project structure, placeholder files, and initial configuration.

---

**Steps**

### Phase 1: Solution & Project Structure

1. Create solution file `DailyBoard.sln`.
2. Create backend projects:
   - `DailyBoard.Domain` (classlib)
   - `DailyBoard.Application` (classlib)
   - `DailyBoard.Infrastructure` (classlib)
   - `DailyBoard.Api` (webapi, minimal API)
3. Create frontend project:
   - `DailyBoard.Web` (Vite + React + TypeScript)
4. Create test projects:
   - `DailyBoard.Domain.Tests`
   - `DailyBoard.Application.Tests`
   - `DailyBoard.Infrastructure.Tests`
   - `DailyBoard.Api.Tests`
5. Set up folder structure and placeholder files per Clean Architecture.

### Phase 2: Backend Setup

6. Add NuGet packages (exact versions for .NET 10):
   - EF Core, Npgsql, MediatR, FluentValidation, ASP.NET Core Identity, JWT, Swashbuckle, etc.
7. Implement domain entities, value objects, and interfaces in `Domain`.
8. Implement repository interfaces in `Application`.
9. Implement MediatR command/query handlers in `Application`.
10. Implement repository implementations, EF Core DbContext, and configuration in `Infrastructure`.
11. Implement minimal API endpoints in `Api` (with placeholder logic).
12. Configure dependency injection, MediatR, Identity, JWT, and global exception middleware.
13. Add XML doc comments to all public methods.

### Phase 3: Database & Seed Data

14. Configure EF Core with PostgreSQL, snake_case, value conversions, soft deletes, and audit fields.
15. Create initial migration and seed data class (demo user, board, cards, schedules).

### Phase 4: Frontend Setup

16. Scaffold Vite React app with TypeScript, Tailwind CSS, React Router v6, Zustand, React Query.
17. Set up routes and placeholder page components for all required pages.
18. Configure Vite proxy for `/api` to backend.

### Phase 5: Testing

19. Set up xUnit test projects with shared fixtures.
20. Add example tests for domain, application, infrastructure, and API layers.

### Phase 6: Dockerization

21. Create `docker-compose.yml` for `postgres`, `api` (dotnet watch), and `web` (Vite dev).
22. Create Dockerfiles for `api` and `web`.
23. Add `.env.example` with all required variables.

### Phase 7: Documentation

24. Write `README.md` with setup, run, and development instructions.

---

**Relevant files**

- `DailyBoard.sln` — solution file
- `src/DailyBoard.Domain/` — entities, value objects, interfaces
- `src/DailyBoard.Application/` — use cases, DTOs, MediatR handlers, validators, repo interfaces
- `src/DailyBoard.Infrastructure/` — EF Core, DbContext, repo implementations, seed data
- `src/DailyBoard.Api/` — minimal API, DI, middleware, endpoint registration
- `src/DailyBoard.Web/` — React app, routes, pages, state management, Tailwind config
- `tests/` — all xUnit test projects
- `docker-compose.yml`, `Dockerfile` (api/web), `.env.example`
- `README.md`

---

**Verification**

1. Solution builds and runs (dotnet build, dotnet run, npm run dev).
2. All projects reference correct dependencies.
3. EF Core migration applies and seed data is created.
4. API endpoints respond (even with placeholder data).
5. React app loads all routes with placeholder UI.
6. Test projects run and pass example tests.
7. Docker Compose brings up all services and frontend can reach backend.
8. README instructions are accurate and complete.

---

**Decisions & Scope**

- Strict Clean Architecture enforced (no cross-layer dependencies).
- No business logic in API; all in Application/Domain.
- Repository pattern and MediatR for CQRS-lite.
- All public methods documented.
- No component libraries in frontend; Tailwind only.
- No Redux; Zustand and React Query for state.
- All code is async with CancellationToken.
- Result<T> pattern for business failures.
- Global exception middleware returns ProblemDetails.
- All warnings as errors; nullable enabled.
- No secrets in source; use user-secrets and .env.

---

**Further Considerations**

1. **Seed Data**: Should demo data be always present in dev, or only on first run?  
   *Recommendation: Only on first run or via explicit flag.*
2. **CI/CD**: Not included in initial scaffold; recommend adding GitHub Actions or similar after scaffold is complete.
3. **Frontend Auth**: JWT handling and protected routes will need further detail in implementation phase.

---

**Next Steps**

- Confirm plan and any preferences (e.g., seed data behavior, CI/CD).
- Proceed to implementation phase per this plan.
