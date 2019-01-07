using System;
using System.Data;
using System.Data.SqlClient;

public static class ReadTable
{
    private static void MainZ()
    {
        // Connection string needs to be changed.
        const string connectionString = "server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=demo;database=demo;pwd=abc123";

        ConnectedAccess(connectionString);

        Console.WriteLine(new string('-', 80));
        Console.WriteLine();

        DisconnectedAccess(connectionString);
    }

    private static void ConnectedAccess(string connectionString)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var command = new SqlCommand("select * from log", connection);
            using(var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n",
                        reader["ip"], reader["time"], reader["request"], reader["status"], reader["size"]);
                }
            }
        }
    }

    private static void DisconnectedAccess(string connectionString)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "select * from log";

            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);

            foreach(var x in table.Select())
            {
                Console.WriteLine($"{x["ip"]}\n{x["time"]}\n{x["request"]}\n{x["status"]}\n{x["size"]}\n");
            }
        }
    }
}
