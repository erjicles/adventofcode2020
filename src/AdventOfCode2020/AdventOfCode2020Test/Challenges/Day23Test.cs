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
                    new int[] { 3, 8,  9,  1,  2,  5,  4,  6,  7 })
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
            var testData = new List<Tuple<int[], int, int, int[]>>()
            {
                new Tuple<int[], int, int, int[]>(
                    new int[] { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    0,
                    3,
                    new int[] { 8, 9, 1 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 3, 2, 8, 9, 1, 5, 4, 6, 7 },
                    1,
                    3,
                    new int[] { 8, 9, 1 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 3, 2, 5, 4, 6, 7, 8, 9, 1 },
                    2,
                    3,
                    new int[] { 4, 6, 7 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 7, 2, 5, 8, 9, 1, 3, 4, 6 },
                    3,
                    3,
                    new int[] { 9, 1, 3 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 3, 2, 5, 8, 4, 6, 7, 9, 1 },
                    4,
                    3,
                    new int[] { 6, 7, 9 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 9, 2, 5, 8, 4, 1, 3, 6, 7 },
                    5,
                    3,
                    new int[] { 3, 6, 7 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 7, 2, 5, 8, 4, 1, 9, 3, 6 },
                    6,
                    3,
                    new int[] { 3, 6, 7 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 8, 3, 6, 7, 4, 1, 9, 2, 5 },
                    7,
                    3,
                    new int[] { 5, 8, 3 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 7, 4, 1, 5, 8, 3, 9, 2, 6 },
                    8,
                    3,
                    new int[] { 7, 4, 1 }),
                new Tuple<int[], int, int, int[]>(
                    new int[] { 5, 7, 4, 1, 8, 3, 9, 2, 6 },
                    0,
                    3,
                    new int[] { 7, 4, 1 })
            };

            foreach (var testExample in testData)
            {
                var pickedUpCupIndexes = CrabCupHelper.GetPickedUpCupIndexes(testExample.Item1, testExample.Item2, testExample.Item3);
                var actual = new int[testExample.Item3];
                for (int i = 0; i < pickedUpCupIndexes.Length; i++)
                {
                    actual[i] = testExample.Item1[pickedUpCupIndexes[i]];
                }
                Assert.Equal(testExample.Item4, actual);
            }
        }

        [Fact]
        public void GetDestinationCupIndexTest()
        {
            var testData = new List<Tuple<int[], int, int[], int>>()
            {
                new Tuple<int[], int, int[], int>(
                    new int[] { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    0,
                    new int[] { 1, 2, 3 },
                    4),
                new Tuple<int[], int, int[], int>(
                    new int[] { 3, 2, 8, 9, 1, 5, 4, 6, 7 },
                    1,
                    new int[] { 2, 3, 4 },
                    8),
                new Tuple<int[], int, int[], int>(
                    new int[] { 3, 2, 5, 4, 6, 7, 8, 9, 1 },
                    2,
                    new int[] { 3, 4, 5 },
                    0),
                new Tuple<int[], int, int[], int>(
                    new int[] { 7, 2, 5, 8, 9, 1, 3, 4, 6 },
                    3,
                    new int[] { 4, 5, 6 },
                    0),
                new Tuple<int[], int, int[], int>(
                    new int[] { 3, 2, 5, 8, 4, 6, 7, 9, 1 },
                    4,
                    new int[] { 5, 6, 7 },
                    0),
                new Tuple<int[], int, int[], int>(
                    new int[] { 9, 2, 5, 8, 4, 1, 3, 6, 7 },
                    5,
                    new int[] { 6, 7, 8 },
                    0),
                new Tuple<int[], int, int[], int>(
                    new int[] { 7, 2, 5, 8, 4, 1, 9, 3, 6 },
                    6,
                    new int[] { 7, 8, 0 },
                    3),
                new Tuple<int[], int, int[], int>(
                    new int[] { 8, 3, 6, 7, 4, 1, 9, 2, 5 },
                    7,
                    new int[] { 8, 0, 1 },
                    5),
                new Tuple<int[], int, int[], int>(
                    new int[] { 7, 4, 1, 5, 8, 3, 9, 2, 6 },
                    8,
                    new int[] { 0, 1, 2 },
                    3),
                new Tuple<int[], int, int[], int>(
                    new int[] { 5, 7, 4, 1, 8, 3, 9, 2, 6 },
                    0,
                    new int[] { 1, 2, 3 },
                    5)
            };

            foreach (var testExample in testData)
            {
                var actual = CrabCupHelper.GetDestinationCupIndex(testExample.Item1, testExample.Item2, testExample.Item3);
                Assert.Equal(testExample.Item4, actual);
            }
        }

        [Fact]
        public void GetNextStateTest()
        {
            var testData = new List<Tuple<int[], int, int, int[], int[]>>()
            {
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    0,
                    4,
                    new int[] { 1, 2, 3 },
                    new int[] { 3, 2, 8, 9, 1, 5, 4, 6, 7 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 3, 2, 8, 9, 1, 5, 4, 6, 7 },
                    1,
                    8,
                    new int[] { 2, 3, 4 },
                    new int [] { 3, 2, 5, 4, 6, 7, 8, 9, 1 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 3, 2, 5, 4, 6, 7, 8, 9, 1 },
                    2,
                    0,
                    new int[] { 3, 4, 5 },
                    new int[] { 7, 2, 5, 8, 9, 1, 3, 4, 6 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 7, 2, 5, 8, 9, 1, 3, 4, 6 },
                    3,
                    0,
                    new int[] { 4, 5, 6 },
                    new int[] { 3, 2, 5, 8, 4, 6, 7, 9, 1 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 3, 2, 5, 8, 4, 6, 7, 9, 1 },
                    4,
                    0,
                    new int[] { 5, 6, 7 },
                    new int[] { 9, 2, 5, 8, 4, 1, 3, 6, 7 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 9, 2, 5, 8, 4, 1, 3, 6, 7 },
                    5,
                    0,
                    new int[] { 6, 7, 8 },
                    new int[] { 7, 2, 5, 8, 4, 1, 9, 3, 6 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 7, 2, 5, 8, 4, 1, 9, 3, 6 },
                    6,
                    3,
                    new int[] { 7, 8, 0 },
                    new int[] { 8, 3, 6, 7, 4, 1, 9, 2, 5 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 8, 3, 6, 7, 4, 1, 9, 2, 5 },
                    7,
                    5,
                    new int[] { 8, 0, 1 },
                    new int[] { 7, 4, 1, 5, 8, 3, 9, 2, 6 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 7, 4, 1, 5, 8, 3, 9, 2, 6 },
                    8,
                    3,
                    new int[] { 0, 1, 2 },
                    new int[] { 5, 7, 4, 1, 8, 3, 9, 2, 6 }),
                new Tuple<int[], int, int, int[], int[]>(
                    new int[] { 5, 7, 4, 1, 8, 3, 9, 2, 6 },
                    0,
                    5,
                    new int[] { 1, 2, 3 },
                    new int[] { 5, 8, 3, 7, 4, 1, 9, 2, 6 })
            };

            foreach (var testExample in testData)
            {
                var actual = CrabCupHelper.GetNextState(testExample.Item1, testExample.Item2, testExample.Item3, testExample.Item4);
                Assert.Equal(testExample.Item5, actual);
            }
        }

        [Fact]
        public void PlayCrabCupsTest()
        {
            var testData = new List<Tuple<int[], int, int, int[]>>()
            {
                new Tuple<int[], int, int, int[]>(
                    new int[] { 3, 8,  9,  1,  2,  5,  4,  6,  7 },
                    3,
                    10,
                    new int[] { 5, 8, 3, 7, 4, 1, 9, 2, 6 })
            };

            foreach (var testExample in testData)
            {
                var actual = CrabCupHelper.PlayCrabCups(testExample.Item1, testExample.Item2, testExample.Item3);
                Assert.Equal(testExample.Item4, actual);
            }
        }

        [Fact]
        public void GetCanonicalCrabCupStringTest()
        {
            var testData = new List<Tuple<int[], string>>()
            {
                new Tuple<int[], string>(
                    new int[] { 5, 8, 3, 7, 4, 1, 9, 2, 6 },
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
    }
}
