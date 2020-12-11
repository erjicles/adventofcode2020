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
        public static WaitingArea EvolveWaitingAreaUntilSteadyState(WaitingArea initialWaitingArea)
        {
            var previousState = initialWaitingArea;
            var nextState = GetNextWaitingAreaState(previousState);
            while (!previousState.Equals(nextState))
            {
                previousState = nextState;
                nextState = GetNextWaitingAreaState(previousState);
            }
            return previousState;
        }
        
        public static WaitingArea GetNextWaitingAreaState(WaitingArea waitingArea)
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
                    int numberOfAdjacentOccupiedSeats = GetNumberOfAdjacentOccupiedSeats(waitingArea, point);
                    if (CellType.ChairOpen.Equals(cellType) && numberOfAdjacentOccupiedSeats == 0)
                    {
                        cellType = CellType.ChairOccupied;
                    }
                    else if (CellType.ChairOccupied.Equals(cellType) && numberOfAdjacentOccupiedSeats >= 4)
                    {
                        cellType = CellType.ChairOpen;
                    }
                    updatedGridCellTypes.Add(point, cellType);
                }
            }
            var result = new WaitingArea(waitingArea.Width, waitingArea.Width, updatedGridCellTypes);
            return result;
        }

        public static int GetNumberOfAdjacentOccupiedSeats(WaitingArea waitingArea, GridPoint point)
        {
            // Get the number of adjacent occupied seats
            int result = 0;
            var adjacentPoints = new List<GridPoint>()
                    {
                        new GridPoint(point.X - 1, point.Y - 1),
                        new GridPoint(point.X, point.Y - 1),
                        new GridPoint(point.X + 1, point.Y - 1),
                        new GridPoint(point.X - 1, point.Y),
                        new GridPoint(point.X + 1, point.Y),
                        new GridPoint(point.X - 1, point.Y + 1),
                        new GridPoint(point.X, point.Y + 1),
                        new GridPoint(point.X + 1, point.Y + 1)
                    };
            foreach (var adjacentPoint in adjacentPoints)
            {
                if (waitingArea.GridCellTypes.ContainsKey(adjacentPoint)
                    && CellType.ChairOccupied.Equals(waitingArea.GridCellTypes[adjacentPoint]))
                {
                    result++;
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
