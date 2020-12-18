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
        public void GetExpressionValueTest()
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
            var testData = new List<Tuple<string, long>>()
            {
                new Tuple<string, long>(
                    "1 + 2 * 3 + 4 * 5 + 6",
                    71),
                new Tuple<string, long>(
                    "1 + (2 * 3) + (4 * (5 + 6))",
                    51),
                new Tuple<string, long>(
                    "2 * 3 + (4 * 5)", 
                    26),
                new Tuple<string, long>(
                    "5 + (8 * 3 + 9 + 3 * 4 * 3)",
                    437),
                new Tuple<string, long>(
                    "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
                    12240),
                new Tuple<string, long>(
                    "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2",
                    13632),
            };

            foreach (var testExample in testData)
            {
                var expression = MathHomeworkHelper.ParseInputLine(testExample.Item1);
                var actual = MathExpressionHelper.GetExpressionValue(expression);
                Assert.Equal(testExample.Item2, actual);
            }
        }
        
        [Fact]
        public void GetDay18Part01AnswerTest()
        {
            long expected = 18213007238947;
            long actual = Day18.GetDay18Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
