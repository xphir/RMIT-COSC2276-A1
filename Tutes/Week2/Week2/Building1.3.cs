using System;

namespace Building1_3
{
    public class Building
    {
        public Building() => Floors = Area = Occupants = 0;

        public Building(int floors, int occupants, int area)
        {
            Floors = floors;
            Occupants = occupants;
            Area = area;
        }

        public int Floors { get; set; }
        public int Occupants { get; set; }
        public int Area { get; set; }

        public int AreaPerPerson => Area / Occupants;

        // Display the area per person.
        public void DisplayAreaPerPerson() => Console.WriteLine(" " + AreaPerPerson + " area per person");
    }

    public static class BuildingDemo
    {
        public static void MainZ()
        {
            var house = new Building(2, 4, 2500);
            var office = new Building
            {
                Floors = 3,
                Occupants = 25,
                Area = 4200
            };
            //office.Floors = 3;
            //office.Occupants = 25;
            //office.Area = 4200;

            Console.WriteLine("house has:\n " +
            house.Floors + " floors\n " +
            house.Occupants + " occupants\n " +
            house.Area + " total area");
            house.DisplayAreaPerPerson();
            Console.WriteLine();

            Console.WriteLine("office has:\n " +
            office.Floors + " floors\n " +
            office.Occupants + " occupants\n " +
            office.Area + " total area");
            Console.WriteLine(" " + office.AreaPerPerson + " area per person");
        }
    }
}
