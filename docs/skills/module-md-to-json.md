# Skill: Convert Module Markdown to JSON

Convert a `docs/module-XX.md` file into a `wwwroot/content/module-XX.json` file following the Agentic Engineering Lab content schema.

## When to use this skill

Use this when:
- A new module markdown file has been authored and needs to be added to the app.
- An existing module has been updated and the JSON needs to be regenerated.
- You are building or testing the module conversion pipeline.

## Reference

Full field-by-field mapping: `docs/module-schema.md`
C# models: `Models/` project folder

---

## Prompt template

Use this prompt in GitHub Copilot CLI from the repo root:

```text
I need to convert a module markdown file to JSON for the Agentic Engineering Lab app.

Reference the schema in @docs/module-schema.md for the exact field mapping rules.
Reference @.github/copilot-instructions.md for repo context.

Source file: @docs/module-XX.md   ← replace XX with the module number

Produce: wwwroot/content/module-XX.json

Rules:
- Follow the field names and types in docs/module-schema.md exactly.
- Use the SafetyLevel enum mapping: "Read-only" → "ReadOnly", "Makes changes" → "MakesChanges", "Destructive" → "Destructive".
- Prose sections (summary, whyItMatters, howItFits, keyConceptsMarkdown, mechanicsMarkdown, variationNotesMarkdown, instructorDemoMarkdown) are raw markdown strings. Preserve all formatting including code blocks, tables, and lists.
- Checklist items: strip the "- [ ] " prefix, keep the text.
- Bullet list items: strip the "- " prefix, keep the text.
- Prompt card text: extract from the ```text fenced code block only.
- Skill check questions: preserve inline code blocks in the question text.
- Sections to omit: "Module completion criteria" and "Source notes".
- Output valid JSON. Do not include comments.
- Write the file to wwwroot/content/module-XX.json.

Do not modify source code.
Do not modify the markdown source file.
```

---

## Verification checklist

After generating the JSON, verify:

- [ ] `id` matches the frontmatter `id` exactly.
- [ ] `safetyLevel` uses the enum value (`ReadOnly`, not `Read-only`).
- [ ] `lab.prompts` is an array — even if there is only one prompt.
- [ ] Optional prompts have `"isOptional": true`.
- [ ] All checklist fields (`beforeYouStart`, `verificationChecklist`, `review.readinessChecklist`) have `- [ ] ` stripped.
- [ ] `lab.evidence.reflectionQuestions` contains module-specific questions (not generic ones).
- [ ] `skillCheck` entries preserve code blocks in `question` text.
- [ ] Prose markdown fields are not truncated or summarized — they are the full section content.
- [ ] `"Module completion criteria"` and `"Source notes"` sections are absent from the output.
- [ ] The file is valid JSON (no trailing commas, no comments).

---

## Common mistakes to watch for

- Summarizing prose sections instead of copying them verbatim.
- Losing the fenced code block content from prompt cards.
- Outputting `"Read-only"` instead of `"ReadOnly"` for safety level.
- Putting a single prompt object instead of an array in `lab.prompts`.
- Including `moduleCompletionCriteria` or `sourceNotes` fields that should be omitted.
- Stripping code blocks from skill check questions.
