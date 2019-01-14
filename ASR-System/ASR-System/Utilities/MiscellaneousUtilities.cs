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
        //REGEX IS ^A|B|C|D$
        public static bool RoomIdValidation(string roomId)
        {
            string regexString = @"^A|B|C|D$";
            return Regex.IsMatch(roomId, regexString);
        }


        //The Staff ID always starts with a letter ‘e’ followed by 5 numbers.
        //REGEX IS ^e+\d{5}$
        public static bool StaffIdValidation(string staffId)
        {
            string regexString = @"^e+\d{5}$";
            return Regex.IsMatch(staffId, regexString);
        }

        //The Student ID always starts with a letter ‘s’ followed by 7 numbers.
        //REGEX IS ^s+\d{7}$
        public static bool StudentIdValidation(string studentId)
        {
            string regexString = @"^s+\d{7}$";
            return Regex.IsMatch(studentId, regexString);
        }

        //Email for a staff always ends with rmit.edu.au
        //REGEX IS ^(e+\d{5})+@+rmit.edu.au$
        public static bool StaffEmailValidation(string staffEmail)
        {
            string regexString = @"^(e+\d{5})+@+rmit.edu.au$";
            return Regex.IsMatch(staffEmail, regexString);
        }


        //Email for a student always ends with student.rmit.edu.au
        //REGEX IS ^(s+\d{7})+@+student.rmit.edu.au$
        public static bool StudentEmailValidation(string studentEmail)
        {
            string regexString = @"^(s+\d{7})+@+student.rmit.edu.au$";
            return Regex.IsMatch(studentEmail, regexString);
        }

        // HH:MM REGEX ^([0-1][0-9]|2[0-3]):[0-5][0-9]$
        // HH:00 REGEX ^([0-1][0-9]|2[0-3]):00$
        public static bool timeRegexCheck(string inputTime)
        {
            string timeRegexString = @"^([0-1][0-9]|2[0-3]):00$";
            return Regex.IsMatch(inputTime, timeRegexString);
        }

        //dd-mm-yyyy REGEX ^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$
        public static bool dateRegexCheck(string inputDate)
        {
            string dateRegexString = @"^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$";
            return Regex.IsMatch(inputDate, dateRegexString);
        }

        //Each ID (Staff and Student) is unique.
        public static bool IDUniqueCheck(string id)
        {
            return false;
        }
    }
}
