using BudgetBuddy.Models;
using BudgetBuddy.Sqlite;

namespace BudgetBuddy.Simple.Menus;

public class GroupsCategoriesMenu : IBaseMenu
{
    readonly string[] menuItems;

    public GroupsCategoriesMenu()
    {
        menuItems = [
            "List groups & categories",
            "Add group",
            "Add category",
            "Rename group",
            //"Delete group",                           // TODO: Think about how we are going to handle this if group has transactions and categories already. Something historics.
            "Rename category",
            //"Delete category",                        // TODO: Think about how we are going to handle this if group has transactions and categories already. Something historics.
        ];
    }

    public void ShowMenu(int menuStartX, int menuStartY)
    {
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            int optionSelected = Utils.MenuSelector(menuItems: menuItems, headerMessage: "Groups & Categories Menu", cancelString: "back");
            if (optionSelected == -1)
                return;
            
            Console.WriteLine();
            switch (optionSelected)
            {
                case 1:
                    MenuActionListGroupsCategories();
                    break;
                case 2:
                    MenuActionAddGroup();
                    break;
                case 3:
                    MenuActionAddCategory();
                    break;
                case 4:
                    MenuActionRenameGroup();
                    break;
                case 5:
                    MenuActionRenameCategory();
                    break;
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Utils.PauseConsole();
                    break;
            }
        } while (true);
    }

    private static void MenuActionListGroupsCategories()
    {
        using var uow = new UnitOfWork(new DatabaseContext());
        List<Group> groups = uow.Groups.GetAllWithCategories().ToList();

        Console.WriteLine("Groups and Categories");
        if (groups.Count == 0)
            Console.WriteLine("No groups or categories have been registered");
        else
        {
            string seperatorLine = new string('-', Console.BufferWidth);

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Name,-20}Category Count: {group.Categories?.Count ?? 0}");
                if (group.Categories == null || group.Categories?.Count == 0)
                    continue;

                Console.WriteLine(seperatorLine);
                Console.WriteLine("Name".PadRight(20) + "Monthly Amount".PadRight(20) + "Goal Amount".PadRight(20));
                foreach (var category in group.Categories!)
                    Console.WriteLine($"{category.Name,-20}{category.MonthlyAmount,-20:C}{category.GoalAmount,-20:C}");
                
                Console.WriteLine();
            }
        }

        Utils.PauseConsole();
    }

    private static void MenuActionAddGroup()
    {
        Console.WriteLine("Create a new group");

        bool addMore;
        do
        {
            string? groupName = Utils.GetStringInput("Enter group name", minLength: Group.MIN_NAME_LENGTH, maxLength: Group.MAX_NAME_LENGTH);
            if (groupName == null)
                return;
            
            using (var uow = new UnitOfWork(new DatabaseContext()))
            {
                Group group = new() { Name = groupName };
                uow.Groups.Add(group);
                uow.Complete();
            }
            Console.WriteLine($"Group '{groupName}' added");

            Console.WriteLine();
            addMore = Utils.GetYesNoInput("Add another group?", isYesDefault: true);
        } while (addMore == true);
    }

    private static void MenuActionRenameGroup()
    {
        using (var uow = new UnitOfWork(new DatabaseContext()))
        {
            List<Group> groups = uow.Groups.GetAll().ToList();
            if (groups.Count == 0)
            {
                Console.WriteLine("No groups created. Please do that first");
                Utils.PauseConsole();
                return;
            }

            string[] groupNames = groups.Where(g => g != null).Select(g => g.Name).ToArray();
            int menuSelection = Utils.MenuSelector(
                menuItems: groupNames,
                selectionMessage: "Choose a group to rename",
                headerMessage: "Available Groups",
                cancelString: "cancel"
            );
            if (menuSelection == -1)
                return;
            
            Console.WriteLine($"Selected group '{groups[menuSelection - 1].Name}'");
            Console.WriteLine();

            Group group = groups[menuSelection - 1];
            string oldName = group.Name;

            string? newName = Utils.GetStringInput("Enter new group name", minLength: Group.MIN_NAME_LENGTH, maxLength: Group.MAX_NAME_LENGTH);
            if (newName == null)
                return;
            
            group.Name = newName;

            Console.WriteLine($"Renamed group '{oldName}' to '{newName}'");
            uow.Complete();
        }

        Utils.PauseConsole();
    }

    private static void MenuActionAddCategory()
    {
        Console.WriteLine("Create a new category");

        using var uow = new UnitOfWork(new DatabaseContext());
        List<Group> groups = uow.Groups.GetAll().ToList();

        string[] groupNames = groups.Where(g => g != null).Select(g => g.Name).ToArray();
        if (groupNames.Length == 0)
        {
            Console.WriteLine("No registered groups. Please add one first");
            Utils.PauseConsole();
            return;
        }

        int menuSelection = Utils.MenuSelector(
            menuItems: groupNames,
            selectionMessage: "Choose which group to add a new category to",
            headerMessage: "Available Groups",
            cancelString: "cancel"
        );
        if (menuSelection == -1)
            return;

        Console.WriteLine($"Selected group '{groups[menuSelection - 1].Name}'");
        Console.WriteLine();

        Group group = groups[menuSelection - 1];

        bool addMore;
        do
        {
            string? categoryName = Utils.GetStringInput("Enter category name", minLength: Category.MIN_NAME_LENGTH, maxLength: Category.MAX_NAME_LENGTH);
            if (categoryName == null)
                return;

            Category category = new() { Name = categoryName };

            decimal? monthlyAmount = Utils.GetDecimalInput("Enter monthly amount", "");
            if (monthlyAmount != null)
                category.MonthlyAmount = (decimal)monthlyAmount;
            
            decimal? goalAmount = Utils.GetDecimalInput("Enter goal amount", "");
            if (goalAmount != null)
                category.GoalAmount = (decimal)goalAmount;

            group.Categories.Add(category);
            uow.Complete();

            Console.WriteLine($"Category '{categoryName}' added to group '{group.Name}'");
            Console.WriteLine();

            addMore = Utils.GetYesNoInput("Add another category?", isYesDefault: true);
        } while (addMore == true);
    }

    private static void MenuActionRenameCategory()
    {
        using (var uow = new UnitOfWork(new DatabaseContext()))
        {
            List<Category> categories = uow.Categories.GetAll().ToList();
            if (categories.Count == 0)
            {
                Console.WriteLine("No categories created. Please do that first");
                Utils.PauseConsole();
                return;
            }

            string[] categoryNames = categories.Where(c => c != null).Select(c => c.Name).ToArray();
            int menuSelection = Utils.MenuSelector(
                menuItems: categoryNames,
                selectionMessage: "Choose a category to rename",
                headerMessage: "Available Categories",
                cancelString: "cancel"
            );
            if (menuSelection == -1)
                return;
            
            Console.WriteLine($"Selected category '{categories[menuSelection - 1].Name}'");
            Console.WriteLine();

            Category category = categories[menuSelection - 1];
            string oldName = category.Name;

            string? newName = Utils.GetStringInput("Enter new category name", minLength: Category.MIN_NAME_LENGTH, maxLength: Category.MAX_NAME_LENGTH);
            if (newName == null)
                return;
            
            category.Name = newName;

            Console.WriteLine($"Renamed category '{oldName}' to '{newName}'");
            uow.Complete();
        }

        Utils.PauseConsole();
    }
}