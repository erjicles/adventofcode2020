using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day18
{
    public class MathExpression
    {
        public bool IsNumberValue { get; private set; } = true;
        public long NumberValue { get; private set; } = 0;
        public MathExpression LeftExpression { get; private set; }
        public MathExpression RightExpression { get; private set; }
        public MathOperator ExpressionOperator { get; private set; }
        
        public MathExpression()
        {
        }

        public MathExpression(long value)
        {
            IsNumberValue = true;
            NumberValue = value;
        }

        public MathExpression(
            long leftExpression,
            MathExpression rightExpression,
            MathOperator experssionOperator)
        {
            IsNumberValue = false;
            LeftExpression = new MathExpression(leftExpression);
            RightExpression = rightExpression;
            ExpressionOperator = experssionOperator;
        }

        public MathExpression(
            MathExpression leftExpression,
            long rightExpression,
            MathOperator experssionOperator)
        {
            IsNumberValue = false;
            LeftExpression = leftExpression;
            RightExpression = new MathExpression(rightExpression);
            ExpressionOperator = experssionOperator;
        }

        public MathExpression(
            MathExpression leftExpression, 
            MathExpression rightExpression, 
            MathOperator experssionOperator)
        {
            IsNumberValue = false;
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
            ExpressionOperator = experssionOperator;
        }

        public static MathExpression Zero { get; private set; } = new MathExpression();
        public static MathExpression One { get; private set; } = new MathExpression(1);

        public static implicit operator MathExpression(long value)
        {
            return new MathExpression(value);
        }

        public static MathExpression operator +(long left, MathExpression right)
        {
            return new MathExpression(left, right, MathOperator.Addition);
        }

        public static MathExpression operator +(MathExpression left, long right)
        {
            return new MathExpression(left, right, MathOperator.Addition);
        }

        public static MathExpression operator +(MathExpression left, MathExpression right)
        {
            return new MathExpression(left, right, MathOperator.Addition);
        }

        public static MathExpression operator *(long left, MathExpression right)
        {
            return new MathExpression(left, right, MathOperator.Multiplication);
        }

        public static MathExpression operator *(MathExpression left, long right)
        {
            return new MathExpression(left, right, MathOperator.Multiplication);
        }

        public static MathExpression operator *(MathExpression left, MathExpression right)
        {
            return new MathExpression(left, right, MathOperator.Multiplication);
        }

        public static long ToLong(MathExpression mathExpression)
        {
            return MathExpressionHelper.GetExpressionValue(mathExpression);
        }

        public static implicit operator long(MathExpression mathExpression)
        {
            return MathExpressionHelper.GetExpressionValue(mathExpression);
        }

        public long Value
        {
            get
            {
                return MathExpressionHelper.GetExpressionValue(this);
            }
        }

    }
}
