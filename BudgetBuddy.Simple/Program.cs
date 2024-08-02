using System.Globalization;
using BudgetBuddy.Controllers;
using BudgetBuddy.Simple.Menus;
using BudgetBuddy.Sqlite;

namespace BudgetBuddy.Simple;

internal class Program
{
    bool shouldApplicationClose;

    readonly string[] mainMenu;
    readonly IBaseMenu[] menus;

    Point menuStartPosition;
    Point aToBStartPosition;

    public Program()
    {
        shouldApplicationClose = false;
        mainMenu = [
            "Accounts",
            "Groups & Categories",
            "Transactions",
            "Category Transfers",
            //"Settings"                                // TODO: This will be for setting formats (time, currency, date, etc)
        ];
        menus = [
            new AccountsMenu(),
            new GroupsCategoriesMenu(),
            new TransactionsMenu(),
            new CategoryTransfersMenu(),
        ];

        menuStartPosition = new();
        aToBStartPosition = new();

        StartApplicationLoop();
    }

    void StartApplicationLoop()
    {
        while (! shouldApplicationClose)
        {
            Console.Clear();
            Console.WriteLine("Budget Buddy - Tester Application");
            Console.WriteLine("Created by Thomas Waaler");
            Console.WriteLine();

            (aToBStartPosition.x, aToBStartPosition.y) = Console.GetCursorPosition();

            using (var uow = new UnitOfWork(new DatabaseContext()))
            {
                decimal availableToBudget = TransactionController.GetAvailableToBudget(uow);
                Console.WriteLine($"Available To Budget: {availableToBudget:C}");
                Console.WriteLine();
            }

            (menuStartPosition.x, menuStartPosition.y) = Console.GetCursorPosition();
            ShowMainMenu();
        }
    }

    void ShowMainMenu()
    {
        Utils.ClearScreen(menuStartPosition.x, menuStartPosition.y, Console.BufferWidth, Console.BufferHeight);

        int optionSelected = Utils.MenuSelector(menuItems: mainMenu);
        if (optionSelected == -1)
        {
            shouldApplicationClose = true;
            return;
        }

        Console.WriteLine();
        if (optionSelected <= menus.Length)
            menus[optionSelected - 1].ShowMenu(menuStartPosition, aToBStartPosition);
        else
        {
            Console.WriteLine("This option is not being handled yet.");
            Utils.PauseConsole();
        }
    }

    static void Main(string[] args)
    {
        // FIXME: Add to settings a way to change the formating of currency
        CultureInfo info = new CultureInfo("nb-NO");
        Thread.CurrentThread.CurrentCulture = info;

        _ = new Program();
    }
}