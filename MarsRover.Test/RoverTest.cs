using MarsRover.Core.Abstraction;
using MarsRover.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace MarsRover.Test
{
    [TestFixture]
    public class RoverTest
    {
        private ServiceProvider serviceProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            serviceProvider = new ServiceCollection()
            .AddSingleton<IRoverService, RoverService>()
            .AddLogging(l => l.AddConsole())
            .BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void CreateRover_ValidCommands_ReturnRover()
        {
            var _roverService = serviceProvider.GetService<IRoverService>();
            var rover = _roverService.CreateRover("1 2 N", "LMLMLMLMM");

            Assert.IsNotNull(rover);
        }

        [Test]
        public void CreateRover_InvalidPosition_ReturnArgumentException()
        {
            var _roverService = serviceProvider.GetService<IRoverService>();
            Assert.Throws<ArgumentException>(() => _roverService.CreateRover("A A B", "LMLMLMLMM"));
        }

        [Test]
        public void CreateRover_TooMuchPositionArgs_ReturnArgumentException()
        {
            var _roverService = serviceProvider.GetService<IRoverService>();
            var ex = Assert.Throws<ArgumentException>(() => _roverService.CreateRover("5 6 8 N", "LMLMLM"));

            Assert.That(ex.Message, Is.EqualTo("Position counts must be 3."));
        }

        [Test]
        public void CreateRover_WrongOrientation_ReturnArgumentException()
        {
            var _roverService = serviceProvider.GetService<IRoverService>();
            var ex = Assert.Throws<ArgumentException>(() => _roverService.CreateRover("5 6 P", "LMLMLM"));

            Assert.That(ex.Message, Is.EqualTo("Invalid Orientation"));
        }

        [Test]
        public void CreateRover_IsInput12P_ReturnLastPosition13N()
        {
            var _roverService = serviceProvider.GetService<IRoverService>();
            var rover = _roverService.CreateRover("1 2 N", "LMLMLMLMM");

            Assert.AreEqual("1 2 N", $"{rover.LastPosition.X} {rover.LastPosition.Y} {rover.LastPosition.OrientationValue}");
        }
    }
}
