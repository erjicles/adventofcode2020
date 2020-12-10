using AdventOfCode2020.Challenges.Day10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day10Test
    {
        [Fact]
        public void TryFindAdapterPathTest()
        {
            // For example, suppose that in your bag, you have adapters with 
            // the following joltage ratings:
            // 16
            // 10
            // 15
            // 5
            // 1
            // 11
            // 7
            // 19
            // 6
            // 12
            // 4
            // With these adapters, your device's built-in joltage adapter 
            // would be rated for 19 + 3 = 22 jolts, 3 higher than the 
            // highest-rated adapter.
            // Because adapters can only connect to a source 1 - 3 jolts lower 
            // than its rating, in order to use every adapter, you'd need to 
            // choose them like this:
            // The charging outlet has an effective rating of 0 jolts, so the 
            // only adapters that could connect to it directly would need to 
            // have a joltage rating of 1, 2, or 3 jolts. Of these, only one 
            // you have is an adapter rated 1 jolt(difference of 1).
            // From your 1 - jolt rated adapter, the only choice is your 4 - 
            // jolt rated adapter(difference of 3).
            // From the 4 - jolt rated adapter, the adapters rated 5, 6, or 7 
            // are valid choices.However, in order to not skip any adapters, 
            // you have to pick the adapter rated 5 jolts(difference of 1).
            // Similarly, the next choices would need to be the adapter rated 
            // 6 and then the adapter rated 7(with difference of 1 and 1).
            // The only adapter that works with the 7 - jolt rated adapter is 
            // the one rated 10 jolts(difference of 3).
            // From 10, the choices are 11 or 12; choose 11(difference of 1) 
            // and then 12(difference of 1).
            // After 12, only valid adapter has a rating of 15 (difference of 
            // 3), then 16(difference of 1), then 19(difference of 3).
            // Finally, your device's built-in adapter is always 3 higher than 
            // the highest adapter, so its rating is 22 jolts (always a 
            // difference of 3).
            // In this example, when using every adapter, there are 7 
            // differences of 1 jolt and 5 differences of 3 jolts.
            // Here is a larger example:
            // 28
            // 33
            // 18
            // 42
            // 31
            // 14
            // 46
            // 20
            // 48
            // 47
            // 24
            // 23
            // 49
            // 45
            // 19
            // 38
            // 39
            // 11
            // 1
            // 32
            // 25
            // 35
            // 8
            // 17
            // 7
            // 9
            // 4
            // 2
            // 34
            // 10
            // 3
            //In this larger example, in a chain that uses all of the 
            // adapters, there are 22 differences of 1 jolt and 10 differences 
            // of 3 jolts.
            var testData = new List<Tuple<IList<int>, IList<Tuple<int, int>>>>()
            {
                new Tuple<IList<int>, IList<Tuple<int, int>>>(
                    new List<int>()
                    {
                        16,
                        10,
                        15,
                        5,
                        1,
                        11,
                        7,
                        19,
                        6,
                        12,
                        4
                    }, new List<Tuple<int, int>>()
                    {
                        new Tuple<int, int>(1, 7),
                        new Tuple<int, int>(3, 5)
                    }),
                new Tuple<IList<int>, IList<Tuple<int, int>>>(
                    new List<int>()
                    {
                        28,
                        33,
                        18,
                        42,
                        31,
                        14,
                        46,
                        20,
                        48,
                        47,
                        24,
                        23,
                        49,
                        45,
                        19,
                        38,
                        39,
                        11,
                        1,
                        32,
                        25,
                        35,
                        8,
                        17,
                        7,
                        9,
                        4,
                        2,
                        34,
                        10,
                        3
                    }, new List<Tuple<int, int>>()
                    {
                        new Tuple<int, int>(1, 22),
                        new Tuple<int, int>(3, 10)
                    })
            };

            foreach (var testExample in testData)
            {
                var isSuccessful = JoltageAdapterHelper.TryFindAdapterPath(testExample.Item1, out IList<Tuple<int, int>> actual);
                Assert.True(isSuccessful);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetNumberOfPossibleAdapterConfigurationsTest()
        {
            // The first example above(the one that starts with 16, 10, 15) 
            // supports the following arrangements:
            // (0), 1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, (22)
            // (0), 1, 4, 5, 6, 7, 10, 12, 15, 16, 19, (22)
            // (0), 1, 4, 5, 7, 10, 11, 12, 15, 16, 19, (22)
            // (0), 1, 4, 5, 7, 10, 12, 15, 16, 19, (22)
            // (0), 1, 4, 6, 7, 10, 11, 12, 15, 16, 19, (22)
            // (0), 1, 4, 6, 7, 10, 12, 15, 16, 19, (22)
            // (0), 1, 4, 7, 10, 11, 12, 15, 16, 19, (22)
            // (0), 1, 4, 7, 10, 12, 15, 16, 19, (22)
            // (The charging outlet and your device's built-in adapter are 
            // shown in parentheses.) Given the adapters from the first 
            // example, the total number of arrangements that connect the 
            // charging outlet to your device is 8.
            // The second example above(the one that starts with 28, 33, 18) 
            // has many arrangements.Here are a few:
            // (0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31,
            // 32, 33, 34, 35, 38, 39, 42, 45, 46, 47, 48, 49, (52)
            // 
            // (0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31,
            // 32, 33, 34, 35, 38, 39, 42, 45, 46, 47, 49, (52)
            // 
            // (0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31,
            // 32, 33, 34, 35, 38, 39, 42, 45, 46, 48, 49, (52)
            // 
            // (0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31,
            // 32, 33, 34, 35, 38, 39, 42, 45, 46, 49, (52)
            // 
            // (0), 1, 2, 3, 4, 7, 8, 9, 10, 11, 14, 17, 18, 19, 20, 23, 24, 25, 28, 31,
            // 32, 33, 34, 35, 38, 39, 42, 45, 47, 48, 49, (52)
            // 
            // (0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45,
            // 46, 48, 49, (52)
            // 
            // (0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45,
            // 46, 49, (52)
            // 
            // (0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45,
            // 47, 48, 49, (52)
            // 
            // (0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45,
            // 47, 49, (52)
            // 
            // (0), 3, 4, 7, 10, 11, 14, 17, 20, 23, 25, 28, 31, 34, 35, 38, 39, 42, 45,
            // 48, 49, (52)
            // In total, this set of adapters can connect the charging outlet 
            // to your device in 19208 distinct arrangements.
            var testData = new List<Tuple<IList<int>, BigInteger>>()
            {
                new Tuple<IList<int>, BigInteger>(
                    new List<int>()
                    {
                        16,
                        10,
                        15,
                        5,
                        1,
                        11,
                        7,
                        19,
                        6,
                        12,
                        4
                    }, 8),
                new Tuple<IList<int>, BigInteger>(
                    new List<int>()
                    {
                        28,
                        33,
                        18,
                        42,
                        31,
                        14,
                        46,
                        20,
                        48,
                        47,
                        24,
                        23,
                        49,
                        45,
                        19,
                        38,
                        39,
                        11,
                        1,
                        32,
                        25,
                        35,
                        8,
                        17,
                        7,
                        9,
                        4,
                        2,
                        34,
                        10,
                        3
                    }, 19208)
            };

            foreach (var testExample in testData)
            {
                var actual = JoltageAdapterHelper.GetNumberOfPossibleAdapterConfigurations(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetDay10Part01AnswerTest()
        {
            int expected = 2201;
            int actual = Day10.GetDay10Part01Answer();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDay10Part02AnswerTest()
        {
            BigInteger expected = 169255295254528;
            BigInteger actual = Day10.GetDay10Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
