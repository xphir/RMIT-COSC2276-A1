using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace ASR_System
{
    public static class MiscellaneousUtilities
    {
        public const int STAFF_BOOKING_LIMIT = 4;
        public const int STUDENT_BOOKING_LIMIT = 1;
        public const int ROOM_BOOKING_LIMIT = 2;
        public const int SLOT_BOOKING_LIMIT = 1;
        public static readonly TimeSpan START_TIME = new TimeSpan(9, 0, 0); // 9:00am
        public static readonly TimeSpan END_TIME = new TimeSpan(14, 0, 0); // 2:00pm

        //From Tute 4 InventoryPriceManagement example
        public static SqlConnection CreateConnection(this string connectionString) => new SqlConnection(connectionString);
        
        public static DataTable GetDataTable(this SqlCommand command)
        {
            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);

            return table;
        }

        public static string ReadUserInput(string request)
        {
            Console.Write("{0}", request);
            string userInput = Console.ReadLine();
            Console.WriteLine();
            return userInput;
        }
    }

    public class DataValidation
    {
        


        

       

        

        //Each ID (Staff and Student) is unique.
        public static bool IDUniqueCheck(string id)
        {
            return false;
        }
    }
}
