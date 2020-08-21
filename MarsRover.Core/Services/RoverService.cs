using MarsRover.Core.Abstraction;
using MarsRover.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MarsRover.Core.Services
{
    public class RoverService : IRoverService
    {
        private readonly ILogger<RoverService> _logger;
        public RoverService(ILogger<RoverService> logger)
        {
            _logger = logger;
        }

        public Rover CreateRover(string positionCommand, string instructionCommand)
        {
            try
            {
                var positionList = positionCommand.Split(" ");

                ValidateRoverPosition(positionList);

                var orientation = positionList[2];
                if (!int.TryParse(positionList[0], out int x) || !int.TryParse(positionList[1], out int y))
                {
                    _logger.LogError("Rover position is not valid.");
                    throw new ArgumentException();
                }

                var position = new RoverPosition
                {
                    X = x,
                    Y = y,
                    OrientationValue = orientation
                };
                return new Rover
                {
                    InstructionCommand = instructionCommand,
                    StartPosition = position,
                    LastPosition = position
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when create Rover: ");
                throw ex;
            }

        }

        public void ValidateRoverPosition(string[] positionList)
        {
            if (positionList.Count() != 3)
            {
                throw new ArgumentException("Position counts must be 3.");
            }

            var orientation = positionList[2];

            if (orientation != "N" && orientation != "E" && orientation != "S" && orientation != "W")
            {
                throw new ArgumentException("Invalid Orientation");
            }
        }
    }
}
