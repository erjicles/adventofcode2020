using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day22
{
    public static class DeckHelper
    {
        public static IList<Deck> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<Deck>();

            var currentPlayerName = string.Empty;
            var currentCards = new Queue<int>();
            foreach (var inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    continue;
                }

                var matchPlayerName = Regex.Match(inputLine, @"^([^:]+):\s*$");
                var matchCard = Regex.Match(inputLine, @"^\s*(\d+)\s*$");
                if (matchPlayerName.Success)
                {
                    if (!string.IsNullOrWhiteSpace(currentPlayerName))
                    {
                        var deck = new Deck(currentPlayerName, currentCards);
                        result.Add(deck);
                    }
                    currentPlayerName = matchPlayerName.Groups[1].Value;
                    currentCards = new Queue<int>();
                }
                else if (matchCard.Success)
                {
                    int card = int.Parse(matchCard.Value);
                    currentCards.Enqueue(card);
                }
                else
                {
                    throw new Exception($"Unrecognized pattern: {inputLine}");
                }
            }
            if (!string.IsNullOrWhiteSpace(currentPlayerName))
            {
                var deck = new Deck(currentPlayerName, currentCards);
                result.Add(deck);
            }
            return result;
        }
    }
}
