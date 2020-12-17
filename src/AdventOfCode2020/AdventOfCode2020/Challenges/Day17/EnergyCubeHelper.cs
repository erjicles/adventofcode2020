﻿using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day17
{
    public static class EnergyCubeHelper
    {
        public static HashSet<GridPoint3D> RunSimulation(
            IList<GridPoint3D> initialActiveCubes, 
            int numberOfCycles)
        {
            var currentState = initialActiveCubes.ToHashSet();
            for (int cycleNumber = 1; cycleNumber <= numberOfCycles; cycleNumber++)
            {
                var nextState = GetNextState(currentState);
                currentState = nextState;
            }
            return currentState;
        }

        public static HashSet<GridPoint3D> GetNextState(HashSet<GridPoint3D> initialState)
        {
            var result = new HashSet<GridPoint3D>();

            // Collect all the points to consider:
            // All currently active points, plus all points adjacent to those
            var pointsToConsider = new HashSet<GridPoint3D>();
            foreach (var point in initialState)
            {
                if (!pointsToConsider.Contains(point))
                {
                    pointsToConsider.Add(point);
                }
                var adjacentPoints = GridHelper3D.GetAdjacentPoints(point);
                foreach (var adjacentPoint in adjacentPoints)
                {
                    if (!pointsToConsider.Contains(adjacentPoint))
                    {
                        pointsToConsider.Add(adjacentPoint);
                    }
                }
            }

            // Process each point, add it to the next state if it should be active
            foreach (var point in pointsToConsider)
            {
                var isCurrentlyActive = initialState.Contains(point);

                // Count # of adjacent active points
                int numberOfActiveAdjacentPoints = GridHelper3D.GetAdjacentPoints(point)
                    .Where(a => initialState.Contains(a))
                    .Count();

                var isNextCubeStateActive = GetIsNextCubeStateActive(isCurrentlyActive, numberOfActiveAdjacentPoints);
                if (isNextCubeStateActive)
                {
                    result.Add(point);
                }

            }

            return result;
        }

        public static bool GetIsNextCubeStateActive(bool isCurrentlyActive, int numberOfActiveAdjacentCubes)
        {
            // If a cube is active and exactly 2 or 3 of its neighbors are 
            // also active, the cube remains active.Otherwise, the cube 
            // becomes inactive.
            // If a cube is inactive but exactly 3 of its neighbors are 
            // active, the cube becomes active. Otherwise, the cube remains
            // inactive.
            if (isCurrentlyActive &&
                (numberOfActiveAdjacentCubes == 2
                || numberOfActiveAdjacentCubes == 3))
            {
                return true;
            }
            else if (!isCurrentlyActive && numberOfActiveAdjacentCubes == 3)
            {
                return true;
            }
            return false;
        }

        public static IList<GridPoint3D> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<GridPoint3D>();
            for (int row = 0; row < inputLines.Count; row++)
            {
                var inputLine = inputLines[row];
                for (int column = 0; column < inputLine.Length; column++)
                {
                    if ("#".Equals(inputLine[column].ToString()))
                    {
                        var point = new GridPoint3D(column, row, 0);
                        result.Add(point);
                    }
                }
            }
            return result;
        }
    }
}
