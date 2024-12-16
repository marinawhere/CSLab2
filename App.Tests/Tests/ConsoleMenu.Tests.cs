using NUnit.Framework;
using System;
using System.IO;
using app.Services.Menus;
using app.Interfaces;

namespace app.Tests.Services.Menus
{
    [TestFixture]
    public class ConsoleMenuTests
    {
        private ConsoleMenu _consoleMenu;
        private StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _consoleMenu = new ConsoleMenu();
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);  // Перенаправляем Console.Out на StringWriter
        }

        [Test]
        public void Show_DisplaysMenuOptions()
        {
            // Act
            _consoleMenu.Show();

            // Assert
            string output = _stringWriter.ToString();
            StringAssert.Contains("Меню:", output);
            StringAssert.Contains("1. Начать тестирование", output);
            StringAssert.Contains("2. Добавить новое слово в словарь", output);
            StringAssert.Contains("3. Удалить слово из словаря", output);
            StringAssert.Contains("4. Выйти из программы", output);
        }

        [Test]
        public void GetUserChoice_ReturnsCorrectUserInput()
        {
            // Arrange
            using var stringReader = new StringReader("2\n");  // Симулируем ввод "2"
            Console.SetIn(stringReader);

            // Act
            string choice = _consoleMenu.GetUserChoice();

            // Assert
            Assert.That(choice, Is.EqualTo("2"));
        }

        [Test]
        public void GetUserChoice_ReturnsEmptyStringWhenInputIsNull()
        {
            // Arrange
            using var stringReader = new StringReader("");  // Симулируем пустой ввод
            Console.SetIn(stringReader);

            // Act
            string choice = _consoleMenu.GetUserChoice();

            // Assert
            Assert.That(choice, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DisplayMessage_PrintsMessageToConsole()
        {
            // Arrange
            string testMessage = "Тестовое сообщение";

            // Act
            _consoleMenu.DisplayMessage(testMessage);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Is.EqualTo(testMessage + Environment.NewLine));
        }

        [Test]
        public void Prompt_DisplaysPromptMessageAndReturnsUserInput()
        {
            // Arrange
            string promptMessage = "Введите значение: ";
            string userInput = "Ответ";
            using var stringReader = new StringReader(userInput + "\n");
            Console.SetIn(stringReader);

            // Act
            string result = _consoleMenu.Prompt(promptMessage);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Is.EqualTo(promptMessage));  // Проверяем, что сообщение-подсказка отображено
            Assert.That(result, Is.EqualTo(userInput));      // Проверяем, что возвращён правильный ввод пользователя
        }

        [Test]
        public void Prompt_ReturnsEmptyStringWhenInputIsNull()
        {
            // Arrange
            string promptMessage = "Введите значение: ";
            using var stringReader = new StringReader("");
            Console.SetIn(stringReader);

            // Act
            string result = _consoleMenu.Prompt(promptMessage);

            // Assert
            string output = _stringWriter.ToString();
            Assert.That(output, Is.EqualTo(promptMessage)); // Проверяем, что сообщение-подсказка отображено
            Assert.That(result, Is.EqualTo(string.Empty));   // Проверяем, что возвращено пустое значение
        }
    }
}