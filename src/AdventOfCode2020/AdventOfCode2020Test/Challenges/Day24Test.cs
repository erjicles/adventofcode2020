using AdventOfCode2020.Challenges.Day24;
using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day24Test
    {
        [Fact]
        public void ParseInputLineTest()
        {
            var testData = new List<Tuple<string, IList<HexMovementDirection>>>()
            {
                new Tuple<string, IList<HexMovementDirection>>(
                    "esenee",
                    new List<HexMovementDirection>() 
                    { 
                        HexMovementDirection.East,
                        HexMovementDirection.SouthEast,
                        HexMovementDirection.NorthEast,
                        HexMovementDirection.East
                    }),
                new Tuple<string, IList<HexMovementDirection>>(
                    "nwwneeswwsee",
                    new List<HexMovementDirection>()
                    {
                        HexMovementDirection.NorthWest,
                        HexMovementDirection.West,
                        HexMovementDirection.NorthEast,
                        HexMovementDirection.East,
                        HexMovementDirection.SouthWest,
                        HexMovementDirection.West,
                        HexMovementDirection.SouthEast,
                        HexMovementDirection.East
                    })
            };

            foreach (var testExample in testData)
            {
                var actual = TileHelper.ParseInputLine(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void MoveHexTest()
        {
            var testData = new List<Tuple<GridPoint, HexMovementDirection, GridPoint>>()
            {
                new Tuple<GridPoint, HexMovementDirection, GridPoint>(
                    GridPoint.Origin,
                    HexMovementDirection.East,
                    new GridPoint(1, 0)),
                new Tuple<GridPoint, HexMovementDirection, GridPoint>(
                    GridPoint.Origin,
                    HexMovementDirection.SouthEast,
                    new GridPoint(0, -1)),
                new Tuple<GridPoint, HexMovementDirection, GridPoint>(
                    GridPoint.Origin,
                    HexMovementDirection.SouthWest,
                    new GridPoint(-1, -1)),
                new Tuple<GridPoint, HexMovementDirection, GridPoint>(
                    GridPoint.Origin,
                    HexMovementDirection.West,
                    new GridPoint(-1, 0)),
                new Tuple<GridPoint, HexMovementDirection, GridPoint>(
                    GridPoint.Origin,
                    HexMovementDirection.NorthWest,
                    new GridPoint(0, 1)),
                new Tuple<GridPoint, HexMovementDirection, GridPoint>(
                    GridPoint.Origin,
                    HexMovementDirection.NorthEast,
                    new GridPoint(1, 1)),
            };

            foreach (var testExample in testData)
            {
                var actual = TileHelper.MoveHex(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetBlackTilesTest()
        {
            var testData = new List<Tuple<IList<string>, int>>()
            {
                new Tuple<IList<string>, int>(
                    new List<string>()
                    {
                        "sesenwnenenewseeswwswswwnenewsewsw",
                        "neeenesenwnwwswnenewnwwsewnenwseswesw",
                        "seswneswswsenwwnwse",
                        "nwnwneseeswswnenewneswwnewseswneseene",
                        "swweswneswnenwsewnwneneseenw",
                        "eesenwseswswnenwswnwnwsewwnwsene",
                        "sewnenenenesenwsewnenwwwse",
                        "wenwwweseeeweswwwnwwe",
                        "wsweesenenewnwwnwsenewsenwwsesesenwne",
                        "neeswseenwwswnwswswnw",
                        "nenwswwsewswnenenewsenwsenwnesesenew",
                        "enewnwewneswsewnwswenweswnenwsenwsw",
                        "sweneswneswneneenwnewenewwneswswnese",
                        "swwesenesewenwneswnwwneseswwne",
                        "enesenwswwswneneswsenwnewswseenwsese",
                        "wnwnesenesenenwwnenwsewesewsesesew",
                        "nenewswnwewswnenesenwnesewesw",
                        "eneswnwswnwsenenwnwnwwseeswneewsenese",
                        "neswnwewnwnwseenwseesewsenwsweewe",
                        "wseweeenwnesenwwwswnew"
                    },
                    10)
            };

            foreach (var testExample in testData)
            {
                var lines = TileHelper.ParseInputLines(testExample.Item1);
                var identifiedTiles = TileHelper.GetIdentifiedTiles(lines);
                var blackTiles = TileHelper.GetBlackTiles(identifiedTiles);
                Assert.Equal(testExample.Item2, blackTiles.Count);
            }
        }

        [Fact]
        public void GetBlackTilesAfterNDaysTest()
        {
            /*
Day 1: 15
Day 2: 12
Day 3: 25
Day 4: 14
Day 5: 23
Day 6: 28
Day 7: 41
Day 8: 37
Day 9: 49
Day 10: 37

Day 20: 132
Day 30: 259
Day 40: 406
Day 50: 566
Day 60: 788
Day 70: 1106
Day 80: 1373
Day 90: 1844
Day 100: 2208
             */
            var testData = new List<Tuple<IList<string>, int, int>>()
            {
                new Tuple<IList<string>, int, int>(
                    new List<string>()
                    {
                        "sesenwnenenewseeswwswswwnenewsewsw",
                        "neeenesenwnwwswnenewnwwsewnenwseswesw",
                        "seswneswswsenwwnwse",
                        "nwnwneseeswswnenewneswwnewseswneseene",
                        "swweswneswnenwsewnwneneseenw",
                        "eesenwseswswnenwswnwnwsewwnwsene",
                        "sewnenenenesenwsewnenwwwse",
                        "wenwwweseeeweswwwnwwe",
                        "wsweesenenewnwwnwsenewsenwwsesesenwne",
                        "neeswseenwwswnwswswnw",
                        "nenwswwsewswnenenewsenwsenwnesesenew",
                        "enewnwewneswsewnwswenweswnenwsenwsw",
                        "sweneswneswneneenwnewenewwneswswnese",
                        "swwesenesewenwneswnwwneseswwne",
                        "enesenwswwswneneswsenwnewswseenwsese",
                        "wnwnesenesenenwwnenwsewesewsesesew",
                        "nenewswnwewswnenesenwnesewesw",
                        "eneswnwswnwsenenwnwnwwseeswneewsenese",
                        "neswnwewnwnwseenwseesewsenwsweewe",
                        "wseweeenwnesenwwwswnew"
                    },
                    0,
                    10),
                new Tuple<IList<string>, int, int>(
                    new List<string>()
                    {
                        "sesenwnenenewseeswwswswwnenewsewsw",
                        "neeenesenwnwwswnenewnwwsewnenwseswesw",
                        "seswneswswsenwwnwse",
                        "nwnwneseeswswnenewneswwnewseswneseene",
                        "swweswneswnenwsewnwneneseenw",
                        "eesenwseswswnenwswnwnwsewwnwsene",
                        "sewnenenenesenwsewnenwwwse",
                        "wenwwweseeeweswwwnwwe",
                        "wsweesenenewnwwnwsenewsenwwsesesenwne",
                        "neeswseenwwswnwswswnw",
                        "nenwswwsewswnenenewsenwsenwnesesenew",
                        "enewnwewneswsewnwswenweswnenwsenwsw",
                        "sweneswneswneneenwnewenewwneswswnese",
                        "swwesenesewenwneswnwwneseswwne",
                        "enesenwswwswneneswsenwnewswseenwsese",
                        "wnwnesenesenenwwnenwsewesewsesesew",
                        "nenewswnwewswnenesenwnesewesw",
                        "eneswnwswnwsenenwnwnwwseeswneewsenese",
                        "neswnwewnwnwseenwseesewsenwsweewe",
                        "wseweeenwnesenwwwswnew"
                    },
                    1,
                    15),
                new Tuple<IList<string>, int, int>(
                    new List<string>()
                    {
                        "sesenwnenenewseeswwswswwnenewsewsw",
                        "neeenesenwnwwswnenewnwwsewnenwseswesw",
                        "seswneswswsenwwnwse",
                        "nwnwneseeswswnenewneswwnewseswneseene",
                        "swweswneswnenwsewnwneneseenw",
                        "eesenwseswswnenwswnwnwsewwnwsene",
                        "sewnenenenesenwsewnenwwwse",
                        "wenwwweseeeweswwwnwwe",
                        "wsweesenenewnwwnwsenewsenwwsesesenwne",
                        "neeswseenwwswnwswswnw",
                        "nenwswwsewswnenenewsenwsenwnesesenew",
                        "enewnwewneswsewnwswenweswnenwsenwsw",
                        "sweneswneswneneenwnewenewwneswswnese",
                        "swwesenesewenwneswnwwneseswwne",
                        "enesenwswwswneneswsenwnewswseenwsese",
                        "wnwnesenesenenwwnenwsewesewsesesew",
                        "nenewswnwewswnenesenwnesewesw",
                        "eneswnwswnwsenenwnwnwwseeswneewsenese",
                        "neswnwewnwnwseenwseesewsenwsweewe",
                        "wseweeenwnesenwwwswnew"
                    },
                    2,
                    12),
                new Tuple<IList<string>, int, int>(
                    new List<string>()
                    {
                        "sesenwnenenewseeswwswswwnenewsewsw",
                        "neeenesenwnwwswnenewnwwsewnenwseswesw",
                        "seswneswswsenwwnwse",
                        "nwnwneseeswswnenewneswwnewseswneseene",
                        "swweswneswnenwsewnwneneseenw",
                        "eesenwseswswnenwswnwnwsewwnwsene",
                        "sewnenenenesenwsewnenwwwse",
                        "wenwwweseeeweswwwnwwe",
                        "wsweesenenewnwwnwsenewsenwwsesesenwne",
                        "neeswseenwwswnwswswnw",
                        "nenwswwsewswnenenewsenwsenwnesesenew",
                        "enewnwewneswsewnwswenweswnenwsenwsw",
                        "sweneswneswneneenwnewenewwneswswnese",
                        "swwesenesewenwneswnwwneseswwne",
                        "enesenwswwswneneswsenwnewswseenwsese",
                        "wnwnesenesenenwwnenwsewesewsesesew",
                        "nenewswnwewswnenesenwnesewesw",
                        "eneswnwswnwsenenwnwnwwseeswneewsenese",
                        "neswnwewnwnwseenwseesewsenwsweewe",
                        "wseweeenwnesenwwwswnew"
                    },
                    100,
                    2208)
            };

            foreach (var testExample in testData)
            {
                var lines = TileHelper.ParseInputLines(testExample.Item1);
                var identifiedTiles = TileHelper.GetIdentifiedTiles(lines);
                var startingBlackTiles = TileHelper.GetBlackTiles(identifiedTiles);
                var endingBlackTiles = TileHelper.GetBlackTilesAfterNDays(startingBlackTiles, testExample.Item2);
                Assert.Equal(testExample.Item3, endingBlackTiles.Count);
            }
        }

        [Fact]
        public void GetDay24Part01AnswerTest()
        {
            int expected = 528;
            int actual = Day24.GetDay24Part01Answer();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDay24Part02AnswerTest()
        {
            int expected = 4200;
            int actual = Day24.GetDay24Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
