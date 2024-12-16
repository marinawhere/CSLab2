using app.Interfaces;

namespace app.Commands;

// Команда для обработки некорректного выбора пользователя.
public class InvalidCommand : ICommand
{
    // Сервис для взаимодействия с пользователем через меню.
    private readonly IMenu _menu;

    // Конструктор команды InvalidCommand.
    // "menu" - Сервис меню для отображения сообщений.
    public InvalidCommand(IMenu menu)
    {
        _menu = menu;
    }

    // Выполняет команду вывода сообщения об ошибке.
    public async Task Execute()
    {
        _menu.DisplayMessage("Неверный выбор. Пожалуйста, выберите вариант от 1 до 4.");
    }
}