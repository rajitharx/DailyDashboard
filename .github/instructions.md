# DailyBoard Project Agent Instructions

## Technologies
- Backend: C# (.NET, ASP.NET Core, MediatR, EF Core, xUnit)
- Frontend: React 18, Vite, TypeScript, Tailwind CSS

## Coding Standards
- **Naming Conventions:**
  - C#: Use PascalCase for classes, methods, and properties. Use camelCase for local variables and parameters. Use singular names for entities and plural for collections.
  - React/TypeScript: Use PascalCase for components, camelCase for variables/functions, UPPER_SNAKE_CASE for constants.
- **File/Folder Structure:**
  - Strict Clean Architecture: Domain, Application, Infrastructure, Api, Web, Tests. No cross-layer dependencies except as allowed by Clean Architecture.
  - Place each class/interface in its own file.
- **Documentation:**
  - All public methods and classes must have XML doc comments (C#) or JSDoc (TypeScript).
- **Testing:**
  - Use TDD: Write failing tests first, then implement code to pass.
  - Use xUnit for C#, React Testing Library/Jest for frontend.
  - All new features must have corresponding tests.
- **Code Suggestions:**
  - Suggest refactoring for readability, maintainability, and performance.
  - Prefer async/await and CancellationToken in C# APIs.
  - Use Result<T> or similar for error handling in business logic.
  - Use MediatR for CQRS (commands/queries) in Application layer.
  - Use dependency injection everywhere.
  - Use Tailwind for all styling in React; avoid component libraries unless approved.
- **Clean Code Principles:**
  - Single Responsibility Principle: Each class/function does one thing.
  - No magic strings/numbers; use constants or enums.
  - Keep methods short and focused.
  - Avoid code duplication; extract shared logic.
  - Use meaningful names everywhere.
- **Other Important Rules:**
  - All warnings as errors; nullable enabled in C#.
  - No secrets in source code; use .env or user-secrets.
  - All code must be async where possible.
  - Use ProblemDetails for API error responses.
  - All code must be self-explanatory or commented.

## Example Prompts
- "Add a new board entity using TDD."
- "Refactor this MediatR handler for clarity."
- "Suggest a React component structure for the dashboard page."
- "Enforce naming conventions in this file."

## Related Customizations to Consider
- Add a .editorconfig for C# and TypeScript formatting.
- Create a PR template enforcing TDD and documentation.
- Add a style guide for React components.

---
This file enforces project-wide standards for code quality, structure, and workflow. All contributors must follow these rules for every change.
