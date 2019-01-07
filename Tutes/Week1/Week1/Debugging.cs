using System;

namespace Week1
{
    public class Debugging
    {
        private static void Main()
        {
            // Debugging example.
            var z = 10;

            Console.WriteLine("Hello World");
            z++;

            Console.WriteLine("Bye World");
            z++;

            Console.WriteLine("Value of z: " + z);

            return;
            Environment.Exit(0);

            // Explain debug points and keyword ref.
            //var i = 10;
            //MethodName(i);
            //Console.WriteLine("Main() Value of i: " + i);
        }

        private static void MethodName(int i)
        {
            for(var j = 0; j < 10; j++)
            {
                i--; // Show dynamic code inserted at runtime.
                //Console.WriteLine(i);
                //var z = j + i;
                //i *= z;
            }

            Console.WriteLine("MethodName() Value of i: " + i);
        }
    }
}
