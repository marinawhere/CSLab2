using app.Interfaces;
using LinqToDB;


namespace app.Services.Repositories;

// Реализация интерфейса управления словарём с использованием LINQ to DB.
public class DictionaryRepository : IDictionaryRepository
{
    private readonly DatabaseConnection _db;

    // Конструктор для инициализации подключения к базе данных.
    public DictionaryRepository(DatabaseConnection db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    // Добавляет новое слово в словарь.
    public async Task AddWordAsync(Word word)
    {
        var words = _db.GetTable<Word>();
        var exists =  words.Contains(word);
        if (!exists)
        {
            await _db.InsertAsync(word);
        }
        else
        {
            throw new InvalidOperationException("Слово с таким английским значением уже существует.");
        }
    }

    // Удаляет слово из словаря по его английскому названию.
    public async Task DeleteWordAsync(string english)
    {
        var words = _db.GetTable<Word>();
        var deletedCount = await words
            .Where(w => w.English.Equals(english, StringComparison.OrdinalIgnoreCase))
            .DeleteAsync();

        if (deletedCount == 0)
        {
            throw new InvalidOperationException("Слово с таким английским значением не найдено.");
        }
    }

    // Получает случайное слово из словаря.
    public async Task<Word?> GetRandomWordAsync()
    {
        var words = _db.GetTable<Word>();
        var count = await words.CountAsync();
        if (count == 0)
        {
            return null;
        }

        int randomIndex = Random.Shared.Next(count);
        return await words.Skip(randomIndex).FirstOrDefaultAsync();
    }

    // Проверяет, содержит ли словарь хотя бы одно слово.
    public async Task<bool> HasWordsAsync()
    {
        var words = _db.GetTable<Word>();
        return await words.AnyAsync();
    }

    // Получает все слова из словаря. eto tak ne dolzno rabotat' 10 ras govoril
    
    // public async Task<List<Word>> GetAllWordsAsync()
    // {
    //     var words = _db.GetTable<Word>();
    //     return await words.ToListAsync();
    // }
}