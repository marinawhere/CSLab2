using LinqToDB.Mapping;

[Table(Name = "word")] // Имя таблицы в БД
public class Word
{
    [Column(Name = "English"), NotNull] // Колонка English (нельзя NULL)
    public string English { get; set; } = null!;

    [Column(Name = "Russian"), NotNull] // Колонка Russian (нельзя NULL)
    public string Russian { get; set; } = null!;

    [Column(Name = "MemoryLevel")] // Колонка MemoryLevel
    public int MemoryLevel { get; set; }
}