using BudgetBuddy.Controllers;
using BudgetBuddy.Models;
using BudgetBuddy.Sqlite;

namespace BudgetBuddy.Simple;

internal class Program
{
    bool shouldApplicationClose;

    readonly string[] mainMenu;
    readonly string[] accountsMenu;
    readonly string[] groupsCategoriesMenu;
    readonly string[] transactionsMenu;
    readonly string[] categoryTransferMenu;

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
        accountsMenu = [
            "List budgeting accounts",
            "Add budgeting account",
            "Change name of a budgeting account",
            "Adjust account balance",                   // TODO: What will happen when the balance is negative?
            //"Remove a budgeting account"              // TODO: Think about how we are going to handle this if account has transactions already. Something historics.
        ];
        groupsCategoriesMenu = [
            "List groups & categories",
            "Add group",
            "Add category",
            "Rename group",
            //"Delete group",                           // TODO: Think about how we are going to handle this if group has transactions and categories already. Something historics.
            "Rename category",
            //"Delete category",                        // TODO: Think about how we are going to handle this if group has transactions and categories already. Something historics.
        ];
        transactionsMenu = [
            "a"
            //"Show last reconciliation",
            //"Show activity data"
        ];
        categoryTransferMenu = [
            //"View funding details",
            //"New transfer",
            //"Find tranfer"
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

        // NOTE: Maybe we could define that the menu items have callback functions instead?? Or an overloaded variant?
        Console.WriteLine();
        switch (optionSelected)
        {
            case 1:
                ShowAccountsMenu();
                break;
            case 2:
                ShowGroupsCategoriesMenu();
                break;
            case 3:
                ShowTransactionsMenu();
                break;
            case 4:
                ShowCategoryTransfersMenu();
                break;
            default:
                Console.WriteLine("This option is not being handled yet.");
                PauseConsole();
                break;
        }
    }

    void ShowAccountsMenu()
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: accountsMenu, headerMessage: "Accounts Menu", cancelString: "back");
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
                    PauseConsole();
                    break;
            }
        } while (true);
    }

    void ShowGroupsCategoriesMenu()
    {
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            int optionSelected = Utils.MenuSelector(menuItems: groupsCategoriesMenu, headerMessage: "Groups & Categories Menu", cancelString: "back");
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
                    PauseConsole();
                    break;
            }
        } while (true);
    }

    void ShowTransactionsMenu()
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: transactionsMenu, headerMessage: "Transactions Menu", cancelString: "back");
            if (optionSelected == -1)
                return;
            
            Console.WriteLine();
            switch (optionSelected)
            {
                case 1:
                {
                    using var uow = new UnitOfWork(new DatabaseContext());
                    Account? account = uow.Accounts.Get(1);
                    if (account is not null)
                    {
                        Console.WriteLine("Name: " + account.Name);
                        Console.WriteLine("Actual Balance: " + account.ActualBalance);
                        Console.WriteLine("Pending Balance: " + account.PendingBalance);
                        Console.WriteLine("Settled Balance: " + account.SettledBalance);
                        PauseConsole();
                    }
                } break;
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    PauseConsole();
                    break;
            }
        } while (true);
    }

    void ShowCategoryTransfersMenu()
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: categoryTransferMenu, headerMessage: "Category Transfers Menu", cancelString: "back");
            if (optionSelected == -1)
                return;
            
            Console.WriteLine();
            switch (optionSelected)
            {
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    PauseConsole();
                    break;
            }
        } while (true);
    }

    void MenuActionListAccounts()
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

        PauseConsole();
    }

    void MenuActionAddAccount()
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

    void MenuActionRenameAccount()
    {
        using (var uow = new UnitOfWork(new DatabaseContext()))
        {
            List<Account> accounts = uow.Accounts.GetAll().ToList();
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts created. Please do that first");
                PauseConsole();
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
        
        PauseConsole();
    }

    void MenuActionListGroupsCategories()
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

        PauseConsole();
    }

    void MenuActionAddGroup()
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

    void MenuActionRenameGroup()
    {
        using (var uow = new UnitOfWork(new DatabaseContext()))
        {
            List<Group> groups = uow.Groups.GetAll().ToList();
            if (groups.Count == 0)
            {
                Console.WriteLine("No groups created. Please do that first");
                PauseConsole();
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

        PauseConsole();
    }

    void MenuActionAddCategory()
    {
        Console.WriteLine("Create a new category");

        using var uow = new UnitOfWork(new DatabaseContext());
        List<Group> groups = uow.Groups.GetAll().ToList();

        string[] groupNames = groups.Where(g => g != null).Select(g => g.Name).ToArray();
        if (groupNames.Length == 0)
        {
            Console.WriteLine("No registered groups. Please add one first");
            PauseConsole();
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

    void MenuActionRenameCategory()
    {
        using (var uow = new UnitOfWork(new DatabaseContext()))
        {
            List<Category> categories = uow.Categories.GetAll().ToList();
            if (categories.Count == 0)
            {
                Console.WriteLine("No categories created. Please do that first");
                PauseConsole();
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

        PauseConsole();
    }

    void MenuActionShowLastReconciliation()
    {
    }

    void MenuActionAdjustAccountBalance()
    {
        using var uow = new UnitOfWork(new DatabaseContext());
        List<Account> accounts = uow.Accounts.GetAll().ToList();
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts created. Please do that first");
            PauseConsole();
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
        PauseConsole();
    }

    void PauseConsole()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue");
        Console.ReadLine();
    }

    static void Main(string[] args)
    {
        _ = new Program();
    }
}