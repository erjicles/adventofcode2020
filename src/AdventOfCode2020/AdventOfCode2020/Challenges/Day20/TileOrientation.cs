using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day20
{
    public class TileOrientation
    {
        public int RotationDegrees { get; private set; }
        public bool IsReflectedHorizontally { get; private set; }
        public TileOrientation(int rotationDegrees, bool isReflectedHorizontally)
        {
            RotationDegrees = rotationDegrees % 360;
            if (RotationDegrees != 0
                && RotationDegrees != 90
                && RotationDegrees != 180
                && RotationDegrees != 270)
            {
                throw new Exception($"Invalid rotation degrees: {RotationDegrees}");
            }
            IsReflectedHorizontally = isReflectedHorizontally;
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
                TileOrientation other = (TileOrientation)obj;
                return (RotationDegrees == other.RotationDegrees)
                    && (IsReflectedHorizontally == other.IsReflectedHorizontally);
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(RotationDegrees, IsReflectedHorizontally);
            int hash = tuple.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Format($"Orientation(Rotation: {RotationDegrees}, H-Reflection:{IsReflectedHorizontally})");
        }

        public static IList<TileOrientation> TileOrientations { get; private set; } = new List<TileOrientation>()
        {
            new TileOrientation(0, false),
            new TileOrientation(0, true),
            new TileOrientation(90, false),
            new TileOrientation(90, true),
            new TileOrientation(180, false),
            new TileOrientation(180, true),
            new TileOrientation(270, false),
            new TileOrientation(270, true),
        };
    }
}
