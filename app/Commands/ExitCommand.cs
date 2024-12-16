using app.Interfaces;

namespace app.Commands;

// Команда для выхода из программы.
public class ExitCommand : ICommand
{
    // Действие, которое выполняется при выходе из программы.
    private readonly Action _exitAction;

    // Конструктор команды ExitCommand.
    // "exitAction" - Действие для выхода из программы.
    public ExitCommand(Action exitAction)
    {
        _exitAction = exitAction;
    }

    // Выполняет команду выхода из программы.
    public async Task Execute()
    {
        _exitAction.Invoke();
    }
}