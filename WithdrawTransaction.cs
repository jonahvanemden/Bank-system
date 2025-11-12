using System;

namespace P3; 

class WithdrawTransaction : Transaction

{
    private Account _account;

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }

    public override void Print()
    {
        if (_success)
        {
            Console.WriteLine($"Withdraw of {this._amount} succeeded");
        }
        else
        {
            Console.WriteLine("Transaction failed");
        }
        
    }

    public override void Execute()
    {

        base.Execute();

        try 
        {

            if (_executed)  // Execute if the transaction has been done before.
            {
                throw new InvalidOperationException("Transaction has already been done.");
            }

            _executed = true;  // Set this transaction to be executed so it cannot be done again.

            _success = _account.Withdraw(_amount);  // Set success to output of Withdraw.
            if (!_success)  // If witdrawal was not successful.
            {
                throw new InvalidOperationException("Insufficient funds or invalid withdrawal amount.");
            }
        }
        catch (InvalidOperationException a)
        {
            _success = false; 
            Console.WriteLine($"{a.Message}");
        }

    }

    public override void Rollback()
    {
        base.Rollback();
    
        try
        {
            
            if (!_executed) // Check if the transaction is already executed. 
            {
                throw new InvalidOperationException("Transaction has not been executed");
            }
            if (_reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }


            bool rollback = _account.Deposit(_amount);  // The rollback cannot happen if deposit back into the account is not possible.
            if (!rollback)
            {
                throw new InvalidOperationException("Rollback failed due to deposit error.");
            }

            _reversed = true;
            Console.WriteLine("Rollback successful money deposited back into account");
        }
        catch (InvalidOperationException b)
        {
            Console.WriteLine($"Rollback failed: {b.Message}");
        }
    }
    
    public override bool Success
    {
        get { return this._success; }
    }

}