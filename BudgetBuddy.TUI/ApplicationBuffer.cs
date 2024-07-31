using System.Drawing;

namespace BudgetBuddyTUI;

public class ApplicationBuffer
{
    char[][] buffer;

    public ApplicationBuffer(int width, int height)
    {
        buffer = new char[height][];
        for (int y = 0; y < height; y++)
        {
            buffer[y] = new char[width];
            for (int x = 0; x < width; x++)
            {
                buffer[y][x] = ' ';
            }
        }
    }

    public void Insert(int x, int y, string text)
    {
        // TODO: Check that the coordinates are inside the buffer. And that the inserted text doesn't go outside of the bounds.
        char[] chars = text.ToCharArray();
        chars.CopyTo(buffer[y], x);
    }

    public void Clear(Rectangle rectangle)
    {
        // TODO: Check that rectangle is inside of bounds
        string lineFiller = new string(' ', rectangle.Width);
        for (int i = 0; i < rectangle.Height; i++)
        {
            Insert(rectangle.Left, rectangle.Top + i, lineFiller);
        }
    }

    public char[] BufferLine(int y)
    {
        // TODO: Check that y is inside the bounds of the buffer
        return buffer[y];
    }
}