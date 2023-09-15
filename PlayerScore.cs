using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;

internal record PlayerScore
{
    public long Score { get; set; }
    public string PlayerName { get; set; } = string.Empty;

    public PlayerScore(long score, string playerName)
    {
        Score = score;
        PlayerName = playerName;
    }

}
