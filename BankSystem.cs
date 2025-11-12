namespace P3; 

public enum MenuOption
{
    Withdraw = 1,  
    Deposit = 2,
    Print = 3,
    Transfer = 4,
    AddAccount = 5, 
    PrintTransactionHistory = 6,   
    Quit = 7
}


class BankSystems
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();  // Create Bank object (list of accounts).


        MenuOption option;  // Initialize variable of enum MenuOption.
        do  // Enter loop.
        {
            option = ReadUserOption();  // Call ReadUserOption and initialize option variable.

            switch (option)  // Take option variable for switch statement and match to specific case. 
            {
                case MenuOption.Withdraw:
                    DoWithdraw(bank);
                    break;
                case MenuOption.Deposit:
                    DoDeposit(bank);
                    break;
                case MenuOption.Print:
                    DoPrint(bank);
                    break;
                case MenuOption.Transfer:
                    DoTransfer(bank);
                    break;
                case MenuOption.AddAccount:
                    DoAddAccount(bank);
                    break;
                case MenuOption.PrintTransactionHistory:  
                    DoPrintTransactionHistory(bank);
                    break;
                case MenuOption.Quit:
                    Console.WriteLine("Quiting program.");
                    break;
            }
        } while (option != MenuOption.Quit);  // Quit do-while loop once quit is selected.

    }

    static void DoDeposit(Bank bank)  // Method to deposit money.
    {

        Account account = FindAccount(bank); // Find the account in the bank system.

        if (account == null) return;  // If account does not exist stop the transaction.

        Console.Write("Enter the amount to deposit: ");

        decimal deposit_am = Convert.ToInt32(Console.ReadLine());
        DepositTransaction depos_transaction = new DepositTransaction(account, deposit_am);  // Instantiate the transaction. 

        bank.ExecuteTransaction(depos_transaction);

        depos_transaction.Print();
    }

    static void DoWithdraw(Bank bank)  // Method to withdraw money.
    {

        Account account = FindAccount(bank); 

        if (account == null) return;

        Console.Write("Enter the amount to withdraw: ");
        decimal deposit_wi = Convert.ToInt32(Console.ReadLine());
        WithdrawTransaction with_transaction = new WithdrawTransaction(account, deposit_wi);  // Instantiate the withdraw transaction with given input.

        bank.ExecuteTransaction(with_transaction);

        with_transaction.Print();
    }

    static void DoTransfer(Bank bank)  // Method to transfer money.
    {

        Account fromAccount = FindAccount(bank); // Find both accounts in the bank.
        Account toAccount = FindAccount(bank);

        if (fromAccount == null) return;  // If either one does not exist stop the transaction.

        if (toAccount == null) return;

        Console.Write("Enter the amount to transfer: ");
        decimal transfer_am = Convert.ToInt32(Console.ReadLine());
        TransferTransaction transfer_transaction = new TransferTransaction(fromAccount, toAccount, transfer_am);  // Instantiate the transfer. 

        bank.ExecuteTransaction(transfer_transaction);

        transfer_transaction.Print();
    }

    static void DoAddAccount(Bank bank)  // Method to add a new account.
    {
        try 
        {

            Console.Write("Enter the new account name: ");
            string accountName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountName))  // Check if user entered a name or not.
            {
                accountName = null;  // Assign null if the input is empty since an empty input is not the same as null value. 
            }

            Console.Write("Enter the starting balance: ");
            decimal accountBalance = Convert.ToDecimal(Console.ReadLine());

            Account newAccount = new Account(accountName, accountBalance);
            bank.AddAccount(newAccount);  // This block will execute successfuly if AddAccount method throws no exceptions.

            Console.WriteLine($"Account: {newAccount.Name} created succesfully."); 
        }
        catch (ArgumentNullException ex)
        {
            
            Console.WriteLine($"{ex.Message}");  // Handle the case where the account name is null.
        }
        catch (InvalidOperationException ex)
        {
            
            Console.WriteLine($"{ex.Message}");  // Handle the case where the account already exists. 
        }
    }

    static void DoPrint(Bank bank)  // Method to print account information.
    {
        Account account = FindAccount(bank); 

        if (account == null) return;
        account.Print();
    }

    static Account FindAccount(Bank bank)  // Method to find account in list of accounts.
    {
        Console.Write("Enter the account name you want to use: ");
        String accountName = Console.ReadLine();
        Account account = bank.GetAccount(accountName);  // Find the acount in the bank.

        if (account == null)
        {
            Console.WriteLine("Unable to find account");

        }
        else
        {
            Console.WriteLine($"Search result: {account.Name}");
        }
        return account;

    }

    private static void DoRollback(Bank bank, Transaction transaction)  // Method to roll back transaction. 
    {
        bank.RollBackTransaction(transaction);
    }

    private static void DoPrintTransactionHistory(Bank bank)
    {
        bank.PrintTransactionHistory();  // Print all transactions.

        Console.WriteLine("Do you want to rollback any transaction? Enter the transaction number or enter 0 to skip:");
        int input = Convert.ToInt32(Console.ReadLine());

        if (input != 0)  // If input is 0 the user does not want to reverse a transaction.
        {
            try
            {
                Transaction transaction = bank.GetTransaction(input - 1);  // Select the transaction the user wants to rollback.
                DoRollback(bank, transaction);  // Rollback the selected transaction.
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid transaction number.");  // If selected transaction does not exist throw exception.
            }
        }
    }

    public static MenuOption ReadUserOption()  // Read user option that returns a menu option. 
    {
        MenuOption choice;  
        do  // Start do-while loop. 
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Print");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Add new account");
            Console.WriteLine("6. Print transaction history");
            Console.WriteLine("7. Quit");

            

            try  // Execute try statement if valid integer is entered.
            {
                int option = Convert.ToInt32(Console.ReadLine());

                if (option >= 1 && option <= 7)  // If integer is one of the options.
                {
                    choice = (MenuOption)option;
                    Console.WriteLine($"{choice} was selected");
                    break;
                }
                else  // Else ask for new input.
                {
                    Console.WriteLine("Invalid option. Please choose a valid option from the menu.");
                }
            }
            catch  // Catch statement if a letter or character is entered. 
            {
                Console.WriteLine("Please enter a number between 1 and 7 not letters or characters.");
            }


        }while(true); // Do this loop until break statement is reached.

        return choice;
    }
}

