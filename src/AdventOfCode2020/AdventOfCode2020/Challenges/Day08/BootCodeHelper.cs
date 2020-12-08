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

        public static bool TryFindFixedProgram(BootCode originalBootCode, out BootCode fixedBootCode, out int accumulator)
        {
            fixedBootCode = null;
            accumulator = 0;
            var possiblyFixedBootCodes = GetPossiblyFixedBootCodes(originalBootCode);
            foreach (var possiblyFixedBootCode in possiblyFixedBootCodes)
            {
                var isSuccessfulTermination = TryRunProgram(possiblyFixedBootCode, out int possibleAccumulator);
                if (isSuccessfulTermination)
                {
                    fixedBootCode = possiblyFixedBootCode;
                    accumulator = possibleAccumulator;
                    return true;
                }
            }
            return false;
        }

        public static IList<BootCode> GetPossiblyFixedBootCodes(BootCode bootCode)
        {
            var result = new List<BootCode>();
            for (int i = 0; i < bootCode.Instructions.Count; i++)
            {
                var currentInstruction = bootCode.Instructions[i];
                if ("jmp".Equals(currentInstruction.Instruction)
                    || "nop".Equals(currentInstruction.Instruction))
                {
                    var instructionsCopy = bootCode.Instructions.ToList();
                    var newInstruction = "jmp".Equals(currentInstruction.Instruction) ? "nop" : "jmp";
                    instructionsCopy[i] = new BootCodeInstruction(newInstruction, currentInstruction.Value);
                    var newBootCode = new BootCode(instructionsCopy);
                    result.Add(newBootCode);
                }
            }
            return result;
        }

        /// <summary>
        /// Runs the given boot code program.
        /// Returns true if the program terminates correctly.
        /// Otherwise, returns false.
        /// </summary>
        /// <param name="bootCode"></param>
        /// <param name="accumulator"></param>
        /// <returns></returns>
        public static bool TryRunProgram(BootCode bootCode, out int accumulator)
        {
            accumulator = 0;
            var instructionsExecuted = new HashSet<int>();
            var currentInstructionIndex = 0;
            while (!instructionsExecuted.Contains(currentInstructionIndex))
            {
                instructionsExecuted.Add(currentInstructionIndex);
                if (currentInstructionIndex == bootCode.Instructions.Count)
                {
                    // Terminate when trying to execute the instruction immediately after the last one
                    return true;
                }
                if (currentInstructionIndex < 0 || currentInstructionIndex > bootCode.Instructions.Count)
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

            return false;
        }
    }
}
