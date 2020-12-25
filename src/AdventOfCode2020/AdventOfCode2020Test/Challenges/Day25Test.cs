using AdventOfCode2020.Challenges.Day25;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day25Test
    {
        [Fact]
        public void GetLoopSizeFromPublicKeyTest()
        {
            var testData = new List<Tuple<BigInteger, int>>()
            {
                new Tuple<BigInteger, int>(
                    5764801,
                    8),
                new Tuple<BigInteger, int>(
                    17807724,
                    11)
            };

            foreach (var testExample in testData)
            {
                var actual = DoorHelper.GetLoopSizeFromPublicKey(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetEncryptionKeyTest()
        {
            var testData = new List<Tuple<BigInteger, BigInteger, BigInteger>>()
            {
                new Tuple<BigInteger, BigInteger, BigInteger>(
                    17807724,
                    5764801,
                    14897079)
            };

            foreach (var testExample in testData)
            {
                var actual = DoorHelper.GetEncryptionKey(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay25Part01AnswerTest()
        {
            BigInteger expected = 19414467;
            BigInteger actual = Day25.GetDay25Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
