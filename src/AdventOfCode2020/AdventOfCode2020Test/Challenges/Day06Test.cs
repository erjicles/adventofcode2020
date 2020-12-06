using AdventOfCode2020.Challenges.Day06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day06Test
    {
        [Fact]
        public void GetUniqueAnswersInGroupTest()
        {
            // However, the person sitting next to you seems to be experiencing a language barrier and asks if you can help. For each of the people in their group, you write down the questions for which they answer "yes", one per line. For example:
            // abcx
            // abcy
            // abcz
            // In this group, there are 6 questions to which anyone answered "yes": a, b, c, x, y, and z. (Duplicate answers to the same question don't count extra; each question counts at most once.)
            // Another group asks for your help, then another, and eventually you've collected answers from every group on the plane (your puzzle input). Each group's answers are separated by a blank line, and within each group, each person's answers are on a single line. For example:
            //
            // abc
            //
            // a
            // b
            // c
            //
            // ab
            // ac
            //
            // a
            // a
            // a
            // a
            //
            // b
            // This list represents answers from five groups:
            // The first group contains one person who answered "yes" to 3 questions: a, b, and c.
            // The second group contains three people; combined, they answered "yes" to 3 questions: a, b, and c.
            // The third group contains two people; combined, they answered "yes" to 3 questions: a, b, and c.
            // The fourth group contains four people; combined, they answered "yes" to only 1 question, a.
            // The last group contains one person who answered "yes" to only 1 question, b.
            var testData = new List<Tuple<IList<string>, int>>() 
            {
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "abcx",
                        "abcy",
                        "abcz"
                    }, 6),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "abc"
                    }, 3),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "a",
                        "b",
                        "c"
                    }, 3),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "ab",
                        "ac",
                    }, 3),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "a",
                        "a",
                        "a",
                        "a"
                    }, 1),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "b"
                    }, 1)
            };

            foreach (var testExample in testData)
            {
                var group = CustomsDeclarationFormHelper.ParseCustomsDeclarationFormGroup(testExample.Item1);
                var actual = CustomsDeclarationFormHelper.GetUniqueAnswersInGroup(group).Count;
                Assert.Equal(testExample.Item2, actual);
            }

        }

        [Fact]
        public void GetUnanimousAnswersInGroupTest()
        {
            // You don't need to identify the questions to which anyone answered "yes"; you need to identify the questions to which everyone answered "yes"!
            // Using the same example as above:
            //
            // abc
            // 
            // a
            // b
            // c
            // 
            // ab
            // ac
            // 
            // a
            // a
            // a
            // a
            // 
            // b
            // 
            // This list represents answers from five groups:
            // In the first group, everyone (all 1 person) answered "yes" to 3 questions: a, b, and c.
            // In the second group, there is no question to which everyone answered "yes".
            // In the third group, everyone answered yes to only 1 question, a.Since some people did not answer "yes" to b or c, they don't count.
            // In the fourth group, everyone answered yes to only 1 question, a.
            // In the fifth group, everyone (all 1 person) answered "yes" to 1 question, b.
            var testData = new List<Tuple<IList<string>, int>>()
            {
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "abcx",
                        "abcy",
                        "abcz"
                    }, 3),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "abc"
                    }, 3),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "a",
                        "b",
                        "c"
                    }, 0),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "ab",
                        "ac",
                    }, 1),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "a",
                        "a",
                        "a",
                        "a"
                    }, 1),
                new Tuple<IList<string>, int>(
                    new List<string>
                    {
                        "b"
                    }, 1)
            };

            foreach (var testExample in testData)
            {
                var group = CustomsDeclarationFormHelper.ParseCustomsDeclarationFormGroup(testExample.Item1);
                var actual = CustomsDeclarationFormHelper.GetUnanimousAnswersInGroup(group).Count;
                Assert.Equal(testExample.Item2, actual);
            }

        }

        [Fact]
        public void GetDay06Part01AnswerTest()
        {
            int expected = 7120;
            int actual = Day06.GetDay06Part01Answer();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDay06Part02AnswerTest()
        {
            int expected = 3570;
            int actual = Day06.GetDay06Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
