using MarsRover.Core.Abstraction;
using MarsRover.Core.Helper;
using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Services
{
    public class CalculateService : ICalculateService
    {
        public void RunInstructions(Rover rover, Plateau plateau)
        {
            foreach (var instruction in rover.InstructionCommand)
            {
                if (instruction == 'M')
                {
                    Move(rover);
                    continue;
                }

                Turn(rover, CommonHelper.ConvertDirectionToEnum(instruction.ToString()));

                if(!IsRoverPositionInPlateau(rover, plateau))
                {
                    throw new Exception("Rover out of the plateau");
                }
            }
        }

        private bool IsRoverPositionInPlateau(Rover rover, Plateau plateau)
        {
            if (rover.LastPosition.X > plateau.MaxXCoordinate || rover.LastPosition.X < 0)
                return false;

            if (rover.LastPosition.Y > plateau.MaxYCoordinate || rover.LastPosition.Y < 0)
                return false;

            return true;
        }

        private void Move(Rover rover)
        {
            RoverPosition newPosition = new RoverPosition
            {
                X = rover.LastPosition.X,
                Y = rover.LastPosition.Y,
                OrientationValue = rover.LastPosition.OrientationValue
            };

            if (rover.LastPosition.Orientation == Orientation.N)
            {
                newPosition.Y++;
            }

            if (rover.LastPosition.Orientation == Orientation.E)
            {
                newPosition.X++;
            }

            if (rover.LastPosition.Orientation == Orientation.S)
            {
                newPosition.Y--;
            }

            if (rover.LastPosition.Orientation == Orientation.W)
            {
                newPosition.X--;
            }

            rover.LastPosition = newPosition;
            rover.PositionHistory.Add(newPosition);
        }

        private void Turn(Rover rover, Direction direction)
        {
            var newOrientationInteger = ((int)rover.LastPosition.Orientation + (int)direction);

            newOrientationInteger = newOrientationInteger == 5 ? 1 : newOrientationInteger;
            newOrientationInteger = newOrientationInteger == 0 ? 4 : newOrientationInteger;

            RoverPosition newPosition = new RoverPosition
            {
                X = rover.LastPosition.X,
                Y = rover.LastPosition.Y,
                OrientationValue = CommonHelper.ConvertEnumOrientation((Orientation)newOrientationInteger)
            };

            rover.LastPosition = newPosition;
            rover.PositionHistory.Add(newPosition);
        }
    }
}
