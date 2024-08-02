using BudgetBuddy.Sqlite;

namespace BudgetBuddy.Simple.Menus;

public class CategoryTransfersMenu : IBaseMenu
{
    readonly string[] menuItems;

    public CategoryTransfersMenu()
    {
        menuItems = [
            "List category transfers",
            "Fill budget for category"
        ];
    }

    public void ShowMenu(Point menuStartPosition, Point aToBStartPosition)
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartPosition.x, menuStartPosition.y, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: menuItems, headerMessage: "Category Transfers Menu", cancelString: "back");
            if (optionSelected == -1)
                return;
            
            Console.WriteLine();
            switch (optionSelected)
            {
                case 2:
                    MenuActionBudgetCategory(aToBStartPosition.y);
                    break;
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Utils.PauseConsole();
                    break;
            }
        } while (true);
    }

    private void MenuActionBudgetCategory(int aToBPositionY)
    {
        throw new NotImplementedException();

        using var uow = new UnitOfWork(new DatabaseContext());
    }
}