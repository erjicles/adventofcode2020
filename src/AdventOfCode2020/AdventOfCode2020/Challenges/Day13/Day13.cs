using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day13
{
    public static class Day13
    {
        public const string FILE_NAME = "Day13Input.txt";

        public static int GetDay13Part01Answer()
        {
            // Answer: 3215
            var foo = GetDay13Input();
            var bar = BusHelper.GetBusId(foo);
            var result = bar.Item1 * bar.Item2;
            return result;
        }

        private static Tuple<int, IList<int>> GetDay13Input()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", FILE_NAME);
            if (!File.Exists(filePath))
            {
                throw new Exception($"Cannot locate file {filePath}");
            }

            var inputLines = File.ReadAllLines(filePath);
            var result = BusHelper.ParseInputLines(inputLines[0], inputLines[1]);
            return result;
        }
    }
}
