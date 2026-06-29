# Agentic Engineering Lab — Repo Instructions

## What this repo is

This is the source for **Agentic Engineering Lab**, a Blazor WebAssembly training app that teaches developers how to use GitHub Copilot CLI in an agentic development workflow.

The app is a static course player. It runs on GitHub Pages or via `dotnet run`. There is no backend, no login, and no learner identity.

## Architecture constraints

- **Blazor WebAssembly only.** No server-side rendering, no API controllers, no SignalR.
- **Static hosting.** All assets must be servable from `wwwroot/` with no server-side logic.
- **No shell execution.** Do not write code that runs git commands, shell commands, MCP tools, or repo scans from the browser. The app guides the learner; the learner runs Copilot CLI locally.
- **Local storage for state.** Progress, checklist state, and reflection notes are persisted in browser local storage. No external state store.

## Content model

Modules are the core content unit. Each module is authored as a markdown file (`docs/module-XX.md`) and converted to a JSON file (`wwwroot/content/module-XX.json`) for the app to load.

The full markdown-to-JSON mapping is in:

```
docs/module-schema.md
```

The C# models that mirror the JSON schema live in the `Models/` project folder. Keep the JSON field names and C# property names in sync with `docs/module-schema.md`.

Key types:
- `Module` — top-level content unit
- `Lab` — the hands-on portion of a module (one per module)
- `PromptCard` — a copyable Copilot CLI prompt (a lab has one or more)
- `SkillCheckQuestion` — Q&A pair with a revealed expected answer
- `SafetyLevel` — enum: `ReadOnly`, `MakesChanges`, `Destructive`

## Converting a module markdown file to JSON

Use the skill in `docs/skills/module-md-to-json.md` as a prompt template when asked to convert a `module-XX.md` to `module-XX.json`.

## Key reusable Blazor components

| Component | Purpose |
|---|---|
| `PromptCard` | Renders a prompt with a Copy button and optional/required label |
| `ChecklistPanel` | Interactive checkbox list; state key passed as parameter |
| `SafetyBadge` | Color-coded pill for `ReadOnly`, `MakesChanges`, `Destructive` |
| `MarkdownSection` | Renders a markdown string as HTML |
| `SkillCheckPanel` | Shows a question, accepts free-text response, reveals expected answer |
| `ReflectionForm` | Module-specific open-ended text fields; saved to local storage |
| `ModuleCompletionButton` | Marks a module complete in local storage |

## Navigation

- `/` — course home with module list and progress
- `/module/{moduleNumber}` — module page

Module pages have six in-order sections: Reading → Demo → Lab → Reflection → Skill Check → Review.

## What not to do

- Do not add authentication.
- Do not add a backend or API.
- Do not call `localStorage` from C# without going through a JS interop service abstraction.
- Do not hardcode module content in `.razor` files — load it from JSON.
- Do not run `/init`, install plugins, or change MCP configuration as part of any feature.
