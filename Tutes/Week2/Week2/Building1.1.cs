using System;

namespace Building1_1
{
    internal class Building
    {
        public int floors; // number of floors.
        public int Occupants; // number of occupants.
        public int Area; // total square footage of building.

        // Display the area per person.
        public void AreaPerPerson()
        {
            Console.WriteLine(" " + Area / Occupants + " area per person");
        }
    }

    public static class BuildingDemo
    {
        public static void Main()
        {
            var house = new Building();
            var office = new Building();

            // Assign values to fields in house.
            house.floors = 2;
            house.Occupants = 4;
            house.Area = 2500;

            // Assign values to fields in office.
            office.floors = 3;
            office.Occupants = 25;
            office.Area = 4200;

            Console.WriteLine("house has:\n " +
                house.floors + " floors\n " +
                house.Occupants + " occupants\n " +
                house.Area + " total area");
            house.AreaPerPerson();
            Console.WriteLine();

            Console.WriteLine("office has:\n " +
                office.floors + " floors\n " +
                office.Occupants + " occupants\n " +
                office.Area + " total area");
            office.AreaPerPerson();
        }
    }
}
