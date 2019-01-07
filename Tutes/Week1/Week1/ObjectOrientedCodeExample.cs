using System;

public class Account
{
    public Account(int bsb, int accountNumber, double balance, Person accountHolder)
    {
        Bsb = bsb;
        AccountNumber = accountNumber;
        Balance = balance;
        AccountHolder = accountHolder;
    }

    public int Bsb { get; }
    public int AccountNumber { get; }
    public double Balance { get; private set; }
    public Person AccountHolder { get; }

    public void WithdrawMoney(double amount)
    {
        // Balance shouldn't become negative.
        if(amount <= Balance)
            Balance -= amount;
    }
}

public class Person
{
    public Person(string firstName, string lastName, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string FirstName { get; }
    public string LastName  { get; }
    public DateTime DateOfBirth { get; }
}

public static class ObjectOrientedCodeExample
{
    private static void MainZ()
    {
        var person = new Person("Bob", "Smith", new DateTime(1990, 6, 20));
        var account = new Account(062620, 12341234, 500.00, person);

        PrintBankDetails(account);
        account.WithdrawMoney(250.00);
        account.WithdrawMoney(750.00);
        PrintBankDetails(account);
        account.WithdrawMoney(750.00);
        PrintBankDetails(account);
    }

    private static void PrintBankDetails(Account account)
    {
        Console.WriteLine("Bank Details");
        Console.WriteLine("BSB           : {0}", account.Bsb);
        Console.WriteLine("Account Number: {0}", account.AccountNumber);
        Console.WriteLine("Balance       : {0:C}", account.Balance);

        // Don't need to cast now - compiler is more informed of what is happening.
        var person = account.AccountHolder;

        Console.WriteLine("Account Owner : {0} {1}", person.FirstName, person.LastName);
        Console.WriteLine();
    }
}
