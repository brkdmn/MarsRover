using MarsRover.Core.Abstraction;
using MarsRover.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace MarsRover.Test
{
    public class CalculateTest
    {
        private ServiceProvider serviceProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            serviceProvider = new ServiceCollection()
            .AddSingleton<IPlateauService, PlateauService>()
            .AddSingleton<IRoverService, RoverService>()
            .AddSingleton<ICalculateService, CalculateService>()
            .AddLogging(l => l.AddConsole())
            .BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void RunIstruction_RoverAndPlateau_MoveRover()
        {
            var _plateauService = serviceProvider.GetService<IPlateauService>();
            var _roverService = serviceProvider.GetService<IRoverService>();
            var _calculateService = serviceProvider.GetService<ICalculateService>();

            var plateau = _plateauService.CreatePlateau("5 5");
            var rover = _roverService.CreateRover("1 2 N", "LMLMLMLMM");
            _calculateService.RunInstructions(rover, plateau);

            Assert.AreEqual("1 3 N",$"{rover.LastPosition.X} {rover.LastPosition.Y} {rover.LastPosition.Orientation}");
        }

        [Test]
        public void RunIstruction_RoverMoveOutOfPlateau_ThrowException()
        {
            var _plateauService = serviceProvider.GetService<IPlateauService>();
            var _roverService = serviceProvider.GetService<IRoverService>();
            var _calculateService = serviceProvider.GetService<ICalculateService>();

            var plateau = _plateauService.CreatePlateau("2 3");
            var rover = _roverService.CreateRover("1 2 N", "MMMMMMMM");

            var ex = Assert.Throws<Exception>(() => _calculateService.RunInstructions(rover, plateau));

            Assert.That(ex.Message, Is.EqualTo("Rover out of the plateau."));
        }
    }
}
