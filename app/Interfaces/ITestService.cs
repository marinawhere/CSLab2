namespace app.Interfaces;

// Определяет контракт для сервиса тестирования знаний пользователя.
public interface ITestService
{
    // Запускает процесс тестирования знаний пользователя.
    Task StartTestingAsync();
}