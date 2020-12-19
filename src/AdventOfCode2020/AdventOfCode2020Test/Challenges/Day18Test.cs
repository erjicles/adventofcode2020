using AdventOfCode2020.Challenges.Day18;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day18Test
    {
        [Fact]
        public void GetExpressionStringValueTest()
        {
            // For example, the steps to evaluate the expression 1 + 2 * 3 + 4 * 5 + 6 are as follows:
            // 
            // 1 + 2 * 3 + 4 * 5 + 6
            //   3 * 3 + 4 * 5 + 6
            //       9 + 4 * 5 + 6
            //          13 * 5 + 6
            //              65 + 6
            //                  71
            // 
            // Parentheses can override this order; for example, here is what happens if parentheses are added to form 1 + (2 * 3) + (4 * (5 + 6)):
            // 
            // 1 + (2 * 3) + (4 * (5 + 6))
            // 1 +    6    + (4 * (5 + 6))
            //      7      + (4 * (5 + 6))
            //      7      + (4 *   11   )
            //      7      +     44
            //             51
            // 
            // Here are a few more examples:
            // 
            // 2 * 3 + (4 * 5) becomes 26.
            // 5 + (8 * 3 + 9 + 3 * 4 * 3) becomes 437.
            // 5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4)) becomes 12240.
            // ((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2 becomes 13632.
            //
            // Part 2:
            // For example, the steps to evaluate the expression 1 + 2 * 3 + 4 * 5 + 6 are now as follows:
            //
            //  1 + 2 * 3 + 4 * 5 + 6
            //  3 * 3 + 4 * 5 + 6
            //  3 * 7 * 5 + 6
            //  3 * 7 * 11
            //     21 * 11
            //         231
            //
            // Here are the other examples from above:
            //
            // 1 + (2 * 3) + (4 * (5 + 6)) still becomes 51.
            // 2 * 3 + (4 * 5) becomes 46.
            // 5 + (8 * 3 + 9 + 3 * 4 * 3) becomes 1445.
            // 5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4)) becomes 669060.
            // ((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2 becomes 23340.

            var testData = new List<Tuple<string, MathRules, long>>()
            {
                new Tuple<string, MathRules, long>(
                    "1 + 2 * 3 + 4 * 5 + 6",
                    new MathRules(),
                    71),
                new Tuple<string, MathRules, long>(
                    "1 + (2 * 3) + (4 * (5 + 6))",
                    new MathRules(),
                    51),
                new Tuple<string, MathRules, long>(
                    "2 * 3 + (4 * 5)",
                    new MathRules(),
                    26),
                new Tuple<string, MathRules, long>(
                    "5 + (8 * 3 + 9 + 3 * 4 * 3)",
                    new MathRules(),
                    437),
                new Tuple<string, MathRules, long>(
                    "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
                    new MathRules(),
                    12240),
                new Tuple<string, MathRules, long>(
                    "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2",
                    new MathRules(),
                    13632),
                new Tuple<string, MathRules, long>(
                    "1 + 2 * 3 + 4 * 5 + 6",
                    new MathRules(true),
                    231),
                new Tuple<string, MathRules, long>(
                    "1 + (2 * 3) + (4 * (5 + 6))",
                    new MathRules(true),
                    51),
                new Tuple<string, MathRules, long>(
                    "2 * 3 + (4 * 5)",
                    new MathRules(true),
                    46),
                new Tuple<string, MathRules, long>(
                    "5 + (8 * 3 + 9 + 3 * 4 * 3)",
                    new MathRules(true),
                    1445),
                new Tuple<string, MathRules, long>(
                    "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
                    new MathRules(true),
                    669060),
                new Tuple<string, MathRules, long>(
                    "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2",
                    new MathRules(true),
                    23340),
            };

            foreach (var testExample in testData)
            {
                var actual = MathHomeworkHelper.GetExpressionStringValue(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }
        
        [Fact]
        public void GetDay18Part01AnswerTest()
        {
            long expected = 18213007238947;
            long actual = Day18.GetDay18Part01Answer();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDay18Part02AnswerTest()
        {
            long expected = 388966573054664;
            long actual = Day18.GetDay18Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
