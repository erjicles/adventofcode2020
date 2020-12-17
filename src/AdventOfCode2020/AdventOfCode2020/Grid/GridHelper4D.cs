using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Grid
{
    public static class GridHelper4D
    {
        /// <summary>
        /// Get all points adjacent to a given point.
        /// Adjacent is defined as all points where any of their X, Y, Z, or W
        /// coordinates are within 1 of the central point's respective coordinate.
        /// </summary>
        /// <param name="centerPoint"></param>
        /// <returns></returns>
        public static IList<GridPoint4D> GetAdjacentPoints(GridPoint4D centerPoint)
        {
            var result = new List<GridPoint4D>();
            var coordinateDeltas = new List<int[]>()
            {
                new int[4]{ -1, -1, -1, -1 },
                new int[4]{ 0, -1, -1, -1 },
                new int[4]{ 1, -1, -1, -1 },
                new int[4]{ -1, 0, -1, -1 },
                new int[4]{ 0, 0, -1, -1 },
                new int[4]{ 1, 0, -1, -1 },
                new int[4]{ -1, 1, -1, -1 },
                new int[4]{ 0, 1, -1, -1 },
                new int[4]{ 1, 1, -1, -1 },
                new int[4]{ -1, -1, 0, -1 },
                new int[4]{ 0, -1, 0, -1 },
                new int[4]{ 1, -1, 0, -1 },
                new int[4]{ -1, 0, 0, -1 },
                new int[4]{ 0, 0, 0, -1 },
                new int[4]{ 1, 0, 0, -1 },
                new int[4]{ -1, 1, 0, -1 },
                new int[4]{ 0, 1, 0, -1 },
                new int[4]{ 1, 1, 0, -1 },
                new int[4]{ -1, -1, 1, -1 },
                new int[4]{ 0, -1, 1, -1 },
                new int[4]{ 1, -1, 1, -1 },
                new int[4]{ -1, 0, 1, -1 },
                new int[4]{ 0, 0, 1, -1 },
                new int[4]{ 1, 0, 1, -1 },
                new int[4]{ -1, 1, 1, -1 },
                new int[4]{ 0, 1, 1, -1 },
                new int[4]{ 1, 1, 1, -1 },

                new int[4]{ -1, -1, -1, 0 },
                new int[4]{ 0, -1, -1, 0 },
                new int[4]{ 1, -1, -1, 0 },
                new int[4]{ -1, 0, -1, 0 },
                new int[4]{ 0, 0, -1, 0 },
                new int[4]{ 1, 0, -1, 0 },
                new int[4]{ -1, 1, -1, 0 },
                new int[4]{ 0, 1, -1, 0 },
                new int[4]{ 1, 1, -1, 0 },
                new int[4]{ -1, -1, 0, 0 },
                new int[4]{ 0, -1, 0, 0 },
                new int[4]{ 1, -1, 0, 0 },
                new int[4]{ -1, 0, 0, 0 },
                //new int[4]{ 0, 0, 0, 0 },
                new int[4]{ 1, 0, 0, 0 },
                new int[4]{ -1, 1, 0, 0 },
                new int[4]{ 0, 1, 0, 0 },
                new int[4]{ 1, 1, 0, 0 },
                new int[4]{ -1, -1, 1, 0 },
                new int[4]{ 0, -1, 1, 0 },
                new int[4]{ 1, -1, 1, 0 },
                new int[4]{ -1, 0, 1, 0 },
                new int[4]{ 0, 0, 1, 0 },
                new int[4]{ 1, 0, 1, 0 },
                new int[4]{ -1, 1, 1, 0 },
                new int[4]{ 0, 1, 1, 0 },
                new int[4]{ 1, 1, 1, 0 },

                new int[4]{ -1, -1, -1, 1 },
                new int[4]{ 0, -1, -1, 1 },
                new int[4]{ 1, -1, -1, 1 },
                new int[4]{ -1, 0, -1, 1 },
                new int[4]{ 0, 0, -1, 1 },
                new int[4]{ 1, 0, -1, 1 },
                new int[4]{ -1, 1, -1, 1 },
                new int[4]{ 0, 1, -1, 1 },
                new int[4]{ 1, 1, -1, 1 },
                new int[4]{ -1, -1, 0, 1 },
                new int[4]{ 0, -1, 0, 1 },
                new int[4]{ 1, -1, 0, 1 },
                new int[4]{ -1, 0, 0, 1 },
                new int[4]{ 0, 0, 0, 1 },
                new int[4]{ 1, 0, 0, 1 },
                new int[4]{ -1, 1, 0, 1 },
                new int[4]{ 0, 1, 0, 1 },
                new int[4]{ 1, 1, 0, 1 },
                new int[4]{ -1, -1, 1, 1 },
                new int[4]{ 0, -1, 1, 1 },
                new int[4]{ 1, -1, 1, 1 },
                new int[4]{ -1, 0, 1, 1 },
                new int[4]{ 0, 0, 1, 1 },
                new int[4]{ 1, 0, 1, 1 },
                new int[4]{ -1, 1, 1, 1 },
                new int[4]{ 0, 1, 1, 1 },
                new int[4]{ 1, 1, 1, 1 },
            };
            // Commented out for optimization purposes
            // CombinationsHelper.GetAllPossibleOutcomesOfNExperiments(new int[4] { -1, 0, 1 }, 4);
            foreach (var delta in coordinateDeltas)
            {
                if (delta[0] == 0 && delta[1] == 0 && delta[2] == 0 && delta[3] == 0)
                    continue;
                var point = new GridPoint4D(
                    centerPoint.X + delta[0],
                    centerPoint.Y + delta[1],
                    centerPoint.Z + delta[2],
                    centerPoint.W + delta[3]);
                result.Add(point);
            }
            return result;
        }
    }
}
