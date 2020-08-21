using MarsRover.Model;

namespace MarsRover.Core.Abstraction
{
    public interface ICalculateService
    {
        public void RunInstructions(Rover rover, Plateau plateau);
    }
}
