using AdventOfCode2020.Challenges.Day04;
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
        public void GetIsValidPassportTest()
        {
            var testData = new List<Tuple<List<string>, PassportPolicy, bool>>() {
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
                        "byr:1937 iyr:2017 cid:147 hgt:183cm"
                    }, Day04.PassportPolicyPart01,
                    true),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
                        "hcl:#cfa07d byr:1929"
                    }, Day04.PassportPolicyPart01,
                    false),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "hcl:#ae17e1 iyr:2013",
                        "eyr:2024",
                        "ecl:brn pid:760753108 byr:1931",
                        "hgt:179cm"
                    }, Day04.PassportPolicyPart01,
                    true),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "hcl:#cfa07d eyr:2025 pid:166559648",
                        "iyr:2011 ecl:brn hgt:59in"
                    }, Day04.PassportPolicyPart01,
                    false),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "eyr:1972 cid:100",
                        "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926"
                    }, Day04.PassportPolicyPart02,
                    false),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "iyr:2019",
                        "hcl:#602927 eyr:1967 hgt:170cm",
                        "ecl:grn pid:012533040 byr:1946"
                    }, Day04.PassportPolicyPart02,
                    false),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "hcl:dab227 iyr:2012",
                        "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277"
                    }, Day04.PassportPolicyPart02,
                    false),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "hgt:59cm ecl:zzz",
                        "eyr:2038 hcl:74454a iyr:2023",
                        "pid:3556412378 byr:2007"
                    }, Day04.PassportPolicyPart02,
                    false),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
                        "hcl:#623a2f"
                    }, Day04.PassportPolicyPart02,
                    true),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "eyr:2029 ecl:blu cid:129 byr:1989",
                        "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm"
                    }, Day04.PassportPolicyPart02,
                    true),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "hcl:#888785",
                        "hgt:164cm byr:2001 iyr:2015 cid:88",
                        "pid:545766238 ecl:hzl",
                        "eyr:2022"
                    }, Day04.PassportPolicyPart02,
                    true),
                new Tuple<List<string>, PassportPolicy, bool>(
                    new List<string>()
                    {
                        "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
                    }, Day04.PassportPolicyPart02,
                    true),
            };

            foreach (var testExample in testData)
            {
                var passport = PassportHelper.ParsePassport(testExample.Item1);
                var actual = PassportHelper.GetIsValidPassport(passport, testExample.Item2);
                Assert.Equal(testExample.Item3, actual);
            }
        }

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
                    2),
                new Tuple<List<string>, PassportPolicy, int>(
                    new List<string>()
                    {
                        "eyr:1972 cid:100",
                        "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
                        "",
                        "iyr:2019",
                        "hcl:#602927 eyr:1967 hgt:170cm",
                        "ecl:grn pid:012533040 byr:1946",
                        "",
                        "hcl:dab227 iyr:2012",
                        "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
                        "",
                        "hgt:59cm ecl:zzz",
                        "eyr:2038 hcl:74454a iyr:2023",
                        "pid:3556412378 byr:2007"
                    }, Day04.PassportPolicyPart02,
                    0),
                new Tuple<List<string>, PassportPolicy, int>(
                    new List<string>()
                    {
                        "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
                        "hcl:#623a2f",
                        "",
                        "eyr:2029 ecl:blu cid:129 byr:1989",
                        "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
                        "",
                        "hcl:#888785",
                        "hgt:164cm byr:2001 iyr:2015 cid:88",
                        "pid:545766238 ecl:hzl",
                        "eyr:2022",
                        "",
                        "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
                    }, Day04.PassportPolicyPart02,
                    4),
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

        [Fact]
        public void GetDay04Part02AnswerTest()
        {
            int expected = 175;
            int actual = Day04.GetDay04Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
