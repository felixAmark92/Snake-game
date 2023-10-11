using Snake_game;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

internal static class Game
{

    private static bool GameOver { get; set; }
    public static async Task<long> Run()
    {
        GameOver = false;

        //Instantiate
        long score = 0;
        var consoleDrawer = new ConsoleDrawer(GetBox(30, 20));
        var playerSnake = new LinkedList<Point>();
        var playerDirection = Direction.Right;
        bool gotApple = true;
        Console.Clear();
        consoleDrawer.DrawBox();
        Console.CursorVisible = false;

        playerSnake.AddFirst(
            new Point(
            consoleDrawer.Box.GetLength(1) / 2,
            consoleDrawer.Box.GetLength(0) / 2));

        Task.Run(SetKey); 
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

            if (KeyQueue.Count > 0)
            {
                SetDirection(ref playerDirection, KeyQueue.Dequeue());
            }
            
            SetPosition(playerDirection, playerSnake.First());

            if (consoleDrawer.PointContains(playerSnake.First(), '#', '@'))
            {
                //Game over
                while (true)
                {
                    GameOver = true;
                    consoleDrawer.UpdateScreenAt(playerSnake.First.Value, "  ");
                    playerSnake.RemoveFirst();
                    if (playerSnake.First == null)
                    {
                        break;
                    }
                    consoleDrawer.UpdateScreenAt(playerSnake.First.Value, "\ud83d\udca5");
                    await Task.Delay(100);
                }

                break;
            }
            if (consoleDrawer.PointContains(playerSnake.First(), 'O'))
            {
                score += 100;
                gotApple = true;
            }

            playerSnake.AddFirst(playerSnake.First().Copy());
            consoleDrawer.UpdateScreenAt(playerSnake.First(), '@', ConsoleColor.Green);

            await Task.Delay(100);
        }

        Console.Clear();
        Console.WriteLine("Game over!");
        Console.ReadLine();

        return score;

        //functions

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
        consoleDrawer.UpdateScreenAt(apple, 'O', ConsoleColor.Red);
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