using System;

// ReSharper disable once InconsistentNaming
public static class Ex4_1
{
    public static void MainZ()
    {
        Console.Write("Enter the desired width: ");
        var height = int.Parse(Console.ReadLine());

        for(var i = 0; i < height; i++)
        {
            Console.WriteLine(new string('*', height - i).PadLeft(height));
            //Console.WriteLine($"{{0,{height}}}", new string('*', height - i));
        }

        /*
        var width = 0;
        var max = height;
        for(var row = 0; row < height; row++)
        {
            for(var column = 0; column < width; column++)
                Console.Write(" ");
            for(var asterisks = 0; asterisks < max; asterisks++)
                Console.Write("*");
            Console.WriteLine();
            width++;
            max--;
        }
        */
    }
}
