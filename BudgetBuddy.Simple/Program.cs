using BudgetBuddy.Simple.Menus;

namespace BudgetBuddy.Simple;

internal class Program
{
    bool shouldApplicationClose;

    readonly string[] mainMenu;
    readonly IBaseMenu[] menus;

    int menuStartX;
    int menuStartY;

    public Program()
    {
        shouldApplicationClose = false;
        mainMenu = [
            "Accounts",
            "Groups & Categories",
            "Transactions",
            //"Category Transfers",
            //"Settings"                                // TODO: This will be for setting formats (time, currency, date, etc)
        ];
        menus = [
            new AccountsMenu(),
            new GroupsCategoriesMenu(),
            new TransactionsMenu(),
        ];

        menuStartX = 0;
        menuStartY = 0;

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

            (menuStartX, menuStartY) = Console.GetCursorPosition();
            ShowMainMenu();
        }
    }

    void ShowMainMenu()
    {
        Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

        int optionSelected = Utils.MenuSelector(menuItems: mainMenu);
        if (optionSelected == -1)
        {
            shouldApplicationClose = true;
            return;
        }

        Console.WriteLine();
        if (optionSelected <= menus.Length)
            menus[optionSelected - 1].ShowMenu(menuStartX, menuStartY);
        else
        {
            Console.WriteLine("This option is not being handled yet.");
            Utils.PauseConsole();
        }
    }

    static void Main(string[] args)
    {
        _ = new Program();
    }
}