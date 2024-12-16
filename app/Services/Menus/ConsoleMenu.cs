using app.Interfaces;

namespace app.Services.Menus;

// Реализация меню для консольного приложения.
public class ConsoleMenu : IMenu
{
    // Отображает меню пользователю.
    public void Show()
    {
        Console.WriteLine();
        Console.WriteLine("Меню:");
        Console.WriteLine("1. Начать тестирование");
        Console.WriteLine("2. Добавить новое слово в словарь");
        Console.WriteLine("3. Удалить слово из словаря");
        Console.WriteLine("4. Выйти из программы");
        Console.WriteLine();
    }

    // Получает выбор пользователя.
    // Возвращает выбор пользователя в виде строки.
    public string GetUserChoice()
    {
        Console.Write("Выберите действие (1-4): ");
        // Используем оператор null-объединения, чтобы избежать возможного null
        return Console.ReadLine() ?? string.Empty;
    }

    // Отображает сообщение пользователю.
    // "message" - Сообщение для отображения.
    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Запрашивает ввод от пользователя с отображением подсказки.
    // "message" - Сообщение-подсказка для ввода.
    // Возвращает введённую пользователем строку.
    public string Prompt(string message)
    {
        Console.Write(message);
        // Используем оператор null-объединения, чтобы избежать возможного null
        return Console.ReadLine() ?? string.Empty;
    }
}