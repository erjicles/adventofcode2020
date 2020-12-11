using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day11
{
    public class EvolutionRules
    {
        public int MinimumNumberOfSeenOccupiedSeatsToFlipToEmpty { get; set; } = 4;
        public bool OnlyConsiderAdjacentCells { get; set; } = true;
    }
}
