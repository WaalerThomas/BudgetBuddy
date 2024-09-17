using System.Drawing;
using System.Globalization;

using BudgetBuddy.Models;
using BudgetBuddy.Sqlite;

namespace BudgetBuddy.TUI;

// TODO: Need a better system for knowing which parts to redraw.

public enum ApplicationState
{
    Normal,
    Insert
}

public enum Page
{
    Dashboard,
    Transactions,
    Balances,
    CategoryTransfers,
    Configuration
}

public static class StringExt
{
    public static string? Truncate(this string? value, int maxLength, string truncationSuffix = "...")
    {
        return value?.Length > maxLength
            ? value.Substring(0, maxLength - truncationSuffix.Length) + truncationSuffix
            : value;
    }
}

public class Program
{
    int consoleWidth;
    int consoleHeight;

    bool applicationShouldClose;

    ApplicationState applicationState;
    ApplicationBuffer windowBuffer;

    Page currentPage;

    Rectangle dateElement;
    Rectangle headerInfoElement;
    Rectangle headerStateElement;
    Rectangle footerElement;
    Rectangle pageElement;

    public Program()
    {
        applicationState = ApplicationState.Normal;

        consoleWidth = Console.BufferWidth;
        consoleHeight = Console.BufferHeight;

        applicationShouldClose = false;

        windowBuffer = new ApplicationBuffer(consoleWidth, consoleHeight);

        currentPage = Page.Dashboard;

        int headerHeight = 3;
        dateElement = new Rectangle(0, 0, 16, headerHeight);
        int remainingWidth = consoleWidth - dateElement.Width + 1;
        headerInfoElement = new Rectangle(dateElement.Right - 1, 0, remainingWidth - 12, headerHeight);
        remainingWidth = remainingWidth - headerInfoElement.Width + 1;
        headerStateElement = new Rectangle(headerInfoElement.Right - 1, 0, remainingWidth, headerHeight);

        footerElement = new Rectangle(0, consoleHeight - headerHeight, consoleWidth, headerHeight);
        pageElement = new Rectangle(0, dateElement.Bottom, consoleWidth, footerElement.Top - dateElement.Bottom);

        AddHeader();
        AddFooter();
        SetPage("Dashboard");
        ApplicationLoop();
    }

    private void ApplicationLoop()
    {
        while (! applicationShouldClose)
        {
            windowBuffer.DrawBuffer();
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (applicationState == ApplicationState.Normal)
        {
            Console.SetCursorPosition(consoleWidth, consoleHeight);
            var readInput = Console.ReadKey();

            switch (readInput.Key)
            {
                case ConsoleKey.F1:
                    SetPage("Dashboard");
                    break;
                case ConsoleKey.F2:
                    SetPage("Transactions");
                    break;
                case ConsoleKey.F3:
                    SetPage("Balances");
                    break;
                case ConsoleKey.F4:
                    SetPage("Category Transfer");
                    break;
                case ConsoleKey.F5:
                    SetPage("Configuration");
                    break;
                case ConsoleKey.Escape:
                    Console.SetCursorPosition(0, consoleHeight);
                    applicationShouldClose = true;
                    break;
                case ConsoleKey.I:
                    applicationState = ApplicationState.Insert;
                    AddHeader();
                    break;
            }
        }
        else if (applicationState == ApplicationState.Insert)
        {
            Console.SetCursorPosition(consoleWidth, consoleHeight);
            var readInput = Console.ReadKey();

            switch (readInput.Key)
            {
                case ConsoleKey.Escape:
                    applicationState = ApplicationState.Normal;
                    AddHeader();
                    break;
            }
        }
    }

    private void AddHeader()
    {
        // ┬ │ ┌ ┐ ─ └ ┘ ┴
        string dateHorizontalLine = new('─', dateElement.Width - 2);
        string budgetHorizontalLine = new('─', headerInfoElement.Width - 2);
        string stateHorizontalLine = new('─', headerStateElement.Width - 2);

        // Constructing the borders
        windowBuffer.Insert(dateElement.Left, dateElement.Top, string.Format("┌{0}┬{1}┬{2}┐", dateHorizontalLine, budgetHorizontalLine, stateHorizontalLine));
        windowBuffer.Insert(dateElement.Left, dateElement.Top + 1, "│");
        windowBuffer.Insert(headerInfoElement.Left, headerInfoElement.Top + 1, "│");
        windowBuffer.Insert(headerStateElement.Left, headerStateElement.Top + 1, "│");
        windowBuffer.Insert(headerStateElement.Right - 1, headerStateElement.Top + 1, "│");
        windowBuffer.Insert(dateElement.Left, dateElement.Bottom - 1, string.Format("└{0}┴{1}┴{2}┘", dateHorizontalLine, budgetHorizontalLine, stateHorizontalLine));

        // Inserting the content
        DateTime dateTime = DateTime.Today;
        string todaysDate = dateTime.ToString("dd MMM yyyy");
        windowBuffer.Insert(dateElement.Left + 2, dateElement.Top + 1, todaysDate);

        int testValue = -99999;
        CultureInfo cultureInfoNO = CultureInfo.GetCultureInfo("nb-NO");
        string budgetValue = string.Format(cultureInfoNO, "{0:C}", testValue).PadRight(12);
        windowBuffer.Insert(headerInfoElement.Left + 2, headerInfoElement.Top + 1, string.Format("Available to Budget: {0}", budgetValue));

        ConsoleColor fg = applicationState == ApplicationState.Normal ? ConsoleColor.Green : ConsoleColor.Cyan;
        windowBuffer.Insert(headerStateElement.Left + 2, headerInfoElement.Top + 1, applicationState.ToString(), new(){ Background = null, Foreground = fg});
    }

    private void AddFooter()
    {
        //Rectangle footer = new Rectangle(0, consoleHeight - footerHeight, consoleWidth, footerHeight);
        string horizontalLine = new string('─', footerElement.Width - 2);

        // Constructing the borders
        windowBuffer.Insert(footerElement.Left, footerElement.Top, string.Format("┌{0}┐", horizontalLine));
        windowBuffer.Insert(footerElement.Left, footerElement.Top + 1, "│");
        windowBuffer.Insert(footerElement.Right - 1, footerElement.Top + 1, "│");
        windowBuffer.Insert(footerElement.Left, footerElement.Bottom - 1, string.Format("└{0}┘", horizontalLine));

        BufferColor highlightedColor = new BufferColor(){ Foreground = ConsoleColor.Red, Background = null };
        BufferColor defaultColor = new BufferColor();
        List<ColoredText> coloredTexts = [
            new("F1", highlightedColor),  new(" Dashboard   ", defaultColor),
            new("F2", highlightedColor),  new(" Transactions   ", defaultColor),
            new("F3", highlightedColor),  new(" Balances   ", defaultColor),
            new("F4", highlightedColor),  new(" Category Transfers   ", defaultColor),
            new("F5", highlightedColor),  new(" Configuration | ", defaultColor),
            new("ESC", highlightedColor), new(" Exit", defaultColor)
        ];
        windowBuffer.Insert(footerElement.Left + 2, footerElement.Top + 1, coloredTexts);
    }

    private void SetPage(string pageName)
    {
        windowBuffer.Clear(pageElement);

        switch (pageName)
        {
            case "Dashboard":
                AddDashboardPage();
                break;
            case "Transactions":
            case "Balances":
            case "Category Transfers":
            case "Configuration":
            default:
                windowBuffer.Insert(4, 10, pageName.PadRight(20));
                break;    
        }
    }

    private void AddDashboardPage()
    {
        int accountNameMaxLength = 25;
        int height = consoleHeight - dateElement.Height - footerElement.Height;
        Rectangle accountsRect = new Rectangle(0, dateElement.Height, accountNameMaxLength, height / 2);
        accountsRect.X = consoleWidth - accountsRect.Width;

        string horizontalLine = new('─', accountsRect.Width - 2);

        // Constructing the borders
        windowBuffer.Insert(accountsRect.Left, accountsRect.Top, string.Format("┌{0}┐", horizontalLine));
        for (int i = 1; i < accountsRect.Height - 1; i++)
        {
            windowBuffer.Insert(accountsRect.Left, accountsRect.Top + i, "│");
            windowBuffer.Insert(accountsRect.Right - 1, accountsRect.Top + i, "│");
        }
        windowBuffer.Insert(accountsRect.Left, accountsRect.Bottom - 1, string.Format("└{0}┘", horizontalLine));

        windowBuffer.Insert(accountsRect.Left + 2, accountsRect.Top + 1, "Accounts:");
        windowBuffer.Insert(accountsRect.Left, accountsRect.Top + 2, string.Format("├{0}┤", horizontalLine));

        Rectangle tableRect = new(pageElement.Left, pageElement.Top, pageElement.Width - accountsRect.Width, pageElement.Height);
        string tableLine = new('─', tableRect.Width - 2);

        windowBuffer.Insert(tableRect.Left, tableRect.Top, string.Format("┌{0}┐", tableLine));
        for (int i = 1; i < tableRect.Height - 1; i++)
        {
            windowBuffer.Insert(tableRect.Left, tableRect.Top + i, "│");
            windowBuffer.Insert(tableRect.Right - 1, tableRect.Top + i, "│");
        }
        windowBuffer.Insert(tableRect.Left, tableRect.Bottom - 1, string.Format("└{0}┘", tableLine));

        int categoryColumnSize = (tableRect.Width - 4) / 4;
        int availableColumnSize = (tableRect.Width - 4 - categoryColumnSize) / 4;

        int categoryX = tableRect.Left + 2;
        int availableX = categoryX + categoryColumnSize;
        int percentageX = availableX + availableColumnSize;
        int activityX = percentageX + availableColumnSize;
        int budgetedX = activityX + availableColumnSize;

        windowBuffer.Insert(categoryX, tableRect.Top + 1, "Category");
        windowBuffer.Insert(availableX, tableRect.Top + 1, "Available");
        windowBuffer.Insert(activityX, tableRect.Top + 1, "Activity");
        windowBuffer.Insert(budgetedX, tableRect.Top + 1, "Budgeted");

        windowBuffer.Insert(tableRect.Left + 1, tableRect.Top + 2, new string('━', tableRect.Width - 2));

        // Test adding the group and categories.
        using var uow = new UnitOfWork(new DatabaseContext());
        List<Group> groups = uow.Groups.GetAllWithCategories().ToList();

        if (groups.Count == 0)
        {
            string noGroupsMsg = "No Groups or Categories have been registered";
            windowBuffer.Insert(
                tableRect.Left + (tableRect.Width / 2) - (noGroupsMsg.Length / 2),
                tableRect.Top + 4,
                noGroupsMsg
            );
        }
        else
        {
            int elementY = tableRect.Top + 3;
            for (int i = 0; i < groups.Count; i++)
            {
                if (i != 0)
                {
                    windowBuffer.Insert(categoryX, elementY, new string('╌', tableRect.Width - 4));
                    elementY++;
                }

                windowBuffer.Insert(categoryX, elementY, groups[i].Name.PadRight(tableRect.Width - 4));
                elementY++;

                foreach (var category in groups[i].Categories)
                {
                    decimal available = uow.Categories.GetAvailableAmount(category.Id);
                    decimal activity = uow.Categories.GetActivityAmount(category.Id);
                    decimal budgeted = uow.Categories.GetBudgetetAmount(category.Id);
                    windowBuffer.Insert(categoryX + 1, elementY, category.Name.Truncate(categoryColumnSize - 2)!);
                    windowBuffer.Insert(availableX, elementY, $"{available:C}");
                    windowBuffer.Insert(percentageX, elementY, $"{available / category.MonthlyAmount:P0}");
                    windowBuffer.Insert(activityX, elementY, $"{activity:C}");
                    windowBuffer.Insert(budgetedX, elementY, $"{budgeted:C}");
                    elementY++;
                }

                // TODO: Add in a check so that we do not exceed the window
            }
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