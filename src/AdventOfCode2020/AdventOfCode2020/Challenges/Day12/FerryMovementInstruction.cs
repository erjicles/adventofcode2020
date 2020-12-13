using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day12
{
    public class FerryMovementInstruction
    {
        public FerryMovementDirection MovementDirection { get; set; }
        public int Value { get; set; }
        public FerryMovementInstruction(FerryMovementDirection ferryMovementDirection, int value)
        {
            MovementDirection = ferryMovementDirection;
            Value = value;
        }
    }
}
