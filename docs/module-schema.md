# Module Schema Specification

This document defines the exact mapping from a `module-XX.md` markdown file to the `module-XX.json` content file loaded by the Agentic Engineering Lab app.

Every module markdown file has the same structure. Each section heading maps to a specific JSON field. Use this spec when converting a markdown file to JSON or when building the conversion tooling.

---

## 1. Frontmatter → top-level module fields

YAML frontmatter maps directly to top-level JSON fields.

| YAML key | JSON field | Type | Notes |
|---|---|---|---|
| `id` | `id` | string | kebab-case unique ID |
| `moduleNumber` | `moduleNumber` | int | 1-based |
| `title` | `title` | string | |
| `shortTitle` | `shortTitle` | string | used in sidebar |
| `course` | `course` | string | |
| `appName` | `appName` | string | |
| `estimatedMinutes` | `estimatedMinutes` | int | |
| `repoRequired` | `repoRequired` | bool | |
| `sourceCodeChangesAllowed` | `sourceCodeChangesAllowed` | bool | |
| `safetyLevel` | `safetyLevel` | string enum | see mapping below |
| `primaryArtifact` | `primaryArtifact` | string or null | file path |
| `prerequisites` | `prerequisites` | string[] | |
| `moduleType` | `moduleType` | string | e.g. `foundational` |
| `status` | `status` | string | `draft` or `published` |

### SafetyLevel enum mapping

| Markdown value | JSON value |
|---|---|
| `Read-only` | `ReadOnly` |
| `Makes changes` | `MakesChanges` |
| `Destructive` | `Destructive` |

---

## 2. Prose sections → markdown string fields

These sections are stored as raw markdown strings and rendered client-side. Capture everything from the heading (exclusive) to the next same-level heading (exclusive).

| Markdown heading | JSON field | Notes |
|---|---|---|
| `## Module summary` | `summary` | |
| `## Why this module matters` | `whyItMatters` | |
| `## How this course works` OR `## How this module fits the course` | `howItFits` | heading varies by module |
| `## Key concepts` | `keyConceptsMarkdown` | includes all sub-concept sections |
| `## GitHub Copilot CLI mechanics used in this module` | `mechanicsMarkdown` | |
| `## LakeBank and SyteLine variation` | `variationNotesMarkdown` | includes all sub-sections |
| `# Instructor demo` | `instructorDemoMarkdown` | includes all demo steps |

---

## 3. Learning objectives → string array

Section: `## Learning objectives`

Parse each bullet (`- `) as a string. Strip the leading `- ` prefix.

```json
"learningObjectives": [
  "Explain agentic development in your own words.",
  "Explain why Copilot CLI is an assistant, not the owner of the work."
]
```

---

## 4. Lab → `lab` object

Section: `# Hands-on lab`

### 4a. Lab title

`## Lab title` → `lab.title` (string)

### 4b. Lab safety level

`## Lab safety level` → two fields:
- First line (e.g. `Read-only.`) → `lab.safetyLevel` (use SafetyLevel enum mapping above, strip trailing `.`)
- Remaining paragraph text → `lab.safetyDescription` (markdown string)

### 4c. Before you start

`## Before you start` → `lab.beforeYouStart` (string[])

Parse each checkbox item (`- [ ] `). Strip the `- [ ] ` prefix. Keep the text as-is.

```json
"beforeYouStart": [
  "I opened a terminal in the target repo.",
  "I am using a safe training branch."
]
```

### 4d. Prompt cards

Each prompt card section maps to one entry in `lab.prompts` (PromptCard[]).

| Heading pattern | `label` | `isOptional` |
|---|---|---|
| `## Lab prompt` | `"Lab prompt"` | `false` |
| `## Optional ...` (any heading starting with `Optional`) | the heading text (strip `## `) | `true` |

Optional explanatory prose before the fenced prompt block maps to `introMarkdown`.

The prompt text is the content of the fenced code block (` ```text `) inside the section and maps to `promptText`.

```json
"prompts": [
  {
    "label": "Lab prompt",
    "introMarkdown": "Read this context before using the prompt.",
    "promptText": "We are starting an agentic development workflow...",
    "isOptional": false
  },
  {
    "label": "Optional targeted file-reference prompt",
    "promptText": "Use @memory-bank/repo-agent-readiness.md as context...",
    "isOptional": true
  }
]
```

### 4e. Expected artifact

`## Expected artifact` → `lab.expectedArtifact`

- The fenced code block path → `lab.expectedArtifact.path` (string)
- The bullet list items below ("The file should include:") → `lab.expectedArtifact.expectedContents` (string[])

```json
"expectedArtifact": {
  "path": "memory-bank/repo-agent-readiness.md",
  "expectedContents": [
    "repo classification",
    "evidence",
    "available AI support"
  ]
}
```

---

## 5. Verification checklist → `lab.verificationChecklist`

Section: `# Verification checklist`

Parse each checkbox item (`- [ ] `). Strip the `- [ ] ` prefix.

```json
"verificationChecklist": [
  "Did Copilot correctly classify the repo?",
  "Did Copilot cite real files or folders?"
]
```

---

## 6. Lab evidence submission → `lab.evidence`

Section: `# Lab evidence submission`

- `## Artifact path` code block → `lab.evidence.artifactPath` (string)
- `## Short summary` descriptive text (e.g. "Write 3-5 sentences...") → `lab.evidence.summaryPrompt` (string)
- Each `### Question heading` (e.g. `### What did Copilot get right?`) → one entry in `lab.evidence.reflectionQuestions` (string[])
  - Strip the `### ` prefix and `?` optional trailing punctuation — keep the full question text as written.

```json
"evidence": {
  "artifactPath": "memory-bank/repo-agent-readiness.md",
  "summaryPrompt": "Write 3-5 sentences describing what Copilot found.",
  "reflectionQuestions": [
    "What did Copilot get right?",
    "What did Copilot miss?",
    "What would you correct before trusting the file?",
    "Would you allow Copilot to start coding now? Why or why not?",
    "Did Copilot change source code?"
  ]
}
```

---

## 7. Skill check → `skillCheck` array

Section: `# Skill check`

Each question block:
- `## Question N` heading → ignored (use array index)
- Body text (before `### Expected answer`) → `skillCheck[n].question` (markdown string — preserve code blocks)
- `### Expected answer` body → `skillCheck[n].expectedAnswer` (markdown string)

```json
"skillCheck": [
  {
    "question": "A developer asks Copilot CLI:\n\n```\nAdd the new feature from the ticket.\n```\n\nWhat is wrong with this request?",
    "expectedAnswer": "The request is too vague. It does not provide ticket details..."
  }
]
```

---

## 8. Module review → `review` object

Section: `# Module review`

- `## Review summary` body → `review.summaryMarkdown` (markdown string)
- `## Readiness checklist` checkbox items → `review.readinessChecklist` (string[], strip `- [ ] `)
- `## Common mistakes` bullet items → `review.commonMistakes` (string[], strip `- `)
- `# Next module` body → `review.nextModuleTeaser` (markdown string)

```json
"review": {
  "summaryMarkdown": "In this module, you learned that agentic development is a supervised workflow...",
  "readinessChecklist": [
    "I can explain agentic development in my own words.",
    "I can explain why \"just fix this\" is a weak prompt."
  ],
  "commonMistakes": [
    "Letting Copilot start coding too soon.",
    "Trusting repo summaries without checking evidence."
  ],
  "nextModuleTeaser": "Module 2: GitHub Copilot CLI Fundamentals\n\nIn Module 2..."
}
```

---

## 9. Sections to ignore

These sections exist in the markdown for authoring reference only. Do not include them in the JSON output.

- `## Module completion criteria`
- `# Source notes`

---

## 10. Complete JSON skeleton

```json
{
  "id": "",
  "moduleNumber": 0,
  "title": "",
  "shortTitle": "",
  "course": "",
  "appName": "",
  "estimatedMinutes": 0,
  "repoRequired": true,
  "sourceCodeChangesAllowed": false,
  "safetyLevel": "ReadOnly",
  "primaryArtifact": null,
  "prerequisites": [],
  "moduleType": "",
  "status": "draft",
  "summary": "",
  "whyItMatters": "",
  "howItFits": "",
  "learningObjectives": [],
  "keyConceptsMarkdown": "",
  "mechanicsMarkdown": "",
  "variationNotesMarkdown": "",
  "instructorDemoMarkdown": "",
  "lab": {
    "title": "",
    "safetyLevel": "ReadOnly",
    "safetyDescription": "",
    "beforeYouStart": [],
    "prompts": [
      {
        "label": "Lab prompt",
        "introMarkdown": "",
        "promptText": "",
        "isOptional": false
      }
    ],
    "expectedArtifact": {
      "path": "",
      "expectedContents": []
    },
    "verificationChecklist": [],
    "evidence": {
      "artifactPath": "",
      "summaryPrompt": "",
      "reflectionQuestions": []
    }
  },
  "skillCheck": [
    {
      "question": "",
      "expectedAnswer": ""
    }
  ],
  "review": {
    "summaryMarkdown": "",
    "readinessChecklist": [],
    "commonMistakes": [],
    "nextModuleTeaser": ""
  }
}
```
