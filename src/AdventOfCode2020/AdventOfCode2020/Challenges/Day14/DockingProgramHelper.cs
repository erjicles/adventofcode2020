using AdventOfCode2020.MathHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day14
{
    public static class DockingProgramHelper
    {
        public static long GetSumOfMemoryValues(Dictionary<long, long> memoryBank)
        {
            var result = memoryBank.Select(kvp => kvp.Value).Sum();
            return result;
        }

        public static Dictionary<long, long> RunProgram(IList<string> initializationProgram, int programVersion = 1)
        {
            if (programVersion < 1 || programVersion > 2)
                throw new Exception($"Invalid program version: {programVersion}");
            var result = new Dictionary<long, long>();
            var currentMask = string.Empty;
            foreach (var instruction in initializationProgram)
            {
                var instructionType = GetInstructionType(instruction);
                if (InstructionType.Mask.Equals(instructionType))
                {
                    currentMask = ParseMask(instruction);
                }
                else if (InstructionType.WriteToMemory.Equals(instructionType))
                {
                    var memoryInstructionData = ParseWriteToMemoryInstruction(instruction);
                    if (programVersion == 1)
                    {
                        var valueWithMaskApplied = GetValueWithMaskApplied(memoryInstructionData.Item2, currentMask);
                        result[memoryInstructionData.Item1] = valueWithMaskApplied;
                    }
                    else
                    {
                        var addressesWithMaskApplied = GetAddressesWithMaskApplied(memoryInstructionData.Item1, currentMask);
                        foreach (var address in addressesWithMaskApplied)
                        {
                            result[address] = memoryInstructionData.Item2;
                        }
                    }
                }
                else
                {
                    throw new Exception($"Invalid instruction type: {instructionType}");
                }
            }
            return result;
        }

        public static IList<long> GetAddressesWithMaskApplied(long decimalAddress, string mask)
        {
            var result = new List<long>();
            var binaryValueBitArray = Convert.ToString(decimalAddress, 2).PadLeft(36, '0').ToCharArray();
            for (int i = 0; i < 36; i++)
            {
                var maskBit = mask[i];
                if ('0'.Equals(maskBit))
                    continue;
                binaryValueBitArray[i] = maskBit;
            }
            var numberOfFloatingBits = binaryValueBitArray.Where(b => 'X'.Equals(b)).Count();
            var floatingBitCombinations = CombinationsHelper.GetAllPossibleOutcomesOfNExperiments(new char[] { '0', '1' }, numberOfFloatingBits);
            var floatingBitPositions = new List<int>();
            for (int i = 0; i < 36; i++)
            {
                if ('X'.Equals(binaryValueBitArray[i]))
                {
                    floatingBitPositions.Add(i);
                }
            }
            var addressBitArrays = new List<char[]>();
            foreach (var floatingBitComination in floatingBitCombinations)
            {
                var currentAddressBitArray = binaryValueBitArray.ToArray();
                for (int i = 0; i < numberOfFloatingBits; i++)
                {
                    var floatingBitPosition = floatingBitPositions[i];
                    currentAddressBitArray[floatingBitPosition] = floatingBitComination[i];
                }
                addressBitArrays.Add(currentAddressBitArray);
            }
            foreach (var addressBitArray in addressBitArrays)
            {
                var addressBinaryString = new string(addressBitArray);
                var address = Convert.ToInt64(addressBinaryString, 2);
                result.Add(address);
            }
            
            return result;
        }

        public static long GetValueWithMaskApplied(long decimalValue, string mask)
        {
            var binaryValueBitArray = Convert.ToString(decimalValue, 2).PadLeft(36, '0').ToCharArray();
            for (int i = 0; i < 36; i++)
            {
                var maskBit = mask[i];
                if ('X'.Equals(maskBit))
                    continue;
                binaryValueBitArray[i] = maskBit;
            }
            var resultString = new string(binaryValueBitArray);
            var result = Convert.ToInt64(resultString, 2);
            return result;
        }

        public static Tuple<long, long> ParseWriteToMemoryInstruction(string writeToMemoryInputLine)
        {
            var match = Regex.Match(writeToMemoryInputLine, @"^mem\[(\d+)\] = (\d+)$");
            if (!match.Success)
            {
                throw new Exception($"Invalid write to memory instruction: {writeToMemoryInputLine}");
            }
            var memoryLocation = long.Parse(match.Groups[1].Value);
            var decomalValue = long.Parse(match.Groups[2].Value);
            var result = new Tuple<long, long>(memoryLocation, decomalValue);
            return result;
        }

        public static string ParseMask(string maskInputLine)
        {
            var match = Regex.Match(maskInputLine, @"^mask = ((0|1|X){36})$");
            if (!match.Success)
            {
                throw new Exception($"Invalid mask input line: {maskInputLine}");
            }
            var mask = match.Groups[1].Value;
            return mask;
        }

        public static InstructionType GetInstructionType(string inputLine)
        {
            if (inputLine.StartsWith("mask"))
            {
                return InstructionType.Mask;
            }
            else if (inputLine.StartsWith("mem"))
            {
                return InstructionType.WriteToMemory;
            }
            else
            {
                throw new Exception($"Invalid input line: {inputLine}");
            }
        }
    }
}
