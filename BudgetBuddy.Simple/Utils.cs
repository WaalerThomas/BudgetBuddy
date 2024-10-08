namespace BudgetBuddy.Simple;

public struct Point
{
    public int x { get; set; }
    public int y { get; set; }
}

public static class Utils
{
    public static void PauseConsole()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue");
        Console.ReadLine();
    }

    /// <summary>
    /// Method <c>MenuSelector</c> displays a numbered menu, selection message, and an optional menu header to the console, then waits for the user to select one of the menu options.
    /// </summary>
    /// <param name="menuItems"></param>
    /// <param name="selectionMessage"></param>
    /// <param name="headerMessage"></param>
    /// <param name="cancelString"></param>
    /// <returns>Selected item index, one-based indexed. Returns -1 when selection has been canceled</returns>
    /// <exception cref="ArgumentException">Thrown when Parameter <c>menuItems</c> does not contain any </exception>
    public static int MenuSelector(string[] menuItems, string? selectionMessage = null, string? headerMessage = null, string cancelString = "exit")
    {
        if (menuItems.Length < 1)
            throw new ArgumentException("Parameter menuItems need to contain at least one element");

        // Write the menu to the console
        if (headerMessage != null)
            Console.WriteLine(headerMessage);
        
        for (int i = 0; i < menuItems.Length; i++)
            Console.WriteLine($"{i + 1}. {menuItems[i]}");

        if (selectionMessage == null)
            Console.WriteLine($"Select an option by entering a number between 1 and {menuItems.Length} (or '{cancelString}' to cancel):");
        else
            Console.WriteLine($"{selectionMessage}:");

        // Handle user input
        string enterNumberMessage = $"Please enter a number between 1 and {menuItems.Length} (or '{cancelString}' to cancel)";
        string? readString;
        while (true)
        {
            readString = Console.ReadLine();
            if (readString == null)
            {
                Console.WriteLine(enterNumberMessage);
                continue;
            }

            readString = readString.Trim().ToLower();

            if (readString == cancelString.ToLower())
                return -1;

            bool failedToParse = !int.TryParse(readString, out int selection);
            if (failedToParse)
            {
                Console.WriteLine(enterNumberMessage);
                continue;
            }

            if (1 <= selection && selection <= menuItems.Length)
                return selection;
            else
                Console.WriteLine($"'{selection}' is outside of the range 1-{menuItems.Length}. Please try again");
        }
    }

    /// <summary>
    /// Method <c>GetStringInput</c> takes input from the user, and checks if input is between the min and max length required.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="minLength">Set the minimum accepted length of text</param>
    /// <param name="maxLength">Set the maximum accepted length of text</param>
    /// <param name="cancelString"></param>
    /// <returns>Returns the string entered, or null when canceled</returns>
    /// <exception cref="ArgumentException">Thrown when Parameter <c>minLength</c> is bigger than Paramter <c>maxLength</c></exception>
    public static string? GetStringInput(string message, uint minLength = 0, uint maxLength = 255, string cancelString = "cancel")
    {
        if (minLength > maxLength)
            throw new ArgumentException("minLength can not be bigger than maxLength");

        Console.WriteLine($"{message} (or '{cancelString}' to cancel)");

        string? readString;
        while (true)
        {
            readString = Console.ReadLine();
            if (readString == null)
                continue;

            if (readString.Trim().ToLower() == cancelString.ToLower())
                return null;

            if (readString.Length < minLength)
            {
                Console.WriteLine($"Entered text is too short. Minimum length is {minLength}");
                continue;
            }
            
            if (readString.Length > maxLength)
            {
                Console.WriteLine($"Enterd text is too long. Maximum length is {maxLength}");
                continue;
            }

            return readString;
        }
    }

    /// <summary>
    /// Method <c>GetDecimalInput</c> takes input from user and tries to convert into decimal.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancelString"></param>
    /// <returns>Returns decimal entered, or null when canceled</returns>
    public static decimal? GetDecimalInput(string message, string cancelString = "cancel")
    {
        Console.WriteLine($"{message} (or '{cancelString}' to cancel)");

        string? readString;
        while (true)
        {
            readString = Console.ReadLine()?.Trim();
            if (readString == null)
                continue;
            
            if (readString.ToLower() == cancelString.ToLower())
                return null;
            
            if (decimal.TryParse(readString, out decimal value))
                return value;
            else
                Console.WriteLine("Not a valid decimal number");
        }
    }

    public static DateOnly? GetDateInput(string message, string cancelString = "cancel")
    {
        Console.WriteLine($"{message} or ENTER for todays date (or '{cancelString}' to cancel)");

        string? readString;
        while (true)
        {
            readString = Console.ReadLine()?.Trim();
            if (readString == null)
            {
                Console.WriteLine("EMPOTY");
                continue;
            }

            if (readString.Length == 0)
                return DateOnly.FromDateTime(DateTime.Now);
            
            if (readString.ToLower() == cancelString.ToLower())
                return null;

            if (DateOnly.TryParse(readString, out DateOnly newDate))
                return newDate;
            else
                Console.WriteLine("Not a valid date format");
        }
    }

    /// <summary>
    /// Method <c>GetYesNoInput</c> asks for a yes or a no from the user.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="isYesDefault"></param>
    /// <param name="isNoDefault"></param>
    /// <returns>Return <c>true</c> for yes, and <c>false</c> for no.</returns>
    /// <exception cref="ArgumentException">Throws when both flags for default value is true</exception>
    public static bool GetYesNoInput(string message, bool isYesDefault = false, bool isNoDefault = false)
    {
        if (isYesDefault && isNoDefault)
            throw new ArgumentException("Can only have one option as default");
        
        string answerMessage = "Please answer yes or no";
        string yesNoText = isYesDefault ? "Y/n" : (isNoDefault ? "y/N" : "y/n");
        string? readString;
        while (true)
        {
            Console.Write($"{message} {yesNoText}: ");
            readString = Console.ReadLine();
            if (readString == null)
            {
                Console.WriteLine(answerMessage);
                continue;
            }

            readString = readString.Trim().ToLower();
            switch (readString)
            {
                case "y":
                case "yes":
                    return true;
                case "n":
                case "no":
                    return false;
                default:
                    if (isYesDefault) return true;
                    if (isNoDefault)  return false;

                    Console.WriteLine(answerMessage);
                    continue;
            }
        }
    }

    /// <summary>
    /// Clears the console in a given region. Sets the cursor position to <c>x</c> and <c>y</c> when finished clearing.
    /// </summary>
    /// <param name="x">Start position X</param>
    /// <param name="y">Start position Y</param>
    /// <param name="width">Region width</param>
    /// <param name="height">Region height</param>
    public static void ClearScreen(int x, int y, int width, int height)
    {
        // TODO: Check that it is inside the console bounds

        string lineText = new string(' ', width);
        for (int i = 0; i < height; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(lineText);
        }

        Console.SetCursorPosition(x, y);
    }

    /// <summary>
    /// Clears the whole line on a given y. Sets the cursor position to the start of line <c>y</c> when finished clearing.
    /// </summary>
    /// <param name="y"></param>
    public static void ClearLine(int y)
    {
        // TODO: Check that the y is inside the bounds. 

        Console.SetCursorPosition(0, y);
        string lineText = new(' ', Console.BufferWidth);
        Console.WriteLine(lineText);

        Console.SetCursorPosition(0, y);
    }
}