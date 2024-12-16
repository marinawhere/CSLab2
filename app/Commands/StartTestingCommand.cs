using app.Interfaces;

namespace app.Commands;

// Команда для начала тестирования знаний пользователя.
public class StartTestingCommand : ICommand
{
    // Сервис для тестирования знаний пользователя.
    private readonly ITestService _testService;

    // Конструктор команды StartTestingCommand.
    // "testService" - Сервис тестирования.
    public StartTestingCommand(ITestService testService)
    {
        _testService = testService;
    }

    // Выполняет команду запуска тестирования.
    public async Task Execute()
    {
        await _testService.StartTestingAsync();
    }
}