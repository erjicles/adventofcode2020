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
