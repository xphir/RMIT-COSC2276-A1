using System;
using System.Collections.Generic;
using System.Linq;

public static class Linq
{
    private static void MainZ()
    {
        var array = new[] { 1, 2, 3, 4, 5, 7, 10, 20, 30, 1, 2, 3, 4 };

        var evenNumbers = new List<int>();
        foreach(var i in array)
        {
            if(i % 2 == 0)
            {
                evenNumbers.Add(i);
            }
        }

        //var evenNumbers = from i in array where i % 2 == 0 select i;
        //var evenNumbers = array.Where(i => i % 2 == 0);
        //var evenNumbers = array.Where(IsIntEven);

        foreach(var i in evenNumbers)
        {
            Console.WriteLine(i);
        }
    }

    private static bool IsIntEven(int i)
    {
        return i % 2 == 0;
    }
}
