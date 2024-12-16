using app.Interfaces;
using app.Services.WordManagement;
using Moq;
using Xunit;
using LinqToDB;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using app;
using Assert = Xunit.Assert;

public class WordManagementTest
{
    private readonly Mock<IMenu> _menuMock;
    private readonly Mock<DatabaseConnection> _dbMock;
    private readonly WordService _wordService;

    public WordManagementTest()
    {
        _menuMock = new Mock<IMenu>();
        _dbMock = new Mock<DatabaseConnection>("MockConnectionString");

        _wordService = new WordService(_dbMock.Object, _menuMock.Object);
    }

    [Fact]
    public async Task AddNewWordAsync_ShouldHandleInvalidInput()
    {
        // Arrange
        _menuMock.SetupSequence(m => m.Prompt(It.IsAny<string>()))
            .Returns(string.Empty) // Empty English word
            .Returns("тест") // Valid Russian word
            .Returns("abc") // Invalid memory level
            .Returns("2"); // Valid memory level

        // Act
        await _wordService.AddNewWordAsync();

        // Assert
        _menuMock.Verify(m => m.DisplayMessage("Слово не может быть пустым."), Times.Once);
    }
    
    [Fact]
    public async Task DeleteWordAsync_ShouldHandleEmptyInput()
    {
        // Arrange
        _menuMock.Setup(m => m.Prompt(It.IsAny<string>())).Returns(string.Empty);

        // Act
        await _wordService.DeleteWordAsync();

        // Assert
        _menuMock.Verify(m => m.DisplayMessage("Слово не может быть пустым."), Times.Once);
    }
}
