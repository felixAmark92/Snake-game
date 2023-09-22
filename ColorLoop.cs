using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;


internal static class ColorLoop
{
    private static uint _colorIndex = 0;

    private static List<ConsoleColor> _colors = new List<ConsoleColor>()
    {
        ConsoleColor.DarkBlue,
        ConsoleColor.Blue,
        ConsoleColor.DarkCyan,
        ConsoleColor.Cyan,
        ConsoleColor.DarkMagenta,
        ConsoleColor.Magenta,
        ConsoleColor.DarkRed,
        ConsoleColor.Red,
        ConsoleColor.DarkYellow,
        ConsoleColor.Yellow,
        ConsoleColor.DarkGreen,
        ConsoleColor.Green,
    };

    //private static List<ConsoleColor> _colors = new List<ConsoleColor>()
    //{
    //    ConsoleColor.Blue,
    //    ConsoleColor.Blue,
    //    ConsoleColor.White,
    //    ConsoleColor.White,

    //};

    public static ConsoleColor GetColor()
    {
        _colorIndex++;
        if (_colorIndex >= _colors.Count)
        {
            _colorIndex = 0;
        }

        return _colors[(int)_colorIndex];
    }

    public static void SetIndex(uint index)
    {
        if (_colors.Count == 0)
        {
            _colorIndex = 0;
        }
        else
        {
            _colorIndex = index % (uint)_colors.Count;
            Debug.WriteLine(_colorIndex);
        }
    }



}
