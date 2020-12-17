using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Grid
{
    public class GridPoint4D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
        public GridPoint XYPoint { get { return new GridPoint(X, Y); } }
        public static GridPoint4D Origin = new GridPoint4D(0, 0, 0, 0);

        public GridPoint4D()
        {
            X = 0;
            Y = 0;
            Z = 0;
            W = 0;
        }

        public GridPoint4D(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public GridPoint4D(GridPoint point2D)
        {
            X = point2D.X;
            Y = point2D.Y;
            Z = 0;
            W = 0;
        }

        public GridPoint4D(GridPoint point2D, int z, int w)
        {
            X = point2D.X;
            Y = point2D.Y;
            Z = z;
            W = w;
        }

        public GridPoint4D Move(MovementDirection direction, int d)
        {
            return direction switch
            {
                MovementDirection.Down => MoveDown(d),
                MovementDirection.Left => MoveLeft(d),
                MovementDirection.Right => MoveRight(d),
                MovementDirection.Up => MoveUp(d),
                MovementDirection.In => MoveIn(d),
                MovementDirection.Out => MoveOut(d),
                MovementDirection.Up4D => MoveUp4D(d),
                MovementDirection.Down4D => MoveDown4D(d),
                _ => throw new Exception($"Invalid movement direction {direction}"),
            };
        }

        public GridPoint4D Move(GridPoint4D displacementVector)
        {
            return Move(Tuple.Create(
                displacementVector.X,
                displacementVector.Y,
                displacementVector.Z,
                displacementVector.W));
        }

        public GridPoint4D Move(Tuple<int, int, int, int> displacementVector)
        {
            return new GridPoint4D(
                X + displacementVector.Item1,
                Y + displacementVector.Item2,
                Z + displacementVector.Item3,
                W + displacementVector.Item4);
        }

        public GridPoint4D MoveRight(int d)
        {
            return new GridPoint4D
            {
                X = X + d,
                Y = Y,
                Z = Z,
                W = W
            };
        }
        public GridPoint4D MoveLeft(int d)
        {
            return new GridPoint4D
            {
                X = X - d,
                Y = Y,
                Z = Z,
                W = W
            };
        }
        public GridPoint4D MoveUp(int d)
        {
            return new GridPoint4D
            {
                X = X,
                Y = Y + d,
                Z = Z,
                W = W
            };
        }
        public GridPoint4D MoveDown(int d)
        {
            return new GridPoint4D
            {
                X = X,
                Y = Y - d,
                Z = Z,
                W = W
            };
        }

        public GridPoint4D MoveIn(int d)
        {
            return new GridPoint4D
            {
                X = X,
                Y = Y,
                Z = Z - d,
                W = W
            };
        }

        public GridPoint4D MoveOut(int d)
        {
            return new GridPoint4D
            {
                X = X,
                Y = Y,
                Z = Z + d,
                W = W
            };
        }

        public GridPoint4D MoveUp4D(int d)
        {
            return new GridPoint4D
            {
                X = X,
                Y = Y,
                Z = Z,
                W = W + d
            };
        }

        public GridPoint4D MoveDown4D(int d)
        {
            return new GridPoint4D
            {
                X = X,
                Y = Y,
                Z = Z,
                W = W - d
            };
        }

        public static bool GetAreAdjacent(GridPoint4D p1, GridPoint4D p2)
        {
            return GetManhattanDistance(p1, p2) == 1;
        }

        /// <summary>
        /// Computes the manhattan distance between two points on the grid.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static int GetManhattanDistance(GridPoint4D p1, GridPoint4D p2)
        {
            return Math.Abs(p1.X - p2.X)
                + Math.Abs(p1.Y - p2.Y)
                + Math.Abs(p1.Z - p2.Z)
                + Math.Abs(p1.W - p2.W);
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
                GridPoint4D p = (GridPoint4D)obj;
                return (X == p.X) && (Y == p.Y) && (Z == p.Z) && (W == p.W);
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(X, Y, Z, W);
            int hash = tuple.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Format("GridPoint4D({0}, {1}, {2}, {3})", X, Y, Z, W);
        }
    }
}
