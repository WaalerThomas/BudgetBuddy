using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using BudgetBuddyCore;

namespace BudgetBuddyTUI;

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

public class Program
{
    int consoleWidth;
    int consoleHeight;

    bool applicationShouldClose;

    Application application;
    ApplicationState applicationState;
    ApplicationBuffer windowBuffer;

    Page currentPage;

    Rectangle dateElement;
    Rectangle headerInfoElement;
    Rectangle footerElement;
    Rectangle pageElement;

    public Program()
    {
        application = new Application();
        application.AddAccount("Bitch");
        application.AddAccount("Something");
        applicationState = ApplicationState.Normal;

        consoleWidth = Console.BufferWidth;
        consoleHeight = Console.BufferHeight;

        applicationShouldClose = false;

        windowBuffer = new ApplicationBuffer(consoleWidth, consoleHeight);

        currentPage = Page.Dashboard;

        int headerHeight = 3;
        dateElement = new Rectangle(0, 0, 15, headerHeight);
        int remainingWidth = consoleWidth - dateElement.Width + 1;
        headerInfoElement = new Rectangle(dateElement.Right - 1, 0, remainingWidth, headerHeight);
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
            DrawApplicationBuffer();
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
                case ConsoleKey.Q:
                    windowBuffer.Clear(new Rectangle(
                        0, 3,
                        consoleWidth, consoleHeight - 6
                    ));
                    break;
                case ConsoleKey.Escape:
                    Console.SetCursorPosition(0, consoleHeight);
                    applicationShouldClose = true;
                    break;
            }
        }
        else if (applicationState == ApplicationState.Insert)
        {
        }
    }

    private void DrawApplicationBuffer()
    {
        Console.Clear();
        for (int i = 0; i < consoleHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write( windowBuffer.BufferLine(i) );
        }
    }

    private void AddHeader()
    {
        // ┬ │ ┌ ┐ ─ └ ┘ ┴

        //Rectangle dateRect = new Rectangle(0, 0, 15, headerHeight);
        //int remainingWidth = consoleWidth - dateRect.Width + 1;
        //Rectangle budgetRect = new Rectangle(dateRect.Right - 1, 0, remainingWidth, headerHeight);

        string dateHorizontalLine = new string('─', dateElement.Width - 2);
        string budgetHorizontalLine = new string('─', headerInfoElement.Width - 2);

        // Constructing the borders
        windowBuffer.Insert(dateElement.Left, dateElement.Top, string.Format("┌{0}┬{1}┐", dateHorizontalLine, budgetHorizontalLine));
        windowBuffer.Insert(dateElement.Left, dateElement.Top + 1, "│");
        windowBuffer.Insert(headerInfoElement.Left, headerInfoElement.Top + 1, "│");
        windowBuffer.Insert(headerInfoElement.Right - 1, headerInfoElement.Top + 1, "│");
        windowBuffer.Insert(dateElement.Left, dateElement.Bottom - 1, string.Format("└{0}┴{1}┘", dateHorizontalLine, budgetHorizontalLine));

        // Inserting the content
        DateTime dateTime = DateTime.Today;
        string todaysDate = dateTime.ToString("dd MMM yyyy");
        windowBuffer.Insert(dateElement.Left + 2, dateElement.Top + 1, todaysDate);

        int testValue = -99999;
        CultureInfo cultureInfoNO = CultureInfo.GetCultureInfo("nb-NO");
        string budgetValue = string.Format(cultureInfoNO, "{0:C}", testValue).PadRight(12);
        windowBuffer.Insert(headerInfoElement.Left + 2, headerInfoElement.Top + 1, string.Format("Available to Budget: {0}", budgetValue));
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

        windowBuffer.Insert(footerElement.Left + 2, footerElement.Top + 1, string.Format("F1 Dashboard   F2 Transactions   F3 Balances   F4 Category Transfers   F5 Configuration │ ESC Exit"));
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
        Rectangle accountsRect = new Rectangle(0, dateElement.Height, accountNameMaxLength, height / 3);
        accountsRect.X = consoleWidth - accountsRect.Width;

        string horizontalLine = new string('─', accountsRect.Width - 2);

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

        /*
        List<Account> accounts = application.Accounts;
        for (int i = 0; i < accounts.Count; i++)
        {
            windowBuffer.Insert(accountsRect.Left + 2, accountsRect.Top + 3 + i, accounts[i].Name);
        }
        */
    }

    static void Main(string[] args)
    {
        _ = new Program();
    }
}