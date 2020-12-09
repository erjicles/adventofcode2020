using AdventOfCode2020.MathHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day09
{
    public static class EXchangeMaskingAdditionSystemHelper
    {
        public static IList<BigInteger> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<BigInteger>();
            foreach (var inputLine in inputLines)
            {
                var currentNumber = BigInteger.Parse(inputLine.Trim());
                result.Add(currentNumber);
            }
            return result;
        }

        public static bool GetIsValidNextNumber(IList<BigInteger> previousNumbers, BigInteger nextNumber)
        {
            var pairSums = CombinationsHelper.CombinationsRosettaWoRecursion(previousNumbers.ToArray(), 2)
                .Where(numberPair => numberPair[0] != numberPair[1])
                .Select(numberPair => numberPair.Aggregate((item1, item2) => item1 + item2));
            var result = pairSums.Contains(nextNumber);
            return result;
        }

        public static bool GetFirstInvalidNumber(
            IList<BigInteger> inputNumbers, 
            int preambleSize, 
            out BigInteger firstInvalidNumber)
        {
            if (inputNumbers.Count <= preambleSize)
            {
                throw new Exception($"Invalid input - # input numbers: {inputNumbers.Count}, Preamble size: {preambleSize}");
            }
            firstInvalidNumber = -1;
            for (int i = 0; i < inputNumbers.Count - preambleSize; i++)
            {
                var previousNumbers = new List<BigInteger>();
                for (int previousNumberIndexOffset = 0; previousNumberIndexOffset < preambleSize; previousNumberIndexOffset++)
                {
                    previousNumbers.Add(inputNumbers[i + previousNumberIndexOffset]);
                }
                var nextNumber = inputNumbers[i + preambleSize];
                var isValidNumber = GetIsValidNextNumber(previousNumbers, nextNumber);
                if (!isValidNumber)
                {
                    firstInvalidNumber = nextNumber;
                    return true;
                }
            }
            return false;
        }
    }
}
