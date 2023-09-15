using FelixLibrary;
using System.Text.Json;
using System.Text.Json.Serialization;

using Snake_game;

internal static class Program
{
    const string LEADERBOARD = "leaderboard.json";
    private static void Main(string[] args)
    {

        int selection;
        long score;
        var mainMenu = new MenuSelection();
        SortedList<long, PlayerScore>? leaderboard;

        mainMenu.Add("Play");
        mainMenu.Add("Leaderboard");
        mainMenu.Add("Quit");


        if (File.Exists(LEADERBOARD))
        {
            string jsonText = File.ReadAllText(LEADERBOARD);
            leaderboard = JsonSerializer.Deserialize<SortedList<long, PlayerScore>>(jsonText);
        }
        else
        {
            leaderboard = new SortedList<long, PlayerScore>();
        }

        
        while (true)
        {
            selection = mainMenu.RunMenuSelection();

            if (selection == 0)
            {
                score = Game.Run();

                if (leaderboard.Count < 10 || score > leaderboard.Last().Value.Score)
                {
                    Console.WriteLine("Enter your player name");
                    var playerScore = new PlayerScore(score, Console.ReadLine());

                    leaderboard.Add(score, playerScore);
                    if (leaderboard.Count > 10)
                    {
                        leaderboard.Remove(leaderboard.Last().Key);
                    }
                    string jsonfile = JsonSerializer.Serialize(leaderboard);
                    File.WriteAllText(LEADERBOARD, jsonfile);


                }

                

            }

            if (selection == 1)
                continue;

            if (selection == 2)
                break;
        }
    }
}