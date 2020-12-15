using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day15
{
    public static class MemoryGameHelper
    {
        public static int GetNumberOnNthTurn(IList<int> startingNumbers, int N)
        {
            var numberTurnDictionary = new Dictionary<int, int>();
            int previousNumber = -1;
            for (int currentTurn = 1; currentTurn <= N; currentTurn++)
            {
                int currentNumber;
                if (currentTurn <= startingNumbers.Count)
                {
                    currentNumber = startingNumbers[currentTurn - 1];
                    numberTurnDictionary.Add(currentNumber, currentTurn);
                }
                else
                {
                    if (!numberTurnDictionary.ContainsKey(previousNumber))
                    {
                        numberTurnDictionary.Add(previousNumber, currentTurn - 1);
                        currentNumber = 0;
                    }
                    else
                    {
                        var previousTurnForPreviousNumber = numberTurnDictionary[previousNumber];
                        var turnDifference = currentTurn - 1 - previousTurnForPreviousNumber;
                        numberTurnDictionary[previousNumber] = currentTurn - 1;
                        currentNumber = turnDifference;
                    }
                }
                previousNumber = currentNumber;
            }
            return previousNumber;
        }

        public static IList<int> ParseInputLine(string inputLine)
        {
            var result = new List<int>();
            foreach (var numberString in inputLine.Split(","))
            {
                result.Add(int.Parse(numberString));
            }
            return result;
        }
    }
}
