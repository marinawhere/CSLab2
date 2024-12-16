// using app;
// using app.Factories;
// using app.Interfaces;
// using Moq;
// using Xunit;
// using Assert = Xunit.Assert;
//
//
// public class DictionaryRepositoryTest
// {
//     private readonly Mock<IDictionaryRepository> _dictionaryRepositoryMock;
//     
//     public DictionaryRepositoryTest()
//     {
//         _dictionaryRepositoryMock = new Mock<IDictionaryRepository>();
//     }
//     
//     [Fact]
//     public async Task AddNewWord()
//     {
//         // Arrange
//         var word = new Word
//         {
//             English = "Test",
//             Russian = "Тест",
//             MemoryLevel = 1
//         };
//
//         _dictionaryRepositoryMock
//             .Setup(repo => repo.AddWordAsync(word))
//             .Returns(Task.CompletedTask);
//
//         _dictionaryRepositoryMock
//             .Setup(repo => repo.GetAllWordsAsync())
//             .ReturnsAsync(new List<Word> { word });
//
//         // Act
//         await _dictionaryRepositoryMock.Object.AddWordAsync(word);
//         var words = await _dictionaryRepositoryMock.Object.GetAllWordsAsync();
//
//         // Assert
//         Assert.Contains(word, words);
//     }
//
//     [Fact]
//     public async Task DeleteWord()
//     {
//         // Arrange
//         var word = new Word
//         {
//             English = "Test",
//             Russian = "Тест",
//             MemoryLevel = 1
//         };
//
//         var words = new List<Word> { word };
//
//         _dictionaryRepositoryMock
//             .Setup(repo => repo.GetAllWordsAsync())
//             .ReturnsAsync(words);
//
//         _dictionaryRepositoryMock
//             .Setup(repo => repo.DeleteWordAsync(word.English))
//             .Callback<string>(english => words.RemoveAll(w => w.English == english))
//             .Returns(Task.CompletedTask);
//
//         // Act
//         await _dictionaryRepositoryMock.Object.DeleteWordAsync(word.English);
//         var updatedWords = await _dictionaryRepositoryMock.Object.GetAllWordsAsync();
//
//         // Assert
//         Assert.DoesNotContain(word, updatedWords);
//     }
//
//     [Fact]
//     public async Task GetRandomWordAsync_ShouldReturnRandomWord_WhenWordsExist()
//     {
//         // Arrange
//         var mockWords = new List<Word>
//         {
//             new Word { English = "Test1", Russian = "Тест1", MemoryLevel = 1 },
//             new Word { English = "Test2", Russian = "Тест2", MemoryLevel = 1 },
//             new Word { English = "Test3", Russian = "Тест3", MemoryLevel = 1 }
//         };
//
//         _dictionaryRepositoryMock
//             .Setup(repo => repo.GetAllWordsAsync())
//             .ReturnsAsync(mockWords);
//
//         // Act
//         var words = await _dictionaryRepositoryMock.Object.GetAllWordsAsync();
//         Word? randomWord = words[new System.Random().Next(words.Count)];
//
//         // Assert
//         Assert.Contains(randomWord, mockWords);
//     }
//
//     // [Fact]
//     // public async Task HasWordsAsync_ShouldReturnTrue_WhenWordsExist()
//     // {
//     //     // Arrange
//     //     var mockWords = new List<Word>
//     //     {
//     //         new Word { English = "Test", Russian = "Тест", MemoryLevel = 1 }
//     //     };
//     //
//     //     _dictionaryRepositoryMock
//     //         .Setup(repo => repo.GetAllWordsAsync())
//     //         .ReturnsAsync(mockWords);
//     //
//     //     // Act
//     //     var words = await _dictionaryRepositoryMock.Object.GetAllWordsAsync();
//     //     bool hasWords = words.Any();
//     //
//     //     // Assert
//     //     Assert.True(hasWords);
//     // }
//
//     [Fact]
//     public async Task GetAllWordsAsync_ShouldReturnAllWords_WhenWordsExist()
//     {
//         // Arrange
//         var mockWords = new List<Word>
//         {
//             new Word { English = "Test1", Russian = "Тест1", MemoryLevel = 1 },
//             new Word { English = "Test2", Russian = "Тест2", MemoryLevel = 2 },
//             new Word { English = "Test3", Russian = "Тест3", MemoryLevel = 3 }
//         };
//
//         _dictionaryRepositoryMock
//             .Setup(repo => repo.GetAllWordsAsync())
//             .ReturnsAsync(mockWords);
//
//         // Act
//         var words = await _dictionaryRepositoryMock.Object.GetAllWordsAsync();
//
//         // Assert
//         Assert.NotNull(words);
//         Assert.Equal(mockWords.Count, words.Count);
//         Assert.All(words, word => Assert.Contains(word, mockWords));
//     }
// }