using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day23
{
    public static class CrabCupHelper
    {
        public static string GetCanonicalCrabCupString(IList<int> cups)
        {
            var canonicalCupOrder = new List<int>();
            var cup1Index = cups.IndexOf(1);
            for (int indexOffset = 1; indexOffset < cups.Count; indexOffset++)
            {
                var cupIndex = (cup1Index + indexOffset) % cups.Count;
                var cupLabel = cups[cupIndex];
                canonicalCupOrder.Add(cupLabel);
            }
            var result = string.Join("", canonicalCupOrder);
            return result;
        }
        public static IList<int> PlayCrabCups(
            IList<int> startingNumbers,
            int numberOfCupsToPickUp,
            int numberOfRounds)
        {
            var numberLinkedList = new LinkedList<int>(startingNumbers);
            var labelToNodeDictionary = new Dictionary<int, LinkedListNode<int>>();
            var currentNode = numberLinkedList.First;
            for (int i = 0; i < startingNumbers.Count; i++)
            {
                labelToNodeDictionary.Add(currentNode.Value, currentNode);
                currentNode = currentNode.Next;
            }

            var currentCupNode = numberLinkedList.First;
            var startTime = DateTime.Now;
            int pingRounds = 1000000;
            for (int roundNumber = 1; roundNumber <= numberOfRounds; roundNumber++)
            {
                var cupsToRemoveNodes = new List<LinkedListNode<int>>();

                if (roundNumber % pingRounds == 0)
                {
                    var endTime = DateTime.Now;
                    var timeDiffSeconds = (endTime - startTime).TotalSeconds;
                    var secondsPerRound = (double)timeDiffSeconds / pingRounds;
                    var totalSeconds = numberOfRounds * secondsPerRound;
                    Console.WriteLine($"---> Round {roundNumber}: {timeDiffSeconds} seconds; Estimated total time for all: {totalSeconds}");
                    startTime = DateTime.Now;
                }

                var nextCupNode = currentCupNode;
                for (int i = 0; i < numberOfCupsToPickUp; i++)
                {
                    nextCupNode = nextCupNode.Next;
                    if (nextCupNode == null)
                    {
                        nextCupNode = numberLinkedList.First;
                    }
                    cupsToRemoveNodes.Add(nextCupNode);
                }

                var destinationCup = currentCupNode.Value;
                for (int i = 1; i < startingNumbers.Count; i++)
                {
                    destinationCup--;
                    if (destinationCup < 1)
                    {
                        destinationCup += startingNumbers.Count;
                    }
                    if (!cupsToRemoveNodes.Select(n => n.Value).Contains(destinationCup))
                    {
                        break;
                    }
                }

                //Console.WriteLine($"Round: {roundNumber}");
                //Console.WriteLine($"{string.Join(" ", numberLinkedList.Select(cup => currentCupNode.Value == cup ? $"({cup})" : $"{cup}"))}");
                //Console.WriteLine($"Picked up cups: {string.Join(",", cupsToRemoveNodes.Select(cn => cn.Value))}");
                //Console.WriteLine($"Destination cup: {destinationCup}");
                //Console.WriteLine("");
                //Console.ReadKey();

                var destinationCupNode = labelToNodeDictionary[destinationCup];
                var destinationNextNode = destinationCupNode.Next;
                
                // Remove them
                foreach (var removeNode in cupsToRemoveNodes)
                {
                    numberLinkedList.Remove(removeNode);
                }

                // Add them
                var addAfterNode = destinationCupNode;
                foreach (var addNode in cupsToRemoveNodes)
                {
                    numberLinkedList.AddAfter(addAfterNode, addNode);
                    addAfterNode = addNode;
                }

                currentCupNode = currentCupNode.Next;
                if (currentCupNode == null)
                {
                    currentCupNode = numberLinkedList.First;
                }

            }

            return numberLinkedList.ToList();
        }

        public static void ProcessRound(
            IList<int> currentState, 
            int currentCupIndex, 
            int destinationCupIndex, 
            IList<int> pickedUpCupIndexes,
            out int nextCurrentCupIndex)
        {
            var totalNumberOfCups = currentState.Count;
            var pickedUpCupLabels = new int[pickedUpCupIndexes.Count];
            for (int i = 0; i < pickedUpCupIndexes.Count; i++)
            {
                pickedUpCupLabels[i] = currentState[pickedUpCupIndexes[i]];
            }

            // First remove them
            var removalIndex = currentCupIndex + 1;
            for (int i = 0; i < pickedUpCupLabels.Length; i++)
            {
                if (removalIndex >= currentState.Count)
                {
                    removalIndex = 0;
                }
                currentState.RemoveAt(removalIndex);
                if (removalIndex < currentCupIndex)
                {
                    currentCupIndex--;
                }
                if (removalIndex < destinationCupIndex)
                {
                    destinationCupIndex--;
                }    
            }

            // Now insert them back
            for (int i = 0; i < pickedUpCupLabels.Length; i++)
            {
                var cupToInsertLabel = pickedUpCupLabels[pickedUpCupLabels.Length - 1 - i];
                var insertionIndex = destinationCupIndex + 1;
                currentState.Insert(insertionIndex, cupToInsertLabel);
                if (currentCupIndex >= insertionIndex)
                {
                    currentCupIndex++;
                }
            }

            nextCurrentCupIndex = (currentCupIndex + 1) % totalNumberOfCups;
        }

        public static int GetDestinationCupIndex(
            IList<int> currentState, 
            int currentCupIndex,
            IList<int> pickedUpCupIndexes)
        {
            var result = -1;
            var currentCupLabel = currentState[currentCupIndex];
            var pickedUpCupLabels = pickedUpCupIndexes.Select(i => currentState[i]).ToHashSet();

            // Create dictionary of cup labels and indexes for easy lookup
            var maxLabel = currentState.Count;
            var minLabel = 1;
            //var labelToIndexDictionary = new Dictionary<int, int>();
            //int maxLabel = currentState[0];
            //int minLabel = currentState[0];
            //for (int i = 0; i < currentState.Count; i++)
            //{
            //    var cupLabel = currentState[i];
            //    labelToIndexDictionary.Add(cupLabel, i);
            //    if (cupLabel < minLabel)
            //    {
            //        minLabel = cupLabel;
            //    }
            //    if (cupLabel > maxLabel)
            //    {
            //        maxLabel = cupLabel;
            //    }
            //}

            

            var minMaxDelta = maxLabel - minLabel;
            for (int labelOffset = 1; labelOffset <= minMaxDelta; labelOffset++)
            {
                var destinationLabel = currentCupLabel - labelOffset;
                if (destinationLabel < minLabel)
                {
                    destinationLabel += minMaxDelta + 1;
                }
                //if (!labelToIndexDictionary.ContainsKey(destinationLabel))
                //    continue;
                if (pickedUpCupLabels.Contains(destinationLabel))
                    continue;
                // labelToIndexDictionary[destinationLabel];
                result = currentState.IndexOf(destinationLabel);
                break;
            }

            return result;
        }


        public static IList<int> GetPickedUpCupIndexes(
            IList<int> currentState, 
            int currentCupIndex, 
            int numberOfCupsToPickUp)
        {
            var result = new List<int>();
            for (int i = 0; i < numberOfCupsToPickUp; i++)
            {
                var cupToPickUpIndex = (currentCupIndex + 1 + i) % currentState.Count;
                result.Add(cupToPickUpIndex);
            }
            return result;
        }

        public static bool GetAreEquivalent(IList<int> left, IList<int> right)
        {
            if (left.Count != right.Count)
                return false;
            if (left.Count == 0)
                return true;
            var startNumber = left[0];
            var rightStartIndex = right.IndexOf(startNumber);
            for (int i = 0; i < left.Count; i++)
            {
                var rightIndex = (rightStartIndex + i) % left.Count;
                if (left[i] != right[rightIndex])
                    return false;
            }
            return true;
        }

        public static IList<int> GetPart2StartingNumbers(IList<int> initialStartingNumbers)
        {
            var result = initialStartingNumbers.ToList();
            for (int i = initialStartingNumbers.Count + 1; i <= 1000000; i++)
            {
                result.Add(i);
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
