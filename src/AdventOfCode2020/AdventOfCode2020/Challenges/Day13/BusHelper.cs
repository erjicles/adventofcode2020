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
        public static BigInteger GetPart2Answer(IList<int> wee)
        {
            BigInteger lcm = -1;
            BigInteger time = -1;
            int index = 0;
            while (true)
            {
                BigInteger id = wee[index];
                if (id == -1)
                {
                    index++;
                    continue;
                }

                if (lcm == -1)
                {
                    lcm = id;
                    time = id - index;
                    index++;
                    continue;
                }

                if ((time + index) % id == 0)
                {
                    index++;
                    if (index >= wee.Count())
                    {
                        break;
                    }

                    lcm *= id;
                    continue;
                }

                time += lcm;
            }

            return time;
        }

        public static Tuple<int, int> GetBusId(Tuple<int, IList<int>> wee)
        {
            var foo = new List<Tuple<int, int>>();
            foreach (var busId in wee.Item2)
            {
                if (busId == -1)
                    continue;
                var rem = wee.Item1 % busId;
                var waitTime = 0;
                if (rem > 0)
                {
                    waitTime = busId - rem;
                }
                foo.Add(new Tuple<int, int>(busId, waitTime));
            }
            var result = foo.OrderBy(p => p.Item2).First();
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
