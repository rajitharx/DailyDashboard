## Purpose
Provide clear, actionable project standards and expectations for contributors, reviewers, and automation (CI). Follow these rules on all changes to application code.

## Scope
Applies to: Domain, Application, Infrastructure, API, Web, and Tests folders. Test projects and CI pipelines must reflect any behavior changes.

## Technologies
- Backend: C# (.NET, ASP.NET Core), MediatR, EF Core, xUnit
- Frontend: React 18, Vite, TypeScript, Tailwind CSS

## Standards

- **Naming:**
  - C#: PascalCase for types/members; camelCase for parameters/locals. Use singular entity names.
  - React/TS: PascalCase components; camelCase functions/variables; UPPER_SNAKE_CASE for constants.

- **Architecture:**
  - Strict Clean Architecture: Domain ← Application ← Infrastructure ← Api/Web. No downward references outside allowed boundaries.
  - One public class/interface per file.

- **Documentation:**
  - Public C# types/members require XML docs. Public TS/JS APIs require JSDoc.

- **Async & Cancellation:**
  - Prefer async/await and accept CancellationToken for cancellable operations.

- **Error Handling:**
  - Use Result<T> or equivalent for domain errors. API errors should use RFC 7807 ProblemDetails.

- **Security & Secrets:**
  - No secrets in source. Use user-secrets, environment variables, or a secrets manager.

- **Formatting & Linting:**
  - Add/adhere to .editorconfig, dotnet-format, and ESLint/Prettier for frontend.

- **Build Policy:**
  - Treat warnings as errors. Enable nullable reference types in C# projects.

## Testing and TDD

- Use TDD: prefer writing failing unit tests first, then implement to pass.
- Use xUnit for C#, React Testing Library + Jest for frontend.
- Every feature/behavior change requires tests. Any change in signatures, models, or business logic must update tests accordingly.
- Keep tests fast, deterministic, and independent. Use in-memory or test doubles for external dependencies.

## Pull Requests & CI

- PR checklist (required):
  - Tests added/updated and passing locally.
  - Linting and formatting applied.
  - XML/JSDoc added for public surface changes.
  - No secrets or credentials introduced.
  - Description explains the rationale and any breaking changes.

- CI must run: build, tests, lint, and formatting checks. Do not merge failing pipelines.

## Examples (prompts for the agent)
- Add a new board entity using TDD.
- Refactor this MediatR handler for clarity and add tests.
- Suggest a React component structure for the dashboard page.
- Enforce naming conventions in a file or set of files.

## Recommended Repo Improvements
- Add a `.editorconfig` and `dotnet format` configuration.
- Create a PR template enforcing the PR checklist above.
- Add a CONTRIBUTING.md with branch, commit, and release guidance.

---
These guidelines help keep the codebase maintainable, testable, and secure. If a rule is unclear or conflicts with a pragmatic need, propose an exception in the PR description for reviewer approval.
