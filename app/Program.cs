using app.Factories;
using app.Interfaces;
using app.Services.Menus;
using app.Services.Repositories;
using app.Services.Testing;
using app.Services.WordManagement;
using LinqToDB;
using MySql.Data.MySqlClient;

namespace app;

public class Program
{
    public static async Task Main(string[] args)
    {
        var connectionString = "Server=127.0.0.1;Port=3306;Database=dictrionarydb;User=root;Password=Q8zb5rr.15;";
        using var db = new DatabaseConnection(connectionString);

        IMenu menu = new ConsoleMenu();
        IDictionaryRepository dictionaryRepository = new DictionaryRepository(db);
        ITestService testService = new TestService(dictionaryRepository,menu);
        IWordService wordService = new WordService(db, menu);
        bool exit = false;
        void ExitAction() => exit = true;
        CommandFactory commandFactory = new CommandFactory(testService,wordService,menu, ExitAction);
        Console.WriteLine("Добро пожаловать в приложение для изучения иностранных слов!");
        while (exit is false)
        {
            menu.Show();
            string choice = menu.GetUserChoice();
            var command = commandFactory.GetCommand(choice);
            await command.Execute();
        }

    }
}