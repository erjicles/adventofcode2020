using AdventOfCode2020.Challenges.Day01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day01Test
    {
        [Fact]
        public void GetNumberIndexesThatSumToTargetTest()
        {
            // Test examples taken from https://adventofcode.com/2020/day/1
            // Specifically, they need you to find the two entries that sum to 2020 and then multiply those two numbers together.
            // For example, suppose your expense report contained the following:
            // 1721
            // 979
            // 366
            // 299
            // 675
            // 1456
            // In this list, the two entries that sum to 2020 are 1721 and 299.
            var testData = new List<Tuple<List<int>, int, List<int>>>() {
                new Tuple<List<int>, int, List<int>>(
                    new List<int>()
                    {
                        1721,
                        979,
                        366,
                        299,
                        675,
                        1456
                    },
                    2020,
                    new List<int>() { 0, 3 })
            };

            foreach (var testExample in testData)
            {
                var calculatedEntries = Day01.GetNumberIndexesThatSumToTarget(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, calculatedEntries);
            }
        }

        [Fact]
        public void GetProductOfFoundEntriesTest()
        {
            // Test examples taken from https://adventofcode.com/2020/day/1
            // Specifically, they need you to find the two entries that sum to 2020 and then multiply those two numbers together.
            // For example, suppose your expense report contained the following:
            // 1721
            // 979
            // 366
            // 299
            // 675
            // 1456
            // In this list, the two entries that sum to 2020 are 1721 and 299.
            // Multiplying them together produces 1721 * 299 = 514579, so the correct answer is 514579.
            var testData = new List<Tuple<List<int>, List<int>, int>>() {
                new Tuple<List<int>, List<int>, int>(
                    new List<int>()
                    {
                        1721,
                        979,
                        366,
                        299,
                        675,
                        1456
                    },
                    new List<int>()
                    { 
                        0,
                        3
                    },
                    514579)
            };

            foreach (var testExample in testData)
            {
                var calculatedResult = Day01.GetProductOfFoundEntries(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, calculatedResult);
            }
        }

        [Fact]
        public void GetDay01Part01AnswerTest()
        {
            int expected = 157059;
            int actual = Day01.GetDay01Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
