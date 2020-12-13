using AdventOfCode2020.Challenges.Day12;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day12Test
    {
        [Fact]
        public void GetDay12Part01AnswerTest()
        {
            int expected = 582;
            int actual = Day12.GetDay12Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
