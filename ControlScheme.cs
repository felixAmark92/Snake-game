using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;

public class ControlScheme
{
    public ConsoleKey UpKey { get; set; }
    public ConsoleKey DownKey { get; set;}
    public ConsoleKey LeftKey { get; set; }
    public ConsoleKey RightKey { get; set;}


    public ControlScheme(ConsoleKey upKey, ConsoleKey downKey, ConsoleKey leftKey, ConsoleKey rightKey)
    {
        UpKey = upKey;
        DownKey = downKey;
        LeftKey = leftKey;
        RightKey = rightKey;
    }
}