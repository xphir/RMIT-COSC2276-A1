using System;

namespace Ex1_1
{
    public abstract class Animal
    {
        protected int Weight { get; }
        protected string Name { get; }

        protected Animal(int weight, string name)
        {
            Weight = weight;
            Name = name;
        }

        public abstract void Speak();
        public abstract void Move();

        public abstract override string ToString();

        //public override string ToString()
        //{
        //    //throw new NotImplementedException();
        //    //throw new NotSupportedException();
        //    //throw new InvalidOperationException();
        //    //throw new Exception("The method or operation is not implemented.");
        //}
    }

    public class Dog : Animal
    {
        private string Breed { get; }

        public Dog(int weight, string name, string breed) : base(weight, name)
        {
            Breed = breed;
        }

        public override void Speak()
        {
            Console.WriteLine("Woof");
        }

        public override void Move()
        {
            Console.WriteLine("Run, run, run, drool.");
        }

        public override string ToString()
        {
            return $"My name is {Name} I weigh {Weight} and I am a {Breed}\n";
        }
    }

    public class Cat : Animal
    {
        public Cat(int weight, string name) : base(weight, name)
        { }

        public override void Speak()
        {
            Console.WriteLine("Meow");
        }

        public override void Move()
        {
            Console.WriteLine("Run, tumble, nap.");
        }

        public override string ToString()
        {
            return $"My name is {Name} I weigh {Weight} and I know how to purr!\n";
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void Purr()
        {
            Console.WriteLine("Purrrrrrrrrrrrrrr\n");
        }
    }

    public static class TestAnimal
    {
        private static void MainZ()
        {
            var myAnimals = new Animal[5];
            myAnimals[0] = new Dog(72, "Milo", "Golden");
            myAnimals[1] = new Cat(12, "Shakespeare");
            myAnimals[2] = new Cat(10, "Allegra");
            myAnimals[3] = new Dog(50, "Dingo", "mixed breed");
            myAnimals[4] = new Dog(20, "Brandy", "Beagle");

            foreach(var a in myAnimals)
            {
                a.Speak();
                a.Move();
                Console.WriteLine(a);

                // Using pattern matching to differentiate a Cat.
                if(a is Cat c1)
                    c1.Purr();

                // Only cats purr.
                // Using keyboard 'is' to differentiate a Cat.
                // ReSharper disable once MergeCastWithTypeCheck
                if(a is Cat)
                {
                    var c2 = (Cat) a;
                    c2.Purr();
                }

                // Using keyboard 'as' to differentiate a Cat.
                var c3 = a as Cat;
                c3?.Purr();
            }
        }
    }
}
