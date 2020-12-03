using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day03
{
    public class SlopeMap
    {
        public Dictionary<GridPoint, CellType> GridCellTypes = new Dictionary<GridPoint, CellType>();
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public SlopeMap(int width, int height, Dictionary<GridPoint, CellType> gridCellTypes)
        {
            Width = width;
            Height = height;
            GridCellTypes = gridCellTypes;
        }

        public GridPoint NormalizeGridPointX(GridPoint rawPoint)
        {
            return new GridPoint(rawPoint.X % Width, rawPoint.Y);
        }
    }
}
