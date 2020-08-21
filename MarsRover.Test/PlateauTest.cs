using MarsRover.Core.Abstraction;
using MarsRover.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace MarsRover.Test
{
    [TestFixture]
    public class PlateauTest
    {
        private ServiceProvider serviceProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            serviceProvider = new ServiceCollection()
            .AddSingleton<IPlateauService, PlateauService>()
            .AddLogging(l => l.AddConsole())
            .BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void CreatePalteau_ValidCoordinates_ReturnPalteau()
        {
            var _plateauService = serviceProvider.GetService<IPlateauService>();
            var plateau = _plateauService.CreatePlateau("5 5");

            Assert.IsNotNull(plateau);
        }

        [Test]
        public void CreatePalteau_IsInput55_ReturnPalteauMaxCoordinates55()
        {
            var _plateauService = serviceProvider.GetService<IPlateauService>();
            var plateau = _plateauService.CreatePlateau("5 6");

            Assert.AreEqual("5 6", $"{plateau.MaxXCoordinate} {plateau.MaxYCoordinate}");
        }

        [Test]
        public void CreatePalteau_InvalidCoordinates_ReturnArgumentException()
        {
            var _plateauService = serviceProvider.GetService<IPlateauService>();
            Assert.Throws<ArgumentException>(() => _plateauService.CreatePlateau("A B"));
        }

        [Test]
        public void CreatePalteau_TooMuchCoordinates_ReturnArgumentException()
        {
            var _plateauService = serviceProvider.GetService<IPlateauService>();
            var ex  = Assert.Throws<ArgumentException>(() => _plateauService.CreatePlateau("5 6 8"));
            
            Assert.That(ex.Message, Is.EqualTo("Coordinate counts must be 2."));
        }
    }
}
