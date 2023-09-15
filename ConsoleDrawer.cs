using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;

internal class ConsoleDrawer
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

    public bool PointContains(Point point, char c)
    {
        return (Box[point.Y, point.X] == c);
    }
    public bool PointContains(Point point, char c, char c2)
    {
        return (Box[point.Y, point.X] == c || Box[point.Y, point.X] == c2);
    }
}
