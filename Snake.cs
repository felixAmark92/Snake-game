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

    public long Score
    {
        get
        {
            return Body.Count() * 100;
        }
    }
    public string Name { get; private set; }
    public bool IsDead { get; set; }
    public bool GotApple { get; set; }
    public bool GotSnakeApple { get; set; }
    public Queue<ConsoleKey> KeyQueue { get; } = new Queue<ConsoleKey>();
    public LinkedList<Point> Body { get;  } = new LinkedList<Point>();
    private Direction Direction { get; set; } = Direction.Idle;
    private ConsoleColor Color { get; }
    private ControlScheme ControlScheme { get;}
    private Point StartPoint { get; set; }


    public Snake(Point startingPoint, ConsoleColor color, ControlScheme controlScheme, string name)
    {
        StartPoint = startingPoint;
        Body.AddFirst(startingPoint);
        Color = color;
        ControlScheme = controlScheme;
        Name = name;
    }



    public void UpdatePosition(ConsoleDrawer consoleDrawer)
    {
        if (KeyQueue.Count > 0)
        {
            SetDirection(KeyQueue.Dequeue());
        }

        if (Direction == Direction.Idle)
        {
            return;
        }


        Body.AddFirst(Body.First().Copy());

        if (GotApple || GotSnakeApple)
        {
            GotApple = false;
            GotSnakeApple = false;
        }
        else
        {
            consoleDrawer.UpdateScreenAt(Body.Last(), ' ');
            Body.RemoveLast();
        }

        SetPosition();

    }

    public void CheckCollision(ConsoleDrawer consoleDrawer)
    {
        if (Direction == Direction.Idle)
        {
            consoleDrawer.UpdateScreenAt(Body.First(), '@', Color);
            return;
        }

        if (consoleDrawer.PointContains(Body.First(), '#', '@'))
        {
           IsDead = true;
        }
        else
        {
            if (consoleDrawer.PointContains(Body.First(), 'O'))
            {
                GotApple = true;
            }
            else if (consoleDrawer.PointContains(Body.First(), 'o'))
            {
                GotSnakeApple = true;
            }

            consoleDrawer.UpdateScreenAt(Body.First(), '@', Color);


        }

    }

    public void OnDeath(ConsoleDrawer consoleDrawer, Point spawnPoint)
    {
        if (Direction == Direction.Idle)
        {
            return;
        }
        bool toggle = true;
        while (Body.Count != 1)
        {


            if (toggle)
            {
                consoleDrawer.UpdateScreenAt(Body.Last(), ' ');
            }
            else
            {
                consoleDrawer.UpdateScreenAt(Body.Last(), 'o', ConsoleColor.Red);
            }
            Body.RemoveLast();
            toggle = !toggle;

        }

        Body.Clear();
        Body.AddFirst(spawnPoint);
        Direction = Direction.Idle;

        IsDead = false;

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