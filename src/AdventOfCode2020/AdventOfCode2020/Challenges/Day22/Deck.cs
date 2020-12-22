using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day22
{
    public class Deck
    {
        public string PlayerName { get; private set; }
        public Queue<int> SpaceCards { get; private set; }

        public Deck(string playerName, Queue<int> spaceCards)
        {
            PlayerName = playerName;
            SpaceCards = spaceCards;
        }

        public Deck(string playerName, IList<int> spaceCards)
        {
            PlayerName = playerName;
            SpaceCards = new Queue<int>();
            foreach (var card in spaceCards)
            {
                SpaceCards.Enqueue(card);
            }
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
                Deck other = (Deck)obj;
                if (!string.Equals(PlayerName, other.PlayerName))
                {
                    return false;
                }
                var areSpaceCardsEqual = SpaceCards.Count == other.SpaceCards.Count
                    && !SpaceCards
                    .Where(card => !other.SpaceCards.Contains(card))
                    .Any();
                return areSpaceCardsEqual;
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(PlayerName, SpaceCards);
            int hash = tuple.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            var result = $"{PlayerName}: {string.Join(", ", SpaceCards.ToList())}";
            return result;
        }
    }
}
