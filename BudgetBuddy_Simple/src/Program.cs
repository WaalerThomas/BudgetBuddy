using BudgetBuddyCore;
using BudgetBuddyCore.Models;
using BudgetBuddySqlite;

namespace BudgetBuddySimple;

internal class Program
{
    // Class specific variables
    bool shouldApplicationClose;
    string[] mainMenu;
    string[] accountsMenu;
    string[] groupsCategoriesMenu;
    string[] transactionsMenu;
    string[] categoryTransferMenu;

    int menuStartX;
    int menuStartY;

    // BudgetBuddy Core related variables
    private readonly DatabaseContext _databaseContext;
    Application application;

    public Program(bool rebuildDatabase = false)
    {
        shouldApplicationClose = false;
        mainMenu = [
            "Accounts",
            "Groups & Categories",
            "Transactions",
            "Category Transfers"
        ];
        accountsMenu = [
            "List budgeting accounts",
            "Add budgeting account",
            "Update balances"
        ];
        groupsCategoriesMenu = [
            "List groups & categories",
            "Add group",
            "Add category",
        ];
        transactionsMenu = [
            "Show last reconciliation",
            "Show activity data",
            "New transaction",
            "Find transaction"
        ];
        categoryTransferMenu = [
            "View funding details",
            "New transfer",
            "Find tranfer"
        ];

        menuStartX = 0;
        menuStartY = 0;

        _databaseContext = new DatabaseContext("hello.db", rebuildDatabase);
        application = new Application(
            new AccountRespository(_databaseContext),
            new GroupRepository(_databaseContext),
            new CategoryRepository(_databaseContext),
            new TransactionRepository(_databaseContext)
        );

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
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
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
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
                    break;
            }
        } while (true);
    }

    void ShowGroupsCategoriesMenu()
    {
        int optionSelected;
        do
        {
            Utils.ClearScreen(menuStartX, menuStartY, Console.BufferWidth, Console.BufferHeight);

            optionSelected = Utils.MenuSelector(menuItems: groupsCategoriesMenu, headerMessage: "Groups & Categories Menu", cancelString: "back");
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
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
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
                case 3:
                    MenuActionNewTransaction();
                    break;
                default:
                    Console.WriteLine("This option is not being handled yet.");
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
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
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
                    break;
            }
        } while (true);
    }

    void MenuActionListAccounts()
    {
        List<Account> accounts = application.GetAccounts();
        
        Console.WriteLine("Budgeting Accounts:");
        if (accounts.Count == 0)
            Console.WriteLine("No accounts have been registered");
        else
        {
            Console.WriteLine("Id".PadRight(10) + "Account Name".PadRight(20) + "Balance".PadRight(15));
            foreach (var account in accounts)
                Console.WriteLine($"{account.Id,-10}{account.Name,-20}{account.Balance.ToString(),-15:C}");
        }

        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue!");
        Console.ReadLine();
    }

    void MenuActionAddAccount()
    {
        Console.WriteLine("Creating a new account");

        bool addMore;
        do
        {
            string? accountName = Utils.GetStringInput("Enter account name", minLength: 1, maxLength: 20);
            if (accountName == null)
                return;

            Account account = new Account() { Name = accountName };
            bool added = application.AddAccount(account);
            if (added)
                Console.WriteLine($"Account '{accountName}' added");
            else
                Console.WriteLine($"Failed to create account '{accountName}'");

            Console.WriteLine();
            addMore = Utils.GetYesNoInput("Add another account?", isYesDefault: true);
        } while (addMore == true);
    }

    void MenuActionListGroupsCategories()
    {
        List<Group> groups = application.GetGroups();

        Console.WriteLine("Groups and Categories");
        if (groups.Count == 0)
            Console.WriteLine("No groups or categories have been registered");
        else
        {
            foreach (var group in groups)
            {
                Console.WriteLine(group);
                if (group.Categories == null)
                    continue;
                
                foreach (var category in group.Categories)
                    Console.WriteLine($"\t{category,-20}{category.MonthlyAmount,-12:C}{category.GoalAmount,-12:C}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue!");
        Console.ReadLine();
    }

    void MenuActionAddGroup()
    {
        Console.WriteLine("Create a new group");

        bool addMore;
        do
        {
            string? groupName = Utils.GetStringInput("Enter group name", minLength: 1, maxLength: 254);
            if (groupName == null)
                return;
            
            Group group = new Group() { Name = groupName };
            bool added = application.AddGroup(group);
            if (added)
                Console.WriteLine($"Group '{groupName}' added");
            else
                Console.WriteLine($"Failed to create group '{groupName}'");

            Console.WriteLine();
            addMore = Utils.GetYesNoInput("Add another group?", isYesDefault: true);
        } while (addMore == true);
    }

    void MenuActionAddCategory()
    {
        Console.WriteLine("Create a new category");

        List<Group> groups = application.GetGroups();

        bool addMore;
        do
        {
            string[] groupsName = groups.Where(g => g != null)
                                        .Select(g => g.ToString())
                                        .ToArray();

            if (groupsName.Length == 0)
            {
                Console.WriteLine("No registered groups. Please add one first");
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
                return;
            }

            int optionSelected = Utils.MenuSelector(
                menuItems: groupsName,
                selectionMessage: "Choose which group the category will be added to",
                headerMessage: "Available Groups",
                cancelString: "cancel"
            );
            if (optionSelected == -1)
                return;
            
            Console.WriteLine($"Selected group '{groups[optionSelected - 1]}'");

            Console.WriteLine();
            string? categoryName = Utils.GetStringInput("Enter category name", minLength: 1, maxLength: 254);
            if (categoryName == null)
                return;

            Category category = new Category() { Name = categoryName, Group = groups[optionSelected - 1] };
            bool added = application.AddCategory(category);
            if (added)
                Console.WriteLine($"Category '{categoryName}' added");
            else
                Console.WriteLine($"Failed to create category '{categoryName}'");

            Console.WriteLine();
            addMore = Utils.GetYesNoInput("Add another category?", isYesDefault: true);
        } while (addMore == true);
    }

    void MenuActionShowLastReconciliation()
    {
    }

    void MenuActionNewTransaction()
    {
        Console.WriteLine("Add a new transaction");
        Account? cashingAccount = application.GetAccountByName("Savings");
        Category? gasolineCategory = application.GetCategoryByName("Gasoline");

        bool failed = false;
        if (cashingAccount == null)
        {
            Console.WriteLine("Account 'Savings' could not be found");
            failed = true;
        }
        if (gasolineCategory == null)
        {
            Console.WriteLine("Category 'Gasoline' could not be found");
            failed = true;
        }
        if (failed)
        {
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
            return;
        }

        Transaction transaction = new Transaction() {
            Account = cashingAccount!,
            Category = gasolineCategory!,
            TransactionStatus = TransactionStatus.Completed,
            Amount = 100.0m,
            Date = DateTime.Now,
            Memo = "",
            FlowControl = null
        };
        bool added = application.AddTransaction(transaction);
        if (added)
            Console.WriteLine($"Transaction '{transaction}' added");
        else
            Console.WriteLine($"Failed to add transaction '{transaction}'");

        Console.WriteLine("Press ENTER to continue");
        Console.ReadLine();
    }

    static void Main(string[] args)
    {
        bool rebuildDatabase = false;
        if (args.Length == 1)
        {
            if (args[0] == "rebuildDatabase")
                rebuildDatabase = true;
        }

        _ = new Program(rebuildDatabase);
    }
}