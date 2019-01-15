﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace ASR_System
{
    public static class MiscellaneousUtilities
    {
        public const int STAFF_DAILY_BOOKING_LIMIT = 4;
        public const int STUDENT_DAILY_BOOKING_LIMIT = 1;
        public const int ROOM_DAILY_BOOKING_LIMIT = 2;
        public const int SLOT_BOOKING_LIMIT = 1;
        public const int ROOM_DOUBLEBOOKING_CHECK = 1;
        public const string PRINT_INDENT = "\t";
        public static readonly TimeSpan START_TIME = new TimeSpan(9, 0, 0); // 9:00am
        public static readonly TimeSpan END_TIME = new TimeSpan(13, 0, 0); // 1:00pm - Booking at 1:00pm will end 2:00pm

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
}
