using FelixLibrary;
using System.Text.Json;

using Snake_game;
using System.Text;

internal static class Program
{
    const string LEADERBOARD = "leaderboard.json";
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var mainMenu = new MenuSelection();
        List<PlayerScore>? leaderboard;

        mainMenu.Add("Play");
        mainMenu.Add("Leaderboard");
        mainMenu.Add("Quit");


        if (File.Exists(LEADERBOARD))
        {
            string jsonText = File.ReadAllText(LEADERBOARD);
            leaderboard = JsonSerializer.Deserialize<List<PlayerScore>>(jsonText);
            leaderboard.Sort((person1, person2) => person1.Score.CompareTo(person2.Score));
            leaderboard.Reverse();
            while (leaderboard.Count > 10)
            {
                leaderboard.Remove(leaderboard.Last());
            }
        }
        else
        {
            leaderboard = new List<PlayerScore>();
        }
        
        while (true)
        {
            var selection = mainMenu.RunMenuSelection();

            if (selection == 0)
            {
                var score = Game.Run();

                if (leaderboard.Count >= 10 && score <= leaderboard.Last().Score)
                    continue;

                Console.Write("New HighScore! Enter your player name: ");
                var playerScore = new PlayerScore(score, Console.ReadLine());

                leaderboard.Add(playerScore);
                
                string contents = JsonSerializer.Serialize(leaderboard);
                leaderboard.Sort((person1, person2) => person1.Score.CompareTo(person2.Score));
                leaderboard.Reverse();
                if (leaderboard.Count > 10)
                    leaderboard.Remove(leaderboard.Last());

                File.WriteAllText(LEADERBOARD, contents);
                Leaderboard(leaderboard);

            }

            if (selection == 1)
            {
                Leaderboard(leaderboard);
            }

            if (selection == 2)
                //Quit
                break;
        }
    }

    private static void Leaderboard(List<PlayerScore> leaderboard)
    {
        Console.Clear();
        for (int i = 0; i < leaderboard.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {leaderboard[i].PlayerName}: {leaderboard[i].Score}");
        }

        Console.ReadKey(true);
    }
}