using System;
namespace ASR_System
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                ShowMainMenu();
            }
            
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("{0}", "Main menu:");
            Console.WriteLine("{0}. {1}", 1, "List rooms");
            Console.WriteLine("{0}. {1}", 2, "List slots");
            Console.WriteLine("{0}. {1}", 3, "Staff menu");
            Console.WriteLine("{0}. {1}", 4, "Student menu");
            Console.WriteLine("{0}. {1}", 5, "Exit");
            Console.WriteLine();

            Console.Write("{0}", "Enter option: ");
            Console.ReadLine();
            Console.WriteLine();
        }
    }
}
