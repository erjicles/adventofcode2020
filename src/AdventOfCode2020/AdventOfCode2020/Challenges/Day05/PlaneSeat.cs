using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day05
{
    public class PlaneSeat
    {
        public string SeatCode { get; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public PlaneSeat(string seatCode)
        {
            SeatCode = seatCode;
            Row = BinarySpacePartitioningHelper.GetSeatRow(seatCode);
            Column = BinarySpacePartitioningHelper.GetSeatColumn(seatCode);
        }

        public int SeatId
        {
            get
            {
                var result = (Row * 8) + Column;
                return result;
            }
        }
    }
}
