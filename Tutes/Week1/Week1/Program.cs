using System;

namespace Week1
{
    public class Program
    {
        private static void MainZ()
        {
            // My first program in C#.
            Console.Write("Please enter your name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Welcome " + name);
            
            Console.WriteLine();
            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }
    }
}
