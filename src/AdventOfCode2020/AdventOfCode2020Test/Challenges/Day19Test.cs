using AdventOfCode2020.Challenges.Day19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day19Test
    {

        [Fact]
        public void GetNumberOfValidMatchesTest()
        {
            var testData = new List<Tuple<IList<string>, IDictionary<string, string>, int>>()
            {
                /*
Part 1:

The received messages (the bottom part of your puzzle input) need to be checked against the rules so you can determine which are valid and which are corrupted. Including the rules and the messages together, this might look like:

0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: "a"
5: "b"

ababbb
bababa
abbbab
aaabbb
aaaabbb
Your goal is to determine the number of messages that completely match rule 0. In the above example, ababbb and abbbab match, but bababa, aaabbb, and aaaabbb do not, producing the answer 2. The whole message must match all of rule 0; there can't be extra unmatched characters in the message. (For example, aaaabbb might appear to match rule 0 above, but it has an extra unmatched b on the end.)

Part 2:

For example:

42: 9 14 | 10 1
9: 14 27 | 1 26
10: 23 14 | 28 1
1: "a"
11: 42 31
5: 1 14 | 15 1
19: 14 1 | 14 14
12: 24 14 | 19 1
16: 15 1 | 14 14
31: 14 17 | 1 13
6: 14 14 | 1 14
2: 1 24 | 14 4
0: 8 11
13: 14 3 | 1 12
15: 1 | 14
17: 14 2 | 1 7
23: 25 1 | 22 14
28: 16 1
4: 1 1
20: 14 14 | 1 15
3: 5 14 | 16 1
27: 1 6 | 14 18
14: "b"
21: 14 1 | 1 14
25: 1 1 | 1 14
22: 14 14
8: 42
26: 14 22 | 1 20
18: 15 15
7: 14 5 | 1 21
24: 14 1

abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaaaabbaaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
babaaabbbaaabaababbaabababaaab
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba
Without updating rules 8 and 11, these rules only match three messages: bbabbbbaabaabba, ababaaaaaabaaab, and ababaaaaabbbaba.

However, after updating rules 8 and 11, a total of 12 messages match:

bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba
                 */
                new Tuple<IList<string>, IDictionary<string, string>, int>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\"",
                        "",
                        "ababbb",
                        "bababa",
                        "abbbab",
                        "aaabbb",
                        "aaaabbb",
                    },
                    null,
                    2),
                new Tuple<IList<string>, IDictionary<string, string>, int>(
                    new List<string>()
                    {
                        "42: 9 14 | 10 1",
                        "9: 14 27 | 1 26",
                        "10: 23 14 | 28 1",
                        "1: \"a\"",
                        "11: 42 31",
                        "5: 1 14 | 15 1",
                        "19: 14 1 | 14 14",
                        "12: 24 14 | 19 1",
                        "16: 15 1 | 14 14",
                        "31: 14 17 | 1 13",
                        "6: 14 14 | 1 14",
                        "2: 1 24 | 14 4",
                        "0: 8 11",
                        "13: 14 3 | 1 12",
                        "15: 1 | 14",
                        "17: 14 2 | 1 7",
                        "23: 25 1 | 22 14",
                        "28: 16 1",
                        "4: 1 1",
                        "20: 14 14 | 1 15",
                        "3: 5 14 | 16 1",
                        "27: 1 6 | 14 18",
                        "14: \"b\"",
                        "21: 14 1 | 1 14",
                        "25: 1 1 | 1 14",
                        "22: 14 14",
                        "8: 42",
                        "26: 14 22 | 1 20",
                        "18: 15 15",
                        "7: 14 5 | 1 21",
                        "24: 14 1",
                        "",
                        "abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa",
                        "bbabbbbaabaabba",
                        "babbbbaabbbbbabbbbbbaabaaabaaa",
                        "aaabbbbbbaaaabaababaabababbabaaabbababababaaa",
                        "bbbbbbbaaaabbbbaaabbabaaa",
                        "bbbababbbbaaaaaaaabbababaaababaabab",
                        "ababaaaaaabaaab",
                        "ababaaaaabbbaba",
                        "baabbaaaabbaaaababbaababb",
                        "abbbbabbbbaaaababbbbbbaaaababb",
                        "aaaaabbaabaaaaababaa",
                        "aaaabbaaaabbaaa",
                        "aaaabbaabbaaaaaaabbbabbbaaabbaabaaa",
                        "babaaabbbaaabaababbaabababaaab",
                        "aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba",
                    },
                    null,
                    3),
                new Tuple<IList<string>, IDictionary<string, string>, int>(
                    new List<string>()
                    {
                        "42: 9 14 | 10 1",
                        "9: 14 27 | 1 26",
                        "10: 23 14 | 28 1",
                        "1: \"a\"",
                        "11: 42 31",
                        "5: 1 14 | 15 1",
                        "19: 14 1 | 14 14",
                        "12: 24 14 | 19 1",
                        "16: 15 1 | 14 14",
                        "31: 14 17 | 1 13",
                        "6: 14 14 | 1 14",
                        "2: 1 24 | 14 4",
                        "0: 8 11",
                        "13: 14 3 | 1 12",
                        "15: 1 | 14",
                        "17: 14 2 | 1 7",
                        "23: 25 1 | 22 14",
                        "28: 16 1",
                        "4: 1 1",
                        "20: 14 14 | 1 15",
                        "3: 5 14 | 16 1",
                        "27: 1 6 | 14 18",
                        "14: \"b\"",
                        "21: 14 1 | 1 14",
                        "25: 1 1 | 1 14",
                        "22: 14 14",
                        "8: 42",
                        "26: 14 22 | 1 20",
                        "18: 15 15",
                        "7: 14 5 | 1 21",
                        "24: 14 1",
                        "",
                        "abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa",
                        "bbabbbbaabaabba",
                        "babbbbaabbbbbabbbbbbaabaaabaaa",
                        "aaabbbbbbaaaabaababaabababbabaaabbababababaaa",
                        "bbbbbbbaaaabbbbaaabbabaaa",
                        "bbbababbbbaaaaaaaabbababaaababaabab",
                        "ababaaaaaabaaab",
                        "ababaaaaabbbaba",
                        "baabbaaaabbaaaababbaababb",
                        "abbbbabbbbaaaababbbbbbaaaababb",
                        "aaaaabbaabaaaaababaa",
                        "aaaabbaaaabbaaa",
                        "aaaabbaabbaaaaaaabbbabbbaaabbaabaaa",
                        "babaaabbbaaabaababbaabababaaab",
                        "aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba",
                    },
                    new Dictionary<string, string>()
                    {
                        { "8: 42", "8: 42 | 42 8" },
                        { "11: 42 31", "11: 42 31 | 42 11 31" }
                    },
                    12),
            };

            foreach (var testExample in testData)
            {
                var satelliteData = SatelliteMessageHelper.ParseInputLines(testExample.Item1, testExample.Item2);
                var actual = SatelliteMessageHelper.GetNumberOfValidMatches(satelliteData, 0);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetIsMatchTest()
        {
            /*
Part 1:

Here's a more interesting example:

0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: "a"
5: "b"
Here, because rule 4 matches a and rule 5 matches b, rule 2 matches two letters that are the same (aa or bb), and rule 3 matches two letters that are different (ab or ba).

Since rule 1 matches rules 2 and 3 once each in either order, it must match two pairs of letters, one pair with matching letters and one pair with different letters. This leaves eight possibilities: aaab, aaba, bbab, bbba, abaa, abbb, baaa, or babb.

Rule 0, therefore, matches a (rule 4), then any of the eight options from rule 1, then b (rule 5): aaaabb, aaabab, abbabb, abbbab, aabaab, aabbbb, abaaab, or ababbb.

Part 2:

As you look over the list of messages, you realize your matching rules aren't quite right. To fix them, completely replace rules 8: 42 and 11: 42 31 with the following:

8: 42 | 42 8
11: 42 31 | 42 11 31
This small change has a big impact: now, the rules do contain loops, and the list of messages they could hypothetically match is infinite. You'll need to determine how these changes affect which messages are valid.

Fortunately, many of the rules are unaffected by this change; it might help to start by looking at which rules always match the same set of values and how those rules (especially rules 42 and 31) are used by the new versions of rules 8 and 11.

(Remember, you only need to handle the rules you have; building a solution that could handle any hypothetical combination of rules would be significantly more difficult.)

For example:

42: 9 14 | 10 1
9: 14 27 | 1 26
10: 23 14 | 28 1
1: "a"
11: 42 31
5: 1 14 | 15 1
19: 14 1 | 14 14
12: 24 14 | 19 1
16: 15 1 | 14 14
31: 14 17 | 1 13
6: 14 14 | 1 14
2: 1 24 | 14 4
0: 8 11
13: 14 3 | 1 12
15: 1 | 14
17: 14 2 | 1 7
23: 25 1 | 22 14
28: 16 1
4: 1 1
20: 14 14 | 1 15
3: 5 14 | 16 1
27: 1 6 | 14 18
14: "b"
21: 14 1 | 1 14
25: 1 1 | 1 14
22: 14 14
8: 42
26: 14 22 | 1 20
18: 15 15
7: 14 5 | 1 21
24: 14 1

abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaaaabbaaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
babaaabbbaaabaababbaabababaaab
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba
Without updating rules 8 and 11, these rules only match three messages: bbabbbbaabaabba, ababaaaaaabaaab, and ababaaaaabbbaba.

However, after updating rules 8 and 11, a total of 12 messages match:

bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba
             */
            var testData = new List<Tuple<IList<string>, IDictionary<string, string>, string, bool>>()
            {
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    }, 
                    null,
                    "ababbb",
                    true),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    null,
                    "abbbab",
                    true),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    null,
                    "bababa",
                    false),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    null,
                    "aaabbb",
                    false),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "0: 4 1 5",
                        "1: 2 3 | 3 2",
                        "2: 4 4 | 5 5",
                        "3: 4 5 | 5 4",
                        "4: \"a\"",
                        "5: \"b\""
                    },
                    null,
                    "aaaabbb",
                    false),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "42: 9 14 | 10 1",
                        "9: 14 27 | 1 26",
                        "10: 23 14 | 28 1",
                        "1: \"a\"",
                        "11: 42 31",
                        "5: 1 14 | 15 1",
                        "19: 14 1 | 14 14",
                        "12: 24 14 | 19 1",
                        "16: 15 1 | 14 14",
                        "31: 14 17 | 1 13",
                        "6: 14 14 | 1 14",
                        "2: 1 24 | 14 4",
                        "0: 8 11",
                        "13: 14 3 | 1 12",
                        "15: 1 | 14",
                        "17: 14 2 | 1 7",
                        "23: 25 1 | 22 14",
                        "28: 16 1",
                        "4: 1 1",
                        "20: 14 14 | 1 15",
                        "3: 5 14 | 16 1",
                        "27: 1 6 | 14 18",
                        "14: \"b\"",
                        "21: 14 1 | 1 14",
                        "25: 1 1 | 1 14",
                        "22: 14 14",
                        "8: 42",
                        "26: 14 22 | 1 20",
                        "18: 15 15",
                        "7: 14 5 | 1 21",
                        "24: 14 1",
                    },
                    null,
                    "bbabbbbaabaabba",
                    true),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "42: 9 14 | 10 1",
                        "9: 14 27 | 1 26",
                        "10: 23 14 | 28 1",
                        "1: \"a\"",
                        "11: 42 31",
                        "5: 1 14 | 15 1",
                        "19: 14 1 | 14 14",
                        "12: 24 14 | 19 1",
                        "16: 15 1 | 14 14",
                        "31: 14 17 | 1 13",
                        "6: 14 14 | 1 14",
                        "2: 1 24 | 14 4",
                        "0: 8 11",
                        "13: 14 3 | 1 12",
                        "15: 1 | 14",
                        "17: 14 2 | 1 7",
                        "23: 25 1 | 22 14",
                        "28: 16 1",
                        "4: 1 1",
                        "20: 14 14 | 1 15",
                        "3: 5 14 | 16 1",
                        "27: 1 6 | 14 18",
                        "14: \"b\"",
                        "21: 14 1 | 1 14",
                        "25: 1 1 | 1 14",
                        "22: 14 14",
                        "8: 42",
                        "26: 14 22 | 1 20",
                        "18: 15 15",
                        "7: 14 5 | 1 21",
                        "24: 14 1",
                    },
                    null,
                    "ababaaaaaabaaab",
                    true),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "42: 9 14 | 10 1",
                        "9: 14 27 | 1 26",
                        "10: 23 14 | 28 1",
                        "1: \"a\"",
                        "11: 42 31",
                        "5: 1 14 | 15 1",
                        "19: 14 1 | 14 14",
                        "12: 24 14 | 19 1",
                        "16: 15 1 | 14 14",
                        "31: 14 17 | 1 13",
                        "6: 14 14 | 1 14",
                        "2: 1 24 | 14 4",
                        "0: 8 11",
                        "13: 14 3 | 1 12",
                        "15: 1 | 14",
                        "17: 14 2 | 1 7",
                        "23: 25 1 | 22 14",
                        "28: 16 1",
                        "4: 1 1",
                        "20: 14 14 | 1 15",
                        "3: 5 14 | 16 1",
                        "27: 1 6 | 14 18",
                        "14: \"b\"",
                        "21: 14 1 | 1 14",
                        "25: 1 1 | 1 14",
                        "22: 14 14",
                        "8: 42",
                        "26: 14 22 | 1 20",
                        "18: 15 15",
                        "7: 14 5 | 1 21",
                        "24: 14 1",
                    },
                    null,
                    "ababaaaaabbbaba",
                    true),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "42: 9 14 | 10 1",
                        "9: 14 27 | 1 26",
                        "10: 23 14 | 28 1",
                        "1: \"a\"",
                        "11: 42 31",
                        "5: 1 14 | 15 1",
                        "19: 14 1 | 14 14",
                        "12: 24 14 | 19 1",
                        "16: 15 1 | 14 14",
                        "31: 14 17 | 1 13",
                        "6: 14 14 | 1 14",
                        "2: 1 24 | 14 4",
                        "0: 8 11",
                        "13: 14 3 | 1 12",
                        "15: 1 | 14",
                        "17: 14 2 | 1 7",
                        "23: 25 1 | 22 14",
                        "28: 16 1",
                        "4: 1 1",
                        "20: 14 14 | 1 15",
                        "3: 5 14 | 16 1",
                        "27: 1 6 | 14 18",
                        "14: \"b\"",
                        "21: 14 1 | 1 14",
                        "25: 1 1 | 1 14",
                        "22: 14 14",
                        "8: 42",
                        "26: 14 22 | 1 20",
                        "18: 15 15",
                        "7: 14 5 | 1 21",
                        "24: 14 1",
                    },
                    new Dictionary<string, string>()
                    {
                        { "8: 42", "8: 42 | 42 8" },
                        { "11: 42 31", "11: 42 31 | 42 11 31" }
                    },
                    "bbabbbbaabaabba",
                    true),
                new Tuple<IList<string>, IDictionary<string, string>, string, bool>(
                    new List<string>()
                    {
                        "42: 9 14 | 10 1",
                        "9: 14 27 | 1 26",
                        "10: 23 14 | 28 1",
                        "1: \"a\"",
                        "11: 42 31",
                        "5: 1 14 | 15 1",
                        "19: 14 1 | 14 14",
                        "12: 24 14 | 19 1",
                        "16: 15 1 | 14 14",
                        "31: 14 17 | 1 13",
                        "6: 14 14 | 1 14",
                        "2: 1 24 | 14 4",
                        "0: 8 11",
                        "13: 14 3 | 1 12",
                        "15: 1 | 14",
                        "17: 14 2 | 1 7",
                        "23: 25 1 | 22 14",
                        "28: 16 1",
                        "4: 1 1",
                        "20: 14 14 | 1 15",
                        "3: 5 14 | 16 1",
                        "27: 1 6 | 14 18",
                        "14: \"b\"",
                        "21: 14 1 | 1 14",
                        "25: 1 1 | 1 14",
                        "22: 14 14",
                        "8: 42",
                        "26: 14 22 | 1 20",
                        "18: 15 15",
                        "7: 14 5 | 1 21",
                        "24: 14 1",
                    },
                    new Dictionary<string, string>()
                    {
                        { "8: 42", "8: 42 | 42 8" },
                        { "11: 42 31", "11: 42 31 | 42 11 31" }
                    },
                    "bbabbbbaabaabba",
                    true)
            };

            foreach (var testExample in testData)
            {
                var satelliteData = SatelliteMessageHelper.ParseInputLines(testExample.Item1, testExample.Item2);
                var actual = SatelliteMessageHelper.GetIsMatch(testExample.Item3, 0, satelliteData);
                Assert.Equal(testExample.Item4, actual);
            }
        }

        [Fact]
        public void GetDay19Part01AnswerTest()
        {
            int expected = 216;
            int actual = Day19.GetDay19Part01Answer();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDay19Part02AnswerTest()
        {
            int expected = 400;
            int actual = Day19.GetDay19Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
