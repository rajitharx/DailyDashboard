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

See `Plan.md` for detailed requirements and progress.
