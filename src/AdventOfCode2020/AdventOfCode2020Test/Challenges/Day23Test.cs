using AdventOfCode2020.Challenges.Day23;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day23Test
    {
        [Fact]
        public void ParseInputLineTest()
        {
            var testData = new List<Tuple<string, IList<int>>>()
            {
                new Tuple<string, IList<int>>(
                    "389125467",
                    new List<int>() { 3, 8,  9,  1,  2,  5,  4,  6,  7 })
            };

            foreach (var testExample in testData)
            {
                var actual = CrabCupHelper.ParseInputLine(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetPickedUpCupIndexesTest()
        {
            var testData = new List<Tuple<IList<int>, int, int, IList<int>>>()
            {
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    0,
                    3,
                    new List<int>() { 8, 9, 1 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 3, 2, 8, 9, 1, 5, 4, 6, 7 },
                    1,
                    3,
                    new List<int>() { 8, 9, 1 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 3, 2, 5, 4, 6, 7, 8, 9, 1 },
                    2,
                    3,
                    new List<int>() { 4, 6, 7 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 7, 2, 5, 8, 9, 1, 3, 4, 6 },
                    3,
                    3,
                    new List<int>() { 9, 1, 3 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 3, 2, 5, 8, 4, 6, 7, 9, 1 },
                    4,
                    3,
                    new List<int>() { 6, 7, 9 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 9, 2, 5, 8, 4, 1, 3, 6, 7 },
                    5,
                    3,
                    new List<int>() { 3, 6, 7 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 7, 2, 5, 8, 4, 1, 9, 3, 6 },
                    6,
                    3,
                    new List<int>() { 3, 6, 7 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 8, 3, 6, 7, 4, 1, 9, 2, 5 },
                    7,
                    3,
                    new List<int>() { 5, 8, 3 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 7, 4, 1, 5, 8, 3, 9, 2, 6 },
                    8,
                    3,
                    new List<int>() { 7, 4, 1 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 5, 7, 4, 1, 8, 3, 9, 2, 6 },
                    0,
                    3,
                    new List<int>() { 7, 4, 1 })
            };

            foreach (var testExample in testData)
            {
                var pickedUpCupIndexes = CrabCupHelper.GetPickedUpCupIndexes(testExample.Item1, testExample.Item2, testExample.Item3);
                var actual = new int[testExample.Item3];
                for (int i = 0; i < pickedUpCupIndexes.Count; i++)
                {
                    actual[i] = testExample.Item1[pickedUpCupIndexes[i]];
                }
                Assert.Equal(testExample.Item4, actual);
            }
        }

        [Fact]
        public void GetDestinationCupIndexTest()
        {
            var testData = new List<Tuple<IList<int>, int, IList<int>, int>>()
            {
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    0,
                    new List<int>() { 1, 2, 3 },
                    4),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 3, 2, 8, 9, 1, 5, 4, 6, 7 },
                    1,
                    new List<int>() { 2, 3, 4 },
                    8),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 3, 2, 5, 4, 6, 7, 8, 9, 1 },
                    2,
                    new List<int>() { 3, 4, 5 },
                    0),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 7, 2, 5, 8, 9, 1, 3, 4, 6 },
                    3,
                    new List<int>() { 4, 5, 6 },
                    0),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 3, 2, 5, 8, 4, 6, 7, 9, 1 },
                    4,
                    new List<int>() { 5, 6, 7 },
                    0),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 9, 2, 5, 8, 4, 1, 3, 6, 7 },
                    5,
                    new List<int>() { 6, 7, 8 },
                    0),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 7, 2, 5, 8, 4, 1, 9, 3, 6 },
                    6,
                    new List<int>() { 7, 8, 0 },
                    3),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 8, 3, 6, 7, 4, 1, 9, 2, 5 },
                    7,
                    new List<int>() { 8, 0, 1 },
                    5),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 7, 4, 1, 5, 8, 3, 9, 2, 6 },
                    8,
                    new List<int>() { 0, 1, 2 },
                    3),
                new Tuple<IList<int>, int, IList<int>, int>(
                    new List<int>() { 5, 7, 4, 1, 8, 3, 9, 2, 6 },
                    0,
                    new List<int>() { 1, 2, 3 },
                    5)
            };

            foreach (var testExample in testData)
            {
                var actual = CrabCupHelper.GetDestinationCupIndex(testExample.Item1, testExample.Item2, testExample.Item3);
                Assert.Equal(testExample.Item4, actual);
            }
        }

        [Fact]
        public void ProcessRoundTest()
        {
            var testData = new List<Tuple<IList<int>, int, int, IList<int>, IList<int>, int>>()
            {
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    0,
                    4,
                    new List<int>() { 1, 2, 3 },
                    new List<int>() { 3, 2, 8, 9, 1, 5, 4, 6, 7 },
                    2),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 3, 2, 8, 9, 1, 5, 4, 6, 7 },
                    1,
                    8,
                    new List<int>() { 2, 3, 4 },
                    new int [] { 3, 2, 5, 4, 6, 7, 8, 9, 1 },
                    5),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 3, 2, 5, 4, 6, 7, 8, 9, 1 },
                    2,
                    0,
                    new List<int>() { 3, 4, 5 },
                    new List<int>() { 7, 2, 5, 8, 9, 1, 3, 4, 6 },
                    8),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 7, 2, 5, 8, 9, 1, 3, 4, 6 },
                    3,
                    0,
                    new List<int>() { 4, 5, 6 },
                    new List<int>() { 3, 2, 5, 8, 4, 6, 7, 9, 1 },
                    4),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 3, 2, 5, 8, 4, 6, 7, 9, 1 },
                    4,
                    0,
                    new List<int>() { 5, 6, 7 },
                    new List<int>() { 9, 2, 5, 8, 4, 1, 3, 6, 7 },
                    1),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 9, 2, 5, 8, 4, 1, 3, 6, 7 },
                    5,
                    0,
                    new List<int>() { 6, 7, 8 },
                    new List<int>() { 7, 2, 5, 8, 4, 1, 9, 3, 6 },
                    9),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 7, 2, 5, 8, 4, 1, 9, 3, 6 },
                    6,
                    3,
                    new List<int>() { 7, 8, 0 },
                    new List<int>() { 8, 3, 6, 7, 4, 1, 9, 2, 5 },
                    2),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 8, 3, 6, 7, 4, 1, 9, 2, 5 },
                    7,
                    5,
                    new List<int>() { 8, 0, 1 },
                    new List<int>() { 7, 4, 1, 5, 8, 3, 9, 2, 6 },
                    6),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 7, 4, 1, 5, 8, 3, 9, 2, 6 },
                    8,
                    3,
                    new List<int>() { 0, 1, 2 },
                    new List<int>() { 5, 7, 4, 1, 8, 3, 9, 2, 6 },
                    5),
                new Tuple<IList<int>, int, int, IList<int>, IList<int>, int>(
                    new List<int>() { 5, 7, 4, 1, 8, 3, 9, 2, 6 },
                    0,
                    5,
                    new List<int>() { 1, 2, 3 },
                    new List<int>() { 5, 8, 3, 7, 4, 1, 9, 2, 6 },
                    8)
            };

            foreach (var testExample in testData)
            {
                CrabCupHelper.ProcessRound(testExample.Item1, testExample.Item2, testExample.Item3, testExample.Item4, out int nextCurrentIndex);
                var areEquivalent = CrabCupHelper.GetAreEquivalent(testExample.Item5, testExample.Item1);
                Assert.True(areEquivalent);
                var nextCurrentCup = testExample.Item1[nextCurrentIndex];
                Assert.Equal(testExample.Item6, nextCurrentCup);
            }
        }

        [Fact]
        public void PlayCrabCupsTest()
        {
            var testData = new List<Tuple<IList<int>, int, int, IList<int>>>()
            {
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    3,
                    10,
                    new List<int>() { 5, 8, 3, 7, 4, 1, 9, 2, 6 }),
                new Tuple<IList<int>, int, int, IList<int>>(
                    new List<int>() { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    3,
                    100,
                    new List<int>() { 1, 6, 7, 3, 8, 4, 5, 2, 9 })
            };

            foreach (var testExample in testData)
            {
                var actual = CrabCupHelper.PlayCrabCups(testExample.Item1, testExample.Item2, testExample.Item3);
                var areEquivalent = CrabCupHelper.GetAreEquivalent(testExample.Item4, actual);
                Assert.True(areEquivalent);
            }
        }

        [Fact]
        public void GetCanonicalCrabCupStringTest()
        {
            var testData = new List<Tuple<IList<int>, string>>()
            {
                new Tuple<IList<int>, string>(
                    new List<int>() { 5, 8, 3, 7, 4, 1, 9, 2, 6 },
                    "92658374")
            };

            foreach (var testExample in testData)
            {
                var actual = CrabCupHelper.GetCanonicalCrabCupString(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }
        
        [Fact]
        public void GetDay23Part01AnswerTest()
        {
            string expected = "97245386";
            string actual = Day23.GetDay23Part01Answer();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDay23Part02AnswerTest()
        {
            long expected = 156180332979;
            long actual = Day23.GetDay23Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
