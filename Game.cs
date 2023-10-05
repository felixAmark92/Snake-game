using Snake_game;

internal static class Game
{
    public static long Run()
    {
        //Instantiate
        long score = 0;
        var consoleDrawer = new ConsoleDrawer(GetBox(30, 20));
        var playerSnake = new LinkedList<Point>();
        var playerDirection = Direction.Right;
        bool gotApple = true;
        uint colorIndex = 0;
        Console.Clear();
        consoleDrawer.DrawBox();
        Console.CursorVisible = false;

        playerSnake.AddFirst(
            new Point(
            consoleDrawer.Box.GetLength(1) / 2,
            consoleDrawer.Box.GetLength(0) / 2));

        SetDirection(ref playerDirection, Console.ReadKey(true).Key);
        //Game loop
        while (true)
        {
            if (gotApple)
            {
                SetRandomApple(consoleDrawer);
                gotApple = false;
            }
            else
            {
                consoleDrawer.UpdateScreenAt(playerSnake.Last(), ' ');
                playerSnake.RemoveLast();
            }

            if (Console.KeyAvailable)
            {
                SetDirection(ref playerDirection, Console.ReadKey(true).Key);
            }
            SetPosition(playerDirection, playerSnake.First());

            if (consoleDrawer.PointContains(playerSnake.First(), '#', '@'))
            {
                while (true)
                {
                    consoleDrawer.UpdateScreenAt(playerSnake.First.Value, "  ");
                    playerSnake.RemoveFirst();
                    if (playerSnake.First == null)
                    {
                        break;
                    }
                    consoleDrawer.UpdateScreenAt(playerSnake.First.Value, "\ud83d\udca5");
                    Thread.Sleep(100);
                }
                //Game over
                break;
            }
            if (consoleDrawer.PointContains(playerSnake.First(), 'O'))
            {
                score += 100;
                gotApple = true;
            }
            
            playerSnake.AddFirst(playerSnake.First().Copy());
            consoleDrawer.UpdateScreenAt(playerSnake.First(), '@', ConsoleColor.Green);

            //ColorLoop.SetIndex(0);
            //foreach (Point item in playerSnake)
            //{
            //    consoleDrawer.UpdateScreenAt(item, '@', ColorLoop.GetColor());
            //}
            ;
            Thread.Sleep(100);
            colorIndex -= 2;
        }

        Console.Clear();
        Console.WriteLine("Game over!");
        Console.ReadLine();

        return score;

        //functions

    }


    

    static void SetPosition(Direction playerDirection, Point playerPosition)
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


    static void SetDirection(ref Direction playerDirection, ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.DownArrow:
                playerDirection = playerDirection == Direction.Up ? Direction.Up : Direction.Down;
                break;
            case ConsoleKey.UpArrow:
                playerDirection = playerDirection == Direction.Down ? Direction.Down : Direction.Up;
                break;
            case ConsoleKey.LeftArrow:
                playerDirection = playerDirection == Direction.Right ? Direction.Right : Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                playerDirection = playerDirection == Direction.Left ? Direction.Left : Direction.Right;
                break;
        }
    }


    private static void SetRandomApple(ConsoleDrawer consoleDrawer)
    {
        var rand = new Random();

        var apple = new Point(rand.Next(1, consoleDrawer.Box.GetLength(1) - 1), rand.Next(1, consoleDrawer.Box.GetLength(0) - 1));

        if (consoleDrawer.Box[apple.Y, apple.X] == '@')
        {
            SetRandomApple(consoleDrawer);
            return;
        }

        consoleDrawer.UpdateScreenAt(apple,'O', ConsoleColor.Red);
    }

    static char[,] GetBox(int width, int height)
    {
        char[,] charBox = new char[height, width];

        for (int i = 0; i < height; i++)
        {
            int j = 0;
            charBox[i, j] = '#';
            for (j = 1; j < width - 1; j++)
            {
                if (i == 0 || i == height - 1)
                {
                    charBox[i, j] = '#';
                }
                else
                {
                    charBox[i, j] = ' ';
                }
            }
            charBox[i, j] = '#';
        }
        return charBox;
    }
}