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
        //Loop to keep the within the menu system if an option fails
        public static void StudentMenuLoop()
        {
            while (true)
            {
                ShowStudentMenu();
                ReadStudentMenu();
            }
        }

        //Self explanatory print menu
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

        //Switch options to read the users choice of sub program
        static void ReadStudentMenu()
        {
            string menuInput = MiscellaneousUtilities.ReadUserInput("Enter option: ");

            if (int.TryParse(menuInput, out int menuNumber))
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

        //DONE

        //Show the students within the system
        static void ListStudents()
        {
            MainEngine.PrintUserList(new StudentManager().StudentList.Cast<User>().ToList(), "--- List students ---", "<no students>");
        }

        //DONE

        //Show staff availible (open slot they own) on a given day
        static void StaffAvailability()
        {
            MainEngine.CheckStaffAvailability();
        }

        //A student can only make 1 booking per day. - DONE
        //A slot can have a maximum of 1 student booked into it. - DONE

        //Create a booking (assign the studentID to a slots booking field) for a specified slot
        static void MakeBooking()
        {
            Console.WriteLine("---Make booking---");
            MainEngine.BookSlot();
        }

        //DONE

        //Delete/Cancel a booking (assign the slot booking field to null) for a specified slot
        static void CancelBooking()
        {
            Console.WriteLine("---Cancel booking---");
            MainEngine.CancelBookSlot();
        }

    }
}
