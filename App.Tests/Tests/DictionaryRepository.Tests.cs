using Moq;
using NUnit.Framework;
using app.Interfaces;
using app.Models;
using app.Services.Repositories;

namespace app.Tests.Services.Repositories
{
    [TestFixture]
    public class DictionaryRepositoryTests
    {
        private Mock<IStorage> _storageMock;
        private DictionaryRepository _repository;

        [SetUp]
        public async Task Setup()
        {
            _storageMock = new Mock<IStorage>();

            // Инициализация словаря с начальными словами
            var initialWords = new List<Word>
            {
                new Word("hello", "привет", 1),
                new Word("world", "мир", 1)
            };

            _storageMock.Setup(s => s.LoadAsync()).ReturnsAsync(initialWords);
            _repository = await DictionaryRepository.CreateAsync(_storageMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _storageMock = null;
            _repository = null;
        }

        [Test]
        public void AddWord_NewWord_ReturnsTrueAndAddsWord()
        {
            // Arrange
            var newWord = new Word("test", "тест", 1);

            // Act
            bool result = _repository.AddWord(newWord);

            // Assert
            Assert.IsTrue(result);
            Assert.Contains(newWord, _repository.GetAllWords());
            _storageMock.Verify(s => s.SaveAsync(It.IsAny<List<Word>>()), Times.Once);
        }

        [Test]
        public void AddWord_ExistingWord_ReturnsFalse()
        {
            // Arrange
            var existingWord = new Word("hello", "привет", 1);

            // Act
            bool result = _repository.AddWord(existingWord);

            // Assert
            Assert.IsFalse(result);
            Assert.That(_repository.GetAllWords().Count, Is.EqualTo(2)); // Не должно увеличиться
            _storageMock.Verify(s => s.SaveAsync(It.IsAny<List<Word>>()), Times.Never);
        }

        [Test]
        public void DeleteWord_ExistingWord_ReturnsTrueAndRemovesWord()
        {
            // Arrange
            string wordToDelete = "hello";

            // Act
            bool result = _repository.DeleteWord(wordToDelete);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(_repository.GetAllWords().Any(w => w.English.Equals(wordToDelete, StringComparison.OrdinalIgnoreCase)));
            _storageMock.Verify(s => s.SaveAsync(It.IsAny<List<Word>>()), Times.Once);
        }

        [Test]
        public void DeleteWord_NonExistingWord_ReturnsFalse()
        {
            // Arrange
            string wordToDelete = "nonexistent";

            // Act
            bool result = _repository.DeleteWord(wordToDelete);

            // Assert
            Assert.IsFalse(result);
            Assert.That(_repository.GetAllWords().Count, Is.EqualTo(2)); // Количество должно остаться прежним
            _storageMock.Verify(s => s.SaveAsync(It.IsAny<List<Word>>()), Times.Never);
        }

        [Test]
        public void GetRandomWord_EmptyList_ReturnsNull()
        {
            // Arrange
            _repository.DeleteWord("hello"); // Удаляем одно слово
            _repository.DeleteWord("world"); // Удаляем другое слово

            // Act
            Word? result = _repository.GetRandomWord();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetRandomWord_NonEmptyList_ReturnsWord()
        {
            // Act
            Word? result = _repository.GetRandomWord();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(_repository.GetAllWords().Any(w => w.English == result.English));
        }

        [Test]
        public void HasWords_EmptyList_ReturnsFalse()
        {
            // Arrange
            _repository.DeleteWord("hello"); // Удаляем одно слово
            _repository.DeleteWord("world"); // Удаляем другое слово

            // Act
            bool result = _repository.HasWords();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasWords_NonEmptyList_ReturnsTrue()
        {
            // Act
            bool result = _repository.HasWords();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetAllWords_ReturnsAllWords()
        {
            // Act
            var words = _repository.GetAllWords();

            // Assert
            Assert.That(words.Count, Is.EqualTo(2));
            Assert.IsTrue(words.Any(w => w.English == "hello"));
            Assert.IsTrue(words.Any(w => w.English == "world"));
        }
    }
}
