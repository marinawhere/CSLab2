//using System.Text.Json;
//using NUnit.Framework;
//using app.Models;
//using app.Services.Storage;

//namespace app.Tests.Services.Storage
//{
//    [TestFixture]
//    public class JsonStorageTests
//    {
//        private string _filePath;
//        private JsonStorage _jsonStorage;

//        [SetUp]
//        public void Setup()
//        {
//            // Создаем временный файл для тестирования
//            _filePath = Path.GetTempFileName();
//            _jsonStorage = new JsonStorage(_filePath);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            // Удаляем временный файл после выполнения каждого теста
//            if (File.Exists(_filePath))
//            {
//                File.Delete(_filePath);
//            }
//        }

//        [Test]
//        public async Task LoadAsync_FileDoesNotExist_ReturnsEmptyList()
//        {
//            // Удаляем файл перед тестом, если он вдруг остался
//            if (File.Exists(_filePath))
//            {
//                File.Delete(_filePath);
//            }

//            // Act
//            var result = await _jsonStorage.LoadAsync();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsEmpty(result);
//        }

//        [Test]
//        public async Task LoadAsync_FileExists_ReturnsDeserializedWords()
//        {
//            // Arrange
//            var words = new List<Word>
//            {
//                new Word ("Test1", "Тест1", 1),
//                new Word ("Test2", "Тест2", 2)
//            };
//            string json = JsonSerializer.Serialize(words);
//            await File.WriteAllTextAsync(_filePath, json);

//            // Act
//            var result = await _jsonStorage.LoadAsync();

//            // Assert
//            Assert.That(result.Count, Is.EqualTo(2));
//            Assert.That(result[0].English, Is.EqualTo("Test1"));
//            Assert.That(result[0].Russian, Is.EqualTo("Тест1"));
//            Assert.That(result[0].MemoryLevel, Is.EqualTo(1));
//            Assert.That(result[1].English, Is.EqualTo("Test2"));
//            Assert.That(result[1].Russian, Is.EqualTo("Тест2"));
//            Assert.That(result[1].MemoryLevel, Is.EqualTo(2));
//        }

//        [Test]
//        public async Task LoadAsync_FileContainsInvalidJson_ReturnsEmptyList()
//        {
//            // Arrange
//            await File.WriteAllTextAsync(_filePath, "{ invalid json");

//            // Act
//            var result = await _jsonStorage.LoadAsync();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsEmpty(result);
//        }

//        [Test]
//        public void Save_ValidWordsList_CreatesFileWithCorrectJsonContent()
//        {
//            // Arrange
//            var words = new List<Word>
//            {
//                new Word ("Test1", "Тест1", 1),
//                new Word ("Test2", "Тест2", 2)
//            };

//            // Act
//            _jsonStorage.Save(words);

//            // Assert
//            Assert.IsTrue(File.Exists(_filePath));

//            string json = File.ReadAllText(_filePath);
//            var deserializedWords = JsonSerializer.Deserialize<List<Word>>(json);

//            Assert.IsNotNull(deserializedWords);
//            Assert.That(deserializedWords.Count, Is.EqualTo(2));
//            Assert.That(deserializedWords[0].English, Is.EqualTo("Test1"));
//            Assert.That(deserializedWords[0].Russian, Is.EqualTo("Тест1"));
//            Assert.That(deserializedWords[0].MemoryLevel, Is.EqualTo(1));
//            Assert.That(deserializedWords[1].English, Is.EqualTo("Test2"));
//            Assert.That(deserializedWords[1].Russian, Is.EqualTo("Тест2"));
//            Assert.That(deserializedWords[1].MemoryLevel, Is.EqualTo(2));
//        }

//        [Test]
//        public void Save_FileIOException_LogsError()
//        {
//            // Arrange
//            var words = new List<Word> { new Word ("Test1", "Тест", 1) };

//            // Удаляем файл и создаем каталог с тем же именем, чтобы вызвать IOException при попытке записи
//            if (File.Exists(_filePath))
//            {
//                File.Delete(_filePath);
//            }
//            Directory.CreateDirectory(_filePath);

//            // Act & Assert
//            Assert.DoesNotThrow(() => _jsonStorage.Save(words));
//        }
//    }
//}
