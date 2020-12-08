using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day08
{
    public class BootCode
    {
        public IList<BootCodeInstruction> Instructions { get; private set; }

        public BootCode(IList<BootCodeInstruction> instructions)
        {
            Instructions = instructions;
        }

    }
}
