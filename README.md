# DailyBoard

Full-stack Clean Architecture solution using .NET 10.0

## Tech Stack
- **Backend:** ASP.NET Core Minimal API (.NET 10.0), MediatR (CQRS), xUnit, Moq
- **Frontend:** React 18, Vite, Tailwind CSS (planned)
- **Persistence:** EF Core (planned), InMemory for tests
- **Testing:** xUnit, Moq, full TDD
- **Docker & PostgreSQL:** Planned for deployment

## Project Structure
- `src/` — All backend projects (Api, Application, Domain, Infrastructure, Web)
- `tests/` — All test projects (unit, integration, API)
- `Plan.md` — Project plan and requirements

## .NET Version
All projects target **.NET 10.0**

## Build & Test
```sh
dotnet build
dotnet test
```
All tests should pass. API integration tests are set up and working.

## Status
- Backend: Core domain, application, infrastructure, and API layers implemented
- Tests: All layers covered, TDD enforced, all tests pass
- Frontend: Not yet implemented

## MCP Server (Node.js)
A lightweight MCP (Model Context Protocol) helper server has been added to support local developer automation and tooling.

- **Location:** `mcp-server/server.js`
- **Purpose:** expose project tasks (build, test, search, git status, health checks) via an MCP-compatible endpoint to streamline CI/dev workflows and editor integrations.
- **Available tools (examples):** `daily-dashboard-mcp` tools for `build`, `test`, `search`, `git_status`, `health_check`, and a `local-engineering-mcp` helper used for local environment checks.
- **Quick start:** run with Node.js from the repository root:

```bash
# from repository root
node mcp-server/server.js
```

Use an MCP client or editor integration to invoke the exposed tasks (for example, the VS Code MCP integration used in this workspace).

See `Plan.md` for detailed requirements and progress.
