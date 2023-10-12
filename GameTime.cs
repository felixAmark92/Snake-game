using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;

public class GameTime
{
    private int Seconds { get; set; }
    private int Minutes { get; set; }
    private int Delay { get; set; } = 1;
    private int Milliseconds { get; set; } = 0;


    public GameTime(int seconds, int minutes, int delay)
    {
        Seconds = seconds;
        Minutes = minutes;
        Delay = delay;
    }


    public void CountDown()
    {
        Milliseconds += 1 * Delay;

        if (Milliseconds >= 1000)
        {
            Seconds--;
            Milliseconds -= 1000;
        }

        if (Seconds < 0)
        {
            Minutes--;
            if (Minutes < 0)
            {
                return;
            }

            Seconds = 59;
        }
    }

    public bool CountFinished()
    {
        return (Seconds <= 0 && Minutes <= 0);
    }

    public override string ToString()
    {
        return $"{Minutes}:{Seconds:00}";
    }
}