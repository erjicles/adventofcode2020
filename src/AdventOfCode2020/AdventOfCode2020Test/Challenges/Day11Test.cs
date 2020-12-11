using AdventOfCode2020.Challenges.Day11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day11Test
    {
        [Fact]
        public void EvolveWaitingAreaUntilSteadyStateTest()
        {
            
            var testData = new List<Tuple<IList<string>, int>>()
            {
                new Tuple<IList<string>, int>(
                    new List<string>()
                    {
                        "L.LL.LL.LL",
                        "LLLLLLL.LL",
                        "L.L.L..L..",
                        "LLLL.LL.LL",
                        "L.LL.LL.LL",
                        "L.LLLLL.LL",
                        "..L.L.....",
                        "LLLLLLLLLL",
                        "L.LLLLLL.L",
                        "L.LLLLL.LL"
                    }, 37),
            };

            foreach (var testExample in testData)
            {
                var initialState = WaitingAreaHelper.ParseInputLines(testExample.Item1);
                var finalState = WaitingAreaHelper.EvolveWaitingAreaUntilSteadyState(initialState);
                Assert.Equal(testExample.Item2, finalState.NumberOfOccupiedSeats);
            }
        }

        [Fact]
        public void GetDay11Part01AnswerTest()
        {
            int expected = 2468;
            int actual = Day11.GetDay11Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
