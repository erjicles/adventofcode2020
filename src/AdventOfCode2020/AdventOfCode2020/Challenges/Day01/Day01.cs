using System;
using System.Collections.Generic;
using System.IO;
#nullable enable

namespace AdventOfCode2020.Challenges.Day01
{
    public static class Day01
    {
        public const string FILE_NAME = "Day01Input.txt";

        public static int GetDay01Part01Answer()
        {
            // ---Day 1: Report Repair ---
            // After saving Christmas five years in a row, you've decided to take a vacation at a nice resort on a tropical island. Surely, Christmas will go on without you.
            // The tropical island has its own currency and is entirely cash - only.The gold coins used there have a little picture of a starfish; the locals just call them stars. None of the currency exchanges seem to have heard of them, but somehow, you'll need to find fifty of these coins by the time you arrive so you can pay the deposit on your room.
            // To save your vacation, you need to get all fifty stars by December 25th.
            // Collect stars by solving puzzles.Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star.Good luck!
            // Before you leave, the Elves in accounting just need you to fix your expense report(your puzzle input); apparently, something isn't quite adding up.
            // Specifically, they need you to find the two entries that sum to 2020 and then multiply those two numbers together.
            // For example, suppose your expense report contained the following:
            // 1721
            // 979
            // 366
            // 299
            // 675
            // 1456
            // In this list, the two entries that sum to 2020 are 1721 and 299. Multiplying them together produces 1721 * 299 = 514579, so the correct answer is 514579.
            // Of course, your expense report is much larger. Find the two entries that sum to 2020; what do you get if you multiply them together?
            // Answer: 157059
            var inputData = GetDay01Input();
            var foundEntries = GetNumberIndexesThatSumToTarget(inputData, 2020);
            var result = GetProductOfFoundEntries(inputData, foundEntries);
            return result;
        }

        public static int GetProductOfFoundEntries(IList<int> sourceList, IList<int> foundIndexes)
        {
            var result = 1;
            foreach (int currentIndex in foundIndexes)
            {
                result *= sourceList[currentIndex];
            }
            return result;
        }

        public static IList<int> GetNumberIndexesThatSumToTarget(IList<int> numberList, int targetSum)
        {
            for (int i = 0; i < numberList.Count; i++)
            {
                if (numberList[i] > targetSum)
                {
                    continue;
                }
                for (int j = i + 1; j < numberList.Count; j++)
                {
                    if (numberList[j] > targetSum)
                    {
                        continue;
                    }
                    var currentSum = numberList[i] + numberList[j];
                    if (currentSum == targetSum)
                    {
                        return new List<int>() { i, j };
                    }
                }
            }
            return new List<int>();
        }

        private static IList<int> GetDay01Input()
        {
            var result = new List<int>();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", FILE_NAME);
            if (!File.Exists(filePath))
            {
                throw new Exception($"Cannot locate file {filePath}");
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (sr.Peek() >= 0)
                {
                    string? currentLine = sr.ReadLine();
                    if (int.TryParse(currentLine, out int currentVal))
                    {
                        result.Add(currentVal);
                    }
                }
            }
            return result;
        }
    }
}
