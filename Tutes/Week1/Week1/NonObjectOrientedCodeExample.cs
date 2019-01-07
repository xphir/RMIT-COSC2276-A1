using System;

public static class NonObjectOrientedCodeExample
{
    private static void MainZ()
    {
        var person = new object[3];
        person[0] = "Bob"; // First name
        person[1] = "Smith"; // Last name
        person[2] = new DateTime(1990, 6, 20); // Date of birth 20/06/1990

        var account = new object[4];
        account[0] = 062620; // Bank Code: 06 | State code: 3 | Branch code: 262
        account[1] = 12341234; // Account number
        account[2] = 500.00; // Balance
        account[3] = person; // Account holder

        PrintBankDetails(account);
        WithdrawMoney(account, 250.00);
        WithdrawMoney(account, 750.00);
        PrintBankDetails(account);
        WithdrawMoneyVersion2(account, 750.00);
        PrintBankDetails(account);
    }

    public static void PrintBankDetails(object[] account)
    {
        Console.WriteLine("Bank Details");
        Console.WriteLine("BSB           : {0}", account[0]);
        Console.WriteLine("Account Number: {0}", account[1]);
        Console.WriteLine("Balance       : {0:C}", account[2]);

        // Need to cast because compiler doesn't know that a "person" is at position 3.
        var person = (object[]) account[3];

        Console.WriteLine("Account Owner : {0} {1}", person[0], person[1]);
        Console.WriteLine();
    }

    public static void WithdrawMoney(object[] account, double amount)
    {
        // Again need to cast because compiler doesn't know that a "balance" is at position 2.
        var balance = (double) account[2];

        // Balance shouldn't become negative.
        if(amount <= balance)
            account[2] = balance - amount;
    }

    public static void WithdrawMoneyVersion2(object[] account, double amount)
    {
        // Again need to cast because compiler doesn't know that a "balance" is at position 2.
        var balance = (double) account[2];

        // Who wrote this code? ... I thought balance was not meant to become negative?
        account[2] = balance - amount;
    }
}
