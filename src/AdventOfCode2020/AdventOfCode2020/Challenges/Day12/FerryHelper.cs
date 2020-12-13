using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day12
{
    public static class FerryHelper
    {
        public static IList<FerryState> GetFerryPathWithWaypoint(
            IList<FerryMovementInstruction> ferryMovementInstructions,
            FerryState startState)
        {
            var result = new List<FerryState>();
            result.Add(startState);
            var currentState = startState;
            foreach (var instruction in ferryMovementInstructions)
            {
                currentState = ApplyFerryMovementInstructionWithWaypoint(currentState, instruction);
                result.Add(currentState);
            }
            return result;
        }

        public static IList<FerryState> GetFerryPath(
            IList<FerryMovementInstruction> ferryMovementInstructions, 
            FerryState startState)
        {
            var result = new List<FerryState>();
            result.Add(startState);
            var currentState = startState;
            foreach (var instruction in ferryMovementInstructions)
            {
                currentState = ApplyFerryMovementInstruction(currentState, instruction);
                result.Add(currentState);
            }
            return result;
        }

        public static FerryState ApplyFerryMovementInstructionWithWaypoint(
            FerryState currentState,
            FerryMovementInstruction ferryMovementInstruction)
        {
            var nextPosition = currentState.Position;
            var nextWaypointPosition = currentState.WaypointPosition;

            if (FerryMovementDirection.East.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextWaypointPosition = nextWaypointPosition.MoveRight(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.North.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextWaypointPosition = nextWaypointPosition.MoveUp(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.West.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextWaypointPosition = nextWaypointPosition.MoveLeft(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.South.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextWaypointPosition = nextWaypointPosition.MoveDown(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.Left.Equals(ferryMovementInstruction.MovementDirection))
            {
                // https://stackoverflow.com/questions/786472/rotate-a-point-by-another-point-in-2d
                // If you rotate point (px, py) around point (ox, oy) by angle theta you'll get:
                // p'x = cos(theta) * (px-ox) - sin(theta) * (py-oy) + ox
                // p'y = sin(theta) * (px-ox) + cos(theta) * (py-oy) + oy
                var oX = nextPosition.X;
                var oY = nextPosition.Y;
                var pX = nextWaypointPosition.X;
                var pY = nextWaypointPosition.Y;
                var dX = pX - oX;
                var dY = pY - oY;
                var theta = (Math.PI / 180) * ferryMovementInstruction.Value;
                var sinTheta = Math.Sin(theta);
                var cosTheta = Math.Cos(theta);
                var newPX = (int)Math.Round((dX * cosTheta) - (dY * sinTheta) + oX);
                var newPY = (int)Math.Round((dX * sinTheta) + (dY * cosTheta) + oY);

                nextWaypointPosition = new GridPoint(newPX, newPY);
            }
            else if (FerryMovementDirection.Right.Equals(ferryMovementInstruction.MovementDirection))
            {
                var oX = nextPosition.X;
                var oY = nextPosition.Y;
                var pX = nextWaypointPosition.X;
                var pY = nextWaypointPosition.Y;
                var dX = pX - oX;
                var dY = pY - oY;
                var theta = (Math.PI / 180) * (360 - ferryMovementInstruction.Value);
                var sinTheta = Math.Sin(theta);
                var cosTheta = Math.Cos(theta);
                var newPX = (int)Math.Round((dX * cosTheta) - (dY * sinTheta) + oX);
                var newPY = (int)Math.Round((dX * sinTheta) + (dY * cosTheta) + oY);

                nextWaypointPosition = new GridPoint(newPX, newPY);
            }
            else if (FerryMovementDirection.Forward.Equals(ferryMovementInstruction.MovementDirection))
            {
                var dXSlope = nextWaypointPosition.X - nextPosition.X;
                var dYSlope = nextWaypointPosition.Y - nextPosition.Y;
                var dX = dXSlope * ferryMovementInstruction.Value;
                var dY = dYSlope * ferryMovementInstruction.Value;
                nextPosition = new GridPoint(nextPosition.X + dX, nextPosition.Y + dY);
                nextWaypointPosition = new GridPoint(nextPosition.X + dXSlope, nextPosition.Y + dYSlope);
            }
            else
            {
                throw new Exception($"Unknown direction: {ferryMovementInstruction.MovementDirection}");
            }
            var nextState = new FerryState(nextPosition, nextWaypointPosition);
            return nextState;
        }

        public static FerryState ApplyFerryMovementInstruction(
            FerryState currentState, 
            FerryMovementInstruction ferryMovementInstruction)
        {
            var nextPosition = currentState.Position;
            var nextHeading = currentState.Heading;

            if (FerryMovementDirection.East.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextPosition = nextPosition.MoveRight(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.North.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextPosition = nextPosition.MoveUp(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.West.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextPosition = nextPosition.MoveLeft(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.South.Equals(ferryMovementInstruction.MovementDirection))
            {
                nextPosition = nextPosition.MoveDown(ferryMovementInstruction.Value);
            }
            else if (FerryMovementDirection.Left.Equals(ferryMovementInstruction.MovementDirection))
            {
                var direction = GetMovementDirectionFromHeading(nextHeading);
                var nextDirection = MovementDirectionHelper.Rotate(direction, ferryMovementInstruction.Value, true);
                nextHeading = GetHeadingFromMovementDirection(nextDirection);
            }
            else if (FerryMovementDirection.Right.Equals(ferryMovementInstruction.MovementDirection))
            {
                var direction = GetMovementDirectionFromHeading(nextHeading);
                var nextDirection = MovementDirectionHelper.Rotate(direction, ferryMovementInstruction.Value, false);
                nextHeading = GetHeadingFromMovementDirection(nextDirection);
            }
            else if (FerryMovementDirection.Forward.Equals(ferryMovementInstruction.MovementDirection))
            {
                var direction = GetMovementDirectionFromHeading(nextHeading);
                nextPosition = nextPosition.Move(direction, ferryMovementInstruction.Value);
            }
            else
            {
                throw new Exception($"Unknown direction: {ferryMovementInstruction.MovementDirection}");
            }
            var nextState = new FerryState(nextPosition, nextHeading);
            return nextState;
        }

        public static FerryMovementDirection GetHeadingFromMovementDirection(MovementDirection direction)
        {
            FerryMovementDirection heading;
            if (MovementDirection.Right.Equals(direction))
            {
                heading = FerryMovementDirection.East;
            }
            else if (MovementDirection.Up.Equals(direction))
            {
                heading = FerryMovementDirection.North;
            }
            else if (MovementDirection.Left.Equals(direction))
            {
                heading = FerryMovementDirection.West;
            }
            else if (MovementDirection.Down.Equals(direction))
            {
                heading = FerryMovementDirection.South;
            }
            else
            {
                throw new Exception($"Invalid direction: {direction}");
            }
            return heading;
        }

        public static MovementDirection GetMovementDirectionFromHeading(FerryMovementDirection heading)
        {
            MovementDirection direction;
            if (FerryMovementDirection.East.Equals(heading))
            {
                direction = MovementDirection.Right;
            }
            else if (FerryMovementDirection.North.Equals(heading))
            {
                direction = MovementDirection.Up;
            }
            else if (FerryMovementDirection.West.Equals(heading))
            {
                direction = MovementDirection.Left;
            }
            else if (FerryMovementDirection.South.Equals(heading))
            {
                direction = MovementDirection.Down;
            }
            else
            {
                throw new Exception($"Invalid heading: {heading}");
            }
            return direction;
        }

        public static IList<FerryMovementInstruction> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<FerryMovementInstruction>();
            foreach (var inputLine in inputLines)
            {
                var ferryMovementInstruction = ParseInputLine(inputLine);
                result.Add(ferryMovementInstruction);
            }
            return result;
        }

        public static FerryMovementInstruction ParseInputLine(string inputLine)
        {
            var match = Regex.Match(inputLine, @"^(E|N|W|S|L|R|F)(\d+)$");
            if (!match.Success)
            {
                throw new Exception($"Unrecognized input line pattern: {inputLine}");
            }
            var rawDirection = match.Groups[1].Value;
            var ferryMovementDirection = FerryMovementDirection.East;
            if ("E".Equals(rawDirection))
            {
                ferryMovementDirection = FerryMovementDirection.East;
            }
            else if ("N".Equals(rawDirection))
            {
                ferryMovementDirection = FerryMovementDirection.North;
            }
            else if ("W".Equals(rawDirection))
            {
                ferryMovementDirection = FerryMovementDirection.West;
            }
            else if ("S".Equals(rawDirection))
            {
                ferryMovementDirection = FerryMovementDirection.South;
            }
            else if ("L".Equals(rawDirection))
            {
                ferryMovementDirection = FerryMovementDirection.Left;
            }
            else if ("R".Equals(rawDirection))
            {
                ferryMovementDirection = FerryMovementDirection.Right;
            }
            else if ("F".Equals(rawDirection))
            {
                ferryMovementDirection = FerryMovementDirection.Forward;
            }
            else
            {
                throw new Exception($"Unrecognized raw direction: {rawDirection}");
            }
            var value = int.Parse(match.Groups[2].Value);
            var result = new FerryMovementInstruction(ferryMovementDirection, value);
            return result;
        }
    }
}
