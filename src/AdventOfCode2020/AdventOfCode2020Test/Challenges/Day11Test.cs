using AdventOfCode2020.Challenges.Day11;
using AdventOfCode2020.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day11Test
    {
        [Fact]
        public void GetNumberOfSeenOccupiedSeatsTest()
        {
            var testData = new List<Tuple<IList<string>, GridPoint, EvolutionRules, int>>()
            {
                new Tuple<IList<string>, GridPoint, EvolutionRules, int>(
                    new List<string>()
                    {
                        ".......#.",
                        "...#.....",
                        ".#.......",
                        ".........",
                        "..#L....#",
                        "....#....",
                        ".........",
                        "#........",
                        "...#.....",
                    }, new GridPoint(3, 4),
                    Day11.EvolutionRulesPart1,
                    2),
                new Tuple<IList<string>, GridPoint, EvolutionRules, int>(
                    new List<string>()
                    {
                        ".......#.",
                        "...#.....",
                        ".#.......",
                        ".........",
                        "..#L....#",
                        "....#....",
                        ".........",
                        "#........",
                        "...#.....",
                    }, new GridPoint(3, 4),
                    Day11.EvolutionRulesPart2,
                    8),
                new Tuple<IList<string>, GridPoint, EvolutionRules, int>(
                    new List<string>()
                    {
                        ".............",
                        ".L.L.#.#.#.#.",
                        "............."
                    }, new GridPoint(1, 1),
                    Day11.EvolutionRulesPart2,
                    0),
                new Tuple<IList<string>, GridPoint, EvolutionRules, int>(
                    new List<string>()
                    {
                        ".............",
                        ".L.L.#.#.#.#.",
                        "............."
                    }, new GridPoint(3, 1),
                    Day11.EvolutionRulesPart2,
                    1),
                new Tuple<IList<string>, GridPoint, EvolutionRules, int>(
                    new List<string>()
                    {
                        ".##.##.",
                        "#.#.#.#",
                        "##...##",
                        "...L...",
                        "##...##",
                        "#.#.#.#",
                        ".##.##."
                    }, new GridPoint(3, 3),
                    Day11.EvolutionRulesPart2,
                    0)
            };

            foreach (var testExample in testData)
            {
                var roomState = WaitingAreaHelper.ParseInputLines(testExample.Item1);
                var actual = WaitingAreaHelper.GetNumberOfSeenOccupiedSeats(roomState, testExample.Item2, testExample.Item3.OnlyConsiderAdjacentCells);
                Assert.Equal(testExample.Item4, actual);
            }
        }

        [Fact]
        public void EvolveWaitingAreaUntilSteadyStateTest()
        {
            
            var testData = new List<Tuple<IList<string>, EvolutionRules, int>>()
            {
                new Tuple<IList<string>, EvolutionRules, int>(
                    new List<string>()
                    {
                        "L.LL.LL.LL",
                        "LLLLLLL.LL",
                        "L.L.L..L..",
                        "LLLL.LL.LL",
                        "L.LL.LL.LL",
                        "L.LLLLL.LL",
                        "..L.L.....",
                        "LLLLLLLLLL",
                        "L.LLLLLL.L",
                        "L.LLLLL.LL"
                    }, Day11.EvolutionRulesPart1, 37),
                new Tuple<IList<string>, EvolutionRules, int>(
                    new List<string>()
                    {
                        "L.LL.LL.LL",
                        "LLLLLLL.LL",
                        "L.L.L..L..",
                        "LLLL.LL.LL",
                        "L.LL.LL.LL",
                        "L.LLLLL.LL",
                        "..L.L.....",
                        "LLLLLLLLLL",
                        "L.LLLLLL.L",
                        "L.LLLLL.LL"
                    }, Day11.EvolutionRulesPart2, 26),
            };

            foreach (var testExample in testData)
            {
                var initialState = WaitingAreaHelper.ParseInputLines(testExample.Item1);
                var finalState = WaitingAreaHelper.EvolveWaitingAreaUntilSteadyState(initialState, testExample.Item2);
                Assert.Equal(testExample.Item3, finalState.NumberOfOccupiedSeats);
            }
        }

        [Fact]
        public void GetDay11Part01AnswerTest()
        {
            int expected = 2468;
            int actual = Day11.GetDay11Part01Answer();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDay11Part02AnswerTest()
        {
            int expected = 2214;
            int actual = Day11.GetDay11Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
