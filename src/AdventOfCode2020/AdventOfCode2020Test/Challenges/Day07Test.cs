using AdventOfCode2020.Challenges.Day07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day07Test
    {
        [Fact]
        public void ParseBagPolicyTest()
        {
            var testData = new List<Tuple<string, string, IList<Tuple<string, int>>>>()
            {
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                    "light red",
                    new List<Tuple<string, int>>()
                    {
                        new Tuple<string, int>("bright white", 1),
                        new Tuple<string, int>("muted yellow", 2)
                    }),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                    "dark orange",
                    new List<Tuple<string, int>>()
                    {
                        new Tuple<string, int>("bright white", 3),
                        new Tuple<string, int>("muted yellow", 4)
                    }),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "bright white bags contain 1 shiny gold bag.",
                    "bright white",
                    new List<Tuple<string, int>>()
                    {
                        new Tuple<string, int>("shiny gold", 1)
                    }),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                    "muted yellow",
                    new List<Tuple<string, int>>()
                    {
                        new Tuple<string, int>("shiny gold", 2),
                        new Tuple<string, int>("faded blue", 9)
                    }),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                    "shiny gold",
                    new List<Tuple<string, int>>()
                    {
                        new Tuple<string, int>("dark olive", 1),
                        new Tuple<string, int>("vibrant plum", 2)
                    }),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                    "dark olive",
                    new List<Tuple<string, int>>()
                    {
                        new Tuple<string, int>("faded blue", 3),
                        new Tuple<string, int>("dotted black", 4)
                    }),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                    "vibrant plum",
                    new List<Tuple<string, int>>()
                    {
                        new Tuple<string, int>("faded blue", 5),
                        new Tuple<string, int>("dotted black", 6)
                    }),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "faded blue bags contain no other bags.",
                    "faded blue",
                    new List<Tuple<string, int>>()),
                new Tuple<string, string, IList<Tuple<string, int>>>(
                    "dotted black bags contain no other bags.",
                    "dotted black",
                    new List<Tuple<string, int>>())
            };

            foreach (var testExample in testData)
            {
                var bagPolicy = BagPolicyHelper.ParseBagPolicy(testExample.Item1);
                Assert.Equal(testExample.Item2, bagPolicy.BagType);
                Assert.Equal(testExample.Item3, bagPolicy.ContentsRequirements);
            }
        }

        [Fact]
        public void GetNumberOfOutermostBagsThatCanContainTargetBagTest()
        {
            // You have a shiny gold bag. If you wanted to carry it in at least
            // one other bag, how many different bag colors would be valid for 
            // the outermost bag? (In other words: how many colors can, 
            // eventually, contain at least one shiny gold bag?)
            // In the above rules, the following options would be available to you:
            // A bright white bag, which can hold your shiny gold bag directly.
            // A muted yellow bag, which can hold your shiny gold bag directly, 
            // plus some other bags.
            // A dark orange bag, which can hold bright white and muted yellow 
            // bags, either of which could then hold your shiny gold bag.
            // A light red bag, which can hold bright white and muted yellow 
            // bags, either of which could then hold your shiny gold bag.
            // So, in this example, the number of bag colors that can 
            // eventually contain at least one shiny gold bag is 4.
            var testData = new List<Tuple<IList<string>, string, int>>()
            {
                new Tuple<IList<string>, string, int>(
                    new List<string>()
                    {
                        "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                        "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                        "bright white bags contain 1 shiny gold bag.",
                        "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                        "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                        "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                        "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                        "faded blue bags contain no other bags.",
                        "dotted black bags contain no other bags."
                    },
                    "shiny gold",
                    4)
            };

            foreach (var testExample in testData)
            {
                var bagPolicies = BagPolicyHelper.ParseInputLines(testExample.Item1);
                var actual = BagPolicyHelper.GetNumberOfOutermostBagsThatCanContainTargetBag(bagPolicies, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetNumberOfBagsRequiredInsideBagTest()
        {
            // Consider again your shiny gold bag and the rules from the above example:
            // faded blue bags contain 0 other bags.
            // dotted black bags contain 0 other bags.
            // vibrant plum bags contain 11 other bags: 5 faded blue bags and 6 dotted black bags.
            // dark olive bags contain 7 other bags: 3 faded blue bags and 4 dotted black bags.
            // So, a single shiny gold bag must contain 1 dark olive bag(and the 7 bags within it) plus 2 vibrant plum bags(and the 11 bags within each of those): 1 + 1 * 7 + 2 + 2 * 11 = 32 bags!
            // Of course, the actual rules have a small chance of going several levels deeper than this example; be sure to count all of the bags, even if the nesting becomes topologically impractical!
            // Here's another example:
            // shiny gold bags contain 2 dark red bags.
            // dark red bags contain 2 dark orange bags.
            // dark orange bags contain 2 dark yellow bags.
            // dark yellow bags contain 2 dark green bags.
            // dark green bags contain 2 dark blue bags.
            // dark blue bags contain 2 dark violet bags.
            // dark violet bags contain no other bags.
            // In this example, a single shiny gold bag must contain 126 other bags.
            var testData = new List<Tuple<IList<string>, string, int>>()
            {
                new Tuple<IList<string>, string, int>(
                    new List<string>()
                    {
                        "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                        "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                        "bright white bags contain 1 shiny gold bag.",
                        "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                        "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                        "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                        "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                        "faded blue bags contain no other bags.",
                        "dotted black bags contain no other bags."
                    },
                    "shiny gold",
                    32),
                new Tuple<IList<string>, string, int>(
                    new List<string>()
                    {
                        "shiny gold bags contain 2 dark red bags.",
                        "dark red bags contain 2 dark orange bags.",
                        "dark orange bags contain 2 dark yellow bags.",
                        "dark yellow bags contain 2 dark green bags.",
                        "dark green bags contain 2 dark blue bags.",
                        "dark blue bags contain 2 dark violet bags.",
                        "dark violet bags contain no other bags.",
                    },
                    "shiny gold",
                    126)
            };

            foreach (var testExample in testData)
            {
                var bagPolicies = BagPolicyHelper.ParseInputLines(testExample.Item1);
                var actual = BagPolicyHelper.GetNumberOfBagsRequiredInsideBag(bagPolicies, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay07Part01AnswerTest()
        {
            int expected = 161;
            int actual = Day07.GetDay07Part01Answer();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDay07Part02AnswerTest()
        {
            int expected = 30899;
            int actual = Day07.GetDay07Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
