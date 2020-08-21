using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Abstraction
{
    public interface IPlateauService
    {
        Plateau CreatePlateau(string upperRightCoordinate);
    }
}
