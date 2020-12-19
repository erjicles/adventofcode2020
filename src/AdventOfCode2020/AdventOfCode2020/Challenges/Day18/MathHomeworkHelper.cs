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
        public static long GetExpressionStringValue(
            string expressionString, 
            MathRules rules)
        {
            long result;
            expressionString = expressionString.Trim();
            if (Regex.IsMatch(expressionString, @"^\d+$"))
            {
                result = long.Parse(expressionString);
            }
            else
            {
                var topLevelOperatorIndexes = GetTopLevelOperatorIndexes(expressionString);
                var termExpressionStrings = GetTermExpressionStrings(
                    expressionString, 
                    topLevelOperatorIndexes);
                var termExpressionStringValues = termExpressionStrings
                    .Select(s => GetExpressionStringValue(s, rules))
                    .ToList();
                var operators = GetOperators(
                    expressionString, 
                    topLevelOperatorIndexes);
                result = GetExpressionValue(
                    termExpressionStringValues, 
                    operators, 
                    rules);
            }
            return result;
        }

        public static long GetExpressionValue(
            IList<long> termValues, 
            IList<MathOperator> operators, 
            MathRules rules)
        {
            if (termValues.Count != operators.Count + 1)
            {
                throw new Exception($"Invalid term/operator counts");
            }

            while (operators.Count > 0)
            {
                EvaluatePrimaryOperator(termValues, operators, rules);
            }

            return termValues[0];
        }

        public static void EvaluatePrimaryOperator(
            IList<long> termValues,
            IList<MathOperator> operators,
            MathRules rules)
        {
            var operatorIndex = 0;
            if (rules.IsPerformingAdditionBeforeMultiplication)
            {
                for (int i = 0; i < operators.Count; i++)
                {
                    var testOperator = operators[i];
                    if (MathOperator.Addition.Equals(testOperator))
                    {
                        operatorIndex = i;
                        break;
                    }
                }
            }
            var expressionOperator = operators[operatorIndex];
            var leftValue = termValues[operatorIndex];
            var rightValue = termValues[operatorIndex + 1];
            long resultValue = 0;
            if (MathOperator.Addition.Equals(expressionOperator))
            {
                resultValue = leftValue + rightValue;
            }
            else if (MathOperator.Multiplication.Equals(expressionOperator))
            {
                resultValue = leftValue * rightValue;
            }
            termValues.RemoveAt(operatorIndex);
            termValues.RemoveAt(operatorIndex);
            termValues.Insert(operatorIndex, resultValue);
            operators.RemoveAt(operatorIndex);
        }

        public static IList<MathOperator> GetOperators(string expressionString, IList<int> operatorIndexes)
        {
            var result = new List<MathOperator>();
            foreach (var operatorIndex in operatorIndexes)
            {
                var operatorString = expressionString[operatorIndex].ToString();
                if ("+".Equals(operatorString))
                {
                    result.Add(MathOperator.Addition);
                }
                else if ("*".Equals(operatorString))
                {
                    result.Add(MathOperator.Multiplication);
                }
                else
                {
                    throw new Exception($"Unknown operator: {operatorString}");
                }
            }
            return result;
        }

        public static IList<string> GetTermExpressionStrings(string expressionString, IList<int> operatorIndexes)
        {
            var result = new List<string>();
            var startIndex = 0;
            for (int i = 0; i < operatorIndexes.Count; i++)
            {
                var endIndex = operatorIndexes[i] - 1;
                var length = endIndex - startIndex + 1;
                var termString = expressionString.Substring(startIndex, length);
                termString = GetNormalizedTermString(termString);
                
                result.Add(termString);
                startIndex = operatorIndexes[i] + 1;
            }
            var finalTermString = expressionString.Substring(startIndex, expressionString.Length - startIndex);
            finalTermString = GetNormalizedTermString(finalTermString);
            result.Add(finalTermString);
            return result;
        }

        public static string GetNormalizedTermString(string rawTermString)
        {
            var result = rawTermString.Trim();

            // Remove enclosing parentheses
            int numberOfEnclosingParentheses = 0;
            var matchOpeningParentheses = Regex.Match(result, @"^(\(+)");
            if (matchOpeningParentheses.Success)
            {
                numberOfEnclosingParentheses = matchOpeningParentheses.Value.Length;
            }
            if (numberOfEnclosingParentheses == 0)
            {
                return result;
            }

            var currentParenthesesCount = numberOfEnclosingParentheses;
            for (int i = numberOfEnclosingParentheses; i < result.Length - numberOfEnclosingParentheses; i++)
            {
                if ("(".Equals(result[i].ToString()))
                {
                    currentParenthesesCount++;
                }
                else if (")".Equals(result[i].ToString()))
                {
                    currentParenthesesCount--;
                }
                if (currentParenthesesCount < numberOfEnclosingParentheses)
                {
                    numberOfEnclosingParentheses = currentParenthesesCount;
                }
            }
            
            if (numberOfEnclosingParentheses > 0)
            {
                result = result.Remove(0, numberOfEnclosingParentheses);
                result = result.Remove(result.Length - numberOfEnclosingParentheses, numberOfEnclosingParentheses);
            }
            result = result.Trim();
            return result;
        }

        public static IList<int> GetTopLevelOperatorIndexes(string expressionString)
        {
            var result = new List<int>();
            var currentLevel = 0;
            for (int i = 0; i < expressionString.Length; i++)
            {
                var currentCharacter = expressionString[i].ToString();
                if ("(".Equals(currentCharacter))
                {
                    currentLevel++;
                }
                else if (")".Equals(currentCharacter))
                {
                    currentLevel--;
                }
                if (currentLevel == 0 && Regex.IsMatch(currentCharacter, @"^(\+|\*)$"))
                {
                    result.Add(i);
                }
            }
            return result;
        }
    }
}
