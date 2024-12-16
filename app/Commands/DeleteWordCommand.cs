using app.Interfaces;

namespace app.Commands;

// Команда для удаления слова из словаря.
public class DeleteWordCommand : ICommand
{
    // Сервис для управления словами в словаре.
    private readonly IWordService _wordService;

    // Конструктор команды DeleteWordCommand.
    // "wordService" - Сервис для управления словами.
    public DeleteWordCommand(IWordService wordService)
    {
        _wordService = wordService;
    }

    // Выполняет команду удаления слова.
    public async Task Execute()
    {
        await _wordService.DeleteWordAsync();
    }
}