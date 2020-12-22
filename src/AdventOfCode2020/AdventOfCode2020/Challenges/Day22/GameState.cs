using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day22
{
    public class GameState
    {
        public IList<Deck> Decks { get; private set; }
        public bool IsAwaitingSubgameWinner { get; private set; }
        public string StateString { get; private set; }
        private int StateStringHashCode { get; }
        public GameState(IList<Deck> decks, bool isAwaitingSubgameWinner)
        {
            var deckCopies = new List<Deck>();
            foreach (var deck in decks)
            {
                var deckCopy = DeckHelper.GetDeckCopy(deck);
                deckCopies.Add(deckCopy);
            }
            Decks = deckCopies;
            IsAwaitingSubgameWinner = isAwaitingSubgameWinner;
            StateString = $"{IsAwaitingSubgameWinner}->{string.Join(";", Decks.Select(deck => deck.ToString()))}";
            StateStringHashCode = StateString.GetHashCode();
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
                GameState other = (GameState)obj;
                if (IsAwaitingSubgameWinner != other.IsAwaitingSubgameWinner)
                {
                    return false;
                }
                if (Decks.Count != other.Decks.Count)
                {
                    return false;
                }
                var areEqual = !Decks.Where(deck => !other.Decks.Contains(deck)).Any();
                return areEqual;
            }
        }

        public override int GetHashCode()
        {
            return StateStringHashCode;
        }

        public override string ToString()
        {
            return StateString;
        }

        public void Print()
        {
            Console.WriteLine($"Awaiting subgame: {IsAwaitingSubgameWinner}");
            foreach (var deck in Decks)
            {
                Console.WriteLine(deck.ToString());
            }
            Console.WriteLine("");
        }
    }
}
