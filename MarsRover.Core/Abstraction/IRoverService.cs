using MarsRover.Model;

namespace MarsRover.Core.Abstraction
{
    public interface IRoverService
    {
        Rover CreateRover(string positionCommand, string instructionCommand);
    }
}
