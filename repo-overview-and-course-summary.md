## 1) What this repo does

This repo is a **browser-based training app** for developers learning agentic workflows with GitHub Copilot CLI. The app teaches and guides; learners do the actual work in their terminal. It is designed as a **static Blazor WebAssembly** app, intended to run locally (including `dotnet run`) and be deployable as a static site (e.g., GitHub Pages), with no backend/login requirements.

**Evidence:** `docs/scaffold.md`, `src/AgenticEngineeringLab/AgenticEngineeringLab.csproj`, `src/AgenticEngineeringLab/wwwroot/content/module-02.json`

## 2) How module content is structured

Module content is authored as markdown in `docs/module-XX.md` and mapped to runtime JSON in `src/AgenticEngineeringLab/wwwroot/content/module-XX.json`. The JSON shape is:

- **metadata**: `id`, `moduleNumber`, `title`, `shortTitle`, `course`, `estimatedMinutes`, `safetyLevel`, etc.
- **prose/markdown sections**: `summary`, `whyItMatters`, `howItFits`, `keyConceptsMarkdown`, `mechanicsMarkdown`, etc.
- **learningObjectives**: string array
- **lab object**: `beforeYouStart`, `prompts`, `expectedArtifact`, `verificationChecklist`, `evidence`
- **skillCheck**: array of `{ question, expectedAnswer }`
- **review**: summary, readiness checklist, common mistakes, next module teaser

The markdown→JSON mapping rules are explicitly defined in `docs/module-schema.md` (frontmatter mapping, prose section mapping, lab mapping, skill check mapping, review mapping).

**Evidence:** `docs/module-schema.md`, `docs/scaffold.md`, `docs/module-02.md`, `src/AgenticEngineeringLab/wwwroot/content/module-02.json`

## 3) Short course description

This course trains developers to use GitHub Copilot CLI in a controlled, evidence-based agentic workflow. It teaches how to classify repos first, build context intentionally, plan before coding, review and test safely, and only then implement changes with clear guardrails. It also teaches when LakeBank workflows apply, when SyteLine plugin context is required, and how MCP-based evidence fits into decision-making. The emphasis is consistent: Copilot assists, and the developer verifies.

**Evidence:** `src/AgenticEngineeringLab/wwwroot/content/module-02.json`, `docs/module-03.md`, `docs/module-11.md`, `docs/module-12.md`, `docs/module-13.md`
