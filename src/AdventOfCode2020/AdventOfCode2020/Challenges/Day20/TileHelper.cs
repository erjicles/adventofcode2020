using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day20
{
    public static class TileHelper
    {
        public static IDictionary<TileOrientation, IDictionary<MovementDirection, string>> GetOrientationEdgeKeys(IList<string> tileDefinition)
        {
            var result = new Dictionary<TileOrientation, IDictionary<MovementDirection, string>>();
            foreach (var orientation in TileOrientation.TileOrientations)
            {
                var tileOrientation = GetTileOrientation(tileDefinition, orientation);
                var edgeKeys = new Dictionary<MovementDirection, string>();
                var edges = new List<MovementDirection>()
                {
                    MovementDirection.Up,
                    MovementDirection.Down,
                    MovementDirection.Left,
                    MovementDirection.Right
                };
                foreach (var edge in edges)
                {
                    var edgeKey = GetTileEdgeKey(tileOrientation, edge);
                    edgeKeys.Add(edge, edgeKey);
                }
                
                result.Add(orientation, edgeKeys);
            }
            return result;
        }

        public static string GetTileEdgeKey(IList<string> tileDefinition, MovementDirection edge)
        {
            if (tileDefinition == null || tileDefinition.Count == 0)
            {
                return string.Empty;
            }

            if (MovementDirection.Up.Equals(edge))
            {
                return tileDefinition[0];
            }
            else if (MovementDirection.Down.Equals(edge))
            {
                return tileDefinition[tileDefinition.Count - 1];
            }
            else if (MovementDirection.Left.Equals(edge))
            {
                var result = new StringBuilder();
                for (int i = 0; i < tileDefinition.Count; i++)
                {
                    result.Append(tileDefinition[i][0]);
                }
                return result.ToString();
            }
            else if (MovementDirection.Right.Equals(edge))
            {
                var result = new StringBuilder();
                for (int i = 0; i < tileDefinition.Count; i++)
                {
                    result.Append(tileDefinition[i][^1]);
                }
                return result.ToString();
            }
            else
            {
                throw new Exception($"Invalid edge: {edge}");
            }
        }

        public static IDictionary<TileOrientation, IList<string>> GetTileOrientations(IList<string> tileDefinition)
        {
            var result = new Dictionary<TileOrientation, IList<string>>();
            foreach (var orientation in TileOrientation.TileOrientations)
            {
                var tileOrientation = GetTileOrientation(tileDefinition, orientation);
                result.Add(orientation, tileOrientation);
            }
            return result;
        }

        public static IList<string> GetTileOrientation(
            IList<string> tileDefinition, 
            TileOrientation tileOrientation)
        {
            var result = GetTileRotation(tileDefinition, tileOrientation.RotationDegrees);
            if (tileOrientation.IsReflectedHorizontally)
            {
                result = GetTileReflectionHorizontal(result);
            }
            return result;
        }

        public static IList<string> GetTileReflectionHorizontal(IList<string> tileDefinition)
        {
            var result = tileDefinition.ToArray();
            for (int i = 0; i < result.Length; i++)
            {
                char[] charArray = result[i].ToCharArray();
                Array.Reverse(charArray);
                result[i] = new string(charArray);
            }
            return result;
        }

        public static IList<string> GetTileRotation(IList<string> tileDefinition, int rotationDegrees)
        {
            if (tileDefinition == null || tileDefinition.Count == 0)
                return tileDefinition;

            var stringLength = tileDefinition[0].Length;
            if (rotationDegrees == 0)
            {
                return tileDefinition.ToList();
            }
            else if (rotationDegrees == 90)
            {
                var result = new List<string>();
                for (int row = 0; row < stringLength; row++)
                {
                    var rowString = new StringBuilder();
                    for (int col = 0; col < tileDefinition.Count; col++)
                    {
                        rowString.Append(tileDefinition[col][stringLength - 1 - row]);
                    }
                    result.Add(rowString.ToString());
                }
                return result;
            }
            else if (rotationDegrees == 180)
            {
                var result = tileDefinition.ToList();
                for (int i = 0; i < result.Count; i++)
                {
                    char[] charArray = result[i].ToCharArray();
                    Array.Reverse(charArray);
                    result[i] = new string(charArray);
                }
                result.Reverse();
                return result;
            }
            else if (rotationDegrees == 270)
            {
                var result = new List<string>();
                for (int row = 0; row < stringLength; row++)
                {
                    var rowString = new StringBuilder();
                    for (int col = 0; col < tileDefinition.Count; col++)
                    {
                        rowString.Append(tileDefinition[tileDefinition.Count - 1 - col][row]);
                    }
                    result.Add(rowString.ToString());
                }
                return result;
            }
            else
            {
                throw new Exception($"Invalid rotation degrees: {rotationDegrees}");
            }
        }
    }
}
