using System;
using System.Data.SqlClient;

public static class UpdateTable
{
    private static void MainZ()
    {
        // Connection string needs to be changed.
        const string connectionString = "server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=demo;database=demo;pwd=abc123";

        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Updates the "Description" field in the "Status" table.
            var command = connection.CreateCommand();
            command.CommandText = "update Status set Description = 'Page Found' where Status = 200";

            var updates = command.ExecuteNonQuery();

            Console.WriteLine($"{updates} rows updated.\n");
        }
    }
}
