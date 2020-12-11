using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day11
{
    public class WaitingArea
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Dictionary<GridPoint, CellType> GridCellTypes { get; private set; } = new Dictionary<GridPoint, CellType>();

        public WaitingArea(int width, int height, Dictionary<GridPoint, CellType> gridCellTypes)
        {
            Width = width;
            Height = height;
            GridCellTypes = gridCellTypes;
        }

        public int NumberOfOccupiedSeats
        {
            get
            {
                var result = GridCellTypes
                    .Where(kvp => CellType.ChairOccupied.Equals(kvp.Value))
                    .Count();
                return result;
            }
        }

        // Equals, GetHashCode, and ToString() adapted from Microsoft example here:
        // https://docs.microsoft.com/en-us/dotnet/api/system.object.equals?view=netcore-3.1
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                WaitingArea other = (WaitingArea)obj;
                if (Width != other.Width)
                {
                    return false;
                }
                if (Height != other.Height)
                {
                    return false;
                }
                return GridCellTypes.Count == other.GridCellTypes.Count
                    && !GridCellTypes.Except(other.GridCellTypes).Any();
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(Width, Height, GridCellTypes);
            int hash = tuple.GetHashCode();
            return hash;
        }
    }
}
