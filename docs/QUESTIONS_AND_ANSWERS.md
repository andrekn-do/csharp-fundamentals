# Project 1 — C# Fundamentals Refresher: Questions and Answers

Questions are grouped by the learning phases in `ROADMAP.md`. Add future questions when the learner provides them, using sequential IDs and the same answer, example, and "What to remember" format. Entries are reference notes, not evidence of mastery.

**Knowledge Questions:** This file answers learner-asked C#/.NET questions. Record implementation bugs, misunderstandings, corrections and practical lessons in `LEARNING_LOG.md`; link between files when helpful rather than duplicating explanations.

## Phase 1: C# Basics and Program Execution

### Q01: Why does Program.cs work without Main()?

**Answer:** It uses top-level statements: executable statements written outside an explicitly declared class or method. The C# compiler generates a `Program` class and an entry point method containing those statements. The application still has an entry point; you do not have to write it yourself.

**Example:** This greeting appeared in the original placeholder `Program.cs`; it illustrates a top-level statement and is not a quote from the current reference application:

```csharp
Console.WriteLine("Task Manager CLI — ready for Day 1");
```

**What to remember:**

- Top-level statements remove entry point boilerplate.
- Only one source file per project can contain top-level statements.
- `Program.cs` is a conventional filename; the filename does not enable the feature.

Reference: [Microsoft Learn: Top-level statements](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements).

### Q02: What does dotnet run --project src/TaskManager do?

**Answer:** From the repository root, `--project src/TaskManager` selects the project in that directory: `TaskManager.csproj`. The .NET CLI implicitly restores dependencies as needed, builds the project using incremental build checks, and runs its application. If restore or build fails, the application does not start.

Restore resolves the project's dependencies and downloads missing NuGet packages when required. It does not mean that packages are downloaded on every run.

**Example:** These commands select the same project when run from the repository root:

```shell
dotnet run --project src/TaskManager
dotnet run --project src/TaskManager/TaskManager.csproj
```

**What to remember:**

- `--project` selects the project to run; a relative path is resolved from your current directory.
- A normal `dotnet run` handles restore and build before launching the application.
- `--no-build` skips building and implicitly skips restore; use it only when suitable build output already exists.

Reference: [Microsoft Learn: dotnet run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run).

### Q03: Does the C# entry-point source file need to be named Program.cs?

**Answer:** No. The filename does not determine the application's entry point. In SDK-style projects, `.cs` files under the project directory are normally included automatically, subject to default exclusions and project configuration. The compiler determines the entry point from their contents.

For example, renaming `Program.cs` to `App.cs` does not prevent top-level statements from working.

**What to remember:** `Program.cs` is a convention, not a language requirement.

Reference: [Microsoft Learn: .NET project SDK overview](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview).

### Q04: How does C# select the entry point when multiple files contain executable code?

**Answer:**

- Only one source file in a compilation can contain top-level statements. Two files with top-level statements produce CS8802.
- Without top-level statements, a single valid `Main()` is selected automatically.
- Multiple valid `Main()` candidates produce CS0017 unless a startup type is specified.
- The compiler selects the entry point during compilation; the .NET runtime does not search source files.

Executable code inside other methods does not make those methods entry points.

**What to remember:** An executable assembly has one selected entry point.

Reference: [Microsoft Learn: Entry-point errors and warnings](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/entry-point-errors).

### Q05: Why doesn't a method execute automatically when it is defined?

**Answer:** Defining a method makes it available but does not execute its body. A method runs when it is invoked.

**Example:** This declaration alone prints nothing:

```csharp
public class Helper
{
    public static void SayGoodbye()
    {
        Console.WriteLine("Goodbye!");
    }
}
```

Calling `Helper.SayGoodbye()` executes the method and prints the message.

**What to remember:** The entry point starts execution; other methods execute only when invoked.

### Q06: Must a C# filename match its class name?

**Answer:** No. A file named `Banana.cs` can define a class named `TaskItem`. Matching the filename to the main type is a readability and maintainability convention.

**What to remember:** File organization and type naming are separate concerns.

### Q07: Can two C# classes have the same name?

**Answer:** Yes, when they belong to different namespaces.

**Example:** In one file:

```csharp
namespace School;
public class Person { }
```

In another file:

```csharp
namespace Company;
public class Person { }
```

These are distinct types: `School.Person` and `Company.Person`.

Two ordinary class declarations with the same name in the same namespace conflict. Compatible `partial` declarations can instead form one class.

If both namespaces are imported and `Person` is ambiguous, use fully qualified names or aliases.

**What to remember:** Namespaces help identify and distinguish types; filenames do not.

### Q08: What happens when another class contains Main(), with or without StartupObject?

**Answer:** The following assumes each explicit `Main()` is a valid entry-point candidate and `Helper` is a class in the global namespace.

| Scenario | Result |
|---|---|
| Top-level statements + `Helper.Main()`, no `StartupObject` | Top-level statements execute; explicit `Main()` is ignored as an entry point (CS7022 warning) |
| Only `Helper.Main()`, no `StartupObject` | `Helper.Main()` is selected |
| Only `Helper.Main()`, `StartupObject = Helper` | `Helper.Main()` is selected explicitly |
| Top-level statements + `Helper.Main()`, `StartupObject = Helper` | Compilation error CS8804 |
| Two valid explicit `Main()` methods in different types, no `StartupObject` | Compilation error CS0017 |
| Two valid explicit `Main()` methods in different types, `StartupObject = Helper` | `Helper.Main()` is selected |

**Example configuration:** Inside a project file's `PropertyGroup`:

```xml
<StartupObject>Helper</StartupObject>
```

`StartupObject` identifies the type containing a valid entry-point method, not a filename or arbitrary method. Include the namespace when applicable, for example `School.Helper`. A valid non-generic struct can also be a startup type.

**What to remember:** Top-level statements take priority. `StartupObject` selects an explicit entry-point type when top-level statements are absent; it cannot override top-level statements.

References: [Microsoft Learn: Top-level statements](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements), [Microsoft Learn: StartupObject](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/advanced#startupobject).

### Q09: If Helper.Main() is selected as the entry point, will Program.Main() execute automatically afterward?

**Answer:** No. Only the selected entry point is invoked automatically when the application starts.

The other explicit `Main()` remains an ordinary static method, which can be called explicitly if it is accessible to the caller.

**What to remember:** A method being named `Main` does not mean it executes automatically.

### Q10: What are top-level statements, and can they use a struct?

**Answer:** Top-level statements are instructions outside an explicitly declared type or method, such as a variable declaration, method call or loop. Local functions can also be declared in that top-level code. A `struct` declaration defines a type; it is not an executable statement. Top-level code can use that type, but in the same file all top-level code must precede type/namespace declarations, or CS8803 results. Types can instead be defined in separate files.

**What to remember:** Using a type and defining it are different. A type can be declared later in the file than its usage.

Reference: [Microsoft Learn: Top-level statements](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements).

### Q11: Can a struct hold null, and how does that differ from `string?`?

**Answer:** An ordinary struct is a value type and cannot hold null. `SomeStruct?` means `Nullable<SomeStruct>`, which can contain a struct value or no value. `string?` instead annotates an existing reference type for compiler null analysis; it does not introduce a wrapper. `default(SomeStruct)` is a struct value with default fields, not an absent value.

**What to remember:** Nullable value types wrap values; nullable reference annotations express intent without changing the runtime type.

### Q12: Why can't I access Title directly on a nullable struct?

**Answer:** A nullable struct variable is a `Nullable<T>` wrapper, not the contained struct itself. Check that it has a value before accessing `.Value` and then the struct's members. Reading `.Value` with no value throws `InvalidOperationException`. This differs from a nullable class reference, whose members can be accessed after a null check without unwrapping.

**What to remember:** Prove a nullable struct contains a value before unwrapping it.

Reference for Q11–Q12: [Microsoft Learn: Nullable value types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types).

### Q13: How do I use a conditional expression inside string interpolation?

**Answer:** Parenthesize the whole conditional expression so its colon is not interpreted as an interpolation format separator.

```csharp
Console.WriteLine($"Status: {(isTaskCompleted ? "Complete" : "Incomplete")}");
```

**What to remember:** Inside interpolation braces, group `condition ? first : second` in parentheses.

Reference: [Microsoft Learn: String interpolation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated).

### Q14: Are variables shared with my local functions global?

**Answer:** No. D02's saved variables are locals in the generated entry-point method. Non-static local functions can capture variables from their enclosing scope and read or update them. Declaring and initializing saved state outside the menu loop preserves it across iterations; initialization inside the loop would reset it each time.

**What to remember:** Shared access through capture does not make a variable global.

### Q15: Why doesn't ?? replace an empty description?

**Answer:** `??` uses its fallback only when the left operand is null. An empty string (`""`) is still a non-null string. Check for emptiness separately when choosing a display label.

**What to remember:** Null, empty and whitespace-only strings are distinct values; trimming whitespace-only input produces an empty string.

### Q16: Why is title != null && title.Length == 0 safe?

**Answer:** `&&` evaluates its right operand only when its left operand is true. A null title makes the first condition false, so `Length` is skipped. Reversing the conditions accesses `Length` first and throws `NullReferenceException` for null.

**What to remember:** Guard a potentially unsafe operation on the left side of `&&`.

### Q17: Does setting isRunning to false immediately exit a while loop?

**Answer:** No. A `while` checks its condition before each iteration. Changing the flag does not interrupt the current iteration. Returning from Add goes back to its caller; a switch `break` exits the switch; remaining loop-body statements still execute before the next condition check.

**What to remember:** Assignment changes state; `return` and `break` transfer control within their respective scopes.

### Key Takeaways

- **Entry points:** The C# compiler selects one entry point. The .NET CLI handles project selection, restore, build, and launch; the .NET runtime executes the compiled application using its designated entry point.
- **Top-level statements:** They generate an entry point and are allowed in only one source file per compilation. They cannot be combined with `StartupObject`.
- **Filenames:** `Program.cs` and matching filenames to class names are conventions, not language requirements.
- **Namespaces:** They distinguish types with the same simple name. Fully qualified names or aliases resolve ambiguity.
- **Method invocation:** Declaring a method does not run its body. Unselected `Main()` methods are not automatically chained after the entry point.

## Phase 2: Task data model

No questions recorded yet.

## Phase 3: Task collections

No questions recorded yet.

## Phase 4: Querying

No questions recorded yet.

## Phase 5: Reliability

No questions recorded yet.

## Phase 6: Persistence

No questions recorded yet.

## Phase 7: Design cleanup

No questions recorded yet.

## Phase 8: Quality and final exercise

No questions recorded yet.
