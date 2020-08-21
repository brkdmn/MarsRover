﻿using MarsRover.Core.Abstraction;
using MarsRover.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IPlateauService, PlateauService>()
            .AddSingleton<ICalculateService, CalculateService>()
            .AddSingleton<IRoverService, RoverService>()
            .AddSingleton<IOperationService, OperationService>()
            .AddLogging(l => l.AddConsole())
            .BuildServiceProvider();

            serviceProvider.GetService<IOperationService>().CreateConsoleScreen();

            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit...");

            Exit();
        }

        public static void Exit()
        {
            var c = Console.ReadKey();
            if(c.Key == ConsoleKey.Enter)
            {
                return;
            }
            Exit();
        }
    }
}
