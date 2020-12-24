using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day24
{
    public static class TileHelper
    {
        public static IList<GridPoint> GetBlackTiles(IList<GridPoint> identifiedTiles)
        {
            var tileDictionary = new Dictionary<GridPoint, int>();
            foreach (var identifiedTile in identifiedTiles)
            {
                if (!tileDictionary.ContainsKey(identifiedTile))
                {
                    tileDictionary.Add(identifiedTile, 0);
                }
                tileDictionary[identifiedTile]++;
            }
            var result = tileDictionary
                .Where(kvp => kvp.Value % 2 == 1)
                .Select(kvp => kvp.Key)
                .ToList();
            return result;
        }

        public static IList<GridPoint> GetIdentifiedTiles(IList<IList<HexMovementDirection>> lines)
        {
            var result = new List<GridPoint>();
            foreach (var line in lines)
            {
                var path = GetPath(GridPoint.Origin, line);
                var endPoint = path.Last();
                result.Add(endPoint);
            }
            return result;
        }

        public static IList<GridPoint> GetPath(
            GridPoint startPoint, 
            IList<HexMovementDirection> directions)
        {
            var result = new List<GridPoint>() { startPoint };
            var currentPoint = startPoint;
            foreach (var direction in directions)
            {
                var nextPoint = MoveHex(currentPoint, direction);
                result.Add(nextPoint);
                currentPoint = nextPoint;
            }
            return result;
        }

        public static GridPoint MoveHex(GridPoint startingPoint, HexMovementDirection direction)
        {
            GridPoint result;
            if (HexMovementDirection.East.Equals(direction))
            {
                result = new GridPoint(startingPoint.X + 1, startingPoint.Y);
            }
            else if (HexMovementDirection.SouthEast.Equals(direction))
            {
                result = new GridPoint(startingPoint.X, startingPoint.Y - 1);
            }
            else if (HexMovementDirection.SouthWest.Equals(direction))
            {
                result = new GridPoint(startingPoint.X - 1, startingPoint.Y - 1);
            }
            else if (HexMovementDirection.West.Equals(direction))
            {
                result = new GridPoint(startingPoint.X - 1, startingPoint.Y);
            }
            else if (HexMovementDirection.NorthWest.Equals(direction))
            {
                result = new GridPoint(startingPoint.X, startingPoint.Y + 1);
            }
            else if (HexMovementDirection.NorthEast.Equals(direction))
            {
                result = new GridPoint(startingPoint.X + 1, startingPoint.Y + 1);
            }
            else
            {
                throw new Exception($"Unknown direction: {direction}");
            }

            return result;
        }

        public static IList<HexMovementDirection> ParseInputLine(string inputLine)
        {
            var result = new List<HexMovementDirection>();
            for (int i = 0; i < inputLine.Length; i++)
            {
                var direction = HexMovementDirection.East;
                var currentCharacter = inputLine[i];
                char? nextCharacter = i == inputLine.Length - 1 ? null : inputLine[i + 1];
                if ('e'.Equals(currentCharacter))
                {
                    direction = HexMovementDirection.East;
                }
                else if ('w'.Equals(currentCharacter))
                {
                    direction = HexMovementDirection.West;
                }
                else if ('s'.Equals(currentCharacter)
                    && 'e'.Equals(nextCharacter))
                {
                    direction = HexMovementDirection.SouthEast;
                    i++;
                }
                else if ('s'.Equals(currentCharacter)
                    && 'w'.Equals(nextCharacter))
                {
                    direction = HexMovementDirection.SouthWest;
                    i++;
                }
                else if ('n'.Equals(currentCharacter)
                    && 'e'.Equals(nextCharacter))
                {
                    direction = HexMovementDirection.NorthEast;
                    i++;
                }
                else if ('n'.Equals(currentCharacter)
                    && 'w'.Equals(nextCharacter))
                {
                    direction = HexMovementDirection.NorthWest;
                    i++;
                }
                else
                {
                    throw new Exception($"Invalid characters: {currentCharacter}, {nextCharacter}");
                }
                result.Add(direction);
            }
            return result;
        }

        public static IList<IList<HexMovementDirection>> ParseInputLines(IList<string> inputLines)
        {
            var result = inputLines.Select(l => ParseInputLine(l)).ToList();
            return result;
        }

    }
}
