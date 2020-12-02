using AdventOfCode2020.Challenges.Day02;
using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day02Test
    {
        [Fact]
        public void GetIsValidPasswordOldJobTest()
        {
            // For example, suppose you have the following list:
            // 1-3 a: abcde
            // 1-3 b: cdefg
            // 2-9 c: ccccccccc
            // Each line gives the password policy and then the password. 
            // The password policy indicates the lowest and highest number of 
            // times a given letter must appear for the password to be valid.
            // For example, 1 - 3 a means that the password must contain a at 
            // least 1 time and at most 3 times.
            // In the above example, 2 passwords are valid.The middle password, 
            // cdefg, is not; it contains no instances of b, but needs at least
            // 1.The first and third passwords are valid: they contain one a or 
            // nine c, both within the limits of their respective policies.
            var testData = new List<Tuple<string, bool>>() {
                new Tuple<string, bool>("1 - 3 a: abcde", true),
                new Tuple<string, bool>("1 - 3 b: cdefg", false),
                new Tuple<string, bool>("2 - 9 c: ccccccccc", true)
            };

            foreach (var testExample in testData)
            {
                var passwordWrapper = PasswordHelper.ParsePassword(testExample.Item1);
                var actual = PasswordHelper.GetIsValidPasswordOldJob(passwordWrapper);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetIsValidPasswordTest()
        {
            // Each policy actually describes two positions in the password, 
            // where 1 means the first character, 2 means the second character, 
            // and so on. (Be careful; Toboggan Corporate Policies have no 
            // concept of "index zero"!) Exactly one of these positions must 
            // contain the given letter. Other occurrences of the letter are 
            // irrelevant for the purposes of policy enforcement.
            // Given the same example list from above:
            // 1-3 a: abcde is valid: position 1 contains a and position 3 does not.
            // 1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
            // 2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.
            var testData = new List<Tuple<string, bool>>() {
                new Tuple<string, bool>("1 - 3 a: abcde", true),
                new Tuple<string, bool>("1 - 3 b: cdefg", false),
                new Tuple<string, bool>("2 - 9 c: ccccccccc", false)
            };

            foreach (var testExample in testData)
            {
                var passwordWrapper = PasswordHelper.ParsePassword(testExample.Item1);
                var actual = PasswordHelper.GetIsValidPassword(passwordWrapper);
                Assert.Equal(testExample.Item2, actual);
            }
        }
    }
}
