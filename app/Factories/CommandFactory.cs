using app.Commands;
using app.Interfaces;

namespace app.Factories;


// Фабрика для создания команд на основе выбора пользователя.
public class CommandFactory
{
    // Сервис для тестирования знаний.
    private readonly ITestService _testService;

    // Сервис для управления словами в словаре.
    private readonly IWordService _wordService;

    // Сервис меню для взаимодействия с пользователем.
    private readonly IMenu _menu;

    // Действие для выхода из программы.
    private readonly Action _exitAction;

    // Конструктор фабрики команд.
    // "testService" - Сервис тестирования.
    // "wordService" - Сервис управления словами.
    // "menu" - Сервис меню.
    // "exitAction" - Действие для выхода из программы.
    public CommandFactory(ITestService testService, IWordService wordService, IMenu menu, Action exitAction)
    {
        _testService = testService;
        _wordService = wordService;
        _menu = menu;
        _exitAction = exitAction;
    }

    // Возвращает соответствующую команду на основе выбора пользователя.
    // "choice" - Выбор пользователя.
    // Возвращает экземпляр класса, реализующего ICommand.
    public ICommand GetCommand(string choice)
    {
        return choice switch
        {
            "1" => new StartTestingCommand(_testService), // Команда для начала тестирования
            "2" => new AddNewWordCommand(_wordService), // Команда для добавления нового слова
            "3" => new DeleteWordCommand(_wordService), // Команда для удаления слова
            "4" => new ExitCommand(_exitAction), // Команда для выхода из программы
            _ => new InvalidCommand(_menu), // Команда для обработки некорректного выбора
        };
    }
}