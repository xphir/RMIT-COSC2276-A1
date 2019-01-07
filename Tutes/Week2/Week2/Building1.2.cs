using System;

namespace Building1_2
{
    public class Building
    {
        private int floors;
        private int _occupants;
        private int _area;

        public Building()
        { }

        public Building(int floors, int area, int occupants)
        {
            Floors = floors;
            Area = area;
            Occupants = occupants;
        }

        public int getFloors()
        {
            return floors;
        }

        public void setFloors(int f)
        {
            if(f <= 0)
                throw new ArgumentException();
            floors = f;
        }

        public int Floors
        {
            get
            {
                return floors;
            }
            set
            {
                if(value <= 0)
                    throw new ArgumentException();
                floors = value;
            }
        }

        public int Occupants
        {
            get => _occupants;
            set => _occupants = value;
        }

        public int Area
        {
            get => _area;
            set => _area = value;
        }

        // Display the area per person.
        public void AreaPerPerson()
        {
            Console.WriteLine(" " + _area / _occupants + " area per person");
        }
    }

    public static class BuildingDemo
    {
        public static void MainZ()
        {
            var house = new Building();
            var office = new Building();

            // assign values to fields in house
            house.setFloors(2);
            house.Occupants = 4;
            house.Area = 2500;

            // assign values to fields in office
            office.Floors = 3;
            office.Occupants = 25;
            office.Area = 4200;
            
            Console.WriteLine("house has:\n " +
            house.getFloors() + " floors\n " +
            house.Occupants + " occupants\n " +
            house.Area + " total area");
            house.AreaPerPerson();
            Console.WriteLine();

            Console.WriteLine("office has:\n " +
            office.Floors + " floors\n " +
            office.Occupants + " occupants\n " +
            office.Area + " total area");
            office.AreaPerPerson();
        }
    }
}
