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

    //private static readonly ConsoleColor[] _colors = new ConsoleColor[]
    //{

    //    ConsoleColor.Green,
    //    ConsoleColor.DarkGreen
    //};

    private static readonly ConsoleColor[] _colors = new ConsoleColor[]
    {
        ConsoleColor.Blue,
        ConsoleColor.Cyan,
        ConsoleColor.Magenta,
        ConsoleColor.Red,
        ConsoleColor.Yellow,
        ConsoleColor.Green,
    };

    //private static ConsoleColor[] _colors = new ConsoleColor[]
    //{
    //    ConsoleColor.Yellow,
    //    ConsoleColor.Yellow,
    //    ConsoleColor.Blue,
    //    ConsoleColor.Blue,

    //};

    public static ConsoleColor GetColor()
    {
        _colorIndex++;
        if (_colorIndex >= _colors.Length)
        {
            _colorIndex = 0;
        }

        return _colors[(int)_colorIndex];
    }

    public static void SetIndex(uint index)
    {
        if (_colors.Length == 0)
        {
            _colorIndex = 0;
        }
        else
        {
            _colorIndex = index % (uint)_colors.Length;
            Debug.WriteLine(_colorIndex);
        }
    }



}
