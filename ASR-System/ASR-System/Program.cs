using System;
namespace ASR_System
{
    class Program
    {
        static void Main(string[] args)
        {
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
