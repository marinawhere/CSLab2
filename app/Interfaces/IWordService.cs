namespace app.Interfaces;


// Определяет контракт для сервиса управления словами в словаре.
public interface IWordService
{
    // Добавляет новое слово в словарь.
    Task AddNewWordAsync();

    // Удаляет существующее слово из словаря.
    Task DeleteWordAsync();
}                                                                                                                                                                                                                                                                                                                                                                                   