using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day13
{
    public static class BusHelper
    {
        public static Tuple<int, int> GetBusId(Tuple<int, IList<int>> wee)
        {
            var foo = new List<Tuple<int, int>>();
            foreach (var busId in wee.Item2)
            {
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
                if ("x".Equals(busIdString))
                {
                    continue;
                }
                var busId = int.Parse(busIdString);
                busIds.Add(busId);
            }
            var result = new Tuple<int, IList<int>>(earliestDepartTime, busIds);
            return result;
        }
    }
}
