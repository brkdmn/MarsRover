using MarsRover.Core.Abstraction;
using MarsRover.Model;
using Microsoft.Extensions.Logging;
using System;

namespace MarsRover.Core.Services
{
    public class PlateauService : IPlateauService
    {
        private readonly ILogger<PlateauService> _logger;
        public PlateauService(ILogger<PlateauService> logger)
        {
            _logger = logger;
        }

        public Plateau CreatePlateau(string upperRightCoordinate)
        {
            try
            {
                var coordinates = upperRightCoordinate.Split(" ");

                if (!int.TryParse(coordinates[0], out int x) || !int.TryParse(coordinates[1], out int y))
                {
                    _logger.LogError("Plateau coordinates are not valid.");
                    throw new ArgumentException();
                }

                return new Plateau
                {
                    MaxXCoordinate = x,
                    MaxYCoordinate = y
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when plateau Rover: ");
                throw ex;
            }

        }
    }
}
