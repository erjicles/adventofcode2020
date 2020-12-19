using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day18
{
    public class MathRules
    {
        public bool IsPerformingAdditionBeforeMultiplication { get; private set; } = false;

        public MathRules()
        {
        }

        public MathRules(bool isPerformingAdditionBeforeMultiplication)
        {
            IsPerformingAdditionBeforeMultiplication = isPerformingAdditionBeforeMultiplication;
        }
    }
}
