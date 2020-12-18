using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day18
{
    public static class MathHomeworkHelper
    {
        public static IList<MathExpression> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<MathExpression>();
            foreach (var inputLine in inputLines)
            {
                var expression = ParseInputLine(inputLine);
                result.Add(expression);
            }
            return result;
        }

        public static MathExpression ParseInputLine(string inputLine)
        {
            // TODO: Make this non-recursive
            var result = new MathExpression();

            int currentIndex = inputLine.Length - 1;
            var currentOperator = MathOperator.Addition;
            var remainingExpressionString = inputLine;
            while (currentIndex >= 0)
            {
                remainingExpressionString = remainingExpressionString.Substring(0, currentIndex + 1);
                var currentCharacter = remainingExpressionString[currentIndex].ToString();
                if (")".Equals(currentCharacter))
                {
                    var openingParenthesisIndex = GetOpeningParenthesisIndex(remainingExpressionString, currentIndex);

                    var parenthesesExpression = ParseInputLine(remainingExpressionString.Substring(openingParenthesisIndex + 1, currentIndex - openingParenthesisIndex - 1));
                    result = new MathExpression(parenthesesExpression, result, currentOperator);

                    currentIndex = openingParenthesisIndex - 1;
                    continue;
                }
                else if (string.IsNullOrWhiteSpace(currentCharacter))
                {
                    currentIndex--;
                    continue;
                }
                else if ("+".Equals(currentCharacter))
                {
                    currentOperator = MathOperator.Addition;
                    var leftExpression = ParseInputLine(remainingExpressionString.Substring(0, currentIndex));
                    result = new MathExpression(leftExpression, result, currentOperator);
                    currentIndex = -1;
                    continue;
                }
                else if ("*".Equals(currentCharacter))
                {
                    currentOperator = MathOperator.Multiplication;
                    var leftExpression = ParseInputLine(remainingExpressionString.Substring(0, currentIndex));
                    result = new MathExpression(leftExpression, result, currentOperator);
                    currentIndex = -1;
                    continue;
                }
                else if (int.TryParse(currentCharacter, out int _))
                {
                    var numberMatch = Regex.Match(remainingExpressionString, @"(\d+)$");
                    if (!numberMatch.Success)
                    {
                        throw new Exception($"Looking for number match failed: {remainingExpressionString}");
                    }
                    var numberString = numberMatch.Groups[1].Value;
                    var numberValue = long.Parse(numberString);
                    result = new MathExpression(numberValue, result, currentOperator);
                    currentIndex -= numberString.Length;
                }
                
            }

            return result;
        }

        public static int GetOpeningParenthesisIndex(string expressionString, int closingParenthesesIndex)
        {
            int parenthesesCount = 0;
            for (int i = closingParenthesesIndex; i >= 0; i--)
            {
                if (")".Equals(expressionString[i].ToString()))
                {
                    parenthesesCount++;
                }
                else if ("(".Equals(expressionString[i].ToString()))
                {
                    parenthesesCount--;
                }
                if (parenthesesCount == 0)
                {
                    return i;
                }
            }
            throw new Exception($"No closing parentheses found: {expressionString}");
        }
    }
}
