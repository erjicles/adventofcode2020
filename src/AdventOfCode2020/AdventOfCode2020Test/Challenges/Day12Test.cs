using AdventOfCode2020.Challenges.Day12;
using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day12Test
    {
        [Fact]
        public void GetFerryPathWithWaypointTest()
        {
            
            var testData = new List<Tuple<IList<string>, GridPoint, int>>()
            {
                new Tuple<IList<string>, GridPoint, int>(
                    new List<string>()
                    {
                        "F10",
                        "N3",
                        "F7",
                        "R90",
                        "F11"
                    }, new GridPoint(10, 1), 
                    286)
            };

            foreach (var testExample in testData)
            {
                var instructions = FerryHelper.ParseInputLines(testExample.Item1);
                var initialState = new FerryState(GridPoint.Origin, testExample.Item2);
                var path = FerryHelper.GetFerryPathWithWaypoint(instructions, initialState);
                var actual = GridPoint.GetManhattanDistance(initialState.Position, path.Last().Position);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay12Part01AnswerTest()
        {
            int expected = 582;
            int actual = Day12.GetDay12Part01Answer();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDay12Part02AnswerTest()
        {
            int expected = 52069;
            int actual = Day12.GetDay12Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
