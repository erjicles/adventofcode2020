using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public static BigInteger GetNumberOfPossibleAdapterConfigurations(IList<int> joltages)
        {
            var sortedJoltages = joltages.ToList();
            sortedJoltages.Sort();
            Dictionary<int, BigInteger> pathCountToNode = new Dictionary<int, BigInteger>();
            pathCountToNode.Add(0, 1);
            for (int i = 0; i < joltages.Count; i++)
            {
                var currentJoltage = sortedJoltages[i];
                pathCountToNode.Add(currentJoltage, 0);
                var isZeroReachable = currentJoltage <= 3;
                var prev1 = i > 0 ? sortedJoltages[i - 1] : -1;
                var prev2 = i > 1 ? sortedJoltages[i - 2] : -1;
                var prev3 = i > 2 ? sortedJoltages[i - 3] : -1;
                if (isZeroReachable)
                {
                    pathCountToNode[currentJoltage] += pathCountToNode[0];
                }
                if (currentJoltage - prev1 <= 3 && prev1 != -1)
                {
                    pathCountToNode[currentJoltage] += pathCountToNode[prev1];
                }
                if (currentJoltage - prev2 <= 3 && prev2 != -1)
                {
                    pathCountToNode[currentJoltage] += pathCountToNode[prev2];
                }
                if (currentJoltage - prev3 <= 3 && prev3 != -1)
                {
                    pathCountToNode[currentJoltage] += pathCountToNode[prev3];
                }
            }
            var result = pathCountToNode[sortedJoltages[joltages.Count - 1]];
            return result;
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
