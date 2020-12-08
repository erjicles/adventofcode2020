using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day08
{
    public class BootCodeInstruction
    {
        public string Instruction { get; private set; } = string.Empty;
        public int Value { get; private set; } = 0;

        public BootCodeInstruction(string instruction, int value)
        {
            Instruction = instruction;
            Value = value;
        }
    }
}
