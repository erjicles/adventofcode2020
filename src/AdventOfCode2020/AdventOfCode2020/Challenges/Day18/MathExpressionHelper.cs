using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day18
{
    public static class MathExpressionHelper
    {
        public static long GetExpressionValue(MathExpression expression)
        {
            // TODO: Make not recursive
            if (expression.IsNumberValue)
            {
                return expression.NumberValue;
            }
            var leftValue = GetExpressionValue(expression.LeftExpression);
            var rightValue = GetExpressionValue(expression.RightExpression);
            long result;
            if (MathOperator.Addition.Equals(expression.ExpressionOperator))
            {
                result = leftValue + rightValue;
            }
            else if (MathOperator.Multiplication.Equals(expression.ExpressionOperator))
            {
                result = leftValue * rightValue;
            }
            else
            {
                throw new Exception($"Unrecognized operator: {expression.ExpressionOperator}");
            }
            return result;
        }
    }
}
