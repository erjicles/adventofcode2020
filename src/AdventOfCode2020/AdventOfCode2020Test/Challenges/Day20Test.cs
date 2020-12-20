using AdventOfCode2020.Challenges.Day20;
using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day20Test
    {
        [Fact]
        public void GetTileReflectionHorizontalTest()
        {
            var testData = new List<Tuple<IList<string>, IList<string>>>()
            {
                new Tuple<IList<string>, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    new List<string>()
                    {
                        "dcba",
                        "hgfe",
                        "lkji",
                        "ponm"
                    })
            };

            foreach (var testExample in testData)
            {
                var actual = TileHelper.GetTileReflectionHorizontal(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetTileRotationTest()
        {
            var testData = new List<Tuple<IList<string>, int, IList<string>>>()
            {
                new Tuple<IList<string>, int, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    0,
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    }),
                new Tuple<IList<string>, int, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    90,
                    new List<string>()
                    {
                        "dhlp",
                        "cgko",
                        "bfjn",
                        "aeim"
                    }),
                new Tuple<IList<string>, int, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    180,
                    new List<string>()
                    {
                        "ponm",
                        "lkji",
                        "hgfe",
                        "dcba"
                    }),
                new Tuple<IList<string>, int, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    270,
                    new List<string>()
                    {
                        "miea",
                        "njfb",
                        "okgc",
                        "plhd"
                    })
            };

            foreach (var testExample in testData)
            {
                var actual = TileHelper.GetTileRotation(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetTileOrientationTest()
        {
            var testData = new List<Tuple<IList<string>, TileOrientation, IList<string>>>()
            {
                new Tuple<IList<string>, TileOrientation, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    new TileOrientation(0, false),
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    }),
                new Tuple<IList<string>, TileOrientation, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    new TileOrientation(90, false),
                    new List<string>()
                    {
                        "dhlp",
                        "cgko",
                        "bfjn",
                        "aeim"
                    }),
                new Tuple<IList<string>, TileOrientation, IList<string>>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    new TileOrientation(90, true),
                    new List<string>()
                    {
                        "plhd",
                        "okgc",
                        "njfb",
                        "miea"
                    }),
            };

            foreach (var testExample in testData)
            {
                var actual = TileHelper.GetTileOrientation(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetTileEdgeKeyTest()
        {
            var testData = new List<Tuple<IList<string>, MovementDirection, string>>()
            {
                new Tuple<IList<string>, MovementDirection, string>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    MovementDirection.Up,
                    "abcd"),
                new Tuple<IList<string>, MovementDirection, string>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    MovementDirection.Down,
                    "mnop"),
                new Tuple<IList<string>, MovementDirection, string>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    MovementDirection.Left,
                    "aeim"),
                new Tuple<IList<string>, MovementDirection, string>(
                    new List<string>()
                    {
                        "abcd",
                        "efgh",
                        "ijkl",
                        "mnop"
                    },
                    MovementDirection.Right,
                    "dhlp"),
            };

            foreach (var testExample in testData)
            {
                var actual = TileHelper.GetTileEdgeKey(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void TryGetTilePositionsAndOrientationsTest()
        {
            var testData = new List<Tuple<IList<string>, long>>()
            {
                new Tuple<IList<string>, long>(
                    new List<string>()
                    {
                        "Tile 2311:",
                        "..##.#..#.",
                        "##..#.....",
                        "#...##..#.",
                        "####.#...#",
                        "##.##.###.",
                        "##...#.###",
                        ".#.#.#..##",
                        "..#....#..",
                        "###...#.#.",
                        "..###..###",
                        "          ",
                        "Tile 1951:",
                        "#.##...##.",
                        "#.####...#",
                        ".....#..##",
                        "#...######",
                        ".##.#....#",
                        ".###.#####",
                        "###.##.##.",
                        ".###....#.",
                        "..#.#..#.#",
                        "#...##.#..",
                        "          ",
                        "Tile 1171:",
                        "####...##.",
                        "#..##.#..#",
                        "##.#..#.#.",
                        ".###.####.",
                        "..###.####",
                        ".##....##.",
                        ".#...####.",
                        "#.##.####.",
                        "####..#...",
                        ".....##...",
                        "          ",
                        "Tile 1427:",
                        "###.##.#..",
                        ".#..#.##..",
                        ".#.##.#..#",
                        "#.#.#.##.#",
                        "....#...##",
                        "...##..##.",
                        "...#.#####",
                        ".#.####.#.",
                        "..#..###.#",
                        "..##.#..#.",
                        "          ",
                        "Tile 1489:",
                        "##.#.#....",
                        "..##...#..",
                        ".##..##...",
                        "..#...#...",
                        "#####...#.",
                        "#..#.#.#.#",
                        "...#.#.#..",
                        "##.#...##.",
                        "..##.##.##",
                        "###.##.#..",
                        "          ",
                        "Tile 2473:",
                        "#....####.",
                        "#..#.##...",
                        "#.##..#...",
                        "######.#.#",
                        ".#...#.#.#",
                        ".#########",
                        ".###.#..#.",
                        "########.#",
                        "##...##.#.",
                        "..###.#.#.",
                        "          ",
                        "Tile 2971:",
                        "..#.#....#",
                        "#...###...",
                        "#.#.###...",
                        "##.##..#..",
                        ".#####..##",
                        ".#..####.#",
                        "#..#.#..#.",
                        "..####.###",
                        "..#.#.###.",
                        "...#.#.#.#",
                        "          ",
                        "Tile 2729:",
                        "...#.#.#.#",
                        "####.#....",
                        "..#.#.....",
                        "....#..#.#",
                        ".##..##.#.",
                        ".#.####...",
                        "####.#.#..",
                        "##.####...",
                        "##..#.##..",
                        "#.##...##.",
                        "          ",
                        "Tile 3079:",
                        "#.#.#####.",
                        ".#..######",
                        "..#.......",
                        "######....",
                        "####.#..#.",
                        ".#...#.##.",
                        "#.#####.##",
                        "..#.###...",
                        "..#.......",
                        "..#.###..."
                    },
                    20899048083289)
                    //new List<IList<int>>()
                    //{
                    //    new List<int>(){ 1951, 2311, 3079 },
                    //    new List<int>(){ 2729, 1427, 2473 },
                    //    new List<int>(){ 2971, 1489, 1171 }
                    //})
            };

            foreach (var testExample in testData)
            {
                var inputTiles = SatelliteImageHelper.ParseInputLines(testExample.Item1);
                bool isSuccessful = SatelliteImageHelper.TryGetTilePositionsAndOrientations(inputTiles, out IList<IList<Tuple<int, TileOrientation>>> tilePlacements);
                Assert.True(isSuccessful);
                long actual = 1;
                actual *= tilePlacements[0][0].Item1;
                actual *= tilePlacements[0][^1].Item1;
                actual *= tilePlacements[^1][0].Item1;
                actual *= tilePlacements[^1][^1].Item1;
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetDay20Part01AnswerTest()
        {
            long expected = 14986175499719;
            long actual = Day20.GetDay20Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
