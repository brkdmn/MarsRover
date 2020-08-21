using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Model
{
    public class RoverPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string OrientationValue { get; set; }
        public Orientation Orientation => string.IsNullOrEmpty(OrientationValue) ? Orientation.Undefined : ConvertOrientation(OrientationValue);

        private Orientation ConvertOrientation(string orientation)
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
    }
}
