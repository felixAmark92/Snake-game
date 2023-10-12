using System.Diagnostics;

namespace Snake_game;

public static class Game
{
    private const int DELAY = 100;
    private static bool GameIsRunning { get; set; }
    private static List<Snake> PlayerList { get; } = new List<Snake>();
    public static async Task<long> Run()
    {
        GameIsRunning = true;
        PlayerList.Clear();
        var gameTime = new GameTime(0, 3, DELAY);


        var controlScheme = new ControlScheme(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
        var controlScheme2 = new ControlScheme(ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D);
        var controlScheme3 = new ControlScheme(ConsoleKey.I, ConsoleKey.K, ConsoleKey.J, ConsoleKey.L);

        //Instantiate
        var consoleDrawer = new ConsoleDrawer(GetBox(50, 25));

        Console.Clear();
        consoleDrawer.DrawBox();
        Console.CursorVisible = false;

        PlayerList.Add(
            new Snake(
                consoleDrawer.GetRandomUnoccupiedPoint(),
                ConsoleColor.DarkMagenta,
                controlScheme,
                "Player 1"));

        PlayerList.Add(
            new Snake(
                consoleDrawer.GetRandomUnoccupiedPoint(),
                ConsoleColor.Green,
                controlScheme2,
                "Player 2"));

        PlayerList.Add(
            new Snake(
                consoleDrawer.GetRandomUnoccupiedPoint(),
                ConsoleColor.Yellow,
                controlScheme3,
                "Player 3"));


        var timerPoint = new Point(consoleDrawer.Box.GetLength(1) / 2, consoleDrawer.Box.GetLength(0) + 1);
        var leaderBoardPoint = new Point(consoleDrawer.Box.GetLength(1) + 3, 0);
        for (int i = 0; i < 20; i++)
        {
            SetRandomApple(consoleDrawer);
        }

        Task.Run(SetKey);

        //Game loop
        while (true)
        {

            PlayerList.ForEach(p => p.UpdatePosition(consoleDrawer));
            PlayerList.ForEach(p => p.CheckCollision(consoleDrawer));

            foreach (var player in PlayerList.Where(player => player.IsDead))
            {
                player.OnDeath(consoleDrawer, consoleDrawer.GetRandomUnoccupiedPoint());
            }

            if (PlayerList.Any(p => p.GotApple))
            {
                SetRandomApple(consoleDrawer);
            }

            var playerLeaderboard = PlayerList.OrderBy(p => p.Score).Reverse().ToArray();

            consoleDrawer.UpdateScreenAt(timerPoint, gameTime.ToString());

            consoleDrawer.UpdateScreenAt(leaderBoardPoint, "Leaderboard");
            for (int i = 0; i < playerLeaderboard.Length; i++)
            {
                consoleDrawer.UpdateScreenAt(
                    new Point(leaderBoardPoint.X, i + 1), 
                    playerLeaderboard[i].Name + ": " + playerLeaderboard[i].Score + "    ");
            }

            await Task.Delay(DELAY);
            gameTime.CountDown();

            if (gameTime.CountFinished())
            {
                GameIsRunning = false;
                DisplayGameEndingScreen();
                return 1;
            }
        }
        
    }

    static async Task SetKey()
    {
        while (GameIsRunning)
        {
            var key = Console.ReadKey(true).Key;

            foreach (var player in PlayerList.Where(player => player.KeyQueue.Count < 2 && player.ValidKey(key)))
            {
                player.KeyQueue.Enqueue(key);
            }
        }
    }

    static void DisplayGameEndingScreen()
    {
        Console.Clear();
        var playerLeaderboard = PlayerList.OrderBy(p => p.Score).Reverse().ToArray();

        for (int i = 0; i < playerLeaderboard.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {playerLeaderboard[i].Name}: {playerLeaderboard[i].Score}");
        }

        while (true)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                break;
            }
        }
    }

    private static void SetRandomApple(ConsoleDrawer consoleDrawer)
    {
        var apple = consoleDrawer.GetRandomUnoccupiedPoint();
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