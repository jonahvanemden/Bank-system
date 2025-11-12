namespace P3; 

class DepositTransaction : Transaction
{
    private Account _account;

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }
    public override void Print()
    {
        if (_success)
        {
            Console.WriteLine($"Deposit of {this._amount} succeeded");
        }
        else
        {
            Console.WriteLine("Transaction failed");
        }
        
    }

    public override void Execute()
    {
        base.Execute();  // Set date time to now.

        try 
        {

            if (_executed)  // Execute if the transaction has been done before.
            {
                throw new InvalidOperationException("Transaction has already been done.");
            }

            _executed = true;  // Set this transaction to be executed so it cannot be done again.

            _success = _account.Deposit(_amount);  // Set success to output of Withdraw.
            if (!_success)  // If witdrawal was not successful.
            {
                throw new InvalidOperationException("Invalid deposit amount, amount must be larger than 0.");
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
        base.Rollback(); // Set date time to now.

        try
        {
            
            if (!_executed)  // Check if the transaction is already executed. 
            {
                throw new InvalidOperationException("Transaction has not been executed");
            }
            if (_reversed)  // Rollback cannot be reversed if it already happened. 
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }


            bool rollback = _account.Withdraw(_amount);  // The rollback cannot happen when withdrawing from the account is not possible. 
            if (!rollback)
            {
                throw new InvalidOperationException("Rollback failed due to withdrawal error.");
            }

            _reversed = true;
            Console.WriteLine("Rollback successful money was withdrawn from the account again.");
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