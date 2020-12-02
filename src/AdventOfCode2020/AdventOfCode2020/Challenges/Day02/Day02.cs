using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#nullable enable

namespace AdventOfCode2020.Challenges.Day02
{
    public static class Day02
    {
        public const string FILE_NAME = "Day02Input.txt";

        public static int GetDay02Part01Answer()
        {
            // ---Day 2: Password Philosophy ---
            // Your flight departs in a few days from the coastal airport; the easiest way down to the coast from here is via toboggan.
            // The shopkeeper at the North Pole Toboggan Rental Shop is having a bad day. "Something's wrong with our computers; we can't log in!" You ask if you can take a look.
            // Their password database seems to be a little corrupted: some of the passwords wouldn't have been allowed by the Official Toboggan Corporate Policy that was in effect when they were chosen.
            // To try to debug the problem, they have created a list(your puzzle input) of passwords(according to the corrupted database) and the corporate policy when that password was set.
            // For example, suppose you have the following list:
            // 1 - 3 a: abcde
            // 1 - 3 b: cdefg
            // 2 - 9 c: ccccccccc
            // Each line gives the password policy and then the password.The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid.For example, 1 - 3 a means that the password must contain a at least 1 time and at most 3 times.
            // In the above example, 2 passwords are valid.The middle password, cdefg, is not; it contains no instances of b, but needs at least 1.The first and third passwords are valid: they contain one a or nine c, both within the limits of their respective policies.
            // How many passwords are valid according to their policies?
            // Answer: 569
            var inputData = GetDay02Input();
            var result = inputData.Where(pw => pw.IsValid).Count();
            return result;
        }

        private static IList<PasswordWrapper> GetDay02Input()
        {
            var result = new List<PasswordWrapper>();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", FILE_NAME);
            if (!File.Exists(filePath))
            {
                throw new Exception($"Cannot locate file {filePath}");
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (sr.Peek() >= 0)
                {
                    string? currentLine = sr.ReadLine();
                    var passwordWrapper = PasswordHelper.ParsePassword(currentLine);
                    result.Add(passwordWrapper);
                }
            }
            return result;
        }
    }
}
