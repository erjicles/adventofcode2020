using AdventOfCode2020.Challenges.Day17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day17Test
    {
        [Fact]
        public void RunSimulationTest()
        {
            // For example, consider the following initial state:
            // 
            // .#.
            // ..#
            // ###
            // 
            // Even though the pocket dimension is 3 - dimensional, this 
            // initial state represents a small 2 - dimensional slice of it. 
            // (In particular, this initial state defines a 3x3x1 region of 
            // the 3 - dimensional space.)
            // Simulating a few cycles from this initial state produces the 
            // following configurations, where the result of each cycle is 
            // shown layer - by - layer at each given z coordinate(and the 
            // frame of view follows the active cells in each cycle):
            // 
            // Before any cycles:
            // 
            // z = 0
            // .#.
            // ..#
            // ###
            // 
            // 
            // After 1 cycle:
            // 
            // z = -1
            // #..
            // ..#
            // .#.
            // 
            // z = 0
            // #.#
            // .##
            // .#.
            // 
            // z = 1
            // #..
            // ..#
            // .#.
            // 
            // 
            // After 2 cycles:
            // 
            // z = -2
            // .....
            // .....
            // ..#..
            // .....
            // .....
            // 
            // z = -1
            // ..#..
            // .#..#
            // ....#
            // .#...
            // .....
            // 
            // z = 0
            // ##...
            // ##...
            // #....
            // ....#
            // .###.
            // 
            // z = 1
            // ..#..
            // .#..#
            // ....#
            // .#...
            // .....
            // 
            // z = 2
            // .....
            // .....
            // ..#..
            // .....
            // .....
            // 
            // 
            // After 3 cycles:
            // 
            // z = -2
            // .......
            // .......
            // ..##...
            // ..###..
            // .......
            // .......
            // .......
            // 
            // z = -1
            // ..#....
            // ...#...
            // #......
            // .....##
            // .#...#.
            // ..#.#..
            // ...#...
            // 
            // z = 0
            // ...#...
            // .......
            // #......
            // .......
            // .....##
            // .##.#..
            // ...#...
            // 
            // z = 1
            // ..#....
            // ...#...
            // #......
            // .....##
            // .#...#.
            // ..#.#..
            // ...#...
            // 
            // z = 2
            // .......
            // .......
            // ..##...
            // ..###..
            // .......
            // .......
            // .......
            //
            // After the full six-cycle boot process completes, 112 cubes are 
            // left in the active state.
            var testData = new List<Tuple<IList<string>, int, int>>()
            {
                new Tuple<IList<string>, int, int>(
                    new List<string>()
                    {
                        ".#.",
                        "..#",
                        "###"
                    }, 6, 112)
            };

            foreach (var testExample in testData)
            {
                var initialActiveCubes = EnergyCubeHelper.ParseInputLines(testExample.Item1);
                var finalState = EnergyCubeHelper.RunSimulation(initialActiveCubes, testExample.Item2);
                var actual = finalState.Count;
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay17Part01AnswerTest()
        {
            int expected = 207;
            int actual = Day17.GetDay17Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
