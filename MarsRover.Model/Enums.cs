using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Model
{
    public enum Orientation
    {
        Undefined,
        N,
        E,
        S,
        W
    }

    public enum Direction
    {
        Left = -1,
        Undefined = 0,
        Right = 1
    }
}
