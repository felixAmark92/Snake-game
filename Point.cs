using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_game;

//Point class because stupid Point in system.drawing is a struct and is causing problem
internal class Point
{
    public int X {  get; set; }
    public int Y { get; set; }


    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point Copy()
    {
        return new Point(X, Y);
    }
}
