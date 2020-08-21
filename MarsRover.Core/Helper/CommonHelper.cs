using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Helper
{
    public static class CommonHelper
    {
        public static Orientation ConvertOrientationToEnum(string orientation)
        {
            switch (orientation)
            {
                case "N": return Orientation.N;
                case "W": return Orientation.W;
                case "S": return Orientation.S;
                case "E": return Orientation.E;
                default: return Orientation.Undefined;
            }
        }

        public static string ConvertEnumOrientation(Orientation orientationValue)
        {
            switch (orientationValue)
            {
                case Orientation.N: return "N";
                case Orientation.W: return "W";
                case Orientation.S: return "S";
                case Orientation.E: return "E";
                default: return string.Empty;
            }
        }

        public static Direction ConvertDirectionToEnum(string direction)
        {
            switch (direction)
            {
                case "L": return Direction.Left;
                case "R": return Direction.Right;
                default: return Direction.Undefined;
            }
        }
    }
}
