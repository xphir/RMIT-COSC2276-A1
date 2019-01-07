using System;

public static class SeriesDemo
{
    public static void MainZ()
    {
        ISeries series = new ByTwos();

        for(var i = 0; i < 5; i++)
            Console.WriteLine("Next value is " + series.GetNext());
        Console.WriteLine();

        Console.WriteLine("Resetting");
        series.Reset();
        for(var i = 0; i < 5; i++)
            Console.WriteLine("Next value is " + series.GetNext());
        Console.WriteLine();

        Console.WriteLine("Starting at 100");
        series.SetStart(100);
        for(var i = 0; i < 5; i++)
            Console.WriteLine("Next value is " + series.GetNext());
        Console.WriteLine();

        Console.WriteLine("New ByTows Starting at 200");
        series = new ByTwos(200);
        for(var i = 0; i < 5; i++)
            Console.WriteLine("Next value is " + series.GetNext());
    }
}
