using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day05
{
    public static class BinarySpacePartitioningHelper
    {
        public const string BinarySpacePartitionPattern = @"^((F|B)+)((L|R)+)$";
        public static int GetSeatRow(string seatCode)
        {
            var result = -1;
            var match = Regex.Match(seatCode, BinarySpacePartitionPattern);
            if (match.Success)
            {
                var rowCodeBinary = match.Groups[1].Value
                    .Replace("F", "0")
                    .Replace("B", "1");
                result = Convert.ToInt32(rowCodeBinary, 2);
            }
            return result;
        }

        public static int GetSeatColumn(string seatCode)
        {
            var result = -1;
            var match = Regex.Match(seatCode, BinarySpacePartitionPattern);
            if (match.Success)
            {
                var rowCodeBinary = match.Groups[3].Value
                    .Replace("L", "0")
                    .Replace("R", "1");
                result = Convert.ToInt32(rowCodeBinary, 2);
            }
            return result;
        }
    }
}
