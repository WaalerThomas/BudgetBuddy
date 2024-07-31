using BudgetBuddy.Controllers;
using BudgetBuddy.Models;
using BudgetBuddy.Sqlite;

namespace BudgetBuddy.Simple.Menus;

public class AccountsMenu : IBaseMenu
{
    readonly string[] menuItems;

    public AccountsMenu()
    {
        menuItems = [
            "List budgeting accounts",
            "Add budgeting account",
            "Change name of a budgeting account",
            "Adjust account balance",                   // TODO: What will happen when the balance is negative?
            //"Remove a budgeting account"              // TODO: Think about how we are going to handle this if account has transactions already. Something historics.
        ];
    }

    public void ShowMenu(int menuStartX, int menuStartY)
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: menuItems, headerMessage: "Accounts Menu", cancelString: "back");
            if (optionSelected == -1)
                return;
            
            Console.WriteLine();
            switch (optionSelected)
            {
                case 1:
                    MenuActionListAccounts();
                    break;
                case 2:
                    MenuActionAddAccount();
                    break;
                case 3:
                    MenuActionRenameAccount();
                    break;
                case 4:
                    MenuActionAdjustAccountBalance();
                    break;
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Utils.PauseConsole();
                    break;
            }
        } while (true);
    }

    private void MenuActionListAccounts()
    {
        var uow = new UnitOfWork(new DatabaseContext());
        List<Account> accountsCopy = uow.Accounts.GetAll().ToList();
        uow.Dispose();
        
        Console.WriteLine("Budgeting Accounts:");
        if (accountsCopy.Count == 0)
            Console.WriteLine("No accounts have been registered");
        else
        {
            Console.WriteLine("Id".PadRight(10) + "Account Name".PadRight(20) + "Actual Balance".PadRight(20) + "Pending Balance".PadRight(20) + "Settled Balance".PadRight(20));
            foreach (var account in accountsCopy)
                Console.WriteLine($"{account.Id,-10}{account.Name,-20}{account.ActualBalance,-20:C}{account.PendingBalance,-20:C}{account.SettledBalance,-20:C}");
        }

        Utils.PauseConsole();
    }

    private void MenuActionAddAccount()
    {
        Console.WriteLine("Creating a new account");

        bool addMore;
        do
        {
            string? accountName = Utils.GetStringInput("Enter account name", minLength: Account.MIN_NAME_LENGTH, maxLength: Account.MAX_NAME_LENGTH);
            if (accountName == null)
                return;

            using (var uow = new UnitOfWork(new DatabaseContext()))
            {
                Account account = new() { Name = accountName };
                uow.Accounts.Add(account);
                uow.Complete();
            }
            Console.WriteLine($"Account '{accountName}' added");

            Console.WriteLine();
            addMore = Utils.GetYesNoInput("Add another account?", isYesDefault: true);
        } while (addMore == true);
    }

    private void MenuActionRenameAccount()
    {
        using (var uow = new UnitOfWork(new DatabaseContext()))
        {
            List<Account> accounts = uow.Accounts.GetAll().ToList();
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts created. Please do that first");
                Utils.PauseConsole();
                return;
            }

            string[] accountNames = accounts.Where(a => a != null).Select(a => a.Name).ToArray();
            int menuSelection = Utils.MenuSelector(
                menuItems: accountNames,
                selectionMessage: "Choose an account to rename",
                headerMessage: "Available Accounts",
                cancelString: "cancel"
            );
            if (menuSelection == -1)
                return;
            
            Console.WriteLine($"Selected account '{accounts[menuSelection - 1].Name}'");
            Console.WriteLine();

            Account account = accounts[menuSelection - 1];
            string oldName = account.Name;
            
            string? newName = Utils.GetStringInput("Enter new account name", minLength: Account.MIN_NAME_LENGTH, maxLength: Account.MAX_NAME_LENGTH);
            if (newName == null)
                return;
            
            account.Name = newName;

            Console.WriteLine($"Renamed account '{oldName}' to '{newName}'");
            uow.Complete();
        }
        
        Utils.PauseConsole();
    }

    private static void MenuActionAdjustAccountBalance()
    {
        using var uow = new UnitOfWork(new DatabaseContext());
        List<Account> accounts = uow.Accounts.GetAll().ToList();
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts created. Please do that first");
            Utils.PauseConsole();
            return;
        }

        Console.WriteLine("Budgeting Accounts:");
        string[] accountNames = accounts.Where(a => a != null).Select(a => a.Name).ToArray();
        int menuSelection = Utils.MenuSelector(
            menuItems: accountNames,
            selectionMessage: "Choose an account to adjust balance",
            headerMessage: "Available Accounts",
            cancelString: "cancel"
        );
        if (menuSelection == -1)
            return;

        Console.WriteLine($"Selected account '{accounts[menuSelection - 1].Name}'");
        Console.WriteLine();

        decimal? value = Utils.GetDecimalInput("Enter adjusting negative/positive balance", "cancel");
        if (value == null)
            return;

        Account account = accounts[menuSelection - 1];
        Transaction transaction = TransactionController.CreateAdjustingAccountTransaction(account, (decimal)value);
        uow.Transactions.Add(transaction);
        uow.Complete();

        Console.WriteLine($"Adjusting balance by {value:C}. New balance: {account.ActualBalance:C}");
        Utils.PauseConsole();
    }
}