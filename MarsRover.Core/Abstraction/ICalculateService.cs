using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Abstraction
{
    public interface ICalculateService
    {
        public void RunInstructions(Rover rover, Plateau plateau);
    }
}
