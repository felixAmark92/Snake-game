using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Snake_game;

public class Snake
{
    public bool IsDead { get; set; }
    public bool GotApple { get; set; }
    public Queue<ConsoleKey> KeyQueue { get; } = new Queue<ConsoleKey>();

    private LinkedList<Point> Body { get;  } = new LinkedList<Point>();
    private Direction Direction { get; set; }
    private ConsoleColor Color { get; }
    private ControlScheme ControlScheme { get;}
    private Point StartPoint { get; set; }


    public Snake(Point startingPoint, ConsoleColor color, ControlScheme controlScheme)
    {
        StartPoint = startingPoint;
        Body.AddFirst(startingPoint);
        Direction = Direction.Up;
        Color = color;
        ControlScheme = controlScheme;
    }



    public void UpdatePosition(ConsoleDrawer consoleDrawer)
    {
        Body.AddFirst(Body.First().Copy());

        if (!GotApple)
        {
           
            consoleDrawer.UpdateScreenAt(Body.Last(), ' ');
            Body.RemoveLast();
        }
        else
        {
            GotApple = false;
        }


        if (KeyQueue.Count > 0)
        {
            SetDirection(KeyQueue.Dequeue());
        }
        

        SetPosition();

        if (consoleDrawer.PointContains(Body.First(), '#', '@'))
        {
            bool toggle = true;
            while (Body.Count != 1)
            {


                if (toggle)
                {
                    consoleDrawer.UpdateScreenAt(Body.Last(), ' ');
                }
                else
                {
                    consoleDrawer.UpdateScreenAt(Body.Last(), 'O', ConsoleColor.Red);
                }
                Body.RemoveLast();
                toggle = !toggle;

            }

            Body.Clear();

            Body.AddFirst(StartPoint);

        }
        else
        {
            if (consoleDrawer.PointContains(Body.First(), 'O'))
            {
                GotApple = true;
            }

            consoleDrawer.UpdateScreenAt(Body.First(), '@', Color);

            
        }

    }


    public bool ValidKey(ConsoleKey key)
    {
        return (
            key == ControlScheme.LeftKey  || 
            key == ControlScheme.RightKey || 
            key == ControlScheme.UpKey || 
            key == ControlScheme.DownKey);
    }

    private void SetDirection( ConsoleKey key)
    {

        if (key == ControlScheme.UpKey)
        {
            Direction = Direction == Direction.Down ? Direction.Down : Direction.Up;
        }
        else if (key == ControlScheme.DownKey)
        {
            Direction = Direction == Direction.Up ? Direction.Up : Direction.Down;
        }
        else if (key == ControlScheme.LeftKey)
        {
            Direction = Direction == Direction.Right ? Direction.Right : Direction.Left;
        }
        else if (key == ControlScheme.RightKey)
        {
            Direction = Direction == Direction.Left ? Direction.Left : Direction.Right;
        }

    }

    private void SetPosition()
    {
        switch (Direction)
        {
            case Direction.Left:
                Body.First().X--;
                break;
            case Direction.Right:
                Body.First().X++;
                break;
            case Direction.Up:
                Body.First().Y--;
                break;
            case Direction.Down:
                Body.First().Y++;
                break;
        }
    }

}