using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;

public class ConsoleDrawer
{
    public char[,] Box { get; private set; }

    public ConsoleDrawer(char[,] box) 
    {
        Box = box;
    }
    public void UpdateScreenAt(Point point, char c)
    {
        Console.CursorLeft = point.X;
        Console.CursorTop = point.Y;
        Console.Write(c);
        Box[point.Y, point.X] = c;
    }
    public void UpdateScreenAt(Point point, char c, ConsoleColor color)
    {
        Console.CursorLeft = point.X;
        Console.CursorTop = point.Y;
        Console.ForegroundColor = color;
        Console.Write(c);
        Console.ResetColor();
        Box[point.Y, point.X] = c;
    }
    public void UpdateScreenAt(Point point, string value)
    {
        Console.CursorLeft = point.X;
        Console.CursorTop = point.Y;
        Console.Write(value);
    }

    public Point GetRandomUnoccupiedPoint()
    {
        var random = new Random();
        while (true)
        {
            var point = new Point(random.Next(1, Box.GetLength(1) - 1), random.Next(1, Box.GetLength(0) - 1));

            if (PointContains(point, ' '))
            {
                return point;
            }
        }
    }

    public void DrawBox()
    {
        for (int i = 0; i < Box.GetLength(0); i++)
        {
            for (int j = 0; j < Box.GetLength(1); j++)
            {
                Console.Write(Box[i, j]);
            }
            Console.WriteLine();
        }
    }

    public bool PointContains(Point point, char value)
    {
        return (Box[point.Y, point.X] == value);
    }
    public bool PointContains(Point point, char value, char c2)
    {
        return (Box[point.Y, point.X] == value || Box[point.Y, point.X] == c2);
    }
}
