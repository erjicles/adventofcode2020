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

--- Part Two ---

You lost to the small crab! Fortunately, crabs aren't very good at recursion. To defend your honor as a Raft Captain, you challenge the small crab to a game of Recursive Combat.

Recursive Combat still starts by splitting the cards into two decks (you offer to play with the same starting decks as before - it's only fair). Then, the game consists of a series of rounds with a few changes:

    Before either player deals a card, if there was a previous round in this game that had exactly the same cards in the same order in the same players' decks, the game instantly ends in a win for player 1. Previous rounds from other games are not considered. (This prevents infinite games of Recursive Combat, which everyone agrees is a bad idea.)
    Otherwise, this round's cards must be in a new configuration; the players begin the round by each drawing the top card of their deck as normal.
    If both players have at least as many cards remaining in their deck as the value of the card they just drew, the winner of the round is determined by playing a new game of Recursive Combat (see below).
    Otherwise, at least one player must not have enough cards left in their deck to recurse; the winner of the round is the player with the higher-value card.

As in regular Combat, the winner of the round (even if they won the round by winning a sub-game) takes the two cards dealt at the beginning of the round and places them on the bottom of their own deck (again so that the winner's card is above the other card). Note that the winner's card might be the lower-valued of the two cards if they won the round due to winning a sub-game. If collecting cards by winning the round causes a player to have all of the cards, they win, and the game ends.

Here is an example of a small game that would loop forever without the infinite game prevention rule:

Player 1:
43
19

Player 2:
2
29
14

During a round of Recursive Combat, if both players have at least as many cards in their own decks as the number on the card they just dealt, the winner of the round is determined by recursing into a sub-game of Recursive Combat. (For example, if player 1 draws the 3 card, and player 2 draws the 7 card, this would occur if player 1 has at least 3 cards left and player 2 has at least 7 cards left, not counting the 3 and 7 cards that were drawn.)

To play a sub-game of Recursive Combat, each player creates a new deck by making a copy of the next cards in their deck (the quantity of cards copied is equal to the number on the card they drew to trigger the sub-game). During this sub-game, the game that triggered it is on hold and completely unaffected; no cards are removed from players' decks to form the sub-game. (For example, if player 1 drew the 3 card, their deck in the sub-game would be copies of the next three cards in their deck.)

Here is a complete example of gameplay, where Game 1 is the primary game of Recursive Combat:

=== Game 1 ===

-- Round 1 (Game 1) --
Player 1's deck: 9, 2, 6, 3, 1
Player 2's deck: 5, 8, 4, 7, 10
Player 1 plays: 9
Player 2 plays: 5
Player 1 wins round 1 of game 1!

-- Round 2 (Game 1) --
Player 1's deck: 2, 6, 3, 1, 9, 5
Player 2's deck: 8, 4, 7, 10
Player 1 plays: 2
Player 2 plays: 8
Player 2 wins round 2 of game 1!

-- Round 3 (Game 1) --
Player 1's deck: 6, 3, 1, 9, 5
Player 2's deck: 4, 7, 10, 8, 2
Player 1 plays: 6
Player 2 plays: 4
Player 1 wins round 3 of game 1!

-- Round 4 (Game 1) --
Player 1's deck: 3, 1, 9, 5, 6, 4
Player 2's deck: 7, 10, 8, 2
Player 1 plays: 3
Player 2 plays: 7
Player 2 wins round 4 of game 1!

-- Round 5 (Game 1) --
Player 1's deck: 1, 9, 5, 6, 4
Player 2's deck: 10, 8, 2, 7, 3
Player 1 plays: 1
Player 2 plays: 10
Player 2 wins round 5 of game 1!

-- Round 6 (Game 1) --
Player 1's deck: 9, 5, 6, 4
Player 2's deck: 8, 2, 7, 3, 10, 1
Player 1 plays: 9
Player 2 plays: 8
Player 1 wins round 6 of game 1!

-- Round 7 (Game 1) --
Player 1's deck: 5, 6, 4, 9, 8
Player 2's deck: 2, 7, 3, 10, 1
Player 1 plays: 5
Player 2 plays: 2
Player 1 wins round 7 of game 1!

-- Round 8 (Game 1) --
Player 1's deck: 6, 4, 9, 8, 5, 2
Player 2's deck: 7, 3, 10, 1
Player 1 plays: 6
Player 2 plays: 7
Player 2 wins round 8 of game 1!

-- Round 9 (Game 1) --
Player 1's deck: 4, 9, 8, 5, 2
Player 2's deck: 3, 10, 1, 7, 6
Player 1 plays: 4
Player 2 plays: 3
Playing a sub-game to determine the winner...

=== Game 2 ===

-- Round 1 (Game 2) --
Player 1's deck: 9, 8, 5, 2
Player 2's deck: 10, 1, 7
Player 1 plays: 9
Player 2 plays: 10
Player 2 wins round 1 of game 2!

-- Round 2 (Game 2) --
Player 1's deck: 8, 5, 2
Player 2's deck: 1, 7, 10, 9
Player 1 plays: 8
Player 2 plays: 1
Player 1 wins round 2 of game 2!

-- Round 3 (Game 2) --
Player 1's deck: 5, 2, 8, 1
Player 2's deck: 7, 10, 9
Player 1 plays: 5
Player 2 plays: 7
Player 2 wins round 3 of game 2!

-- Round 4 (Game 2) --
Player 1's deck: 2, 8, 1
Player 2's deck: 10, 9, 7, 5
Player 1 plays: 2
Player 2 plays: 10
Player 2 wins round 4 of game 2!

-- Round 5 (Game 2) --
Player 1's deck: 8, 1
Player 2's deck: 9, 7, 5, 10, 2
Player 1 plays: 8
Player 2 plays: 9
Player 2 wins round 5 of game 2!

-- Round 6 (Game 2) --
Player 1's deck: 1
Player 2's deck: 7, 5, 10, 2, 9, 8
Player 1 plays: 1
Player 2 plays: 7
Player 2 wins round 6 of game 2!
The winner of game 2 is player 2!

...anyway, back to game 1.
Player 2 wins round 9 of game 1!

-- Round 10 (Game 1) --
Player 1's deck: 9, 8, 5, 2
Player 2's deck: 10, 1, 7, 6, 3, 4
Player 1 plays: 9
Player 2 plays: 10
Player 2 wins round 10 of game 1!

-- Round 11 (Game 1) --
Player 1's deck: 8, 5, 2
Player 2's deck: 1, 7, 6, 3, 4, 10, 9
Player 1 plays: 8
Player 2 plays: 1
Player 1 wins round 11 of game 1!

-- Round 12 (Game 1) --
Player 1's deck: 5, 2, 8, 1
Player 2's deck: 7, 6, 3, 4, 10, 9
Player 1 plays: 5
Player 2 plays: 7
Player 2 wins round 12 of game 1!

-- Round 13 (Game 1) --
Player 1's deck: 2, 8, 1
Player 2's deck: 6, 3, 4, 10, 9, 7, 5
Player 1 plays: 2
Player 2 plays: 6
Playing a sub-game to determine the winner...

=== Game 3 ===

-- Round 1 (Game 3) --
Player 1's deck: 8, 1
Player 2's deck: 3, 4, 10, 9, 7, 5
Player 1 plays: 8
Player 2 plays: 3
Player 1 wins round 1 of game 3!

-- Round 2 (Game 3) --
Player 1's deck: 1, 8, 3
Player 2's deck: 4, 10, 9, 7, 5
Player 1 plays: 1
Player 2 plays: 4
Playing a sub-game to determine the winner...

=== Game 4 ===

-- Round 1 (Game 4) --
Player 1's deck: 8
Player 2's deck: 10, 9, 7, 5
Player 1 plays: 8
Player 2 plays: 10
Player 2 wins round 1 of game 4!
The winner of game 4 is player 2!

...anyway, back to game 3.
Player 2 wins round 2 of game 3!

-- Round 3 (Game 3) --
Player 1's deck: 8, 3
Player 2's deck: 10, 9, 7, 5, 4, 1
Player 1 plays: 8
Player 2 plays: 10
Player 2 wins round 3 of game 3!

-- Round 4 (Game 3) --
Player 1's deck: 3
Player 2's deck: 9, 7, 5, 4, 1, 10, 8
Player 1 plays: 3
Player 2 plays: 9
Player 2 wins round 4 of game 3!
The winner of game 3 is player 2!

...anyway, back to game 1.
Player 2 wins round 13 of game 1!

-- Round 14 (Game 1) --
Player 1's deck: 8, 1
Player 2's deck: 3, 4, 10, 9, 7, 5, 6, 2
Player 1 plays: 8
Player 2 plays: 3
Player 1 wins round 14 of game 1!

-- Round 15 (Game 1) --
Player 1's deck: 1, 8, 3
Player 2's deck: 4, 10, 9, 7, 5, 6, 2
Player 1 plays: 1
Player 2 plays: 4
Playing a sub-game to determine the winner...

=== Game 5 ===

-- Round 1 (Game 5) --
Player 1's deck: 8
Player 2's deck: 10, 9, 7, 5
Player 1 plays: 8
Player 2 plays: 10
Player 2 wins round 1 of game 5!
The winner of game 5 is player 2!

...anyway, back to game 1.
Player 2 wins round 15 of game 1!

-- Round 16 (Game 1) --
Player 1's deck: 8, 3
Player 2's deck: 10, 9, 7, 5, 6, 2, 4, 1
Player 1 plays: 8
Player 2 plays: 10
Player 2 wins round 16 of game 1!

-- Round 17 (Game 1) --
Player 1's deck: 3
Player 2's deck: 9, 7, 5, 6, 2, 4, 1, 10, 8
Player 1 plays: 3
Player 2 plays: 9
Player 2 wins round 17 of game 1!
The winner of game 1 is player 2!


== Post-game results ==
Player 1's deck: 
Player 2's deck: 7, 5, 6, 2, 4, 1, 10, 8, 9, 3

After the game, the winning player's score is calculated from the cards they have in their original deck using the same rules as regular Combat. In the above game, the winning player's score is 291.

Defend your honor as Raft Captain by playing the small crab in a game of Recursive Combat using the same two decks as before. What is the winning player's score?

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
        public void TryGetWinnerTest()
        {
            var testData = new List<Tuple<IList<Deck>, Tuple<bool, Deck>>>()
            {
                new Tuple<IList<Deck>, Tuple<bool, Deck>>(
                    new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 9, 2, 6, 3, 1 }),
                        new Deck("Player 2", new List<int>() { 5, 8, 4, 7, 10 })
                    },
                    new Tuple<bool, Deck>(false, null)),
                new Tuple<IList<Deck>, Tuple<bool, Deck>>(
                    new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ }),
                        new Deck("Player 2", new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 })
                    },
                    new Tuple<bool, Deck>(
                        true,
                        new Deck("Player 2", new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 })))
            };

            foreach (var testExample in testData)
            {
                var gameState = new GameState(testExample.Item1, false);
                var isWinnerFound = CombatHelper.TryGetWinner(gameState, out Deck winner);
                Assert.Equal(testExample.Item2.Item1, isWinnerFound);
                Assert.Equal(testExample.Item2.Item2, winner);
            }
        }

        [Fact]
        public void GetNextGameStateGivenRoundWinnerTest()
        {
            var testData = new List<Tuple<GameState, Deck, GameState>>()
            {
                new Tuple<GameState, Deck, GameState>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 9, 2, 6, 3, 1 }),
                        new Deck("Player 2", new List<int>() { 5, 8, 4, 7, 10 })
                    }, false),
                    new Deck("Player 2", new List<int>(){ }),
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 2, 6, 3, 1 }),
                        new Deck("Player 2", new List<int>(){ 8, 4, 7, 10, 5, 9 })
                    }, false)),
                new Tuple<GameState, Deck, GameState>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 9, 2, 6, 3, 1 }),
                        new Deck("Player 2", new List<int>() { 5, 8, 4, 7, 10 })
                    }, false),
                    new Deck("Player 1", new List<int>(){ }),
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 2, 6, 3, 1, 9, 5 }),
                        new Deck("Player 2", new List<int>(){ 8, 4, 7, 10 })
                    }, false)),
                new Tuple<GameState, Deck, GameState>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ }),
                        new Deck("Player 2", new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 })
                    }, false),
                    new Deck("Player 1", new List<int>(){ }),
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ }),
                        new Deck("Player 2", new List<int>(){ 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 })
                    }, false)),
                new Tuple<GameState, Deck, GameState>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ }),
                        new Deck("Player 2", new List<int>() { 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 })
                    }, false),
                    new Deck("Player 2", new List<int>(){ }),
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ }),
                        new Deck("Player 2", new List<int>(){ 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 })
                    }, false))
            };

            foreach (var testExample in testData)
            {
                var actual = CombatHelper.GetNextGameStateGivenSubgameWinner(testExample.Item1, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void TryStartSubgameTest()
        {
            var testData = new List<Tuple<GameState, Tuple<bool, GameState>>>()
            {
                new Tuple<GameState, Tuple<bool, GameState>>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 9, 2, 6, 3, 1 }),
                        new Deck("Player 2", new List<int>() { 5, 8, 4, 7, 10 })
                    }, false),
                    new Tuple<bool, GameState>(
                        false,
                        null)),
                new Tuple<GameState, Tuple<bool, GameState>>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 9, 2, 6, 3, 1, 5, 8, 4, 7, 10 }),
                        new Deck("Player 2", new List<int>() { })
                    }, false),
                    new Tuple<bool, GameState>(
                        false,
                        null)),
                new Tuple<GameState, Tuple<bool, GameState>>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 4, 9, 8, 5, 2 }),
                        new Deck("Player 2", new List<int>() { 3, 10, 1, 7, 6 })
                    }, false),
                    new Tuple<bool, GameState>(
                        true,
                        new GameState(new List<Deck>()
                        {
                            new Deck("Player 1", new List<int>(){ 9, 8, 5, 2 }),
                            new Deck("Player 2", new List<int>() { 10, 1, 7 })
                        }, false))),
                new Tuple<GameState, Tuple<bool, GameState>>(
                    new GameState(new List<Deck>()
                    {
                        new Deck("Player 1", new List<int>(){ 2, 8, 1 }),
                        new Deck("Player 2", new List<int>() { 6, 3, 4, 10, 9, 7, 5 })
                    }, false),
                    new Tuple<bool, GameState>(
                        true,
                        new GameState(new List<Deck>()
                        {
                            new Deck("Player 1", new List<int>(){ 8, 1 }),
                            new Deck("Player 2", new List<int>() { 3, 4, 10, 9, 7, 5 })
                        }, false))),
            };

            foreach (var testExample in testData)
            {
                var isShouldStartSubgame = CombatHelper.TryStartSubgame(
                    testExample.Item1, 
                    out GameState subgameState);
                Assert.Equal(testExample.Item2.Item1, isShouldStartSubgame);
                Assert.Equal(testExample.Item2.Item2, subgameState);
            }
        }

            [Fact]
        public void TryPlayClassicRoundTest()
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
                var initialGameState = new GameState(decks, false);
                var areStillPlaying = CombatHelper.TryPlayClassicRound(
                    initialGameState, 
                    out GameState finalGameState);
                var cardLists = finalGameState.Decks.Select(deck => deck.SpaceCards.ToList()).ToList();
                Assert.Equal(testExample.Item2, cardLists);
                Assert.Equal(testExample.Item3, areStillPlaying);
            }
        }

        [Fact]
        public void TryPlayGameTest()
        {
            var testData = new List<Tuple<IList<string>, bool, bool, Deck>>()
            {
                new Tuple<IList<string>, bool, bool, Deck>(
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
                    false,
                    true,
                    new Deck("Player 2", new List<int>(){ 3, 2, 10, 6, 8, 5, 9, 4, 7, 1 })),
                new Tuple<IList<string>, bool, bool, Deck>(
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
                    true,
                    new Deck("Player 2", new List<int>(){ 7, 5, 6, 2, 4, 1, 10, 8, 9, 3 }))
            };

            foreach (var testExample in testData)
            {
                var decks = DeckHelper.ParseInputLines(testExample.Item1);
                var isSuccessful = CombatHelper.TryPlayGame(decks, testExample.Item2, out Deck winner);
                Assert.Equal(testExample.Item3, isSuccessful);
                Assert.NotNull(winner);
                Assert.Equal(testExample.Item4.PlayerName, winner.PlayerName);
                Assert.Equal(testExample.Item4.SpaceCards, winner.SpaceCards);
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

        [Fact]
        public void GetDay22Part02AnswerTest()
        {
            long expected = 35836;
            long actual = Day22.GetDay22Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
