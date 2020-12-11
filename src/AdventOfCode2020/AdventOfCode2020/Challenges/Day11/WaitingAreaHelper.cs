using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day11
{
    public static class WaitingAreaHelper
    {
        public static WaitingArea EvolveWaitingAreaUntilSteadyState(
            WaitingArea initialWaitingArea,
            EvolutionRules evolutionRules)
        {
            WaitingArea previousState = null;
            var nextState = initialWaitingArea;
            while (!nextState.Equals(previousState))
            {
                previousState = nextState;
                nextState = GetNextWaitingAreaState(previousState, evolutionRules);
            }
            return previousState;
        }
        
        public static WaitingArea GetNextWaitingAreaState(
            WaitingArea waitingArea,
            EvolutionRules evolutionRules)
        {
            // For each cell:
            // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
            // If a seat is occupied(#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
            // Otherwise, the seat's state does not change.
            // Floor(.) never changes; seats don't move, and nobody sits on the floor.
            var updatedGridCellTypes = new Dictionary<GridPoint, CellType>();
            for (int row = 0; row < waitingArea.Height; row++)
            {
                for (int column = 0; column < waitingArea.Width; column++)
                {
                    var point = new GridPoint(column, row);
                    var cellType = CellType.Floor;
                    if (waitingArea.GridCellTypes.ContainsKey(point))
                    {
                        cellType = waitingArea.GridCellTypes[point];
                    }
                    if (CellType.Floor.Equals(cellType))
                    {
                        updatedGridCellTypes.Add(point, cellType);
                        continue;
                    }
                    int numberOfAdjacentOccupiedSeats = GetNumberOfSeenOccupiedSeats(waitingArea, point, evolutionRules.OnlyConsiderAdjacentCells);
                    if (CellType.ChairOpen.Equals(cellType) && numberOfAdjacentOccupiedSeats == 0)
                    {
                        cellType = CellType.ChairOccupied;
                    }
                    else if (CellType.ChairOccupied.Equals(cellType) && numberOfAdjacentOccupiedSeats >= evolutionRules.MinimumNumberOfSeenOccupiedSeatsToFlipToEmpty)
                    {
                        cellType = CellType.ChairOpen;
                    }
                    updatedGridCellTypes.Add(point, cellType);
                }
            }
            var result = new WaitingArea(waitingArea.Width, waitingArea.Width, updatedGridCellTypes);
            return result;
        }

        public static int GetNumberOfSeenOccupiedSeats(
            WaitingArea waitingArea, 
            GridPoint point,
            bool onlyConsiderAdjacentCells)
        {
            int result = 0;
            // Find seen seats along 8 directional rays
            var raySlopes = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(-1, 0),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 1)
            };

            foreach (var raySlope in raySlopes)
            {
                var nextPoint = new GridPoint(point.X + raySlope.Item1, point.Y + raySlope.Item2);
                while (waitingArea.GridCellTypes.ContainsKey(nextPoint))
                {
                    if (CellType.ChairOccupied.Equals(waitingArea.GridCellTypes[nextPoint]))
                    {
                        result++;
                        break;
                    }
                    else if (CellType.ChairOpen.Equals(waitingArea.GridCellTypes[nextPoint]))
                    {
                        break;
                    }
                    if (onlyConsiderAdjacentCells)
                        break;
                    nextPoint = new GridPoint(nextPoint.X + raySlope.Item1, nextPoint.Y + raySlope.Item2);
                }
            }
            return result;
        }

        public static WaitingArea ParseInputLines(IList<string> inputLines)
        {
            int width = inputLines.Count > 0 ? inputLines[0].Length : 0;
            int height = inputLines.Count;
            var gridCellTypes = new Dictionary<GridPoint, CellType>();
            for (int row = 0; row < inputLines.Count; row++)
            {
                var inputLine = inputLines[row];
                for (int column = 0; column < inputLine.Length; column++)
                {
                    var cellType = CellType.Floor;
                    var cellDefinition = inputLine[column].ToString();
                    if ("L".Equals(cellDefinition))
                    {
                        cellType = CellType.ChairOpen;
                    }
                    else if ("#".Equals(cellDefinition))
                    {
                        cellType = CellType.ChairOccupied;
                    }
                    var point = new GridPoint(column, row);
                    gridCellTypes.Add(point, cellType);
                }
            }
            var result = new WaitingArea(width, height, gridCellTypes);
            return result;
        }
    }
}
