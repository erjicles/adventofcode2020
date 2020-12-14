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
        public static long GetSumOfMemoryValues(Dictionary<int, long> memoryBank)
        {
            var result = memoryBank.Select(kvp => kvp.Value).Sum();
            return result;
        }

        public static Dictionary<int, long> RunProgram(IList<string> initializationProgram)
        {
            var result = new Dictionary<int, long>();
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
                    var valueWithMaskApplied = GetValueWithMaskApplied(memoryInstructionData.Item2, currentMask);
                    result[memoryInstructionData.Item1] = valueWithMaskApplied;
                }
                else
                {
                    throw new Exception($"Invalid instruction type: {instructionType}");
                }
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

        public static Tuple<int, long> ParseWriteToMemoryInstruction(string writeToMemoryInputLine)
        {
            var match = Regex.Match(writeToMemoryInputLine, @"^mem\[(\d+)\] = (\d+)$");
            if (!match.Success)
            {
                throw new Exception($"Invalid write to memory instruction: {writeToMemoryInputLine}");
            }
            var memoryLocation = int.Parse(match.Groups[1].Value);
            var decomalValue = long.Parse(match.Groups[2].Value);
            var result = new Tuple<int, long>(memoryLocation, decomalValue);
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
