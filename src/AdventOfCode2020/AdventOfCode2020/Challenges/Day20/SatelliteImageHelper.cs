using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day20
{
    public static class SatelliteImageHelper
    {
        public static bool TryGetTilePositionsAndOrientations(
            IList<Tile> inputTiles,
            out IList<IList<Tuple<int, TileOrientation>>> tilePlacements)
        {
            var tileIdDictionary = inputTiles.ToDictionary(tile => tile.TileId);
            var tileIdSet = inputTiles.Select(tile => tile.TileId).ToHashSet();
            tilePlacements = null;

            if (!TryGetSquareEdgeLength(inputTiles.Count, out int edgeLength))
            {
                throw new Exception($"Cannot create square from tiles given: {inputTiles.Count}");
            }

            // Each candidate contains the currently placed tiles/orientations, and the set of remaining tile ids to place
            var candidates = new Stack<Tuple<IList<IList<Tuple<int, TileOrientation>>>, HashSet<int>>>();

            // Seed the candidates with each tile's possible orientations at the top-left
            foreach (var tile in inputTiles)
            {
                foreach (var orientation in TileOrientation.TileOrientations)
                {
                    var candidateGrid = new List<IList<Tuple<int, TileOrientation>>>();
                    var candidateFirstRow = new List<Tuple<int, TileOrientation>>();
                    candidateFirstRow.Add(new Tuple<int, TileOrientation>(tile.TileId, orientation));
                    candidateGrid.Add(candidateFirstRow);
                    var remainingTileIds = tileIdSet.ToHashSet();
                    remainingTileIds.Remove(tile.TileId);

                    var candidate = new Tuple<IList<IList<Tuple<int, TileOrientation>>>, HashSet<int>>(
                        candidateGrid,
                        remainingTileIds);
                    candidates.Push(candidate);
                }
                
            }

            // Main loop
            // For each candidate, attempt to place a valid tile at the next position
            // If all tiles are placed, return that placement
            while (candidates.Count > 0)
            {
                var candidate = candidates.Pop();
                var candidatesToPush = new List<Tuple<IList<IList<Tuple<int, TileOrientation>>>, HashSet<int>>>();

                var currentPlacements = candidate.Item1;
                var remainingTileIds = candidate.Item2;

                // Calculate the next position to consider
                var lastPlacedRow = currentPlacements.Count;
                var lastPlacedColumn = currentPlacements[lastPlacedRow - 1].Count;
                var nextRowIndex = lastPlacedRow - 1;
                var nextColumnIndex = lastPlacedColumn;
                if (lastPlacedColumn >= edgeLength)
                {
                    nextRowIndex = lastPlacedRow;
                    nextColumnIndex = 0;
                }

                // Get the left and up adjacent tiles
                Tuple<int, TileOrientation> tileToLeft = null;
                string tileToLeftEdgeKeyRight = string.Empty;
                Tuple<int, TileOrientation> tileToUp = null;
                string tileToUpEdgeKeyDown = string.Empty;
                if (nextColumnIndex > 0)
                {
                    tileToLeft = currentPlacements[nextRowIndex][nextColumnIndex - 1];
                    tileToLeftEdgeKeyRight = tileIdDictionary[tileToLeft.Item1].OrientationEdgeKeys[tileToLeft.Item2][MovementDirection.Right];
                }
                if (nextRowIndex > 0)
                {
                    tileToUp = currentPlacements[nextRowIndex - 1][nextColumnIndex];
                    tileToUpEdgeKeyDown = tileIdDictionary[tileToUp.Item1].OrientationEdgeKeys[tileToUp.Item2][MovementDirection.Down];
                }

                // Loop through all remaining tile ids
                // If any orientations are valid, add that as a new candidate
                foreach (var nextTileId in remainingTileIds)
                {
                    var nextTile = tileIdDictionary[nextTileId];
                    foreach (var orientation in TileOrientation.TileOrientations)
                    {
                        var edgeKeyLeft = nextTile.OrientationEdgeKeys[orientation][MovementDirection.Left];
                        var edgeKeyUp = nextTile.OrientationEdgeKeys[orientation][MovementDirection.Up];
                        var isValidLeft = tileToLeft == null
                            || string.Equals(tileToLeftEdgeKeyRight, edgeKeyLeft);
                        var isValidTop = tileToUp == null
                            || string.Equals(tileToUpEdgeKeyDown, edgeKeyUp);
                        var isValidPlcement = isValidLeft && isValidTop;
                        if (isValidPlcement)
                        {
                            var nextRemainingTileIds = remainingTileIds.ToHashSet();
                            nextRemainingTileIds.Remove(nextTileId);
                            var nextPlacements = new List<IList<Tuple<int, TileOrientation>>>();
                            foreach (var row in currentPlacements)
                            {
                                nextPlacements.Add(row.ToList());
                            }
                            if (nextColumnIndex == 0)
                            {
                                nextPlacements.Add(new List<Tuple<int, TileOrientation>>());
                            }
                            var placement = new Tuple<int, TileOrientation>(nextTileId, orientation);
                            nextPlacements[nextRowIndex].Add(placement);
                            var candidateToPush = new Tuple<IList<IList<Tuple<int, TileOrientation>>>, HashSet<int>>(
                                nextPlacements,
                                nextRemainingTileIds);
                            if (nextRemainingTileIds.Count == 0)
                            {
                                tilePlacements = nextPlacements;
                                return true;
                            }
                            candidatesToPush.Add(candidateToPush);
                        }
                    }
                }

                // Add the candidates back to the stack if placement was successful
                foreach (var candidateToPush in candidatesToPush)
                {
                    candidates.Push(candidateToPush);
                }
            }

            return false;
        }

        public static bool TryGetSquareEdgeLength(int tileCount, out int edgeLength)
        {
            edgeLength = (int)Math.Sqrt(tileCount);
            if (tileCount == edgeLength * edgeLength)
                return true;
            return false;
        }
        public static IList<Tile> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<Tile>();
            int currentTileId = -1;
            var currentTileDefinition = new List<string>();
            foreach (var inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                    continue;

                var matchTileId = Regex.Match(inputLine, @"^\s*Tile (\d+):\s*$");
                var matchTileLine = Regex.Match(inputLine, @"^\s*((\.|#)+)\s*$");
                if (matchTileId.Success)
                {
                    // Add the previous tile
                    if (currentTileId > -1)
                    {
                        var tileToAdd = new Tile(currentTileId, currentTileDefinition);
                        result.Add(tileToAdd);
                    }

                    // Set the new tile id and reset the definition
                    currentTileId = int.Parse(matchTileId.Groups[1].Value);
                    currentTileDefinition = new List<string>();
                }
                else if (matchTileLine.Success)
                {
                    currentTileDefinition.Add(matchTileLine.Groups[1].Value.Trim());
                }
                else
                {
                    throw new Exception($"Unexpected input line: {inputLine}");
                }
            }
            if (currentTileId > -1)
            {
                var finalTile = new Tile(currentTileId, currentTileDefinition);
                result.Add(finalTile);
            }
            return result;
        }
    }
}
