using System;

namespace P3; 

class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private WithdrawTransaction _withdraw;
    private DepositTransaction _deposit;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        this._fromAccount = fromAccount;
        this._toAccount = toAccount;
        _withdraw = new WithdrawTransaction(_fromAccount, _amount);
        _deposit = new DepositTransaction(_toAccount, _amount);

    }

    public override void Print()
    {
        if (Success)
        {
            Console.WriteLine($"Transferred ${_amount} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account");
            _withdraw.Print();
            _deposit.Print();
        }
        else
        {
            Console.WriteLine("Transfer did not succeed.");
        }
        
    }

    public override void Execute()
    {
        base.Execute();

        try
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction has already been executed.");
            }

            
            _withdraw.Execute();  // First execute the withdraw transaction.
            if (!_withdraw.Success)
            {
                throw new InvalidOperationException("Insufficient funds to transfer.");
            }

            _deposit.Execute();  // If withdraw was successful execute the deposit transaction.
            if (!_deposit.Success)
            { 
                throw new InvalidOperationException("Failed to deposit the amount to the target account.");
            }

            _executed = true;
        }
        catch (InvalidOperationException ex)
        {
        Console.WriteLine($"Transaction failed: {ex.Message}");  // Provide meaningful feedback.
        }
    }

    public override void Rollback()
    {
        base.Rollback();

        try
        {
            if (!_executed)
            {
                throw new InvalidOperationException("Transaction has not been executed.");
            }

            if (_reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }

            
            _deposit.Rollback();  // Reverse the deposit.

        
            _withdraw.Rollback();  // Reverse the withdrawal.

            _reversed = true;
            Console.WriteLine("Transfer rollback successful.");
        }
        catch(InvalidOperationException c)
        {
            Console.WriteLine($"Rollback failed: {c.Message}");
        }
    }

    public override bool Success
    {
        get { return _withdraw.Success && _deposit.Success; }
    }

}