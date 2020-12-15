using AdventOfCode2020.Challenges.Day15;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day15Test
    {
        [Fact]
        public void GetNumberOnNthTurnTest()
        {
            // For example, suppose the starting numbers are 0,3,6:
            // Turn 1: The 1st number spoken is a starting number, 0.
            // Turn 2: The 2nd number spoken is a starting number, 3.
            // Turn 3: The 3rd number spoken is a starting number, 6.
            // Turn 4: Now, consider the last number spoken, 6.Since that was 
            // the first time the number had been spoken, the 4th number spoken
            // is 0.
            // Turn 5: Next, again consider the last number spoken, 0. Since 
            // it had been spoken before, the next number to speak is the 
            // difference between the turn number when it was last spoken (the 
            // previous turn, 4) and the turn number of the time it was most 
            // recently spoken before then(turn 1). Thus, the 5th number 
            // spoken is 4 - 1, 3.
            // Turn 6: The last number spoken, 3 had also been spoken before, 
            // most recently on turns 5 and 2.So, the 6th number spoken is 
            // 5 - 2, 3.
            // Turn 7: Since 3 was just spoken twice in a row, and the last 
            // two turns are 1 turn apart, the 7th number spoken is 1.
            // Turn 8: Since 1 is new, the 8th number spoken is 0.
            // Turn 9: 0 was last spoken on turns 8 and 4, so the 9th number 
            // spoken is the difference between them, 4.
            // Turn 10: 4 is new, so the 10th number spoken is 0.
            // (The game ends when the Elves get sick of playing or dinner is 
            // ready, whichever comes first.)
            // Their question for you is: what will be the 2020th number 
            // spoken? In the example above, the 2020th number spoken will be 
            // 436.
            // Here are a few more examples:
            // Given the starting numbers 1,3,2, the 2020th number spoken is 1.
            // Given the starting numbers 2,1,3, the 2020th number spoken is 10.
            // Given the starting numbers 1,2,3, the 2020th number spoken is 27.
            // Given the starting numbers 2,3,1, the 2020th number spoken is 78.
            // Given the starting numbers 3,2,1, the 2020th number spoken is 438.
            // Given the starting numbers 3,1,2, the 2020th number spoken is 1836.
            // Impressed, the Elves issue you a challenge: determine the 
            // 30000000th number spoken. For example, given the same starting 
            // numbers as above:
            // Given 0,3,6, the 30000000th number spoken is 175594.
            // Given 1,3,2, the 30000000th number spoken is 2578.
            // Given 2,1,3, the 30000000th number spoken is 3544142.
            // Given 1,2,3, the 30000000th number spoken is 261214.
            // Given 2,3,1, the 30000000th number spoken is 6895259.
            // Given 3,2,1, the 30000000th number spoken is 18.
            // Given 3,1,2, the 30000000th number spoken is 362.
            var testData = new List<Tuple<string, int, int>>()
            {
                new Tuple<string, int, int>("0,3,6", 2020, 436),
                new Tuple<string, int, int>("1,3,2", 2020, 1),
                new Tuple<string, int, int>("2,1,3", 2020, 10),
                new Tuple<string, int, int>("1,2,3", 2020, 27),
                new Tuple<string, int, int>("2,3,1", 2020, 78),
                new Tuple<string, int, int>("3,2,1", 2020, 438),
                new Tuple<string, int, int>("3,1,2", 2020, 1836),
                //new Tuple<string, int, int>("0,3,6", 30000000, 175594),
                //new Tuple<string, int, int>("1,3,2", 30000000, 2578),
                //new Tuple<string, int, int>("2,1,3", 30000000, 3544142),
                //new Tuple<string, int, int>("1,2,3", 30000000, 261214),
                //new Tuple<string, int, int>("2,3,1", 30000000, 6895259),
                //new Tuple<string, int, int>("3,2,1", 30000000, 18),
                //new Tuple<string, int, int>("3,1,2", 30000000, 362),
            };

            foreach (var testExample in testData)
            {
                var startingNumbers = MemoryGameHelper.ParseInputLine(testExample.Item1);
                var actual = MemoryGameHelper.GetNumberOnNthTurn(startingNumbers, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay15Part01AnswerTest()
        {
            int expected = 1428;
            int actual = Day15.GetDay15Part01Answer();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDay15Part02AnswerTest()
        {
            int expected = 3718541;
            int actual = Day15.GetDay15Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
