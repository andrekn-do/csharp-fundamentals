# Learning log

This is the author's personal history, not a public challenge checklist. Entries describe implementations and findings at the time; later corrections may supersede earlier notes. The current reference is in `src/TaskManager/`; learners work independently in `starter/TaskManager/` using [TASKS.md](TASKS.md).

**Current author progress (2026-10-15):** D01 and D02 approved complete; D03 upcoming, with no D03 implementation present. Historical D01 placeholder actions and its original exit strategy have been superseded by D02. Optional stale-comment/import cleanup noted at the D01 review was subsequently applied. No historical source snapshots or reference tags are available.

For each completed session, add a short entry:

## YYYY-MM-DD — Dxx: Feature
- Built:
- C# concepts used:
- Difficulty / bug encountered:
- What I can explain now:
- What I still need to review:
- Reviewer findings / action taken:

**Practical Mistakes & Lessons:** Record observed work, not assumed learning. For each mistake, capture its cause, correction, why the correction works and what to remember. Keep general learner-asked concept explanations in `QUESTIONS_AND_ANSWERS.md` and avoid duplicating them here.

## 2026-10-12 — D01: Console menu and input handling

- **Built:** Repeating console menu with `ShowMenu`, `GetInput`, and `HandleChoice` local functions; placeholder actions for choices 1–3; clean termination for choice 0 or end-of-input (EOF). No task storage was introduced.
- **C# concepts used:** Top-level statements, console I/O, nullable `string?`, `switch`, `while`, boolean return values, `break`, `int.TryParse` with an `out` parameter, range checks, sentinel values, and null-conditional `?.Trim()`.

### Mistakes, causes, and corrections

| Initial mistake or gap | Cause and impact | Correction and what to remember |
|---|---|---|
| Described choice 0 as stopping the loop, while the original code called `Environment.Exit(0)` inside `while (true)`. | The process terminated; the loop condition never changed. Action handling also owned process termination. | `HandleChoice` now returns false for exit and true to continue. The outer loop checks that result and executes its own `break`, allowing execution to finish normally. |
| Assigned `Console.ReadLine()` to `string` despite its possible null result. | With nullable annotations enabled, this produced CS8600 and contradicted the variable's declared intent. | Changed to `string?`, accurately expressing that null is possible. This fixes the warning; it does not by itself change runtime behavior or handle EOF. |
| Mapped EOF to the same -1 result as invalid input. | An exhausted input stream repeatedly returned null, causing continuous menu and invalid-choice output. | EOF now returns -2, which requests termination. Empty or invalid input returns -1 and retries. `TryParse` safely returns false for null, so the subsequent null check is valid. |
| Initially answered Q2 without distinguishing nullable annotations, empty input, and EOF. | `string?` is a compiler annotation, not automatic null handling; an empty string is different from null. | In follow-up reasoning, correctly traced null through failed parsing, -1, and the default branch, and explained that changing the annotation alone would not fix the loop. |

### Decisions and challenge

- Reserved -1 for invalid input and -2 for EOF. During discussion, identified that typed -2 could collide with the EOF sentinel; this was a proposed-design risk, not an observed shipped bug. The implemented range check maps every parsed integer outside 0–3 to -1, so typed -2 cannot trigger exit.
- Added `Console.ReadLine()?.Trim()` for the whitespace challenge. Non-null input is trimmed; null remains null because `?.` skips the call. Whitespace-only input becomes empty and is rejected. The prior `TryParse` implementation already accepted surrounding whitespace and rejected whitespace-only input, so trimming was not required to pass the challenge.
- Demonstrated understanding that `break` inside the switch exits the switch, whereas the outer `break` exits the loop; also explained the boolean continuation contract and the need to distinguish invalid input from EOF.
- **Final understanding check:** Correctly identified that calling `.Trim()` when `ReadLine()` returns null causes an error; review clarified the exception as `NullReferenceException` and why `?.` skips the call. Whitespace behavior was verified and the existing parsing behavior explained during review.

### Verification and review

- Fresh .NET 10 build: zero warnings and zero errors, using separate output under `obj` because an earlier normal rebuild encountered locked application output files.
- Verified all seven D01 acceptance criteria using `1`, `2`, `3`, `99`, empty input, and `0`.
- Verified spaces-only and tabs-only input are invalid; choices 1–3 and 0 with surrounding spaces or tabs behave correctly.
- Verified typed -2, -1, 4, nonnumeric text, and integer overflow remain invalid. Immediate EOF and EOF after an action terminate cleanly with exit code 0.
- No blocking review findings remain. Optional cleanup remains: stale introductory comments and an unused `System.Numerics` import.
- **Closed 2026-10-13:** Final code review and understanding check passed; learner explicitly approved D01 completion. No application code was edited by the reviewer, and no Git commit was created.

## 2026-10-14 — D02: Create and manage one task

- **Built:** One in-memory task with title, optional description and completion flag. Add validates and re-prompts; successful replacement resets completion. List handles missing/empty data; Complete handles missing and already-completed tasks. EOF exits without saving partial input.
- **Practiced:** Local-variable scope and capture, nullable strings, assignment, local functions, boolean loop control, short-circuit `&&`, null checks, string trimming, interpolation and conditional expressions. Knowledge answers are in Q10–Q17 of `QUESTIONS_AND_ANSWERS.md`.

### Practical mistakes and lessons

| Mistake / misunderstanding | Cause | Correction and lesson |
|---|---|---|
| Exploratory struct was declared before remaining top-level code (CS8803), assigned null and accessed through a nullable wrapper. | Mixed type definitions with executable code and treated value types like references. | Returned to D02's simple variables. Type-declaration ordering and nullable structs were explained (Q10–Q12); struct usage was not retained in the reviewed implementation. |
| EOF at the description prompt overwrote saved state and reported success. | Assigned fields without checking the second read for null. | Checks now precede all saved-state assignments. Keep existing state untouched until the entire operation succeeds. |
| Blank titles ended Add; proposed treating EOF like an empty title. | Used an early return for retryable input and did not distinguish retry from termination. | Added a title-input loop that retries empty input, stops on null, then handles EOF separately. Retrying EOF would repeatedly read null. |
| Exit case failed to compile (CS0163). | Assumed assigning the loop flag also ended the switch section. | Added a switch `break`. A state change is not a control-flow statement. |
| Empty descriptions stayed blank; completion displayed raw booleans. | Used `??` for an empty-string case and displayed storage values directly. | Added an empty-description check and translated completion to `Complete`/`Incomplete`. Choose readable output independently of stored values. |
| Conditional expression inside interpolation failed to compile (CS8361 and related syntax errors). | The interpolation parser treated the colon as a formatting separator. | Parenthesized the conditional expression; localized syntax example was explicitly requested (Q13). |
| Called captured locals “global variables” and initially thought changing `isRunning` stopped the loop immediately. | Confused enclosing scope with global state, and condition checks with immediate jumps. | Explained capture during review. Learner correctly reasoned that initialization inside the loop resets state and that code after `HandleChoice` still executes before the next condition check. |
| Saved locals used PascalCase. | Used property-style naming for local variables. | Renamed to `taskTitle`, `taskDescription` and `isTaskCompleted`; use camelCase for locals. |

- **Demonstrated understanding:** Saved assignments belong after validation; `??` only replaces null; initialization inside the loop resets values; the current iteration continues after a flag changes. Learner identified the null-dereference risk of reversing the validation conditions; review explained short-circuit evaluation.
- **Verified:** All D02 criteria; title retries, trimming, missing-task actions, completion/repeated completion, replacement/reset, empty descriptions, invalid choices and fresh-run state. EOF at the menu, title, title retry and description exits cleanly; replacement EOF reports no success. Preservation of the old state at EOF was checked by source inspection.
- **Closed:** Fresh build had zero warnings/errors, no blocking findings remained, understanding checks passed and learner approved completion on 2026-10-14. Deeper reference/value-type behavior remains practice for D03, not assumed mastery. No reviewer source edits or Git commit.
