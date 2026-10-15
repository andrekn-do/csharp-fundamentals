# Challenges — C# Fundamentals Refresher

Work in `starter/TaskManager/`, keeping your changes as you progress. These are reusable requirements, not the author's completion checklist. Public boxes intentionally start unchecked. [Personal progress](LEARNING_LOG.md) is separate.

Follow **build → test → explain → review → improve**. Advance when the acceptance checks pass, no known correctness issues remain and you can explain your choices. Independent learners need neither AI nor author approval. Optional hints and [Q&A](QUESTIONS_AND_ANSWERS.md) are available when needed; try first before comparing source. Take more than one session if necessary.

Available: [D01](#d01-console-menu) → [D02](#d02-create-and-manage-one-task) → [D03](#d03-model-one-task-with-a-class-and-enum). [D04–D10](#planned-challenges) are provisional.

## D01: Console menu

### Goal
Create an interactive menu that repeatedly accepts a choice until exit. Practice console I/O, branching, loops and methods.

### Prerequisites
.NET 10 SDK, an editor, and the runnable starter project. No earlier challenge is required.

### Feature requirements
- On launch, show `1. Add task`, `2. List tasks`, `3. Complete task`, `0. Exit`.
- Read input and print an appropriate placeholder for choices 1–3. Do not store tasks yet.
- Choice 0 exits normally. Any invalid choice, including empty/whitespace-only input, shows a helpful message and returns to the menu.
- EOF (end-of-input, when `Console.ReadLine()` returns null) exits cleanly without repeated output. EOF differs from pressing Enter on an empty line.
- Keep menu display and choice handling in understandable methods/local functions where appropriate. Choose `switch` or `if`, and any clear loop structure.

### Acceptance criteria
- [ ] All four options appear on launch.
- [ ] Choices 1–3 print the correct placeholders and redisplay the menu.
- [ ] Choice 0 exits with a successful exit code.
- [ ] Unsupported numbers, nonnumeric text and empty/whitespace-only input explain the problem and retry.
- [ ] EOF exits cleanly without an exception or repeated output.
- [ ] Responsibilities and the stopping condition are understandable; build has no compiler warnings/errors.

### Manual test cases and expected behavior
1. Enter `1`, `2`, `3`: each prints its placeholder and returns to the menu.
2. Enter `99`, `-2`, `hello`, an empty line and a spaces-only line: each is invalid and retries.
3. Enter `0`: application exits normally without another menu.
4. Start again and signal EOF: application exits without repeated messages. On Windows, use Ctrl+Z then Enter; on Unix-like terminals, Ctrl+D on an empty line. Redirected input reaching its end also signals EOF.

### C# concepts to practice
`Console.WriteLine`, `Console.ReadLine`, `string`/`string?`, conditions, `switch` or `if`, `while` or `do...while`, local functions/methods, booleans, `break` and `return`.

### Optional hints
- Decide what owns navigation and when it checks the exit decision.
- Empty input and ended input need different handling.
- Compare a switch statement and expression in scratch code if curious; do not force both into the application. Numeric parsing is optional for this menu.

### Understanding questions
1. What actually stops your loop, and when does it check that decision?
2. How do an empty line and EOF differ in the value returned by `ReadLine()`?
3. What does `break` exit when it appears inside a switch inside a loop?
4. Why separate display/input from choice handling?

### Next and reference guidance
Continue to [D02](#d02-create-and-manage-one-task) after validating and explaining your work. No models, collections, persistence, external packages or test framework are needed yet. [Current reference](../src/TaskManager/Program.cs) includes D02 behavior; it is not a D01-only snapshot. Reference code is optional and one possible approach.

## D02: Create and manage one task

### Goal
Extend D01 to create, display and complete one task in memory. Practice variables, state changes, methods, parameters and return values before introducing a class.

### Prerequisites
Complete D01's behavior and explain its input/exit flow. Continue in the same starter project. Be comfortable with strings, booleans and simple functions.

### Feature requirements
- Store one task's title, optional description and completion flag using simple variables. Distinguish missing state from an existing incomplete task.
- Add: prompt for a required title, trim whitespace and re-prompt for blank titles. Prompt for a description; an empty description is valid.
- Save only after all input succeeds. A successful add replaces any previous task, starts incomplete and gives a clear confirmation.
- List: display title, description or a readable indication of no description, and `Incomplete`/`Complete` status. Explain when no task exists.
- Complete: mark the task complete. Explain when no task exists or it is already complete.
- Preserve D01 navigation, invalid-choice handling and EOF exit. EOF at either Add prompt exits without saving partial input or replacing existing state.
- Use understandable methods/local functions; parameters and return values where helpful. Capturing enclosing local state is also acceptable at this stage.

### Acceptance criteria
- [ ] Before adding, List and Complete report no task and return to the menu.
- [ ] Add accepts trimmed valid fields, confirms success and returns to the menu.
- [ ] Blank/whitespace-only titles re-prompt without overwriting stored state.
- [ ] List displays the fields, a readable empty-description case and initial `Incomplete` status.
- [ ] Complete changes status to `Complete`; repeating it gives a helpful message.
- [ ] A successful replacement discards the old task and resets status to `Incomplete`.
- [ ] State persists across menu iterations and is absent after restart.
- [ ] Invalid choices retry; 0 and EOF exit cleanly. EOF during Add never saves partial input or reports success.
- [ ] Names and data flow are understandable; build has no compiler warnings/errors.

### Manual test cases and expected behavior
1. Fresh run, List then Complete: both explain that no task exists.
2. Add, enter empty and spaces-only titles, then `  Buy milk  ` and `  Two cartons  `: titles re-prompt; List shows trimmed fields and `Incomplete`.
3. Complete, List, Complete again: status persists as `Complete`; the repeated action explains that it is already complete.
4. Add `Read chapter` with an empty description: List shows only the replacement, an empty-description label and `Incomplete`.
5. Try `99`, text, `-2` and empty menu input: each retries. Exit with 0; restart and List reports no task.
6. Signal EOF at the menu, title prompt, title retry and description prompt: clean exit without looping or partial-save confirmation.
7. Repeat description EOF while replacing a completed task. No replacement success is reported. Inspect the assignments (or use a debugger) to confirm old state remains untouched until input succeeds.

### C# concepts to practice
`string`, `string?`, `bool`, assignment, comparisons and boolean operators, scope, methods/local functions, parameters, return values, interpolation, string validation and short-circuit evaluation. Reuse D01's input handling.

### Optional hints
- Decide how to represent “no task”; it is different from “incomplete.”
- State that survives menu iterations needs an appropriate scope.
- Keep temporary replacement input separate from stored state until validation finishes.

### Understanding questions
1. How does your code distinguish no task from an incomplete task, and preserve state between iterations?
2. What happens to the old task when replacement input is invalid or ends early, and why?
3. How do your functions access state or communicate results, and why did you choose that arrangement?
4. Why must an empty description and EOF behave differently?

### Next and reference guidance
Continue to [D03](#d03-model-one-task-with-a-class-and-enum) after checking behavior and understanding. No custom task type, enum, collections, task IDs, dates, LINQ, persistence, external packages or test framework yet. Compare with the [current reference](../src/TaskManager/Program.cs) after attempting the challenge; it currently represents D02 and will evolve.

## D03: Model one task with a class and enum

### Goal
Replace D02's separate saved variables with one task object. Practice classes, properties, constructors and enums while preserving the single-task CLI behavior.

### Prerequisites
Complete D02 and understand its state lifetime, validation and EOF behavior. Continue in the starter project; no collections are required.

### Feature requirements
- Define `TaskItem` in `starter/TaskManager/TaskItem.cs` and `TaskItemStatus` in `starter/TaskManager/TaskItemStatus.cs`. The enum has `Incomplete` and `Complete` values.
- Expose `Title`, `Description` and `Status` as public properties rather than public fields. Title and description are non-null strings; an empty description is valid.
- Use get-only title/description properties initialized through an ordinary constructor accepting those two values. Explicitly initialize status to `Incomplete`. Status may use a public setter for this milestone.
- Keep trimming and input validation before construction; constructor exception validation is deferred.
- Store one nullable task reference: null means no task. Remove duplicate saved title/description/boolean state.
- Add constructs and stores a new object only after successful input. List reads properties; Complete changes enum status and handles repeated completion.
- Keep console I/O/navigation in `Program.cs` and data in the class. Preserve all D02 behavior. The author implements these types under `src/TaskManager/` in their separate workflow.

### Acceptance criteria
- [ ] Class and enum are in their own files; build has no compiler warnings/errors.
- [ ] Title/description are initialized non-null properties, and construction explicitly initializes `Incomplete` status.
- [ ] One nullable reference represents saved state; no duplicate saved fields or completion boolean remains.
- [ ] List and Complete safely handle the no-task case.
- [ ] Add trims input, retries blank titles, accepts empty descriptions and saves only after all checks.
- [ ] List shows title, a readable description/empty-description label and named enum status.
- [ ] Complete updates status and explains repeated completion.
- [ ] Successful replacement starts incomplete; EOF during replacement does not replace the object or report success.
- [ ] D01 navigation and D02 state-lifetime behavior are preserved.

### Manual test cases and expected behavior
1. Fresh run, List then Complete: both report no task.
2. Add using empty/whitespace titles then `  Buy milk  ` and `  Two cartons  `: retry invalid titles; List shows trimmed fields and `Incomplete`.
3. Complete, List, Complete again: stored status is `Complete`, with a helpful repeated-action message.
4. Replace with `Read chapter` and an empty description: only the new task is shown, with a placeholder description and `Incomplete`.
5. Invalid choices retry; 0 exits; restarting loses the task.
6. EOF at menu, title, title retry and description exits cleanly. During replacement EOF, no success is reported; inspect assignment timing to confirm the old object is untouched.

### C# concepts to practice
`class`, reference types, nullable references, `new`, ordinary constructors, parameters, auto-properties, get-only properties, `get`/`set`, basic access modifiers, `enum`, member access and enum comparison. camelCase locals/parameters; PascalCase types/properties/enum members.

### Optional hints
- Collect validated input before constructing and saving a replacement object.
- Consider which properties need to change after construction.
- Guard the missing-object case before accessing properties. Keep the model free of console interaction.

### Understanding questions
1. How does null differ from an incomplete task, and why check before property access?
2. What does the constructor guarantee, and why initialize status explicitly instead of relying on enum defaults?
3. Why use properties, and why allow different write access for title/description and status?
4. If two variables reference the same object and one changes status, what does the other observe? How is assigning a new object to one variable different?

### Next and reference guidance
D04 is planned; its detailed requirements are not available yet. No collections, task IDs, dates, extra statuses, LINQ, persistence, async, inheritance, interfaces or new architecture in D03. The [current reference](../src/TaskManager/Program.cs) has not implemented D03; use the [Q&A](QUESTIONS_AND_ANSWERS.md) for concepts rather than expecting a completed model example.

## Planned challenges

These are provisional topics, not ready-to-follow specifications. Dates and other roadmap concepts will be introduced when justified by concrete requirements.

| ID | Planned topic |
|---|---|
| D04 | Manage multiple tasks in memory |
| D05 | Edit, delete, complete and find by ID |
| D06 | Query and summarize using LINQ |
| D07 | Validate input and handle failure cases |
| D08 | Save/load JSON |
| D09 | Refactor responsibilities and review design |
| D10 | Focused tests and an independent enhancement |

Return to the [roadmap](ROADMAP.md) or [README](../README.md). Current reference code is not a set of historical snapshots; see the README's reference guidance.
