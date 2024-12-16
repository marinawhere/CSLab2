using Moq;
using NUnit.Framework;
using app.Interfaces;
using app.Models;
using app.Services.Testing;

namespace app.Tests.Services.Testing
{
    [TestFixture]
    public class TestServiceTests
    {
        private Mock<IDictionaryRepository> _repositoryMock;
        private Mock<IMenu> _menuMock;
        private TestService _testService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IDictionaryRepository>();
            _menuMock = new Mock<IMenu>();
            _testService = new TestService(_repositoryMock.Object, _menuMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _repositoryMock = null;
            _menuMock = null;
            _testService = null;
        }

        [Test]
        public void StartTesting_EmptyRepository_DisplaysEmptyDictionaryMessage()
        {
            // Arrange
            _repositoryMock.Setup(r => r.HasWords()).Returns(false);

            // Act
            _testService.StartTesting();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Словарь пуст. Добавьте слова для начала тестирования."), Times.Once);
        }
        
        [Test]
        public void StartTesting_NonEmptyRepository_UserExits_TestingEnds()
        {
            // Arrange
            var word = new Word("hello", "привет", 1);
            _repositoryMock.Setup(r => r.HasWords()).Returns(true);
            _repositoryMock.Setup(r => r.GetRandomWord()).Returns(word);
            _menuMock.Setup(m => m.Prompt(It.IsAny<string>())).Returns("exit");

            // Act
            _testService.StartTesting();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Начинаем тестирование! Введите 'exit' для выхода в главное меню."), Times.Once);
            _menuMock.Verify(m => m.Prompt(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void StartTesting_NonEmptyRepository_NoMoreWords_DisplaysEmptyDictionaryMessage()
        {
            // Arrange
            var word = new Word("hello", "привет", 1);
            _repositoryMock.Setup(r => r.HasWords()).Returns(true);
            _repositoryMock.SetupSequence(r => r.GetRandomWord())
                .Returns(word)  // Первое слово
                .Returns((Word)null);  // После этого возвращаем null

            _menuMock.Setup(m => m.Prompt(It.IsAny<string>())).Returns("привет");

            // Act
            _testService.StartTesting();

            // Assert
            _menuMock.Verify(m => m.DisplayMessage("Словарь пуст. Добавьте слова для продолжения тестирования."), Times.Once);
        }
    }
}
