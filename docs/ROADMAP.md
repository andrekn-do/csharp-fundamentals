# Project 1 Roadmap — Task Manager CLI

## Goal
Regain fluency in common C# syntax through small, incremental features. Build independently, validate behavior and explain decisions. AI and peer review are optional for independent learners; the author uses Codex for reviews and documentation.

Prioritize everyday programming through actual feature requirements: variables, types, operators, control flow, methods and return values, then models, collections, LINQ, reliability, persistence and basic testing. Explore deeper compiler/runtime discussions when useful without delaying practical progress. Aim for small tasks; a challenge may span multiple sessions.

## Phases
1. **Menu and navigation** — strings, console I/O, conditions, `switch`, loops, methods.
2. **Task data model** — classes, properties, constructors, enums, `DateTime`/`DateOnly`.
3. **Task collections** — `List<T>`, arrays, dictionaries, add/edit/delete, nullability.
4. **Querying** — LINQ, lambdas, filters, sorting, aggregate statistics.
5. **Reliability** — validation, `TryParse`, exceptions, pattern matching.
6. **Persistence** — files, JSON serialization, `using`, `async`/`await`.
7. **Design cleanup** — access modifiers, interfaces when valuable, composition, methods and separation of responsibilities.
8. **Quality and final exercise** — focused tests, debugging, refactoring, independent feature challenge.

No calendar deadlines. Proceed when the relevant skills can be demonstrated. [D01–D03](TASKS.md) have detailed requirements; D04–D10 and later topics remain provisional until developed. Dates and other topics may be introduced in smaller follow-up challenges when justified.

## Progression of guidance
- Phases 1–2: requirements plus relevant concepts and optional hints.
- Phases 3–5: mostly requirements and acceptance criteria; hints by request.
- Phases 6–8: independent feature ownership, structured review and concept checks.

Every challenge calls for behavior checks and an explanation of the relevant concepts. Independent learners can self-review and proceed without author approval. The author's workflow separately requires code/understanding review and explicit approval before personal completion or advancement; see [AGENTS.md](../AGENTS.md).

## Completion criteria
- CLI runs and handles ordinary invalid input sensibly.
- Tasks can be created, viewed, updated, removed, searched and persisted.
- Code demonstrates idiomatic, understandable C# without premature architecture.
- Learner explains C# choices and solves an independent follow-up task.

## Current availability

The author has approved D01 and D02 as complete; D03 is upcoming. This is personal progress, not a completion claim for readers. The [learning log](LEARNING_LOG.md) preserves the actual reviews and corrections. Work in `starter/TaskManager/`; compare with the evolving `src/TaskManager/` reference after attempting a challenge.
