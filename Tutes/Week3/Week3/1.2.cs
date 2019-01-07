using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex1_2
{
    public abstract class Animal : IComparable<Animal>
    {
        public int Weight { get; }
        public string Name { get; }

        protected Animal(int weight, string name)
        {
            Weight = weight;
            Name = name;
        }

        public abstract void Speak();
        public abstract void Move();

        public int CompareTo(Animal a)
        {
            //return Weight - a.Weight;
            // OR
            return Weight.CompareTo(a.Weight);
        }
    }

    public class Dog : Animal
    {
        public string Breed { get; }

        public Dog(int weight, string name, string breed) : base(weight, name) => Breed = breed;

        public override void Speak() => Console.WriteLine("Woof");
        public override void Move() => Console.WriteLine("Run, run, run, drool.");
        public override string ToString() => $"My name is {Name} I weigh {Weight} and I am a {Breed}\n";
    }

    public class Cat : Animal
    {
        public Cat(int weight, string name) : base(weight, name)
        { }

        public override void Speak() => Console.WriteLine("Meow");
        public override void Move() => Console.WriteLine("Run, tumble, nap.");
        public override string ToString() => $"My name is {Name} I weigh {Weight} and I know how to purr!\n";

        // ReSharper disable once UnusedMember.Global
        public void Purr() => Console.WriteLine("Purrrrrrrrrrrrrrr\n");
    }

    public static class TestAnimal
    {
        private static void MainZ()
        {
            var animals = new List<Animal>
            {
                new Dog(72, "Milo", "Golden"),
                new Cat(12, "Shakespeare"),
                new Cat(10, "Allegra"),
                new Dog(50, "Dingo", "mixed breed"),
                new Dog(20, "Brandy", "Beagle")
            };

            foreach(var a in animals)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("--- After sorting by size ---");
            Console.WriteLine();

            animals.Sort();
            foreach(var a in animals)
            {
                Console.WriteLine(a);
            }

            // OR LINQ
            // Language Integrated Query
            //foreach(var a in animals.OrderBy(x => x.Weight)) // Show .OrderByDescending, .Reverse, .OfType
            //{
            //    Console.WriteLine(a);
            //}
        }
    }
}
