using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day16
{
    public class Ticket
    {
        public IList<int> FieldValues { get; private set; }
        public Ticket(IList<int> fieldValues)
        {
            FieldValues = fieldValues;
        }
    }
}
