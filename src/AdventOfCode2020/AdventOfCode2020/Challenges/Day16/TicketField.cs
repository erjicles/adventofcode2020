using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day16
{
    public class TicketField
    {
        public string Name { get; private set; }
        public IList<Tuple<int, int>> Ranges { get; private set; }

        public TicketField(string name, IList<Tuple<int, int>> ranges)
        {
            Name = name;
            Ranges = ranges;
        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TicketField other = (TicketField)obj;
                return string.Equals(Name, other.Name)
                    && Ranges.Count == other.Ranges.Count
                    && !Ranges.Where(r => !other.Ranges.Contains(r)).Any();
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(Name, Ranges);
            int hash = tuple.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"{Name}: ");
            bool firstRange = true;
            foreach (var range in Ranges)
            {
                if (!firstRange)
                    result.Append(" or ");
                firstRange = false;
                result.Append($"{range.Item1}-{range.Item2}");
            }
            return result.ToString();
        }
    }
}
