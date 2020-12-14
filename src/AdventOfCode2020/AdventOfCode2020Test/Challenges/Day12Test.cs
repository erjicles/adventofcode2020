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
        public void GetFerryPathTest()
        {
            // For example:
            //
            // F10
            // N3
            // F7
            // R90
            // F11
            // These instructions would be handled as follows:
            // 
            // F10 would move the ship 10 units east(because the ship starts 
            // by facing east) to east 10, north 0.
            // N3 would move the ship 3 units north to east 10, north 3.
            // F7 would move the ship another 7 units east(because the ship is 
            // still facing east) to east 17, north 3.
            // R90 would cause the ship to turn right by 90 degrees and face 
            // south; it remains at east 17, north 3.
            // F11 would move the ship 11 units south to east 17, south 8.
            // At the end of these instructions, the ship's Manhattan distance 
            // (sum of the absolute values of its east/west position and its 
            // north/south position) from its starting position is 17 + 8 = 25.
            var testData = new List<Tuple<IList<string>, FerryMovementDirection, int>>()
            {
                new Tuple<IList<string>, FerryMovementDirection, int>(
                    new List<string>()
                    {
                        "F10",
                        "N3",
                        "F7",
                        "R90",
                        "F11"
                    }, FerryMovementDirection.East, 25)
            };

            foreach (var testExample in testData)
            {
                var instructions = FerryHelper.ParseInputLines(testExample.Item1);
                var initialState = new FerryState(GridPoint.Origin, testExample.Item2);
                var path = FerryHelper.GetFerryPath(instructions, initialState);
                var actual = GridPoint.GetManhattanDistance(initialState.Position, path.Last().Position);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetFerryPathWithWaypointTest()
        {
            // For example, using the same instructions as above:
            // F10 moves the ship to the waypoint 10 times(a total of 100 
            // units east and 10 units north), leaving the ship at east 100, 
            // north 10.The waypoint stays 10 units east and 1 unit north of 
            // the ship.
            // N3 moves the waypoint 3 units north to 10 units east and 4 
            // units north of the ship.The ship remains at east 100, north 10.
            // F7 moves the ship to the waypoint 7 times(a total of 70 units 
            // east and 28 units north), leaving the ship at east 170, north 
            // 38.The waypoint stays 10 units east and 4 units north of the ship.
            // R90 rotates the waypoint around the ship clockwise 90 degrees, 
            // moving it to 4 units east and 10 units south of the ship. The 
            // ship remains at east 170, north 38.
            // F11 moves the ship to the waypoint 11 times(a total of 44 units 
            // east and 110 units south), leaving the ship at east 214, south 
            // 72.The waypoint stays 4 units east and 10 units south of the ship.
            // After these operations, the ship's Manhattan distance from its 
            // starting position is 214 + 72 = 286.
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
