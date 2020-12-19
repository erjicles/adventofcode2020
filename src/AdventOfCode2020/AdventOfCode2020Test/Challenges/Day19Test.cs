using AdventOfCode2020.Challenges.Day19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day19Test
    {
        [Fact]
        public void GetNumberOfValidMatchesTest()
        {
            var testData = new List<Tuple<IList<string>, int>>()
            {
                new Tuple<IList<string>, int>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\"",
                        "",
                        "ababbb",
                        "bababa",
                        "abbbab",
                        "aaabbb",
                        "aaaabbb",
                    },
                    2),
            };

            foreach (var testExample in testData)
            {
                var satelliteData = SatelliteMessageHelper.ParseInputLines(testExample.Item1);
                var actual = SatelliteMessageHelper.GetNumberOfValidMatches(satelliteData, 0);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetIsMatchTest()
        {
            var testData = new List<Tuple<IList<string>, string, bool>>()
            {
                new Tuple<IList<string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    "ababbb",
                    true),
                new Tuple<IList<string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    "abbbab",
                    true),
                new Tuple<IList<string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    "bababa",
                    false),
                new Tuple<IList<string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    "aaabbb",
                    false),
                new Tuple<IList<string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    "aaaabbb",
                    false)
            };

            foreach (var testExample in testData)
            {
                var satelliteData = SatelliteMessageHelper.ParseInputLines(testExample.Item1);
                var actual = SatelliteMessageHelper.GetIsMatch(testExample.Item2, 0, satelliteData);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay19Part01AnswerTest()
        {
            int expected = 216;
            int actual = Day19.GetDay19Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
