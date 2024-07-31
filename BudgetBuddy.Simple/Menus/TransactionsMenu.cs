namespace BudgetBuddy.Simple.Menus;

public class TransactionsMenu : IBaseMenu
{
    readonly string[] menuItems;

    public TransactionsMenu()
    {
        menuItems = [
            "Add a transaction"
            //"Show last reconciliation",
            //"Show activity data"
        ];
    }

    public void ShowMenu(int menuStartX, int menuStartY)
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: menuItems, headerMessage: "Transactions Menu", cancelString: "back");
            if (optionSelected == -1)
                return;
            
            Console.WriteLine();
            switch (optionSelected)
            {
                case 1:
                    MenuActionAddTransaction();
                    break;
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Utils.PauseConsole();
                    break;
            }
        } while (true);
    }

    private static void MenuActionAddTransaction()
    {
        throw new NotImplementedException();
    }
}