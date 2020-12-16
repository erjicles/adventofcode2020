using AdventOfCode2020.Challenges.Day16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day16Test
    {
        [Fact]
        public void GetTicketScanningErrorRateForInvalidValuesTest()
        {
            // For example, suppose you have the following notes:
            // class: 1-3 or 5-7
            // row: 6-11 or 33-44
            // seat: 13-40 or 45-50
            // 
            // your ticket:
            // 7,1,14
            // 
            // nearby tickets:
            // 7,3,47
            // 40,4,50
            // 55,2,20
            // 38,6,12
            // It doesn't matter which position corresponds to which field; 
            // you can identify invalid nearby tickets by considering only 
            // whether tickets contain values that are not valid for any field.
            // In this example, the values on the first nearby ticket are all 
            // valid for at least one field. This is not true of the other 
            // three nearby tickets: the values 4, 55, and 12 are are not 
            // valid for any field. Adding together all of the invalid values 
            // produces your ticket scanning error rate: 4 + 55 + 12 = 71.
            var testData = new List<Tuple<IList<string>, int>>()
            {
                new Tuple<IList<string>, int>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50",
                        "",
                        "your ticket:",
                        "7,1,14",
                        "",
                        "nearby tickets:",
                        "7,3,47",
                        "40,4,50",
                        "55,2,20",
                        "38,6,12"
                    }, 71)
            };

            foreach (var testExample in testData)
            {
                var ticketData = TicketHelper.ParseInputLines(testExample.Item1);
                var invalidValues = TicketHelper.GetInvalidNearbyTicketValuesForAllFields(ticketData);
                var actual = TicketHelper.GetTicketScanningErrorRateForInvalidValues(invalidValues);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetInvalidNearbyTicketValuesForAllFieldsTest()
        {
            // For example, suppose you have the following notes:
            // class: 1-3 or 5-7
            // row: 6-11 or 33-44
            // seat: 13-40 or 45-50
            // 
            // your ticket:
            // 7,1,14
            // 
            // nearby tickets:
            // 7,3,47
            // 40,4,50
            // 55,2,20
            // 38,6,12
            // It doesn't matter which position corresponds to which field; 
            // you can identify invalid nearby tickets by considering only 
            // whether tickets contain values that are not valid for any field.
            // In this example, the values on the first nearby ticket are all 
            // valid for at least one field. This is not true of the other 
            // three nearby tickets: the values 4, 55, and 12 are are not 
            // valid for any field.
            var testData = new List<Tuple<IList<string>, IList<int>>>()
            {
                new Tuple<IList<string>, IList<int>>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50",
                        "",
                        "your ticket:",
                        "7,1,14",
                        "",
                        "nearby tickets:",
                        "7,3,47",
                        "40,4,50",
                        "55,2,20",
                        "38,6,12"
                    }, new List<int>(){ 4, 55, 12 })
            };

            foreach (var testExample in testData)
            {
                var ticketData = TicketHelper.ParseInputLines(testExample.Item1);
                var actual = TicketHelper.GetInvalidNearbyTicketValuesForAllFields(ticketData);
                var areEqual = actual.Count == testExample.Item2.Count
                    && !actual.Where(v => !testExample.Item2.Contains(v)).Any();
                Assert.True(areEqual);
            }
        }

        [Fact]
        public void GetIsValidValueForAnyFieldTest()
        {
            // For example, suppose you have the following notes:
            // class: 1-3 or 5-7
            // row: 6-11 or 33-44
            // seat: 13-40 or 45-50
            // 
            // your ticket:
            // 7,1,14
            // 
            // nearby tickets:
            // 7,3,47
            // 40,4,50
            // 55,2,20
            // 38,6,12
            // It doesn't matter which position corresponds to which field; 
            // you can identify invalid nearby tickets by considering only 
            // whether tickets contain values that are not valid for any field.
            // In this example, the values on the first nearby ticket are all 
            // valid for at least one field. This is not true of the other 
            // three nearby tickets: the values 4, 55, and 12 are are not 
            // valid for any field.
            var testData = new List<Tuple<IList<string>, int, bool>>()
            {
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 7, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 3, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 47, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 40, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 4, false),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 50, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 55, false),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 2, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 20, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 38, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 6, true),
                new Tuple<IList<string>, int, bool>(
                    new List<string>()
                    {
                        "class: 1-3 or 5-7",
                        "row: 6-11 or 33-44",
                        "seat: 13-40 or 45-50"
                    }, 12, false)
            };

            foreach (var testExample in testData)
            {
                var ticketFields = TicketHelper.ParseTicketFieldInputLines(testExample.Item1);
                var actual = TicketHelper.GetIsValidValueForAnyField(testExample.Item2, ticketFields);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void ParseTicketFieldInputLineTest()
        {
            //     class: 1-3 or 5-7
            //row: 6-11 or 33-44
            //seat: 13-40 or 45-50
            var testData = new List<Tuple<string, TicketField>>()
            {
                new Tuple<string, TicketField>(
                    "class: 1-3 or 5-7",
                    new TicketField(
                        "class", 
                        new List<Tuple<int, int>>()
                        { 
                            new Tuple<int, int>(1, 3), 
                            new Tuple<int, int>(5, 7) 
                        })),
                new Tuple<string, TicketField>(
                    "row: 6-11 or 33-44",
                    new TicketField(
                        "row",
                        new List<Tuple<int, int>>()
                        {
                            new Tuple<int, int>(6, 11),
                            new Tuple<int, int>(33, 44)
                        })),
                new Tuple<string, TicketField>(
                    "seat: 13-40 or 45-50",
                    new TicketField(
                        "seat",
                        new List<Tuple<int, int>>()
                        {
                            new Tuple<int, int>(13, 40),
                            new Tuple<int, int>(45, 50)
                        })),
            };

            foreach (var testExample in testData)
            {
                var actual = TicketHelper.ParseTicketFieldInputLine(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetDay16Part01AnswerTest()
        {
            int expected = 29878;
            int actual = Day16.GetDay16Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
