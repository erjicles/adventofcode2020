using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day08
{
    public static class BootCodeHelper
    {
        public static BootCodeInstruction ParseBootCodeInstruction(string instructionLine)
        {
            var match = Regex.Match(instructionLine, @"^(\w+) ((\+|-)\d+)$");
            if (!match.Success)
            {
                throw new Exception($"Unrecognized instruction line: {instructionLine}");
            }
            var instruction = match.Groups[1].Value.Trim();
            var value = int.Parse(match.Groups[2].Value.Trim());
            var result = new BootCodeInstruction(instruction, value);
            return result;
        }

        public static BootCode ParseInputLines(IList<string> inputLines)
        {
            var bootCodeInstructions = new List<BootCodeInstruction>();
            foreach (var inputLine in inputLines)
            {
                var instruction = ParseBootCodeInstruction(inputLine);
                bootCodeInstructions.Add(instruction);
            }
            var result = new BootCode(bootCodeInstructions);
            return result;
        }

        public static int RunProgram(BootCode bootCode)
        {
            var accumulator = 0;
            var instructionsExecuted = new HashSet<int>();
            var currentInstructionIndex = 0;
            while (!instructionsExecuted.Contains(currentInstructionIndex))
            {
                instructionsExecuted.Add(currentInstructionIndex);
                if (currentInstructionIndex < 0 || currentInstructionIndex >= bootCode.Instructions.Count)
                {
                    throw new Exception($"Instruction index out of range - Current index: {currentInstructionIndex}, # Instructions: {bootCode.Instructions.Count}");
                }
                var instruction = bootCode.Instructions[currentInstructionIndex];
                if ("acc".Equals(instruction.Instruction))
                {
                    accumulator += instruction.Value;
                    currentInstructionIndex++;
                }
                else if ("jmp".Equals(instruction.Instruction))
                {
                    currentInstructionIndex += instruction.Value;
                }
                else if ("nop".Equals(instruction.Instruction))
                {
                    currentInstructionIndex++;
                }
                else
                {
                    throw new Exception($"Invalid instruction: {instruction.Instruction}");
                }
            }

            return accumulator;
        }
    }
}
