namespace P3; 

class Bank
{
    private List<Account> _accounts;
    private List<Transaction> _transactions;

    public Bank()
    {
        _accounts = new List<Account>();
        _transactions = new List<Transaction>();
    }

    public void AddAccount(Account account)
    {
        if (account.Name == null)  // If the account name entered is emtpy or null value throw exception.
        {
            throw new ArgumentNullException("Account name cannot be null.");
        }

        for (int i = 0; i < _accounts.Count; i++)  // Iterate over list of accounts.
        {
            if (_accounts[i].Name == account.Name)  // If there is already and account in the list with the same name throw exception.
            {
                throw new InvalidOperationException("This account already exists.");
            }
        }
        _accounts.Add(account);  // If acount not duplicate or null value add the new account to the list. 
    }


    public Account GetAccount(String name)
    {
        foreach (var account in _accounts)  // Iterate over elements in _accounts List.
        {
            if (account.Name == name)  // If matching name is found return this account.
            {
                return account;
            }
        }
        return null; 
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);  // Add transaction to the list of transactions.
        transaction.Execute();
    }

    public void RollBackTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);  // Add rollback to the list of transactions.
        transaction.Rollback();
    }

    public void PrintTransactionHistory()
    {
        for (int i = 0; i < _transactions.Count; i++)  // Print transaction, number, status, and date time.
        {
            Console.WriteLine($"{i + 1}. " + _transactions[i] + " Succesful: " + _transactions[i].Success + " " + "Executed at: " + _transactions[i].DateStamp);
        }
    }

    public Transaction GetTransaction(int index) // To get the transaction at this index. This is mainly used to rollback transactions. 
    {
        if (index < 0 || index >= _transactions.Count)  // Check if given index of transaction to rollback exists.
        {
            throw new ArgumentOutOfRangeException("Invalid transaction number."); 
        }
        return _transactions[index];  // Return the selected transaction.
    }
}