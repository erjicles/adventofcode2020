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
    }
}
