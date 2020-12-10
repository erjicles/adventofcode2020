using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day10
{
    public static class JoltageAdapterHelper
    {
        public static bool TryFindAdapterPath(IList<int> joltages, out IList<Tuple<int, int>> joltageDifferenceDistribution)
        {
            var joltageDifferences = new List<int>();
            joltageDifferenceDistribution = new List<Tuple<int, int>>();
            int currentJoltage = 0;
            var sortedJoltages = joltages.ToList();
            sortedJoltages.Sort();
            foreach (var nextJoltage in sortedJoltages)
            {
                if (currentJoltage + 3 < nextJoltage)
                {
                    return false;
                }
                joltageDifferences.Add(nextJoltage - currentJoltage);
                currentJoltage = nextJoltage;
            }
            joltageDifferences.Add(3);
            joltageDifferenceDistribution = joltageDifferences
                .GroupBy(j => j)
                .Select(k => new Tuple<int, int>(k.Key, k.Count()))
                .ToList();
            return true;
        }


        public static IList<int> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<int>();
            foreach (var inputLine in inputLines)
            {
                var num = int.Parse(inputLine);
                result.Add(num);
            }
            return result;
        }
    }
}
