using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day03
{
    public static class SlopeMapHelper
    {
        public static SlopeMap ParseSlopeMap(IList<string> inputLines)
        {
            var height = inputLines.Count;
            var width = inputLines.Count > 0 ? inputLines[0].Length : 0;
            var gridCellTypes = new Dictionary<GridPoint, CellType>();

            int y = 0;
            foreach (var inputLine in inputLines)
            {
                int x = 0;
                foreach (var pointDefinition in inputLine)
                {
                    CellType type = CellType.Empty;
                    if (pointDefinition == '.')
                    {
                        type = CellType.Empty;
                    }
                    else if (pointDefinition == '#')
                    {
                        type = CellType.Tree;
                    }
                    else
                    {
                        throw new Exception($"Unrecognized slope character: {pointDefinition}");
                    }

                    GridPoint point = new GridPoint(x, y);
                    gridCellTypes.Add(point, type);

                    x++;
                }
                y++;
            }

            var result = new SlopeMap(width, height, gridCellTypes);
            return result;
        }

        public static int GetNumberOfTreesForSlope(
            GridPoint startPoint, 
            int slopeX, 
            int slopeY, 
            SlopeMap slopeMap)
        {
            GridPoint currentPosition = startPoint;
            int numberOfTrees = 0;
            while (currentPosition.Y < slopeMap.Height)
            {
                currentPosition = slopeMap.NormalizeGridPointX(currentPosition);
                var type = slopeMap.GridCellTypes[currentPosition];
                if (CellType.Tree.Equals(type))
                {
                    numberOfTrees++;
                }
                currentPosition = currentPosition.MoveRight(slopeX).MoveUp(slopeY);
            }
            return numberOfTrees;
        }

        public static IList<int> GetNumberOfTreesForSlopes(
            GridPoint startPoint,
            IList<Tuple<int, int>> slopes,
            SlopeMap slopeMap)
        {
            var result = new List<int>();
            foreach (var slope in slopes)
            {
                int numberOfTreesForSlope = GetNumberOfTreesForSlope(startPoint, slope.Item1, slope.Item2, slopeMap);
                result.Add(numberOfTreesForSlope);
            }
            return result;
        }

        public static int GetProductOfSlopeResults(IList<int> slopeResults)
        {
            int result = 1;
            foreach (var slopeResult in slopeResults)
            {
                result *= slopeResult;
            }
            return result;
        }
    }
}
