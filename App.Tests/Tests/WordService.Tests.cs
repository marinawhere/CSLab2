using Moq;
using NUnit.Framework;
using app.Interfaces;
using app.Models;
using app.Services.WordManagement;

namespace app.Tests.Services.WordManagement
{
    [TestFixture]
    public class WordServiceTests
    {
        private Mock<IDictionaryRepository> _repositoryMock;
        private Mock<IMenu> _menuMock;
        private WordService _wordService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IDictionaryRepository>();
            _menuMock = new Mock<IMenu>();
            _wordService = new WordService(_repositoryMock.Object, _menuMock.Object);
        }

        [Test]
        public void AddNewWord_ValidInput_AddsWordToRepository()
        {
            // Arrange
            _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
                .Returns("hello")     // Ввод слова на английском
                .Returns("привет")    // Ввод перевода на русском
                .Returns("2");        // Уровень запоминания

            _repositoryMock.Setup(r => r.AddWord(It.IsAny<Word>())).Returns(true);

            // Act
            _wordService.AddNewWord();

            // Assert
            _repositoryMock.Verify(r => r.AddWord(It.Is<Word>(
                w => w.English == "hello" && w.Russian == "привет" && w.MemoryLevel == 2)), Times.Once);

            _menuMock.Verify(m => m.DisplayMessage("Слово 'hello' успешно добавлено в словарь."), Times.Once);
        }

        [Test]
        public void AddNewWord_EmptyEnglishInput_DisplaysErrorMessage()
        {
            // Arrange
            _menuMock.Setup(m => m.Prompt(It.IsAny<string>())).Returns("");

            // Act
            _wordService.AddNewWord();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Слово не может быть пустым."), Times.Once);
            _repositoryMock.Verify(r => r.AddWord(It.IsAny<Word>()), Times.Never);
        }

        [Test]
        public void AddNewWord_EmptyRussianInput_DisplaysErrorMessage()
        {
            // Arrange
            _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
                .Returns("hello")   // Ввод слова на английском
                .Returns("");       // Пустой ввод перевода на русском

            // Act
            _wordService.AddNewWord();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Перевод не может быть пустым."), Times.Once);
            _repositoryMock.Verify(r => r.AddWord(It.IsAny<Word>()), Times.Never);
        }

        [Test]
        public void AddNewWord_InvalidMemoryLevelInput_DisplaysErrorAndRetries()
        {
            // Arrange
            _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
                .Returns("hello")     // Ввод слова на английском
                .Returns("привет")    // Ввод перевода на русском
                .Returns("5")         // Неверный уровень запоминания
                .Returns("2");        // Корректный уровень запоминания

            _repositoryMock.Setup(r => r.AddWord(It.IsAny<Word>())).Returns(true);

            // Act
            _wordService.AddNewWord();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Неверный уровень. Пожалуйста, введите число от 1 до 3."), Times.Once);
            _repositoryMock.Verify(r => r.AddWord(It.Is<Word>(
                w => w.English == "hello" && w.Russian == "привет" && w.MemoryLevel == 2)), Times.Once);
        }

        [Test]
        public void AddNewWord_WordAlreadyExists_DisplaysErrorMessage()
        {
            // Arrange
            _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
                .Returns("hello")   // Ввод слова на английском
                .Returns("привет")  // Ввод перевода на русском
                .Returns("1");      // Уровень запоминания

            _repositoryMock.Setup(r => r.AddWord(It.IsAny<Word>())).Returns(false);

            // Act
            _wordService.AddNewWord();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Слово 'hello' уже существует в словаре."), Times.Once);
        }

        [Test]
        public void DeleteWord_ValidWord_DeletesWordFromRepository()
        {
            // Arrange
            _menuMock.Setup(m => m.Prompt(It.IsAny<string>())).Returns("hello");
            _repositoryMock.Setup(r => r.DeleteWord("hello")).Returns(true);

            // Act
            _wordService.DeleteWord();

            // Assert
            _repositoryMock.Verify(r => r.DeleteWord("hello"), Times.Once);
            _menuMock.Verify(m => m.DisplayMessage("Слово 'hello' успешно удалено из словаря."), Times.Once);
        }

        [Test]
        public void DeleteWord_EmptyEnglishInput_DisplaysErrorMessage()
        {
            // Arrange
            _menuMock.Setup(m => m.Prompt(It.IsAny<string>())).Returns("");

            // Act
            _wordService.DeleteWord();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Слово не может быть пустым."), Times.Once);
            _repositoryMock.Verify(r => r.DeleteWord(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void DeleteWord_WordDoesNotExist_DisplaysErrorMessage()
        {
            // Arrange
            _menuMock.Setup(m => m.Prompt(It.IsAny<string>())).Returns("hello");
            _repositoryMock.Setup(r => r.DeleteWord("hello")).Returns(false);

            // Act
            _wordService.DeleteWord();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Слово 'hello' не найдено в словаре."), Times.Once);
        }
    }
}
