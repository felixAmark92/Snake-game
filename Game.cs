using Snake_game;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

public static class Game
{
    private static bool GameIsRunning { get; set; }
    public static async Task<long> Run()
    {
        GameIsRunning = true;


        var controlScheme2 = new ControlScheme(ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D);

        var controlScheme = new ControlScheme(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);

        //Instantiate
        long score = 0;
        var consoleDrawer = new ConsoleDrawer(GetBox(50, 25));
        
        Console.Clear();
        consoleDrawer.DrawBox();
        Console.CursorVisible = false;

        var playerSnake = new Snake(
            new Point(
                consoleDrawer.Box.GetLength(1) / 2,
                consoleDrawer.Box.GetLength(0) / 2),
            ConsoleColor.DarkMagenta,
            controlScheme);

        var playerSnake2 = new Snake(
            new Point(
                consoleDrawer.Box.GetLength(1) / 4,
                consoleDrawer.Box.GetLength(0) / 2),
            ConsoleColor.Green,
            controlScheme2);


        for (int i = 0; i < 10; i++)
        {
            SetRandomApple(consoleDrawer);
        }


        Task.Run(SetKey);

        Task.Yield();
        //Game loop
        while (true)
        {
            if (!playerSnake2.IsDead)
            {
                playerSnake2.UpdatePosition(consoleDrawer);
            }

            if (!playerSnake.IsDead)
            {
                playerSnake.UpdatePosition(consoleDrawer);
            }
            

            if (playerSnake.GotApple || playerSnake2.GotApple)
            {
                SetRandomApple(consoleDrawer);
            }

            if (playerSnake.IsDead && playerSnake2.IsDead)
            {
                GameIsRunning = false;
                Console.Clear();
                Console.WriteLine("Game over!");
                Console.ReadKey(true);
                return score;

            }

            await Task.Delay(100);
        }

        async Task SetKey()
        {
            while (GameIsRunning)
            {
                var key = Console.ReadKey(true).Key;


                if (playerSnake2.KeyQueue.Count < 2 && playerSnake2.ValidKey(key))
                {
                    playerSnake2.KeyQueue.Enqueue(key);
                }
                if (playerSnake.KeyQueue.Count < 2 && playerSnake.ValidKey(key))
                {
                    playerSnake.KeyQueue.Enqueue(key);
                }
            }
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