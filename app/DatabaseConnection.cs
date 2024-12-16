using LinqToDB;

namespace app;

// Подключение к БД
public class DatabaseConnection : LinqToDB.Data.DataConnection
{
    public DatabaseConnection(string connectionString) 
        : base(ProviderName.MySql, connectionString)
    {
    }

}