using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ASR_System
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public static string ConnectionString { get; } = Configuration["ConnectionString"];

        static void Main(string[] args)
        {
            Utilities.DataValidation.testDataValidation();
            WelcomeMessage();
            MainMenu();
        }

        //MAIN MENU

        static void WelcomeMessage()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
        }

        static void MainMenu()
        {
            while (true)
            {
                ShowMainMenu();
                ReadMainMenu();
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("{0}", "Main menu:");
            Console.WriteLine("{0}. {1}", 1, "List rooms");
            Console.WriteLine("{0}. {1}", 2, "List slots");
            Console.WriteLine("{0}. {1}", 3, "Staff menu");
            Console.WriteLine("{0}. {1}", 4, "Student menu");
            Console.WriteLine("{0}. {1}", 5, "Exit");
            Console.WriteLine();

        }

        static void ReadMainMenu()
        {
            string menuInput = ReadUserInput();

            if (Int32.TryParse(menuInput, out int menuNumber))
            {
                switch (menuNumber)
                {
                    case 1:
                        ShowListRooms();
                        break;
                    case 2:
                        ShowListSlots();
                        break;
                    case 3:
                        StaffMenu();
                        break;
                    case 4:
                        StudentMenu();
                        break;
                    case 5:
                        Console.WriteLine("Terminating ASR.");
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input: {0}", menuNumber);
                        break;
                }
            }
            else
            {
                Console.WriteLine("invalid input value: {0}", menuInput);
            }
        }

        static void ShowListRooms()
        {
            Console.WriteLine("---List rooms---"); 
        }

        static void ShowListSlots()
        {
            Console.WriteLine("---List slots---");
        }

        //STAFF MENU

        static void StaffMenu()
        {
            while(true)
            {
                ShowStaffMenu();
                ReadStaffMenu();
            }
        }

        static void ShowStaffMenu()
        {
            Console.WriteLine();
            Console.WriteLine("{0}", "------------------------------------------------------------");
            Console.WriteLine("{0}", "Staff menu:");
            Console.WriteLine("{0}. {1}", 1, "List staff");
            Console.WriteLine("{0}. {1}", 2, "Room availability");
            Console.WriteLine("{0}. {1}", 3, "Create slot");
            Console.WriteLine("{0}. {1}", 4, "Remove slot");
            Console.WriteLine("{0}. {1}", 5, "Exit");
            Console.WriteLine();

        }

        static void ReadStaffMenu()
        {
            string menuInput = ReadUserInput();

            if (Int32.TryParse(menuInput, out int menuNumber))
            {
                switch (menuNumber)
                {
                    case 1:
                        ListStaff();
                        break;
                    case 2:
                        RoomAvailability();
                        break;
                    case 3:
                        CreateSlot();
                        break;
                    case 4:
                        RemoveSlot();
                        break;
                    case 5:
                        Console.WriteLine("Exiting staff menu.");
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid input: {0}", menuNumber);
                        break;
                }
            }
            else
            {
                Console.WriteLine("invalid input value: {0}", menuInput);
            }
        }

        static void ListStaff()
        {
            Console.WriteLine("ListStaff");
        }

        static void RoomAvailability()
        {
            Console.WriteLine("RoomAvailability");
        }

        static void CreateSlot()
        {
            Console.WriteLine("CreateSlot");
        }

        static void RemoveSlot()
        {
            Console.WriteLine("RemoveSlot");
        }

        //STUDENT MENU

        static void StudentMenu()
        {
            while (true)
            {
                ShowStaffMenu();
                ReadStudentMenu();
            }
        }

        static void ShowStudentMenu()
        {
            Console.WriteLine();
            Console.WriteLine("{0}", "------------------------------------------------------------");
            Console.WriteLine("{0}", "Student menu:");
            Console.WriteLine("{0}. {1}", 1, "List students");
            Console.WriteLine("{0}. {1}", 2, "Staff availability");
            Console.WriteLine("{0}. {1}", 3, "Make booking");
            Console.WriteLine("{0}. {1}", 4, "Cancel booking");
            Console.WriteLine("{0}. {1}", 5, "Exit");
            Console.WriteLine();

        }

        static void ReadStudentMenu()
        {
            string menuInput = ReadUserInput();

            if (Int32.TryParse(menuInput, out int menuNumber))
            {
                switch (menuNumber)
                {
                    case 1:
                        ListStudents();
                        break;
                    case 2:
                        StaffAvailability();
                        break;
                    case 3:
                        MakeBooking();
                        break;
                    case 4:
                        CancelBooking();
                        break;
                    case 5:
                        Console.WriteLine("Exiting student menu.");
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid input: {0}", menuNumber);
                        break;
                }
            }
            else
            {
                Console.WriteLine("invalid input value: {0}", menuInput);
            }
        }

        static void ListStudents()
        {
            Console.WriteLine("ListStudents");
        }

        static void StaffAvailability()
        {
            Console.WriteLine("StaffAvailability");
        }

        static void MakeBooking()
        {
            Console.WriteLine("MakeBooking");
        }

        static void CancelBooking()
        {
            Console.WriteLine("CancelBooking");
        }


        //HELPER METHODS

        static string ReadUserInput()
        {
            Console.Write("{0}", "Enter option: ");
            string userInput = Console.ReadLine();
            Console.WriteLine();
            return userInput;
        }


    }
}

namespace Utilities
{
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

        //Each ID (Staff and Student) is unique.
        public static Boolean idUniqueCheck(string id)
        {
            return false;
        }
    }
}
