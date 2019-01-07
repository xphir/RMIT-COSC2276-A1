using System;

public struct Point
{
    public int XCoord;
    public int YCoord;

    public Point(int x, int y)
    {
        XCoord = x;
        YCoord = y;
    }

    public void Print() => Console.WriteLine("x={0}, y={1}", XCoord, YCoord);
}

public static class Program
{
    private static void Swap(Point p)
    {
        var x = p.XCoord;
        p.XCoord = p.YCoord;
        p.YCoord = x;
    }

    public static void MainZ()
    {
        int i = 10; // structs: faster because on stack - i.e., the data is present without additional lookup.
        string s = ""; // classes: slower because on heap - i.e., a pointer/reference.

        var p = new Point(1, 2);
        p.Print();
        Swap(p);
        p.Print();
        Console.WriteLine();

        // Alternatively.
        var p2 = new Point
        {
            XCoord = 1,
            YCoord = 2
        };
        //p2.XCoord = 1;
        //p2.YCoord = 2;
        p2.Print();
        Swap(p2);
        p2.Print();
        Console.WriteLine();

        // Alternatively - no need to use new as it is not a reference type.
        Point p3;
        p3.XCoord = 1;
        p3.YCoord = 2;
        p3.Print();
        Swap(p3);
        p3.Print();
    }
}
