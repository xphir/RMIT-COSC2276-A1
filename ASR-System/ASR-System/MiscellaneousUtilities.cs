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
        //FromTute 4 InventoryPriceManagement example

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

    class Testing
    {


        public static Boolean dateFormatCheck(string inputDate)
        {
            //Enter date for slot (dd-mm-yyyy): 30-01-2019

            string stringDateOnly = "30-01-2019";
            string stringTimeOnly = "25:00";



            DateTime dateOnly;
            TimeSpan timeOnly;

            dateOnly = DateTime.Parse(stringDateOnly);
            timeOnly = TimeSpan.Parse(stringTimeOnly);


            DateTime combinedDate = dateOnly.Add(timeOnly);

            DateTime dayDate;

            Console.WriteLine("dateOnly: " + dateOnly);
            Console.WriteLine("timeOnly: " + timeOnly);
            Console.WriteLine("combinedDate: " + combinedDate);

            if (DateTime.TryParse(inputDate, out dayDate))
            {
                String.Format("{0:dd-MM-yyyy}", dayDate);
            }
            else
            {
                Console.WriteLine("Invalid"); // <-- Control flow goes here
            }
            Console.WriteLine(dayDate);
            return false;
        }

        public static DateTime dateCheck(string inputDate)
        {


            DateTime dateOnly;

            if (DateTime.TryParse(inputDate, out dateOnly))
            {
                return dateOnly;
            }
            else
            {
                Console.WriteLine("Invalid Date Entered: Please enter a valid date format (dd-mm-yyyy)");
            }

            return dateOnly;
        }

        public static void readCreateSlotInput()
        {
            Console.WriteLine("Enter room name: ");
            string inputRoom = Console.ReadLine();

            Console.WriteLine("Enter date for slot(dd - mm - yyyy): ");
            string inputDate = Console.ReadLine();

            Console.WriteLine("Enter time for slot (hh:mm): ");
            string inputTime = Console.ReadLine();

            Console.WriteLine("Enter staff ID: ");
            string inputStaffID = Console.ReadLine();

        }

        public static Boolean hourFormatCheck(string inputTime, string inputDate)
        {
            DateTime dateOnly;
            TimeSpan timeOnly;
            DateTime CombinedDate;

            //Set Hours of operation
            TimeSpan startTime = new TimeSpan(9, 0, 0); // 9:00am
            TimeSpan endTime = new TimeSpan(14, 0, 0); // 2:00pm

            if (!DataValidation.dateRegexCheck(inputDate))
            {
                Console.WriteLine("Invalid Date Entered: Please enter a valid date format (dd-mm-yyyy)");
                return false;
            }

            if (!DataValidation.timeRegexCheck(inputTime))
            {
                Console.WriteLine("Invalid Time Entered: Please enter a valid time format (hh:00)");
                return false;
            }


            if (TimeSpan.TryParse(inputTime, out timeOnly))
            {
                //TIME IS VALID
                if ((timeOnly.Minutes == 0) && (timeOnly.Seconds == 0) && (timeOnly.Milliseconds == 0))
                {
                    //TIME IS ON THE HOUR
                    if ((timeOnly >= startTime) && (timeOnly <= endTime))
                    {
                        //TIME IS WITHIN RANGE
                    }
                    else
                    {
                        Console.WriteLine("Invalid Time Entered: Please enter a time between 09:00 and 14:00");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Time Entered: Please enter a time that starts on the hour");
                }
            }
            else
            {
                Console.WriteLine("Invalid Time Entered: Please enter a valid time format (hh:mm)");
            }

            return false;
        }
    }

    public class DataValidation
    {
        //DATA VALIDATION
        //e12345@rmit.edu.au
        //s1234567@student.rmit.edu.au
        public static void testDataValidation()
        {
            //STAFF ID
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("STARTING DATA VALIDATION CHECKS");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("staffIdValidation TRUE 01: e12356 " + staffIdValidation("e12356"));
            Console.WriteLine("staffIdValidation TRUE 02: e98765 " + staffIdValidation("e98765"));
            Console.WriteLine("staffIdValidation FALSE 01: e1234 " + staffIdValidation("e1234"));
            Console.WriteLine("staffIdValidation FALSE 02: s1234567 " + staffIdValidation("s1234567"));

            //STUDENT ID
            Console.WriteLine("studentIdValidation TRUE 01: s1234567 " + studentIdValidation("s1234567"));
            Console.WriteLine("studentIdValidation TRUE 02: s3530160 " + studentIdValidation("s3530160"));
            Console.WriteLine("studentIdValidation FALSE 01: s123456 " + studentIdValidation("s123456"));
            Console.WriteLine("studentIdValidation FALSE 02: e12356 " + studentIdValidation("e12356"));

            //STAFF EMAIL
            Console.WriteLine("staffEmailValidation TRUE 01: e12345@rmit.edu.au " + staffEmailValidation("e12345@rmit.edu.au"));
            Console.WriteLine("staffEmailValidation TRUE 02: e98765@rmit.edu.au " + staffEmailValidation("e98765@rmit.edu.au"));
            Console.WriteLine("staffEmailValidation FALSE 01: e12345@student.rmit.edu.au " + staffEmailValidation("e12345@student.rmit.edu.au"));
            Console.WriteLine("staffEmailValidation FALSE 02: s1234567@student.rmit.edu.au " + staffEmailValidation("s1234567@student.rmit.edu.au"));
            Console.WriteLine("staffEmailValidation FALSE 03: e12345@gmail.com " + staffEmailValidation("e12345@gmail.com"));

            //STUDENT EMAIL
            Console.WriteLine("studentEmailValidation TRUE 01: s1234567@student.rmit.edu.au " + studentEmailValidation("s1234567@student.rmit.edu.au"));
            Console.WriteLine("studentEmailValidation TRUE 02: s3530160@student.rmit.edu.au " + studentEmailValidation("s3530160@student.rmit.edu.au"));
            Console.WriteLine("studentEmailValidation FALSE 01: e12345@rmit.edu.au " + studentEmailValidation("e12345@rmit.edu.au"));
            Console.WriteLine("studentEmailValidation FALSE 02: s1234567@rmit.edu.au " + studentEmailValidation("s1234567@rmit.edu.au"));
            Console.WriteLine("studentEmailValidation FALSE 03: e12345@gmail.com " + studentEmailValidation("e12345@gmail.com"));

            Console.WriteLine("END OF DATA VALIDATION CHECKS");
            Console.WriteLine("------------------------------------------------------------");
            Console.ReadLine();
        }

        //The Staff ID always starts with a letter ‘e’ followed by 5 numbers.

        //REGEX IS ^e+\d{5}$
        public static Boolean staffIdValidation(string staffId)
        {
            string regexString = @"^e+\d{5}$";
            return Regex.IsMatch(staffId, regexString);
        }

        //The Student ID always starts with a letter ‘s’ followed by 7 numbers.
        //REGEX IS ^s+\d{7}$
        public static Boolean studentIdValidation(string studentId)
        {
            string regexString = @"^s+\d{7}$";
            return Regex.IsMatch(studentId, regexString);
        }

        //Email for a staff always ends with rmit.edu.au
        //REGEX IS ^(e+\d{5})+@+rmit.edu.au$
        public static Boolean staffEmailValidation(string staffEmail)
        {
            string regexString = @"^(e+\d{5})+@+rmit.edu.au$";
            return Regex.IsMatch(staffEmail, regexString);
        }


        //Email for a student always ends with student.rmit.edu.au
        //REGEX IS ^(s+\d{7})+@+student.rmit.edu.au$
        public static Boolean studentEmailValidation(string studentEmail)
        {
            string regexString = @"^(s+\d{7})+@+student.rmit.edu.au$";
            return Regex.IsMatch(studentEmail, regexString);
        }

        // HH:MM REGEX ^([0-1][0-9]|2[0-3]):[0-5][0-9]$
        // HH:00 REGEX ^([0-1][0-9]|2[0-3]):00$
        public static Boolean timeRegexCheck(string inputTime)
        {
            string timeRegexString = @"^([0-1][0-9]|2[0-3]):00$";
            return Regex.IsMatch(inputTime, timeRegexString);
        }

        //dd-mm-yyyy REGEX ^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$
        public static Boolean dateRegexCheck(string inputDate)
        {
            string dateRegexString = @"^((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})$";
            return Regex.IsMatch(inputDate, dateRegexString);
        }

        //Each ID (Staff and Student) is unique.
        public static Boolean idUniqueCheck(string id)
        {
            return false;
        }

    }

    class SQLTests
    {
        static void testSQLParam()
        {
            // conn and reader declared outside try
            // block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;

            string azureSQLServerConnection = "server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=s3530160;database=s3530160;pwd=abc123;";

            string inputIP = "131.170.27.121";
            try
            {
                // instantiate and open connection
                conn = new SqlConnection(azureSQLServerConnection);
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select * from log where ip = @IP", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@RoomID";
                param.SqlDbType = SqlDbType.VarChar;
                param.Size = 10;
                param.Direction = ParameterDirection.Input;
                //param.Value = RoomID;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}",
                        reader["status"],
                        reader["size"]);
                }
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        static void testSQL2()
        {
            string connectionString = "server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=s3530160;database=s3530160;pwd=abc123;";
            string commandString = "select * from dbo.[User] WHERE UserID LIKE 's%'";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(commandString, connection);

            SqlDataAdapter daStaffUsers = new SqlDataAdapter(command);

            SqlCommandBuilder cmdBldr = new SqlCommandBuilder(daStaffUsers);

            DataSet dsStaffUsers = new DataSet();

            try
            {
                daStaffUsers.Fill(dsStaffUsers, "User");
            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
            }

            DataTable table = dsStaffUsers.Tables[0];

            Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", "ID", "Name", "Email"));
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", row["UserID"], row["Name"], row["Email"]));
            }

            //daStaffUsers.Update(dsStaffUsers, "User");
        }




        static void testSQL()
        {
            string azureSQLServerConnection = "server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=s3530160;database=s3530160;pwd=abc123;";
            //Connection string needs to be changed
            string listStudentsCommand = "select * from dbo.[User] WHERE UserID LIKE 's%'";
            SqlDataAdapter da = new SqlDataAdapter(listStudentsCommand, azureSQLServerConnection);
            // the CommandBuilder automatically creates SQL statements as required
            SqlCommandBuilder bld = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();

            SqlConnection connection = new SqlConnection(azureSQLServerConnection);

            SqlCommand command = new SqlCommand(listStudentsCommand, connection);

            SqlDataAdapter daStaffUsers = new SqlDataAdapter(command);

            SqlCommandBuilder cmdBldr = new SqlCommandBuilder(daStaffUsers);

            DataSet dsStaffUsers = new DataSet();

            daStaffUsers.Fill(dsStaffUsers, "User");

            daStaffUsers.Update(dsStaffUsers, "User");
            try
            {
                // logEntries is the name of the DataTable object created inside the DataSet
                da.Fill(ds, "User");

            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
            }

            DataTable table = ds.Tables[0];
            Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", "ID", "Name", "Email"));
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(String.Format("\t{0,-20}{1,-20}{2,-20}", row["UserID"], row["Name"], row["Email"]));
            }
        }
    }
}
