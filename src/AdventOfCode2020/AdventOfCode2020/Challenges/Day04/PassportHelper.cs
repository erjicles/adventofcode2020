using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day04
{
    public static class PassportHelper
    {
        public static IList<Passport> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<Passport>();
            var passportDefinition = new StringBuilder();
            foreach (var inputLine in inputLines)
            {
                // The passport definition is complete if we've encountered a blank line
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    var passport = ParsePassport(passportDefinition.ToString());
                    result.Add(passport);
                    passportDefinition.Clear();
                }
                else
                {
                    if (passportDefinition.Length > 0)
                    {
                        passportDefinition.Append(' ');
                    }
                    passportDefinition.Append(inputLine);
                }
            }
            // Add the final passport if the definition is non-empty
            if (passportDefinition.Length > 0)
            {
                var passport = ParsePassport(passportDefinition.ToString());
                result.Add(passport);
            }
            return result;
        }

        public static Passport ParsePassport(string passportDefinition)
        {
            var matchByr = Regex.Match(passportDefinition, @"byr:([^\s]+)");
            var matchIyr = Regex.Match(passportDefinition, @"iyr:([^\s]+)");
            var matchEyr = Regex.Match(passportDefinition, @"eyr:([^\s]+)");
            var matchHgt = Regex.Match(passportDefinition, @"hgt:([^\s]+)");
            var matchHcl = Regex.Match(passportDefinition, @"hcl:([^\s]+)");
            var matchEcl = Regex.Match(passportDefinition, @"ecl:([^\s]+)");
            var matchPid = Regex.Match(passportDefinition, @"pid:([^\s]+)");
            var matchCid = Regex.Match(passportDefinition, @"cid:([^\s]+)");

            var result = new Passport();
            if (matchByr.Success)
            {
                // byr:1937
                // byr:1929
                // byr:1931
                result.BirthYear = matchByr.Groups[1].Value;
            }
            if (matchIyr.Success)
            {
                // iyr:2017
                // iyr:2013
                // iyr:2013
                // iyr:2011
                result.IssueYear = matchIyr.Groups[1].Value;
            }
            if (matchEyr.Success)
            {
                // eyr:2020
                // eyr:2023
                // eyr:2024
                // eyr:2025
                result.ExpirationYear = matchEyr.Groups[1].Value;
            }
            if (matchHgt.Success)
            {
                // hgt:183cm
                // hgt:179cm
                // hgt:59in
                result.Height = matchHgt.Groups[1].Value;
            }
            if (matchHcl.Success)
            {
                // hcl:#fffffd
                // hcl:#cfa07d
                // hcl:#ae17e1
                // hcl:#cfa07d
                result.HairColor = matchHcl.Groups[1].Value;
            }
            if (matchEcl.Success)
            {
                // ecl:gry
                // ecl:amb
                // ecl:brn
                // ecl:brn 
                result.EyeColor = matchEcl.Groups[1].Value;
            }
            if (matchPid.Success)
            {
                // pid:860033327
                // pid:028048884
                // pid:760753108
                // pid:166559648
                result.PassportId = matchPid.Groups[1].Value;
            }
            if (matchCid.Success)
            {
                // cid:147
                // cid:350
                result.CountryId = matchCid.Groups[1].Value;
            }

            return result;

        }

        public static bool GetIsValidPassport(
            Passport passport, 
            PassportPolicy passportPolicy)
        {
            if (passportPolicy.IsRequiredBirthYear
                && string.IsNullOrWhiteSpace(passport.BirthYear))
            {
                return false;
            }
            if (passportPolicy.IsRequiredCountryId
                && string.IsNullOrWhiteSpace(passport.CountryId))
            {
                return false;
            }
            if (passportPolicy.IsRequiredExpirationYear
                && string.IsNullOrWhiteSpace(passport.ExpirationYear))
            {
                return false;
            }
            if (passportPolicy.IsRequiredEyeColor
                && string.IsNullOrWhiteSpace(passport.EyeColor))
            {
                return false;
            }
            if (passportPolicy.IsRequiredHairColor
                && string.IsNullOrWhiteSpace(passport.HairColor))
            {
                return false;
            }
            if (passportPolicy.IsRequiredHeight
                && string.IsNullOrWhiteSpace(passport.Height))
            {
                return false;
            }
            if (passportPolicy.IsRequiredIssueYear
                && string.IsNullOrWhiteSpace(passport.IssueYear))
            {
                return false;
            }
            if (passportPolicy.IsRequiredPassportId
                && string.IsNullOrWhiteSpace(passport.PassportId))
            {
                return false;
            }
            return true;
        }

        public static int GetNumberOfValidPassports(
            IList<Passport> passports, 
            PassportPolicy passportPolicy)
        {
            var result = passports
                .Where(p => PassportHelper.GetIsValidPassport(p, passportPolicy))
                .Count();
            return result;
        }
    }
}
