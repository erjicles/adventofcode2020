using AdventOfCode2020.Challenges.Day05;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day05Test
    {
        [Fact]
        public void PlaneSeatTest()
        {
            // So, decoding FBFBBFFRLR reveals that it is the seat at row 44, column 5.
            // Every seat also has a unique seat ID: multiply the row by 8, then add the column. In this example, the seat has ID 44 * 8 + 5 = 357.
            // BFFFBBFRRR: row 70, column 7, seat ID 567.
            // FFFBBBFRRR: row 14, column 7, seat ID 119.
            // BBFFBBFRLL: row 102, column 4, seat ID 820.
            var testData = new List<Tuple<string, int, int, int>>() {
                new Tuple<string, int, int, int>("FBFBBFFRLR", 44, 5, 357),
                new Tuple<string, int, int, int>("BFFFBBFRRR", 70, 7, 567),
                new Tuple<string, int, int, int>("FFFBBBFRRR", 14, 7, 119),
                new Tuple<string, int, int, int>("BBFFBBFRLL", 102, 4, 820)
            };

            foreach (var testExample in testData)
            {
                var planeSeat = new PlaneSeat(testExample.Item1);
                Assert.Equal(testExample.Item2, planeSeat.Row);
                Assert.Equal(testExample.Item3, planeSeat.Column);
                Assert.Equal(testExample.Item4, planeSeat.SeatId);
            }
        }

        [Fact]
        public void GetDay05Part01AnswerTest()
        {
            int expected = 835;
            int actual = Day05.GetDay05Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
