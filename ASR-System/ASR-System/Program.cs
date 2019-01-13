using System;
using Microsoft.Extensions.Configuration;

namespace ASR_System
{
    
    public static class Program
    {
        //Getting connection details from JSON file
        private static IConfigurationRoot Configuration { get; } = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public static string ConnectionString { get; } = Configuration["ConnectionString"];


        static void Main(string[] args)
        {
            Console.WriteLine(ConnectionString);
            Console.ReadLine();
            WelcomeMessage();
            View.MainMenu.MainMenuLoop();
            //Console.ReadLine();
            //testSQL2();
            //Console.ReadLine();
            //Utilities.DataValidation.testDataValidation();
            //WelcomeMessage();
            //MainMenu();
        }

        //MAIN MENU

        static void WelcomeMessage()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
        }

    }
}