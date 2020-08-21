using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Model
{
    public class Rover
    {

        public Rover()
        {
            StartPosition = new RoverPosition();
            PositionHistory = new List<RoverPosition>();
        }
        public RoverPosition StartPosition { get; set; }
        public RoverPosition LastPosition { get; set; }
        public List<RoverPosition> PositionHistory { get; set; }
        public string InstructionCommand { get; set; }

    }
}
