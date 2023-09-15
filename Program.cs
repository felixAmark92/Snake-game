using FelixLibrary;

using Snake_game;

internal static class Program
{
    private static void Main(string[] args)
    {
        int selection;
        long score;
        var mainMenu = new MenuSelection();
        mainMenu.Add("Play");
        mainMenu.Add("Leaderboard");
        mainMenu.Add("Quit");

        
        while (true)
        {
            selection = mainMenu.RunMenuSelection();

            if (selection == 0)
                Game.Run();

            if (selection == 1)
                continue;

            if (selection == 2)
                break;
        }
    }
}