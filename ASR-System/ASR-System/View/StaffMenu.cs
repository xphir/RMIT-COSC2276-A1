using System;
using System.Collections.Generic;
using System.Text;
using ASR_System.Controller;
using ASR_System.Model;


namespace ASR_System.View
{
    class StaffMenu
    {
        public static void StaffMenuLoop()
        {
            while (true)
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
            string menuInput = MiscellaneousUtilities.ReadUserInput("Enter option: ");

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
    }
}
