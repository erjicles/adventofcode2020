using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day22
{
    public static class CombatHelper
    {
        public static long GetWinnerScore(Deck winner)
        {
            long result = 0;
            var cards = winner.SpaceCards.ToList();
            while (cards.Count > 0)
            {
                var multiplier = cards.Count;
                var card = cards[0];
                cards.RemoveAt(0);
                var partialScore = card * multiplier;
                result += partialScore;
            }
            return result;
        }

        public static bool TryPlayGame(IList<Deck> decks, out Deck winner)
        {
            var areStillPlaying = true;
            winner = null;
            while (areStillPlaying)
            {
                areStillPlaying = TryPlayRound(decks);
            }

            var winners = decks
                .Where(deck => deck.SpaceCards.Count > 0)
                .ToList();

            if (winners.Count != 1)
            {
                return false;
            }
            winner = winners[0];
            return true;
        }

        public static bool TryPlayRound(IList<Deck> decks)
        {
            var topCards = new List<int>();
            var bestCardPlayerIndex = -1;
            var bestCard = -1;
            for (int playerIndex = 0; playerIndex < decks.Count; playerIndex++)
            {
                var deck = decks[playerIndex];
                if (deck.SpaceCards.Count == 0)
                {
                    continue;
                }
                var topCard = deck.SpaceCards.Peek();
                topCards.Add(topCard);

                if (topCard > bestCard)
                {
                    bestCard = topCard;
                    bestCardPlayerIndex = playerIndex;
                }
            }

            // If one or fewer players still have cards, then the game is over
            if (topCards.Count < 2)
            {
                return false;
            }

            // Remove the top card of remaining decks
            foreach (var deck in decks)
            {
                if (deck.SpaceCards.Count > 0)
                {
                    deck.SpaceCards.Dequeue();
                }
            }

            // Add the cards to the winner's deck
            if (bestCardPlayerIndex >= 0)
            {
                var deck = decks[bestCardPlayerIndex];
                var cardsToAdd = topCards
                    .OrderByDescending(card => card)
                    .Select(card => card)
                    .ToList();
                foreach (var card in cardsToAdd)
                {
                    deck.SpaceCards.Enqueue(card);
                }
            }

            return true;
        }
    }
}
