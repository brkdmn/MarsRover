using MarsRover.Model;

namespace MarsRover.Core.Abstraction
{
    public interface IPlateauService
    {
        Plateau CreatePlateau(string upperRightCoordinate);
    }
}
