using System;

namespace P3; 

class Account
{
    private decimal _balance;
    private String _name;

    public Account(String _name, decimal _balance)
    {
        this._name = _name;
        this._balance = _balance;
    }

    public bool Deposit(decimal amount)  // Method to add money to the balance. 
    {

        if (amount > 0)  // Deposit only possible if amount is larger than 0.
        {
            this._balance += amount;
            Console.WriteLine("Deposit succeeded");
            return true;
        }
        else
        {
            Console.WriteLine("Deposit failed");
            return false;
        }
        
    }

    public bool Withdraw(decimal amount)  // Method to withdraw money from the balance.
    {

        if (amount > this._balance || amount < 0)  // Can only withdraw positive amount and no amount that is larger than balance on account. 
        {
            Console.WriteLine("Withdraw failed");
            return false;
        }
        else
        {
            Console.WriteLine("Withdraw succeeded");
            this._balance -= amount;
            return true;
            
        }
        
    }

    public void Print()  // Method that prints out the name and account balance.
    {   
        string balance = this._balance.ToString("C");
        Console.WriteLine($"{this._name} has an account balance of {balance}");
    }

    public String Name  // Property to get name of the account.
    {
        get { return this._name; }
    }

}
