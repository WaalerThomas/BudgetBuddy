using BudgetBuddy.Controllers;
using BudgetBuddy.Models;
using BudgetBuddy.Sqlite;

namespace BudgetBuddy.Simple.Menus;

public class TransactionsMenu : IBaseMenu
{
    readonly string[] menuItems;

    public TransactionsMenu()
    {
        menuItems = [
            "List Transactions",
            "Add a transaction"
            //"Show last reconciliation",
            //"Show activity data"
        ];
    }

    public void ShowMenu(Point menuStartPosition, Point aToBStartPosition)
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartPosition.x, menuStartPosition.y, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: menuItems, headerMessage: "Transactions Menu", cancelString: "back");
            if (optionSelected == -1)
                return;
            
            Console.WriteLine();
            switch (optionSelected)
            {
                case 1:
                    MenuActionListTransactions();
                    break;
                case 2:
                    MenuActionAddTransaction(aToBStartPosition.y);
                    break;
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Utils.PauseConsole();
                    break;
            }
        } while (true);
    }

    private static void MenuActionListTransactions()
    {
        var uow = new UnitOfWork(new DatebaseContext());
        List<Transaction> transactionsCopy = uow.Transactions.GetAllWithExtra().ToList();

        Console.WriteLine("Transactions:");
        if (transactionsCopy.Count == 0)
            Console.WriteLine("No transactions have been registered");
        else
        {
            Console.WriteLine("Id".PadRight(10) + "Date".PadRight(12) + "Type".PadRight(20) + "Amount".PadRight(20) + "Category".PadRight(20) + "Account".PadRight(20) + "Status".PadRight(12));
            foreach (var t in transactionsCopy)
                Console.WriteLine($"{t.Id,-10}{t.EntryDate,-12}{t.TransactionType.Name,-20}{t.Amount,-20:C}{t.Category?.Name,-20}{t.Account?.Name,-20}{t.TransactionStatus.Name,-12}");
        }

        Utils.PauseConsole();
    }

    private static void MenuActionAddTransaction(int aToBPositionY)
    {
        Console.WriteLine("Register transaction");

        using var uow = new UnitOfWork(new DatabaseContext());

        // FIXME: Implement this method.

        DateOnly? selectedDate = Utils.GetDateInput("Enter transaction date");
        if (selectedDate is null)
            return;

        // Selecting an account
        // FIXME: Getting selection input from the user is being reused a lot. Can we make some parts of it into functions?
        List<Account> accounts = uow.Accounts.GetAll().ToList();
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts created. Please do that first");
            Utils.PauseConsole();
            return;
        }

        Console.WriteLine($"Selected transaction date '{selectedDate}'");
        Console.WriteLine();

        string[] accountNames = accounts.Where(a => a != null).Select(a => a.Name).ToArray();
        int menuSelection = Utils.MenuSelector(
            menuItems: accountNames,
            selectionMessage: "Choose an account",
            headerMessage: "Available Accounts",
            cancelString: "cancel"
        );
        if (menuSelection == -1)
            return;
        
        Account account = accounts[menuSelection - 1];
        Console.WriteLine($"Selected account '{account.Name}' | Balance: {account.ActualBalance:C}");
        Console.WriteLine();

        // Selecting a category
        List<Category> categories = uow.Categories.GetAll().ToList();
        if (categories.Count == 0)
        {
            Console.WriteLine("No categories created. Please do that first");
            Utils.PauseConsole();
            return;
        }

        string[] categoryNames = categories.Where(c => c != null).Select(c => c.Name).ToArray();
        menuSelection = Utils.MenuSelector(
            menuItems: categoryNames,
            selectionMessage: "Choose a category",
            headerMessage: "Available Categories",
            cancelString: "cancel"
        );
        if (menuSelection == -1)
            return;
        
        Category category = categories[menuSelection - 1];
        Console.WriteLine($"Selected category '{category.Name}'");
        Console.WriteLine();

        // Select amount
        decimal? amount = Utils.GetDecimalInput("Enter adjusting negative/positive balance", "cancel");
        if (amount is null)
            return;
        
        Console.WriteLine();
        
        // Select Transaction Status
        string[] statusNames = ["Settled", "Pending"];
        menuSelection = Utils.MenuSelector(
            menuItems: statusNames,
            selectionMessage: "Choose a transaction status",
            headerMessage: "Availables statuses",
            cancelString: "cancel"
        );
        if (menuSelection == -1)
            return;

        Console.WriteLine($"Selected status '{statusNames[menuSelection - 1]}'");
        Console.WriteLine();

        Console.WriteLine($"Adding transaction '{selectedDate} {amount:C} {category.Name} {account.Name} {statusNames[menuSelection - 1]}'");

        // TODO: Add the actual transaction to the database
        // TODO: Make a better way to choose transaction status
        Transaction transaction = TransactionController.CreateCategoryTransaction((DateOnly)selectedDate, account, category, (decimal)amount, (TransactionStatusEnum)menuSelection - 1);
        uow.Transactions.Add(transaction);
        uow.Complete();

        // Updated the "Available to Budget"
        // TODO: Move this to a method somewhere to update the "available to budget" on screen
        (int x, int y) = Console.GetCursorPosition();
        Utils.ClearLine(aToBPositionY);
        decimal availableToBudget = TransactionController.GetAvailableToBudget(uow);
        Console.WriteLine($"Available To Budget: {availableToBudget:C}");
        Console.SetCursorPosition(x, y);

        Utils.PauseConsole();
    }
}

internal class DatebaseContext : DatabaseContext
{
}