using AdventOfCode2020.Challenges.Day09;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day09Test
    {
        [Fact]
        public void GetIsValidNextNumberTest()
        {
            // For example, suppose your preamble consists of the numbers 1 
            // through 25 in a random order. To be valid, the next number must 
            // be the sum of two of those numbers:
            // 26 would be a valid next number, as it could be 1 plus 25(or many other pairs, like 2 and 24).
            // 49 would be a valid next number, as it is the sum of 24 and 25.
            // 100 would not be valid; no two of the previous 25 numbers sum to 100.
            // 50 would also not be valid; although 25 appears in the previous 25 numbers, the two numbers in the pair must be different.
            // Suppose the 26th number is 45, and the first number (no longer 
            // an option, as it is more than 25 numbers ago) was 20. Now, for 
            // the next number to be valid, there needs to be some pair of 
            // numbers among 1 - 19, 21 - 25, or 45 that add up to it:
            // 26 would still be a valid next number, as 1 and 25 are still within the previous 25 numbers.
            // 65 would not be valid, as no two of the available numbers sum to it.
            // 64 and 66 would both be valid, as they are the result of 19 + 45 and 21 + 45 respectively.
            // Here is a larger example which only considers the previous 5 numbers (and has a preamble of length 5):
            // 35
            // 20
            // 15
            // 25
            // 47
            // 40
            // 62
            // 55
            // 65
            // 95
            // 102
            // 117
            // 150
            // 182
            // 127
            // 219
            // 299
            // 277
            // 309
            // 576
            // In this example, after the 5 - number preamble, almost every 
            // number is the sum of two of the previous 5 numbers; the only 
            // number that does not follow this rule is 127.
            var testData = new List<Tuple<IList<BigInteger>, BigInteger, bool>>()
            {
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25
                    }, 26, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25
                    }, 49, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25
                    }, 100, false),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25
                    }, 50, false),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 45
                    }, 26, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 45
                    }, 65, false),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 45
                    }, 64, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 45
                    }, 66, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        35, 20, 15, 25, 47
                    }, 40, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        20, 15, 25, 47, 40
                    }, 62, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        15, 25, 47, 40, 62
                    }, 55, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        25, 47, 40, 62, 55
                    }, 65, true),
                new Tuple<IList<BigInteger>, BigInteger, bool>(
                    new List<BigInteger>()
                    {
                        95, 102, 117, 150, 182
                    }, 127, false),
            };

            foreach (var testExample in testData)
            {
                var actual = EXchangeMaskingAdditionSystemHelper.GetIsValidNextNumber(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void TryGetFirstInvalidNumberTest()
        {
            // Here is a larger example which only considers the previous 5 numbers (and has a preamble of length 5):
            // 35
            // 20
            // 15
            // 25
            // 47
            // 40
            // 62
            // 55
            // 65
            // 95
            // 102
            // 117
            // 150
            // 182
            // 127
            // 219
            // 299
            // 277
            // 309
            // 576
            // In this example, after the 5 - number preamble, almost every 
            // number is the sum of two of the previous 5 numbers; the only 
            // number that does not follow this rule is 127.
            var testData = new List<Tuple<IList<BigInteger>, int, BigInteger>>()
            {
                new Tuple<IList<BigInteger>, int, BigInteger>(
                    new List<BigInteger>()
                    {
                        35,
                        20,
                        15,
                        25,
                        47,
                        40,
                        62,
                        55,
                        65,
                        95,
                        102,
                        117,
                        150,
                        182,
                        127,
                        219,
                        299,
                        277,
                        309,
                        576
                    }, 5, 127)
            };

            foreach (var testExample in testData)
            {
                var isInvalidNumberFound = EXchangeMaskingAdditionSystemHelper.TryGetFirstInvalidNumber(testExample.Item1, testExample.Item2, out BigInteger actual);
                Assert.True(isInvalidNumberFound);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void TryGetContiguousSetOfAtLeastTwoNumbersThatSumToTargetTest()
        {
            // Again consider the above example:
            // 35
            // 20
            // 15
            // 25
            // 47
            // 40
            // 62
            // 55
            // 65
            // 95
            // 102
            // 117
            // 150
            // 182
            // 127
            // 219
            // 299
            // 277
            // 309
            // 576
            // In this list, adding up all of the numbers from 15 through 40 
            // produces the invalid number from step 1, 127. (Of course, the 
            // contiguous set of numbers in your actual list might be much 
            // longer.)
            var testData = new List<Tuple<IList<BigInteger>, BigInteger, IList<BigInteger>>>()
            {
                new Tuple<IList<BigInteger>, BigInteger, IList<BigInteger>>(
                    new List<BigInteger>()
                    {
                        35,
                        20,
                        15,
                        25,
                        47,
                        40,
                        62,
                        55,
                        65,
                        95,
                        102,
                        117,
                        150,
                        182,
                        127,
                        219,
                        299,
                        277,
                        309,
                        576
                    }, 127,
                    new List<BigInteger>()
                    {
                        15,
                        25,
                        47,
                        40
                    })
            };

            foreach (var testExample in testData)
            {
                var isSetFound = EXchangeMaskingAdditionSystemHelper.TryGetContiguousSetOfAtLeastTwoNumbersThatSumToTarget(testExample.Item1, testExample.Item2, out IList<BigInteger> actual);
                Assert.True(isSetFound);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void TryGetEncryptionWeaknessTest()
        {
            // Again consider the above example:
            // 35
            // 20
            // 15
            // 25
            // 47
            // 40
            // 62
            // 55
            // 65
            // 95
            // 102
            // 117
            // 150
            // 182
            // 127
            // 219
            // 299
            // 277
            // 309
            // 576
            // In this list, adding up all of the numbers from 15 through 40 
            // produces the invalid number from step 1, 127. (Of course, the 
            // contiguous set of numbers in your actual list might be much 
            // longer.)
            // To find the encryption weakness, add together the smallest and 
            // largest number in this contiguous range; in this example, 
            // these are 15 and 47, producing 62.
            var testData = new List<Tuple<IList<BigInteger>, int, BigInteger>>()
            {
                new Tuple<IList<BigInteger>, int, BigInteger>(
                    new List<BigInteger>()
                    {
                        35,
                        20,
                        15,
                        25,
                        47,
                        40,
                        62,
                        55,
                        65,
                        95,
                        102,
                        117,
                        150,
                        182,
                        127,
                        219,
                        299,
                        277,
                        309,
                        576
                    }, 5,
                    62)
            };

            foreach (var testExample in testData)
            {
                var isWeaknessFound = EXchangeMaskingAdditionSystemHelper.TryGetEncryptionWeakness(testExample.Item1, testExample.Item2, out BigInteger actual);
                Assert.True(isWeaknessFound);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay09Part01AnswerTest()
        {
            BigInteger expected = 1309761972;
            BigInteger actual = Day09.GetDay09Part01Answer();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDay09Part02AnswerTest()
        {
            BigInteger expected = 177989832;
            BigInteger actual = Day09.GetDay09Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
