﻿using AdventOfCode2020.Challenges.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day04Test
    {
        [Fact]
        public void GetNumberOfValidPassportsTest()
        {
            // Passport data is validated in batch files (your puzzle input). 
            // Each passport is represented as a sequence of key:value pairs 
            // separated by spaces or newlines. Passports are separated by 
            // blank lines.
            var testData = new List<Tuple<List<string>, PassportPolicy, int>>() {
                new Tuple<List<string>, PassportPolicy, int>(
                    // The first passport is valid - all eight fields are 
                    // present. The second passport is invalid - it is missing 
                    // hgt (the Height field).
                    // The third passport is interesting; the only missing 
                    // field is cid, so it looks like data from North Pole 
                    // Credentials, not a passport at all! Surely, nobody would 
                    // mind if you made the system temporarily ignore missing 
                    // cid fields. Treat this "passport" as valid.
                    // The fourth passport is missing two fields, cid and byr.
                    // Missing cid is fine, but missing any other field is not, 
                    // so this passport is invalid.
                    // According to the above rules, your improved system would 
                    // report 2 valid passports.
                    new List<string>()
                    {
                        "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
                        "byr:1937 iyr:2017 cid:147 hgt:183cm",
                        "",
                        "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
                        "hcl:#cfa07d byr:1929",
                        "",
                        "hcl:#ae17e1 iyr:2013",
                        "eyr:2024",
                        "ecl:brn pid:760753108 byr:1931",
                        "hgt:179cm",
                        "",
                        "hcl:#cfa07d eyr:2025 pid:166559648",
                        "iyr:2011 ecl:brn hgt:59in"
                    }, Day04.PassportPolicyPart01,
                    2)
            };

            foreach (var testExample in testData)
            {
                var passports = PassportHelper.ParseInputLines(testExample.Item1);
                var actual = PassportHelper.GetNumberOfValidPassports(passports, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay04Part01AnswerTest()
        {
            int expected = 228;
            int actual = Day04.GetDay04Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}