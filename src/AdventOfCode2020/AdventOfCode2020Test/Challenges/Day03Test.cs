using AdventOfCode2020.Challenges.Day03;
using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day03Test
    {
        [Fact]
        public void GetNumberOfTreesTest()
        {
            // ..##.......
            // #...#...#..
            // .#....#..#.
            // ..#.#...#.#
            // .#...##..#.
            // ..#.##.....
            // .#.#.#....#
            // .#........#
            // #.##...#...
            // #...##....#
            // .#..#...#.#
            // These aren't the only trees, though; due to something you read about once involving arboreal genetics and biome stability, the same pattern repeats to the right many times:..##.........##.........##.........##.........##.........##.......  --->
            // #...#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
            // .#....#..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
            // ..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
            // .#...##..#..#...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
            // ..#.##.......#.##.......#.##.......#.##.......#.##.......#.##.....  --->
            // .#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
            // .#........#.#........#.#........#.#........#.#........#.#........#
            // #.##...#...#.##...#...#.##...#...#.##...#...#.##...#...#.##...#...
            // #...##....##...##....##...##....##...##....##...##....##...##....#
            // .#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#.#..#...#.#  --->
            // You start on the open square(.) in the top-left corner and need to reach the bottom(below the bottom - most row on your map).
            // The toboggan can only follow a few specific slopes(you opted for a cheaper model that prefers rational numbers) ; start by counting all the trees you would encounter for the slope right 3, down 1:
            // From your starting position at the top - left, check the position that is right 3 and down 1.Then, check the position that is right 3 and down 1 from there, and so on until you go past the bottom of the map.
            // The locations you'd check in the above example are marked here with O where there was an open square and X where there was a tree:..##.........##.........##.........##.........##.........##.......  --->
            // #..O#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#..
            // .#....X..#..#....#..#..#....#..#..#....#..#..#....#..#..#....#..#.
            // ..#.#...#O#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#..#.#...#.#
            // .#...##..#..X...##..#..#...##..#..#...##..#..#...##..#..#...##..#.
            // ..#.##.......#.X#.......#.##.......#.##.......#.##.......#.##.....  --->
            // .#.#.#....#.#.#.#.O..#.#.#.#....#.#.#.#....#.#.#.#....#.#.#.#....#
            // .#........#.#........X.#........#.#........#.#........#.#........#
            // #.##...#...#.##...#...#.X#...#...#.##...#...#.##...#...#.##...#...
            // #...##....##...##....##...#X....##...##....##...##....##...##....#
            // .#..#...#.#.#..#...#.#.#..#...X.#.#..#...#.#.#..#...#.#.#..#...#.#  --->
            // In this example, traversing the map using this slope would cause you to encounter 7 trees.
            var testData = new List<Tuple<List<string>, int, int, int, int, int>>() {
                new Tuple<List<string>, int, int, int, int, int>(
                    new List<string>()
                    {
                        "..##.......",
                        "#...#...#..",
                        ".#....#..#.",
                        "..#.#...#.#",
                        ".#...##..#.",
                        "..#.##.....",
                        ".#.#.#....#",
                        ".#........#",
                        "#.##...#...",
                        "#...##....#",
                        ".#..#...#.#",
                    }, 0, 0,
                    3, 1,
                    7)
            };

            foreach (var testExample in testData)
            {
                var slopeMap = SlopeMapHelper.ParseSlopeMap(testExample.Item1);
                var startPoint = new GridPoint(testExample.Item2, testExample.Item3);
                int slopeX = testExample.Item4;
                int slopeY = testExample.Item5;
                int actual = SlopeMapHelper.GetNumberOfTreesForSlope(startPoint, slopeX, slopeY, slopeMap);
                Assert.Equal(testExample.Item6, actual);
            }
        }

        [Fact]
        public void GetDay03Part01AnswerTest()
        {
            int expected = 171;
            int actual = Day03.GetDay03Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
