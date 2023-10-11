using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;

public class Snake
{
    private LinkedList<Point> Body { get;  } = new LinkedList<Point>();
    private Direction Direction { get; set; }
    private Queue<ConsoleKey> KeyQueue { get; } = new Queue<ConsoleKey>();


    private async Task SetKey()
    {
        while (!GameOver)
        {
            var key = Console.ReadKey().Key;
            if (KeyQueue.Count < 2)
            {
                KeyQueue.Enqueue(key);
            }
        }
    }

    void SetDirection( ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.DownArrow:
                Direction = Direction == Direction.Up ? Direction.Up : Direction.Down;
                break;
            case ConsoleKey.UpArrow:
                Direction = Direction == Direction.Down ? Direction.Down : Direction.Up;
                break;
            case ConsoleKey.LeftArrow:
                Direction = Direction == Direction.Right ? Direction.Right : Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                Direction = Direction == Direction.Left ? Direction.Left : Direction.Right;
                break;
        }
    }

    private void SetPosition(Direction playerDirection, Point playerPosition)
    {
        switch (playerDirection)
        {
            case Direction.Left:
                playerPosition.X--;
                break;
            case Direction.Right:
                playerPosition.X++;
                break;
            case Direction.Up:
                playerPosition.Y--;
                break;
            case Direction.Down:
                playerPosition.Y++;
                break;
        }
    }

}