using BudgetBuddy.Controllers;
using BudgetBuddy.Models;
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
                case 1:
                    MenuActionListTransferCategories();
                    break;
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

    private void MenuActionListTransferCategories()
    {
        var uow = new UnitOfWork(new DatabaseContext());
        List<CategoryTransfer> categoryTransfersCopy = uow.CategoryTransfers.GetAllWithExtra().ToList();

        Console.WriteLine("Category Transfers:");
        if (categoryTransfersCopy.Count == 0)
            Console.WriteLine("No category transfers have been registered");
        else
        {
            Console.WriteLine("Id".PadRight(10) + "Date".PadRight(12) + "Amount".PadRight(20) + "From".PadRight(20) + "To".PadRight(20) + "Transfer Type".PadRight(20));
            foreach (var t in categoryTransfersCopy)
                Console.WriteLine($"{t.Id,-10}{t.EntryDate,-12}{t.Amount,-20:C}{t.FromCategory?.Name,-20}{t.ToCategory?.Name,-20}{t.TransferType.Name,-20}");
        }

        Utils.PauseConsole();
    }

    private void MenuActionBudgetCategory(int aToBPositionY)
    {
        using var uow = new UnitOfWork(new DatabaseContext());

        // Does the user want to transfer from "Available to Budget" or from a different category?
        string[] transferTypes = ["Available to budget", "From a category"];
        int optionSelect = Utils.MenuSelector(transferTypes, "Choose where to transfer funds from");
        if (optionSelect == -1)
            return;
        
        CategoryTransferTypeEnum transferType = (CategoryTransferTypeEnum)optionSelect;
        Console.WriteLine($"Selected transfer type '{transferTypes[optionSelect - 1]}'");
        Console.WriteLine();

        DateOnly? selectedDate = Utils.GetDateInput("Enter transaction date");
        if (selectedDate is null)
            return;
        
        Console.WriteLine($"Selected transfer date '{selectedDate}'");
        Console.WriteLine();

        List<Category> categories = uow.Categories.GetAll().ToList();
        if (categories.Count == 0)
        {
            Console.WriteLine("No categories created. Please do that first");
            Utils.PauseConsole();
            return;
        }
        string[] categoryNames = categories.Where(c => c != null).Select(c => c.Name).ToArray();

        Category? fromCategory = null;
        if (transferType == CategoryTransferTypeEnum.FromCategory)
        {
            optionSelect = Utils.MenuSelector(
                menuItems: categoryNames,
                selectionMessage: "Choose the category to transfer from",
                headerMessage: "Available Categories"
            );
            if (optionSelect == -1)
                return;
            
            fromCategory = categories[optionSelect - 1];
            Console.WriteLine($"Selected from category '{fromCategory.Name}'");
            Console.WriteLine();
        }

        // TODO: Do not allow selected the same category for from and to.
        // Maybe remove the already selected category so that it cannot be selected?
        optionSelect = Utils.MenuSelector(
            menuItems: categoryNames,
            selectionMessage: "Choose the category to transfer into",
            headerMessage: "Available Categories"
        );
        if (optionSelect == -1)
            return;

        Category toCategory = categories[optionSelect - 1];

        // TODO: What if the category we are transfering from doesn't have the available funds?
        decimal? amount = Utils.GetDecimalInput("Enter transfer amount", "cancel");
        if (amount is null)
            return;

        // Creating the entry
        CategoryTransfer categoryTransfer = new()
        {
            EntryDate = (DateOnly)selectedDate,
            Amount = (decimal)amount,
            FromCategory = fromCategory,
            ToCategory = toCategory,
            TransferType = CategoryTransferController.TypeFromEnum(transferType)
        };
        uow.CategoryTransfers.Add(categoryTransfer);
        uow.Complete();

        // Updated the "Available to Budget"
        // TODO: Move this to a method somewhere to update the "available to budget" on screen
        (int x, int y) = Console.GetCursorPosition();
        Utils.ClearLine(aToBPositionY);
        decimal availableToBudget = TransactionController.GetAvailableToBudget(uow);
        Console.WriteLine($"Available To Budget: {availableToBudget:C}");
        Console.SetCursorPosition(x, y);

        Console.WriteLine($"Added category transfer | {selectedDate} - {amount} - {fromCategory?.Name} - {toCategory?.Name} - {categoryTransfer.TransferType.Name}");
        Utils.PauseConsole();
    }
}