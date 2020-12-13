using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day12
{
    public static class Day12
    {
        public const string FILE_NAME = "Day12Input.txt";
        public static int GetDay12Part01Answer()
        {
            // Answer: 582
            var instructions = GetDay12Input();
            var initialState = new FerryState();
            var ferryPath = FerryHelper.GetFerryPath(instructions, initialState);
            var result = GridPoint.GetManhattanDistance(initialState.Position, ferryPath.Last().Position);
            return result;
        }

        private static IList<FerryMovementInstruction> GetDay12Input()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", FILE_NAME);
            if (!File.Exists(filePath))
            {
                throw new Exception($"Cannot locate file {filePath}");
            }

            var inputLines = File.ReadAllLines(filePath);
            var result = FerryHelper.ParseInputLines(inputLines);
            return result;
        }
    }
}
