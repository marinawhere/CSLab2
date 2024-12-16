using app.Interfaces;
using LinqToDB;
using System;

namespace app.Services.WordManagement
{
    // Сервис для управления словами в словаре (добавление и удаление).
    public class WordService : IWordService
    {
        private readonly DatabaseConnection _db;
        private readonly IMenu _menu;

        // Конструктор принимает подключение к базе данных и интерфейс меню.
        public WordService(DatabaseConnection db, IMenu menu)
        {
            _db = db;
            _menu = menu;
        }

        // Асинхронно добавляет новое слово в словарь.
        public async Task AddNewWordAsync()
        {
            var words = _db.GetTable<Word>();
            Console.WriteLine();
            string english = _menu.Prompt("Введите новое слово на английском: ").Trim();

            if (string.IsNullOrEmpty(english))
            {
                _menu.DisplayMessage("Слово не может быть пустым.");
                return;
            }

            string russian = _menu.Prompt("Введите перевод этого слова на русский: ").Trim();

            if (string.IsNullOrEmpty(russian))
            {
                _menu.DisplayMessage("Перевод не может быть пустым.");
                return;
            }

            int memoryLevel;
            while (true)
            {
                string levelInput = _menu.Prompt("Введите начальный уровень запоминания (от 1 до 3): ").Trim();
                if (int.TryParse(levelInput, out memoryLevel) && memoryLevel is >= 1 and <= 3)
                {
                    break;
                }

                _menu.DisplayMessage("Неверный уровень. Пожалуйста, введите число от 1 до 3.");
            }

            try
            {
                // Проверка на существование слова
                var existingWord = await words.FirstOrDefaultAsync(w => w.English == english);
                if (existingWord != null)
                {
                    _menu.DisplayMessage($"Слово '{english}' уже существует в словаре.");
                    return;
                }

                // Добавление нового слова
                await _db.InsertAsync(new Word
                {
                    English = english,
                    Russian = russian,
                    MemoryLevel = memoryLevel
                });

                _menu.DisplayMessage($"Слово '{english}' успешно добавлено в словарь.");
            }
            catch (Exception ex)
            {
                _menu.DisplayMessage($"Ошибка: {ex.Message}");
            }
        }

        // Асинхронно удаляет слово из словаря.
        public async Task DeleteWordAsync()
        {
            var words = _db.GetTable<Word>();
            Console.WriteLine();
            string english = _menu.Prompt("Введите слово на английском, которое хотите удалить из словаря: ").Trim();

            if (string.IsNullOrEmpty(english))
            {
                _menu.DisplayMessage("Слово не может быть пустым.");
                return;
            }

            try
            {
                // Удаление слова
                int rowsAffected = await words
                    .Where(w => w.English == english)
                    .DeleteAsync();

                if (rowsAffected > 0)
                {
                    _menu.DisplayMessage($"Слово '{english}' успешно удалено из словаря.");
                }
                else
                {
                    _menu.DisplayMessage($"Слово '{english}' не найдено в словаре.");
                }
            }
            catch (Exception ex)
            {
                _menu.DisplayMessage($"Ошибка: {ex.Message}");
            }
        }
    }
}