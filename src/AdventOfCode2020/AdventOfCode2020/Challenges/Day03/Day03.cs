using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace AdventOfCode2020.Challenges.Day03
{
    public static class Day03
    {
        public const string FILE_NAME = "Day03Input.txt";

        public static int GetDay03Part01Answer()
        {
            // ---Day 3: Toboggan Trajectory ---
            // With the toboggan login problems resolved, you set off toward the airport. While travel by toboggan might be easy, it's certainly not safe: there's very minimal steering and the area is covered in trees.You'll need to see which angles will take you near the fewest trees.
            // Due to the local geology, trees in this area only grow on exact integer coordinates in a grid. You make a map(your puzzle input) of the open squares(.) and trees(#) you can see. For example:
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
            // Starting at the top-left corner of your map and following a slope of right 3 and down 1, how many trees would you encounter?
            // Answer: 171
            var slopeMap = GetDay03Input();
            var startPoint = new GridPoint(0, 0);
            int slopeX = 3;
            int slopeY = 1;
            var result = SlopeMapHelper.GetNumberOfTreesForSlope(startPoint, slopeX, slopeY, slopeMap);
            return result;
        }

        private static SlopeMap GetDay03Input()
        {
            var inputLines = new List<string>();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", FILE_NAME);
            if (!File.Exists(filePath))
            {
                throw new Exception($"Cannot locate file {filePath}");
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (sr.Peek() >= 0)
                {
                    string? currentLine = sr.ReadLine();
                    if (currentLine != null)
                    {
                        inputLines.Add(currentLine);
                    }
                }
            }

            var result = SlopeMapHelper.ParseSlopeMap(inputLines);

            return result;
        }
    }
}
