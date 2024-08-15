using System.Drawing;
using System.Runtime.CompilerServices;

namespace BudgetBuddy.TUI;

// TODO: Look into a more efficiant way of storing the colors for the buffer elements

// NOTE: If resize is needed, it will be easier to just create a new object with the new size
// NOTE: null for colors means "use console default color"

public struct BufferColor
{
    public ConsoleColor? Foreground { get; set; }
    public ConsoleColor? Background { get; set; }
}

public struct ColoredText(string text, BufferColor bufferColor)
{
    public string Text { get; private set; } = text;
    public BufferColor BufferColor { get; private set; } = bufferColor;
}

public class ApplicationBuffer
{
    readonly char[][] buffer;
    readonly BufferColor[][] bufferColors;

    public ApplicationBuffer(int width, int height)
    {
        buffer = new char[height][];
        bufferColors = new BufferColor[height][];

        for (int y = 0; y < height; y++)
        {
            buffer[y] = new char[width];
            bufferColors[y] = new BufferColor[width];

            for (int x = 0; x < width; x++)
                buffer[y][x] = ' ';
        }
    }

    public void DrawBuffer()
    {
        // NOTE: Doing the naive approach first
        ConsoleColor bg = Console.BackgroundColor;
        ConsoleColor fg = Console.ForegroundColor;

        BufferColor currentColor = new() { Foreground = null, Background = null };
        int startPosY = -1;
        int startPosX = -1;
        List<char> chars = new();

        Console.Clear();
        for (int i = 0; i < buffer.Length; i++)
        {
            for (int j = 0; j < buffer[i].Length; j++)
            {
                // TODO: Make a comparison method to make this if shorter
                if (currentColor.Background != bufferColors[i][j].Background || currentColor.Foreground != bufferColors[i][j].Foreground)
                {
                    Console.ForegroundColor = currentColor.Foreground ?? fg;
                    Console.BackgroundColor = currentColor.Background ?? bg;
                    Console.SetCursorPosition(startPosX, startPosY);
                    Console.Write( chars.ToArray() );

                    currentColor.Foreground = bufferColors[i][j].Foreground;
                    currentColor.Background = bufferColors[i][j].Background;

                    startPosY = -1;
                    startPosX = -1;
                    chars.Clear();
                }

                if (startPosX == -1 || startPosY == -1)
                {
                    startPosY = i;
                    startPosX = j;
                }
                
                chars.Add(buffer[i][j]);
           }
        }

        // Checking if there is any characters left that needs printing
        if (chars.Count > 0)
        {
            Console.ForegroundColor = currentColor.Foreground ?? fg;
            Console.BackgroundColor = currentColor.Background ?? bg;
            Console.SetCursorPosition(startPosX, startPosY);
            Console.Write( chars.ToArray() );
        }

        Console.ResetColor();
    }

    public void Insert(int x, int y, string text, BufferColor? bufferColor = null)
    {
        // TODO: Check that the coordinates are inside the buffer. And that the inserted text doesn't go outside of the bounds.
        char[] chars = text.ToCharArray();
        chars.CopyTo(buffer[y], x);

        if (bufferColor is not null)
        {
            for (int i = 0; i < chars.Length; i++)
                bufferColors[y][x + i] = (BufferColor)bufferColor;
        }
    }

    public void Insert(int x, int y, List<ColoredText> coloredTexts)
    {
        if (coloredTexts.Count == 0)
            throw new ArgumentException("List of ColoredText cannot be empty", nameof(coloredTexts));

        int currentX = x;
        int currentY = y;

        foreach (var coloredText in coloredTexts)
        {
            if (x + coloredText.Text.Length > buffer[0].Length)
            {
                // TODO: Unsure what we should do when the text to insert is out of bounds. Will we wrap? Will we shorten? ...
                throw new NotImplementedException();
            }

            char[] chars = coloredText.Text.ToCharArray();
            chars.CopyTo(buffer[currentY], currentX);

            for (int i = 0; i < chars.Length; i++)
                bufferColors[currentY][currentX + i] = coloredText.BufferColor;
            
            // Update the current x
            currentX += chars.Length;
        }
    }

    public void Clear(Rectangle rectangle)
    {
        // TODO: Check that rectangle is inside of bounds
        string lineFiller = new string(' ', rectangle.Width);
        for (int i = 0; i < rectangle.Height; i++)
        {
            Insert(rectangle.Left, rectangle.Top + i, lineFiller);

            // TODO: Find a better way to clear the buffer colors.
            for (int j = 0; j< rectangle.Width; j++)
            {
                if (bufferColors[rectangle.Top + i][rectangle.Left + j].Background != null)
                    bufferColors[rectangle.Top + i][rectangle.Left + j].Background = null;
                if (bufferColors[rectangle.Top + i][rectangle.Left + j].Foreground != null)
                    bufferColors[rectangle.Top + i][rectangle.Left + j].Foreground = null;
            }
        }
    }

    public char[] BufferLine(int y)
    {
        // TODO: Check that y is inside the bounds of the buffer
        return buffer[y];
    }
}