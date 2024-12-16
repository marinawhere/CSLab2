using app.Interfaces;
using app.Services.Testing;
using Moq;
using Xunit;

public class TestingServiceTest
{
    private readonly Mock<IDictionaryRepository> _repositoryMock;
    private readonly Mock<IMenu> _menuMock;
    private readonly TestService _testService;

    public TestingServiceTest()
    {
        _repositoryMock = new Mock<IDictionaryRepository>();
        _menuMock = new Mock<IMenu>();
        _testService = new TestService(_repositoryMock.Object, _menuMock.Object);
    }

    [Fact]
    public async Task StartTestingAsync_ShouldDisplayMessage_WhenDictionaryIsEmpty()
    {
        // Arrange
        _repositoryMock.Setup(r => r.HasWordsAsync()).ReturnsAsync(false);

        // Act
        await _testService.StartTestingAsync();

        // Assert
        _menuMock.Verify(m => m.DisplayMessage("Словарь пуст. Добавьте слова для начала тестирования."), Times.Once);
    }

    [Fact]
    public async Task StartTestingAsync_ShouldExit_WhenUserTypesExit()
    {
        // Arrange
        _repositoryMock.Setup(r => r.HasWordsAsync()).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.GetRandomWordAsync())
            .ReturnsAsync(new Word { English = "test", Russian = "тест" });
        _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
            .Returns("exit"); // Simulate user input "exit"

        // Act
        await _testService.StartTestingAsync();

        // Assert
        _menuMock.Verify(m => m.Prompt("Переведите слово: test\nВаш ответ: "), Times.Once);
        _menuMock.Verify(m => m.DisplayMessage("Начинаем тестирование! Введите 'exit' для выхода в главное меню."),
            Times.Once);
    }

    [Fact]
    public async Task StartTestingAsync_ShouldDisplayCorrectMessage_WhenAnswerIsCorrect()
    {
        // Arrange
        _repositoryMock.Setup(r => r.HasWordsAsync()).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.GetRandomWordAsync())
            .ReturnsAsync(new Word { English = "test", Russian = "тест" });
        _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
            .Returns("тест") // Simulate correct user input
            .Returns("exit"); // Exit test

        // Act
        await _testService.StartTestingAsync();

        // Assert
        _menuMock.Verify(m => m.DisplayMessage("Правильно!"), Times.Once);
    }

    [Fact]
    public async Task StartTestingAsync_ShouldDisplayCorrectMessage_WhenAnswerIsIncorrect()
    {
        // Arrange
        _repositoryMock.Setup(r => r.HasWordsAsync()).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.GetRandomWordAsync())
            .ReturnsAsync(new Word { English = "test", Russian = "тест" });
        _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
            .Returns("wrong") // Simulate incorrect user input
            .Returns("exit"); // Exit test

        // Act
        await _testService.StartTestingAsync();

        // Assert
        _menuMock.Verify(m => m.DisplayMessage("Неправильно. Правильный ответ: тест"), Times.Once);
    }
}