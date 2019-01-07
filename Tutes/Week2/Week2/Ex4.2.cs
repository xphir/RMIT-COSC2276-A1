using System;

// ReSharper disable once InconsistentNaming
public static class Ex4_2
{
    public static void MainZ()
    {
        string user, pass;
        var count = 0;
        do
        {
            Console.Write("Enter username: ");
            user = Console.ReadLine();
            
            Console.Write("Enter password: ");
            pass = Console.ReadLine();
            
            count++;
        }
        while((user != "user" || pass != "pass") && count != 5);
        
        Console.WriteLine(count == 5 ? "Login attempt fail!" : "Password correct!");

        //if(count == 5)
        //    Console.WriteLine("Login attempt fail!");
        //else
        //    Console.WriteLine("Password correct!");
    }
}
