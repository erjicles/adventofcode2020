using AdventOfCode2020.Challenges.Day13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day13Test
    {
        [Fact]
        public void GetBusIdWithLowestWaitTimeTest()
        {
            // For example, suppose you have the following notes:
            // 939
            // 7,13,x,x,59,x,31,19
            // Here, the earliest timestamp you could depart is 939, and the 
            // bus IDs in service are 7, 13, 59, 31, and 19.
            // The earliest bus you could take is bus ID 59. It doesn't depart 
            // until timestamp 944, so you would need to wait 944 - 939 = 5 
            // minutes before it departs. Multiplying the bus ID by the number 
            // of minutes you'd need to wait gives 295.
            var testData = new List<Tuple<string, string, Tuple<int, int>>>()
            {
                new Tuple<string, string, Tuple<int, int>>(
                    "939",
                    "7,13,x,x,59,x,31,19", 
                    new Tuple<int, int>(59, 5))
            };

            foreach (var testExample in testData)
            {
                var busData = BusHelper.ParseInputLines(testExample.Item1, testExample.Item2);
                var actual = BusHelper.GetBusIdWithLowestWaitTime(busData);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetEarliestValidTimestampTest()
        {
            // For example, suppose you have the same list of bus IDs as above:
            // 7,13,x,x,59,x,31,19
            // An x in the schedule means there are no constraints on what bus IDs must depart at that time.
            // This means you are looking for the earliest timestamp(called t) such that:
            // Bus ID 7 departs at timestamp t.
            // Bus ID 13 departs one minute after timestamp t.
            // There are no requirements or restrictions on departures at two or three minutes after timestamp t.
            // Bus ID 59 departs four minutes after timestamp t.
            // There are no requirements or restrictions on departures at five minutes after timestamp t.
            // Bus ID 31 departs six minutes after timestamp t.
            // Bus ID 19 departs seven minutes after timestamp t.
            // The only bus departures that matter are the listed bus IDs at their specific offsets from t.Those bus IDs can depart at other times, and other bus IDs can depart at those times.For example, in the list above, because bus ID 19 must depart seven minutes after the timestamp at which bus ID 7 departs, bus ID 7 will always also be departing with bus ID 19 at seven minutes after timestamp t.
            // In this example, the earliest timestamp at which this occurs is 1068781
            // In the above example, bus ID 7 departs at timestamp 1068788(seven minutes after t).This is fine; the only requirement on that minute is that bus ID 19 departs then, and it does.
            // Here are some other examples:
            // The earliest timestamp that matches the list 17,x,13,19 is 3417.
            // 67,7,59,61 first occurs at timestamp 754018.
            // 67,x,7,59,61 first occurs at timestamp 779210.
            // 67,7,x,59,61 first occurs at timestamp 1261476.
            // 1789,37,47,1889 first occurs at timestamp 1202161486.
            var testData = new List<Tuple<string, BigInteger>>()
            {
                new Tuple<string, BigInteger>(
                    "7,13,x,x,59,x,31,19",
                    1068781),
                new Tuple<string, BigInteger>(
                    "17,x,13,19",
                    3417),
                new Tuple<string, BigInteger>(
                    "67,7,59,61",
                    754018),
                new Tuple<string, BigInteger>(
                    "67,x,7,59,61",
                    779210),
                new Tuple<string, BigInteger>(
                    "67,7,x,59,61",
                    1261476),
                new Tuple<string, BigInteger>(
                    "1789,37,47,1889",
                    1202161486)
            };

            foreach (var testExample in testData)
            {
                var busData = BusHelper.ParseInputLines("1", testExample.Item1);
                var actual = BusHelper.GetEarliestValidTimestamp(busData.Item2);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetDay13Part01AnswerTest()
        {
            int expected = 3215;
            int actual = Day13.GetDay13Part01Answer();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDay13Part02AnswerTest()
        {
            BigInteger expected = 1001569619313439;
            BigInteger actual = Day13.GetDay13Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
