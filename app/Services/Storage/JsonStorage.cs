// using System.Text.Encodings.Web;
// using System.Text.Json;
// using app.Interfaces;
//
// namespace app.Services.Storage;
//
// // Реализация хранилища данных с использованием JSON-файла.
// public class JsonStorage : IStorage
// {
//     // Путь к файлу JSON для хранения словаря.
//     private readonly string _filePath;
//
//     // Инициализирует новый экземпляр класса "JsonStorage".
//     // "filePath" - Путь к файлу JSON.
//     // exception "ArgumentNullException" - Выбрасывается, если filePath равен null или пустой строке.
//     public JsonStorage(string filePath)
//     {
//         _filePath = filePath;
//     }
//
//     // Загружает список слов из JSON-файла.
//     // Возвращает список объектов "Word".
//     public async Task<List<Word>> LoadAsync()
//     {
//         if (!File.Exists(_filePath))
//         {
//             // Файл не существует, возвращаем пустой список
//             return new List<Word>();
//         }
//
//         try
//         {
//             string json = await File.ReadAllTextAsync(_filePath);
//             // Десериализуем JSON в список слов
//             return JsonSerializer.Deserialize<List<Word>>(json) ?? new List<Word>();
//         }
//         //TODO: sdfsd  не делать обработку общих исключений (??) DONE
//         catch (FileNotFoundException)
//         {
//             Console.WriteLine("Файл не найден. Начинаем с пустого словаря.");
//             return new List<Word>();
//         }
//
//         catch (JsonException ex)
//         {
//             Console.WriteLine($"Ошибка при десериализации JSON: {ex.Message}");
//             Console.WriteLine("Начинаем с пустого словаря.");
//             return new List<Word>();
//         }
//
//         catch (IOException ex)
//         {
//             Console.WriteLine($"Ошибка ввода-вывода при загрузке словаря: {ex.Message}");
//             Console.WriteLine("Начинаем с пустого словаря.");
//             return new List<Word>();
//         }
//     }
//
//     // Сохраняет список слов в JSON-файл.
//     // "words" - Список слов для сохранения.
//
//     // Асинхронно сохраняет список слов в JSON-файл.
//     // "words" - Список слов для сохранения.
//     public async Task SaveAsync(List<Word> words)
//     {
//         try
//         {
//             var options = new JsonSerializerOptions
//             {
//                 WriteIndented = true, // Форматирование JSON для читаемости
//                 Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Разрешение не-ASCII символов
//             };
//
//             // Сериализуем список слов в JSON
//             string json = JsonSerializer.Serialize(words, options);
//
//             // Асинхронно записываем JSON в файл
//             await File.WriteAllTextAsync(_filePath, json);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Ошибка при сохранении словаря: {ex.Message}");
//         }
//     }
//
//     //public void Save(List<Word> words)
//     //{
//     //    try
//     //    {
//     //        var options = new JsonSerializerOptions
//     //        {
//     //            WriteIndented = true, // Форматирование JSON для читаемости
//     //            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Разрешение не-ASCII символов
//     //        };
//     //        // Сериализуем список слов в JSON
//     //        string json = JsonSerializer.Serialize(words, options);
//     //        File.WriteAllText(_filePath, json);
//     //    }
//     //    catch (Exception ex)
//     //    {
//     //        Console.WriteLine($"Ошибка при сохранении словаря: {ex.Message}");
//     //    }
//     //}
// }