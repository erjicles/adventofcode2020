using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day23
{
    public static class CrabCupHelper
    {
        public static string GetCanonicalCrabCupString(int[] cups)
        {
            var canonicalCupOrder = new List<int>();
            var cup1Index = Array.IndexOf(cups, 1);
            for (int indexOffset = 1; indexOffset < cups.Length; indexOffset++)
            {
                var cupIndex = (cup1Index + indexOffset) % cups.Length;
                var cupLabel = cups[cupIndex];
                canonicalCupOrder.Add(cupLabel);
            }
            var result = string.Join("", canonicalCupOrder);
            return result;
        }
        public static int[] PlayCrabCups(
            IList<int> startingNumbers,
            int numberOfCupsToPickUp,
            int numberOfRounds)
        {
            var currentState = startingNumbers.ToArray();
            var currentCupIndex = 0;
            for (int roundNumber = 1; roundNumber <= numberOfRounds; roundNumber++)
            {
                var pickedUpCupIndexes = GetPickedUpCupIndexes(currentState, currentCupIndex, numberOfCupsToPickUp);
                var destinationCupIndex = GetDestinationCupIndex(currentState, currentCupIndex, pickedUpCupIndexes);
                currentState = GetNextState(currentState, currentCupIndex, destinationCupIndex, pickedUpCupIndexes);
                currentCupIndex = (currentCupIndex + 1) % currentState.Length;
            }
            return currentState;
        }

        public static int[] GetNextState(
            int[] currentState, 
            int currentCupIndex, 
            int destinationCupIndex, 
            int[] pickedUpCupIndexes)
        {
            var result = new int[currentState.Length];
            
            var destinationCupLabel = currentState[destinationCupIndex];
            var pickedUpCupLabels = new int[pickedUpCupIndexes.Length];
            for (int i = 0; i < pickedUpCupIndexes.Length; i++)
            {
                pickedUpCupLabels[i] = currentState[pickedUpCupIndexes[i]];
            }

            int moveToIndexOffset = 0;
            for (int i = 1; i <= currentState.Length - pickedUpCupIndexes.Length; i++)
            {
                var getFromIndex = (currentCupIndex + i + pickedUpCupIndexes.Length) % currentState.Length;
                var moveToIndex = (currentCupIndex + i + moveToIndexOffset) % currentState.Length;

                result[moveToIndex] = currentState[getFromIndex];

                var getFromLabel = currentState[getFromIndex];
                if (getFromLabel == destinationCupLabel)
                {
                    for (int j = 0; j < pickedUpCupIndexes.Length; j++)
                    {
                        var insertionIndex = (moveToIndex + 1 + j) % currentState.Length;
                        result[insertionIndex] = pickedUpCupLabels[j];
                    }
                    moveToIndexOffset = pickedUpCupIndexes.Length;
                }

            }

            return result;
        }

        public static int GetDestinationCupIndex(
            int[] currentState, 
            int currentCupIndex,
            int[] pickedUpCupIndexes)
        {
            var result = -1;
            var currentCupLabel = currentState[currentCupIndex];
            var labelToIndexDictionary = new Dictionary<int, int>();
            var pickedUpCupLabels = new HashSet<int>();

            // Create dictionary of cup labels and indexes for easy lookup
            int maxLabel = currentState[0];
            int minLabel = currentState[0];
            for (int i = 0; i < currentState.Length; i++)
            {
                var cupLabel = currentState[i];
                labelToIndexDictionary.Add(cupLabel, i);
                if (cupLabel < minLabel)
                {
                    minLabel = cupLabel;
                }
                if (cupLabel > maxLabel)
                {
                    maxLabel = cupLabel;
                }
            }

            // Create set of picked up cup labels for easy lookup
            for (int i = 0; i < pickedUpCupIndexes.Length; i++)
            {
                pickedUpCupLabels.Add(currentState[pickedUpCupIndexes[i]]);
            }

            var minMaxDelta = maxLabel - minLabel;
            for (int labelOffset = 1; labelOffset <= minMaxDelta; labelOffset++)
            {
                var destinationLabel = currentCupLabel - labelOffset;
                if (destinationLabel < minLabel)
                {
                    destinationLabel += minMaxDelta + 1;
                }
                if (!labelToIndexDictionary.ContainsKey(destinationLabel))
                    continue;
                if (pickedUpCupLabels.Contains(destinationLabel))
                    continue;
                result = labelToIndexDictionary[destinationLabel];
                break;
            }

            return result;
        }

        public static int[] GetPickedUpCupIndexes(
            int[] currentState, 
            int currentCupIndex, 
            int numberOfCupsToPickUp)
        {
            var result = new int[numberOfCupsToPickUp];
            for (int i = 0; i < result.Length; i++)
            {
                var cupToPickUpIndex = (currentCupIndex + 1 + i) % currentState.Length;
                result[i] = cupToPickUpIndex;
            }
            return result;
        }

        public static IList<int> ParseInputLine(string inputLine)
        {
            var result = new List<int>();
            foreach (var numberString in inputLine.ToCharArray())
            {
                var number = int.Parse(numberString.ToString());
                result.Add(number);
            }
            return result;
        }
    }
}
