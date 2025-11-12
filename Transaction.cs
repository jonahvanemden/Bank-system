using System;

namespace P3; 

public abstract class Transaction
{
    protected decimal _amount;
    protected bool _success;
    public bool _executed;
    public bool _reversed;
    private DateTime _dateStamp;

    public Transaction(decimal amount)
    {
        this._amount = amount;
        _success = false;
        _executed = false;
        _reversed = false;
        _dateStamp = DateTime.Now;
    }

    public abstract bool Success { get; }  // Abstract Succes property, this is not instantiated in the Transaction class but will be instantiated through either one of the specific transaction classes.

    public bool Executed
    {
        get { return _executed; }
    }

    public bool Reversed
    {
        get { return _reversed; }
    }

    public DateTime DateStamp
    {
        get { return _dateStamp; }
    }

    public abstract void Print();  // Abstraxt Print, this will be instantiated through the specific child transfer class. 

    public virtual void Execute()
    {
        _dateStamp = DateTime.Now;  // Set time that the transfer was executed.
    }
    public virtual void Rollback()
    {
        _dateStamp = DateTime.Now; // Set time that rollback was executed.
    }
}
