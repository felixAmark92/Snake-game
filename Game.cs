using System.Diagnostics;

namespace Snake_game;

public static class Game
{
    private static bool GameIsRunning { get; set; }
    private static List<Snake> PlayerList { get; } = new List<Snake>();
    public static async Task<long> Run()
    {
        GameIsRunning = true;

        PlayerList.Clear();

        var gameTime = new GameTime(0, 5, 100);


        var controlScheme2 = new ControlScheme(ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D);
        var controlScheme = new ControlScheme(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);

        //Instantiate
        var consoleDrawer = new ConsoleDrawer(GetBox(50, 25));

        Console.Clear();
        consoleDrawer.DrawBox();
        Console.CursorVisible = false;

        PlayerList.Add(
            new Snake(
                new Point(
                    consoleDrawer.Box.GetLength(1) / 2,
                    consoleDrawer.Box.GetLength(0) / 2),
                ConsoleColor.DarkMagenta,
                controlScheme,
                "Player 1"));

        PlayerList.Add(
            new Snake(
                new Point(
                    consoleDrawer.Box.GetLength(1) / 4,
                    consoleDrawer.Box.GetLength(0) / 2),
                ConsoleColor.Green,
                controlScheme2,
                "Player 2"));


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


            //if (playerSnake2.Body.First().X == playerSnake.Body.First().X &&
            //    playerSnake2.Body.First().Y == playerSnake.Body.First().Y)
            //{
            //    playerSnake2.OnDeath(consoleDrawer, consoleDrawer.GetRandomUnoccupiedPoint());
            //    playerSnake.OnDeath(consoleDrawer, consoleDrawer.GetRandomUnoccupiedPoint());
            //}

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
                consoleDrawer.UpdateScreenAt(new Point(leaderBoardPoint.X, i + 1), playerLeaderboard[i].Name + ": " + playerLeaderboard[i].Score + "    ");
            }

            await Task.Delay(100);
            gameTime.CountDown();

            if (gameTime.CountFinished())
            {
                GameIsRunning = false;
                return 1;
            }
        }

        async Task SetKey()
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