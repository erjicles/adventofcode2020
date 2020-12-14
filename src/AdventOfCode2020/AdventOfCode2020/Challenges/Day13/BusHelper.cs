using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day13
{
    public static class BusHelper
    {
        public static BigInteger GetEarliestValidTimestamp(IList<int> busIds)
        {
            BigInteger result = -1;
            BigInteger lcm = -1;
            
            int busIdIndex = 0;
            while (true)
            {
                int currentBusId = busIds[busIdIndex];
                if (currentBusId == -1)
                {
                    busIdIndex++;
                    continue;
                }

                // Handle the first bus
                if (lcm == -1)
                {
                    lcm = currentBusId;
                    result = currentBusId - busIdIndex;
                    busIdIndex++;
                    continue;
                }

                // Check if the timestamp works for this bus
                if ((result + busIdIndex) % currentBusId == 0)
                {
                    busIdIndex++;
                    if (busIdIndex >= busIds.Count)
                    {
                        break;
                    }

                    lcm *= currentBusId;
                    continue;
                }

                result += lcm;
            }

            return result;
        }

        public static Tuple<int, int> GetBusIdWithLowestWaitTime(Tuple<int, IList<int>> busData)
        {
            var waitTimesByBusId = new List<Tuple<int, int>>();
            foreach (var busId in busData.Item2)
            {
                if (busId == -1)
                    continue;
                var timeOffset = busData.Item1 % busId;
                var waitTime = 0;
                if (timeOffset > 0)
                {
                    waitTime = busId - timeOffset;
                }
                waitTimesByBusId.Add(new Tuple<int, int>(busId, waitTime));
            }
            var result = waitTimesByBusId.OrderBy(p => p.Item2).First();
            return result;
        }

        public static Tuple<int, IList<int>> ParseInputLines(string line0, string line1)
        {
            var earliestDepartTime = int.Parse(line0);
            var busIds = new List<int>();
            foreach (var busIdString in line1.Split(","))
            {
                var busId = -1;
                if (!"x".Equals(busIdString))
                {
                    busId = int.Parse(busIdString);
                }
                busIds.Add(busId);
            }
            var result = new Tuple<int, IList<int>>(earliestDepartTime, busIds);
            return result;
        }
    }
}
