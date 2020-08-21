using MarsRover.Core.Abstraction;
using MarsRover.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Core.Services
{
    public class OperationService : IOperationService
    {
        private readonly ICalculateService _calculateService;
        private readonly IRoverService _roverService;
        private readonly IPlateauService _plateauService;
        private readonly ILogger<OperationService> _logger;

        public OperationService
        (
            ICalculateService calculateService,
            IRoverService roverService,
            IPlateauService plateauService,
            ILogger<OperationService> logger
        )
        {
            _calculateService = calculateService;
            _roverService = roverService;
            _plateauService = plateauService;
            _logger = logger;
        }


        public void CreateConsoleScreen()
        {
            Console.WriteLine("Select Operation:");
            Console.WriteLine("(1) Run Example Instructions");
            Console.WriteLine("(2) Enter Instructions Manually");
            Console.Write("-> ");

            var selectedOperation = Console.ReadLine();

            if (string.IsNullOrEmpty(selectedOperation))
            {
                _logger.LogError("Selected Operation cannot be null.");
                return;
            }

            var calculatedRovers = selectedOperation switch
            {
                "1" => ExampleInstructions(),
                "2" => ManuallyInstructions(),
                _ => null,
            };

            if(calculatedRovers == null || !calculatedRovers.Any())
            {
                _logger.LogError("Calculated Roves null or empty");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Output:");
            Console.WriteLine();
            foreach (var rover in calculatedRovers)
            {
                Console.WriteLine($"{rover.LastPosition.X} {rover.LastPosition.Y} {rover.LastPosition.Orientation}");
            };
        }

        private List<Rover> ManuallyInstructions()
        {
            Plateau plateau;
            Rover rover;
            List<Rover> roverList = new List<Rover>();

            Console.WriteLine();
            Console.Write("Plateau Upper-Right Coordinat: ");
            plateau = _plateauService.CreatePlateau(Console.ReadLine());

            Console.Write("Rover Position: ");
            var roverPosition = Console.ReadLine();
            Console.Write("Instructions: ");
            var instructions = Console.ReadLine();

            rover =_roverService.CreateRover(roverPosition, instructions);
            _calculateService.RunInstructions(rover, plateau);
            

            roverList.Add(rover);


            var continueLoop = true;

            while (continueLoop)
            {
                Console.WriteLine();
                Console.WriteLine("Would you like to enter a new rover?");
                Console.Write("('Y' or 'N') -> ");

                var response = Console.ReadLine();
                if (response == "Y")
                {
                    Console.Write("Rover Position: ");
                    roverPosition = Console.ReadLine();
                    Console.Write("Instructions: ");
                    instructions = Console.ReadLine();

                    rover = _roverService.CreateRover(roverPosition, instructions);
                    _calculateService.RunInstructions(rover, plateau);

                    roverList.Add(rover);
                }
                else if (response == "N")
                {
                    return roverList;
                }
                else
                {

                    continue;
                }

            }

            return roverList;
        }

        private List<Rover> ExampleInstructions()
        {

            Console.WriteLine("Test Input:");
            Console.WriteLine();
            Console.WriteLine("5 5");
            Console.WriteLine("1 2 N");
            Console.WriteLine("LMLMLMLMM");
            Console.WriteLine("3 3 E");
            Console.WriteLine("MMRMMRMRRM");
            
            List<Rover> roverList = new List<Rover>();

            var plateau = _plateauService.CreatePlateau("5 5");
            var rover = _roverService.CreateRover("1 2 N", "LMLMLMLMM");
            _calculateService.RunInstructions(rover, plateau);
            
            roverList.Add(rover);

            rover = _roverService.CreateRover("3 3 E", "MMRMMRMRRM");
            _calculateService.RunInstructions(rover, plateau);

            roverList.Add(rover);

            return roverList;
        }
    }
}
