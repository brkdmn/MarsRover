using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Abstraction
{
    public interface IRoverService
    {
        Rover CreateRover(string positionCommand, string instructionCommand);
    }
}
