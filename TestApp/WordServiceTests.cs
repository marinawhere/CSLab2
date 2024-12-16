using app.Commands;
using app.Factories;
using Xunit;
using Moq;
using app.Interfaces;
using Assert = Xunit.Assert;

public class WordServiceTests
{
    private readonly Mock<IWordService> _wordServiceMock;
    private readonly Mock<IMenu> _menuMock;
    private readonly Mock<IDictionaryRepository> _dictionaryRepositoryMock;
    private readonly Mock<ITestService> _testServiceMock;
    private readonly CommandFactory _commandFactory;

    public WordServiceTests()
    {
        _wordServiceMock = new Mock<IWordService>();
        _menuMock = new Mock<IMenu>();
        _dictionaryRepositoryMock = new Mock<IDictionaryRepository>();
        _testServiceMock = new Mock<ITestService>();

        bool exit = false;
        void ExitAction() => exit = true;

        _commandFactory = new CommandFactory(
            _testServiceMock.Object,
            _wordServiceMock.Object,
            _menuMock.Object,
            ExitAction
        );
    }

    [Fact]
    public void CreateAddCommand_ShouldReturnAddNewWordCommand()
    {
        // Arrange
        string choice = "2";

        // Act
        ICommand command = _commandFactory.GetCommand(choice);

        // Assert
        Assert.IsType<AddNewWordCommand>(command);
    }

    [Fact]
    public void CreateTestCommand_ShouldReturnStartTestingCommand()
    {
        // Arrange
        string choice = "1";

        // Act
        ICommand command = _commandFactory.GetCommand(choice);

        // Assert
        Assert.IsType<StartTestingCommand>(command);
    }

    [Fact]
    public void CreateDeleteCommand_ShouldReturnDeleteWordCommand()
    {
        // Arrange
        string choice = "3";

        // Act
        ICommand command = _commandFactory.GetCommand(choice);

        // Assert
        Assert.IsType<DeleteWordCommand>(command);
    }

    [Fact]
    public void CreateExitCommand_ShouldReturnExitCommand()
    {
        // Arrange
        string choice = "4";

        // Act
        ICommand command = _commandFactory.GetCommand(choice);

        // Assert
        Assert.IsType<ExitCommand>(command);
    }
    
    [Fact]
    public void InvalidInput_ShouldReturnInvaileCommand()
    {
        // Arrange
        string choice = "5";

        // Act
        ICommand command = _commandFactory.GetCommand(choice);

        // Assert
        Assert.IsType<InvalidCommand>(command);
    }

    [Fact]
    public void AddNewWord_ShouldCallRepositoryAddWordAsync()
    {
        // Arrange
        var word = new Word { English = "Test", Russian = "Тест", MemoryLevel = 1 };

        _dictionaryRepositoryMock.Setup(repo => repo.AddWordAsync(word))
            .Returns(Task.CompletedTask)
            .Verifiable();
        
    }

    [Fact]
    public void DeleteWord_ShouldCallRepositoryDeleteWordAsync()
    {
        // Arrange
        string wordToDelete = "Test";

        _menuMock.Setup(menu => menu.Prompt(It.IsAny<string>()))
            .Returns(wordToDelete);

        _dictionaryRepositoryMock.Setup(repo => repo.DeleteWordAsync(wordToDelete))
            .Returns(Task.CompletedTask)
            .Verifiable();
    }
}