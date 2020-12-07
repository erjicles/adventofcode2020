using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day07
{
    public class BagPolicy
    {
        public string BagType { get; private set; }
        public IList<Tuple<string, int>> ContentsRequirements { get; private set; }
        public BagPolicy(
            string bagType, 
            IList<Tuple<string, int>> contentsRequirements)
        {
            BagType = bagType;
            ContentsRequirements = contentsRequirements;
        }
    }
}
