using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day20
{
    public class Tile
    {
        public int TileId { get; private set; }
        public IList<string> TileDefinition { get; private set; }
        public IDictionary<TileOrientation, IList<string>> Orientations { get; private set; }
        public IDictionary<TileOrientation, IDictionary<MovementDirection, string>> OrientationEdgeKeys { get; private set; }
        public Tile(int tileId, IList<string> tileDefinition)
        {
            TileId = tileId;
            TileDefinition = tileDefinition;
            Orientations = TileHelper.GetTileOrientations(TileDefinition);
            OrientationEdgeKeys = TileHelper.GetOrientationEdgeKeys(tileDefinition);
        }
    }
}
