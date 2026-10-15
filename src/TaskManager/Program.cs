Console.WriteLine("Welcome to Task Manager CLI");

string? taskTitle = null;
string? taskDescription = null;
bool isTaskCompleted = false;

bool isRunning = true;

void ShowMenu()
{
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. List tasks");
    Console.WriteLine("3. Complete task");
    Console.WriteLine("0. Exit");
}

void HandleChoice(int choice)
{
    switch (choice)
    {
        case 1:
            AddTask();
            break;
        case 2:
            ListTask();
            break;
        case 3:
            CompleteTask();
            break;
        case 0: 
        case -2:
            Console.WriteLine("Exiting...");
            isRunning = false;
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

void AddTask()
{
    Console.WriteLine("Adding task...");
    Console.WriteLine("Enter task title:");
    string? title = Console.ReadLine()?.Trim();
    while (title != null && title.Length == 0)
    {
        Console.WriteLine("Task title cannot be empty. Please enter a valid title:");
        title = Console.ReadLine()?.Trim();
    }
    if (title == null)
    {
        Console.WriteLine("Out of input. Add task failed.");
        isRunning = false;
        return;
    }

    Console.WriteLine("Enter task description:");
    string? description = Console.ReadLine()?.Trim();

    if (description == null)
    {
        Console.WriteLine("Out of input. Add task failed.");
        isRunning = false;
        return;
    }

    taskTitle = title;
    taskDescription = description;
    isTaskCompleted = false;
    Console.WriteLine("Add task successful.");
}

void ListTask()
{
    if (taskTitle == null)
    {
        Console.WriteLine("No tasks available.");
        return;
    }
    Console.WriteLine("Listing tasks...");
    Console.WriteLine($"Title: {taskTitle}");
    if (taskDescription != null && taskDescription.Length > 0)
    {
        Console.WriteLine($"Description: {taskDescription}");
    }
    else
    {
        Console.WriteLine("Description: No description provided");
    }
    Console.WriteLine($"Status: {(isTaskCompleted ? "Complete" : "Incomplete")}");
}

void CompleteTask()
{
    if (taskTitle == null)
    {
        Console.WriteLine("No tasks available.");
        return;
    }
    if (isTaskCompleted)
    {
        Console.WriteLine("Task is already completed.");
        return;
    }
    isTaskCompleted = true;
    Console.WriteLine("Task marked as completed.");
}


int GetInput()
{
    string? input = Console.ReadLine()?.Trim();
    int choice;
    if (int.TryParse(input, out choice))
    {
        if (choice >= 0 && choice <= 3)
        {
            return choice;
        }
        else
        {
            return -1;
        }
    }
    else if (input == null)
    {
        return -2;
    }
    else 
    {
        return -1;
    }
}

while (isRunning)
{
    ShowMenu();
    int choice = GetInput();
    HandleChoice(choice);
}


