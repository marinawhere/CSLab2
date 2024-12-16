namespace app.Interfaces;

// Представляет абстракцию для выполнения команды.
public interface ICommand
{
    // Выполняет действие, ассоциированное с командой.
    Task Execute();
}