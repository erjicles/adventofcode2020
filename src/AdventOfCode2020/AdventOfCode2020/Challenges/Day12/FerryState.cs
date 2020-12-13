using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day12
{
    public class FerryState
    {
        public GridPoint Position { get; private set; } = GridPoint.Origin;
        public FerryMovementDirection Heading { get; private set; } = FerryMovementDirection.East;
        public GridPoint WaypointPosition { get; private set; } = GridPoint.Origin;
        public FerryState()
        {

        }

        public FerryState(GridPoint position, FerryMovementDirection heading)
        {
            Position = position;
            Heading = heading;
        }

        public FerryState(GridPoint position, GridPoint waypointPosition)
        {
            Position = position;
            WaypointPosition = waypointPosition;
        }
    }
}
