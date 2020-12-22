using AdventOfCode2020.Challenges.Day22;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day22Test
    {
        /*
For example, consider the following starting decks:

Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10

This arrangement means that player 1's deck contains 5 cards, with 9 on top and 1 on the bottom; player 2's deck also contains 5 cards, with 5 on top and 10 on the bottom.

The first round begins with both players drawing the top card of their decks: 9 and 5. Player 1 has the higher card, so both cards move to the bottom of player 1's deck such that 9 is above 5. In total, it takes 29 rounds before a player has all of the cards:

-- Round 1 --
Player 1's deck: 9, 2, 6, 3, 1
Player 2's deck: 5, 8, 4, 7, 10
Player 1 plays: 9
Player 2 plays: 5
Player 1 wins the round!

-- Round 2 --
Player 1's deck: 2, 6, 3, 1, 9, 5
Player 2's deck: 8, 4, 7, 10
Player 1 plays: 2
Player 2 plays: 8
Player 2 wins the round!

-- Round 3 --
Player 1's deck: 6, 3, 1, 9, 5
Player 2's deck: 4, 7, 10, 8, 2
Player 1 plays: 6
Player 2 plays: 4
Player 1 wins the round!

-- Round 4 --
Player 1's deck: 3, 1, 9, 5, 6, 4
Player 2's deck: 7, 10, 8, 2
Player 1 plays: 3
Player 2 plays: 7
Player 2 wins the round!

-- Round 5 --
Player 1's deck: 1, 9, 5, 6, 4
Player 2's deck: 10, 8, 2, 7, 3
Player 1 plays: 1
Player 2 plays: 10
Player 2 wins the round!

...several more rounds pass...

-- Round 27 --
Player 1's deck: 5, 4, 1
Player 2's deck: 8, 9, 7, 3, 2, 10, 6
Player 1 plays: 5
Player 2 plays: 8
Player 2 wins the round!

-- Round 28 --
Player 1's deck: 4, 1
Player 2's deck: 9, 7, 3, 2, 10, 6, 8, 5
Player 1 plays: 4
Player 2 plays: 9
Player 2 wins the round!

-- Round 29 --
Player 1's deck: 1
Player 2's deck: 7, 3, 2, 10, 6, 8, 5, 9, 4
Player 1 plays: 1
Player 2 plays: 7
Player 2 wins the round!


== Post-game results ==
Player 1's deck: 
Player 2's deck: 3, 2, 10, 6, 8, 5, 9, 4, 7, 1

Once the game ends, you can calculate the winning player's score. The bottom card in their deck is worth the value of the card multiplied by 1, the second-from-the-bottom card is worth the value of the card multiplied by 2, and so on. With 10 cards, the top card is worth the value on the card multiplied by 10. In this example, the winning player's score is:

   3 * 10
+  2 *  9
+ 10 *  8
+  6 *  7
+  8 *  6
+  5 *  5
+  9 *  4
+  4 *  3
+  7 *  2
+  1 *  1
= 306

So, once the game ends, the winning player's score is 306.
         */
        [Fact]
        public void ParseInputLinesTest()
        {
            var testData = new List<Tuple<IList<string>, IList<IList<int>>>>()
            {
                new Tuple<IList<string>, IList<IList<int>>>(
                    new List<string>()
                    {
                        "Player 1:",
                        "9",
                        "2",
                        "6",
                        "3",
                        "1",
                        "",
                        "Player 2:",
                        "5",
                        "8",
                        "4",
                        "7",
                        "10"
                    },
                    new List<IList<int>>()
                    {
                        new List<int>() { 9, 2, 6, 3, 1 },
                        new List<int>() { 5, 8, 4, 7, 10 }
                    })
            };

            foreach (var testExample in testData)
            {
                var actual = DeckHelper.ParseInputLines(testExample.Item1);
                var areEqual = (actual.Count == testExample.Item2.Count)
                    && !actual.Where(actualDeck => !testExample.Item2
                        .Where(expectedDeck => 
                            actualDeck.SpaceCards.Count == expectedDeck.Count
                            && actualDeck.SpaceCards.ToList().All(
                                actualCard => expectedDeck.Contains(actualCard)))
                        .Any())
                    .Any();
                Assert.True(areEqual);
            }
        }

        [Fact]
        public void TryPlayRoundTest()
        {
            var testData = new List<Tuple<IList<IList<int>>, IList<IList<int>>, bool>>()
            {
                new Tuple<IList<IList<int>>, IList<IList<int>>, bool>(
                    new List<IList<int>>()
                    {
                        new List<int>() { 9, 2, 6, 3, 1 },
                        new List<int>() { 5, 8, 4, 7, 10 }
                    },
                    new List<IList<int>>()
                    {
                        new List<int>() { 2, 6, 3, 1, 9, 5 },
                        new List<int>() { 8, 4, 7, 10 }
                    }, 
                    true),
                new Tuple<IList<IList<int>>, IList<IList<int>>, bool>(
                    new List<IList<int>>()
                    {
                        new List<int>() { 1 },
                        new List<int>() { 7, 3, 2, 10, 6, 8, 5, 9, 4 }
                    },
                    new List<IList<int>>()
                    {
                        new List<int>() { },
                        new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 }
                    },
                    true),
                new Tuple<IList<IList<int>>, IList<IList<int>>, bool>(
                    new List<IList<int>>()
                    {
                        new List<int>() { },
                        new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 }
                    },
                    new List<IList<int>>()
                    {
                        new List<int>() { },
                        new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 }
                    },
                    false),
            };

            foreach (var testExample in testData)
            {
                var decks = new List<Deck>();
                foreach (var cardList in testExample.Item1)
                {
                    var deck = new Deck(string.Empty, cardList);
                    decks.Add(deck);
                }
                var areStillPlaying = CombatHelper.TryPlayRound(decks);
                var cardLists = decks.Select(deck => deck.SpaceCards.ToList()).ToList();
                Assert.Equal(testExample.Item2, cardLists);
                Assert.Equal(testExample.Item3, areStillPlaying);
            }
        }

        [Fact]
        public void TryPlayGameTest()
        {
            var testData = new List<Tuple<IList<string>, bool, Deck>>()
            {
                new Tuple<IList<string>, bool, Deck>(
                    new List<string>()
                    {
                        "Player 1:",
                        "9",
                        "2",
                        "6",
                        "3",
                        "1",
                        "",
                        "Player 2:",
                        "5",
                        "8",
                        "4",
                        "7",
                        "10"
                    },
                    true,
                    new Deck("Player 2", new List<int>(){ 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 }))
            };

            foreach (var testExample in testData)
            {
                var decks = DeckHelper.ParseInputLines(testExample.Item1);
                var isSuccessful = CombatHelper.TryPlayGame(decks, out Deck winner);
                Assert.Equal(testExample.Item2, isSuccessful);
                Assert.NotNull(winner);
                Assert.Equal(testExample.Item3.PlayerName, winner.PlayerName);
                Assert.Equal(testExample.Item3.SpaceCards, winner.SpaceCards);
            }
        }

        [Fact]
        public void GetWinnerScoreTest()
        {
            var testData = new List<Tuple<Deck, long>>()
            {
                new Tuple<Deck, long>(
                    new Deck(string.Empty, new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 }),
                    306)
            };

            foreach (var testExample in testData)
            {
                var actual = CombatHelper.GetWinnerScore(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetDay22Part01AnswerTest()
        {
            long expected = 32598;
            long actual = Day22.GetDay22Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
