# C# Fundamentals Refresher — Task Manager CLI

A self-paced .NET 10 console project for developers returning to C#. Build a small task manager through practical challenges: **build, test, explain, review, improve**. Beginners can follow along with the optional hints and concept references.

## Two independent projects

| Directory | Purpose |
|---|---|
| [`starter/TaskManager/`](starter/TaskManager/) | Minimal runnable project where independent learners implement the challenges. No completed features. |
| [`src/TaskManager/`](src/TaskManager/) | The author's evolving reference implementation. Read after attempting a challenge. |

Detailed challenges **D01–D03** are available in [TASKS.md](docs/TASKS.md). The author has completed D01 and D02; the current reference supports one in-memory task. D03 is upcoming. D04–D10 are planned, with requirements still to be developed.

## Start D01

Install the **.NET 10 SDK** and use a C# editor such as Visual Studio, VS Code or Rider. Download or clone this repository and open a terminal at its root. Git is optional for learning.

```shell
dotnet --version
dotnet build starter/TaskManager/TaskManager.csproj
dotnet run --project starter/TaskManager/TaskManager.csproj
```

The starter prints a greeting and exits. Read [D01: Console menu](docs/TASKS.md#d01-console-menu), then implement it in `starter/TaskManager/Program.cs`. Continue developing that same project for D02 and D03. Complete each checklist, run the manual cases and explain your choices before progressing. No AI tool or author approval is needed for independent learners; peer review is welcome.

## Compare with the reference

Run the author's application separately:

```shell
dotnet run --project src/TaskManager/TaskManager.csproj
```

This is one possible approach, not the only correct answer. The current source includes later work than D01; it is not a D01-only snapshot. Task-specific historical snapshots/tags are not available. They will be added only if genuine source snapshots are recovered or explicitly labeled reference versions are approved. Do not infer a historical implementation from the current source.

## Learning references

- [Roadmap](docs/ROADMAP.md): progression and provisional future topics.
- [Challenges](docs/TASKS.md): requirements, manual tests, optional hints and understanding questions.
- [C#/.NET Q&A](docs/QUESTIONS_AND_ANSWERS.md): conceptual explanations from real learning questions; consult when needed.
- [Personal learning log](docs/LEARNING_LOG.md): the author's actual progress, mistakes and corrections, including historical implementations.

The author implements application features personally. AI assists with task planning, code review, documentation and repository maintenance, including the starter setup. Personal challenge completion requires the author's approval; that workflow does not restrict independent learners.

## Contributions and license

Report unclear requirements, incorrect explanations or reproducible bugs through the repository's issues when available, or propose a small documentation correction. Include the challenge ID and expected behavior; avoid posting full solutions in challenge discussions.

Licensed under the [MIT License](LICENSE), copyright © 2026 Andre Khoi Nguyen Do.
