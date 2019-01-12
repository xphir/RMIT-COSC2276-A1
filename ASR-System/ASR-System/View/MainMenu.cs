using System;
using System.Collections.Generic;
using System.Text;

namespace ASR_System.View
{
    class MainMenu
    {
        public static void MainMenuLoop()
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
            string menuInput = MiscellaneousUtilities.ReadUserInput("Enter option: ");

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
                        StaffMenu.StaffMenuLoop();
                        break;
                    case 4:
                        StudentMenu.StudentMenuLoop();
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
    }
}
