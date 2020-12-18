using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day18
{
    public class MathRules
    {
        public static IList<MathOperator> OrderOfOperationsDefault { get; private set; } = new List<MathOperator>()
        {
            MathOperator.Addition,
            MathOperator.Multiplication
        };
        public IList<MathOperator> OrderOfOperations { get; private set; } = OrderOfOperationsDefault;

        public MathRules()
        {
        }

        public MathRules(IList<MathOperator> orderOfOperations)
        {
            OrderOfOperations = orderOfOperations;
        }
    }
}
