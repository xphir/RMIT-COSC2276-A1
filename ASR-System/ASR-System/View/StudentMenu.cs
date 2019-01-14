using ASR_System.Controller;
using ASR_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASR_System.View
{
    class StudentMenu
    {
        public static void StudentMenuLoop()
        {
            while (true)
            {
                ShowStudentMenu();
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
            string menuInput = MiscellaneousUtilities.ReadUserInput("Enter option: ");

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
                        MainMenu.MainMenuLoop();
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
            MainEngine.PrintUserList(new StudentManager().StudentList.Cast<User>().ToList(), "--- List students ---", "<no students>");
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

    }
}
