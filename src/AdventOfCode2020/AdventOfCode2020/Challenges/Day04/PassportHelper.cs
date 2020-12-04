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

        public static Passport ParsePassport(IList<string> inputLines)
        {
            var passportDefinition = new StringBuilder();
            foreach (var inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    continue;
                }
                if (passportDefinition.Length > 0)
                {
                    passportDefinition.Append(' ');
                }
                passportDefinition.Append(inputLine);
            }
            var result = ParsePassport(passportDefinition.ToString());
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

            // If the policy doesn't validate fields, stop now
            if (!passportPolicy.IsValidatingFields)
            {
                return true;
            }

            // Validate birth year
            if (!string.IsNullOrWhiteSpace(passport.BirthYear))
            {
                var birthYear = int.Parse(passport.BirthYear);
                if (birthYear < passportPolicy.BirthYearMin
                    || birthYear > passportPolicy.BirthYearMax)
                {
                    return false;
                }
            }

            // Validate issue year
            if (!string.IsNullOrWhiteSpace(passport.IssueYear))
            {
                var issueYear = int.Parse(passport.IssueYear);
                if (issueYear < passportPolicy.IssueYearMin
                    || issueYear > passportPolicy.IssueYearMax)
                {
                    return false;
                }
            }

            // Validate expiration year
            if (!string.IsNullOrWhiteSpace(passport.ExpirationYear))
            {
                var expirationYear = int.Parse(passport.ExpirationYear);
                if (expirationYear < passportPolicy.ExpirationYearMin
                    || expirationYear > passportPolicy.ExpirationYearMax)
                {
                    return false;
                }
            }

            // Validate height
            if (!string.IsNullOrWhiteSpace(passport.Height))
            {
                // hgt(Height) - a number followed by either cm or in:
                // If cm, the number must be at least 150 and at most 193.
                // If in, the number must be at least 59 and at most 76.
                var heightMatch = Regex.Match(passport.Height, passportPolicy.HeightPattern);
                if (!heightMatch.Success)
                {
                    return false;
                }
                var heightNumber = int.Parse(heightMatch.Groups[1].Value);
                var unit = heightMatch.Groups[2].Value;
                if ("cm".Equals(unit))
                {
                    if (heightNumber < passportPolicy.HeightCmMin
                        || heightNumber > passportPolicy.HeightCmMax)
                    {
                        return false;
                    }
                }
                else if ("in".Equals(unit))
                {
                    if (heightNumber < passportPolicy.HeightInMin
                        || heightNumber > passportPolicy.HeightInMax)
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception($"Unrecognized unit: {unit}");
                }
            }

            // Validate hair color
            if (!string.IsNullOrWhiteSpace(passport.HairColor))
            {
                var hairColorMatch = Regex.Match(passport.HairColor, passportPolicy.HairColorPattern);
                if (!hairColorMatch.Success)
                {
                    return false;
                }
            }

            // Validate eye color
            if (!string.IsNullOrWhiteSpace(passport.EyeColor))
            {
                var eyeColorMatch = Regex.Match(passport.EyeColor, passportPolicy.EyeColorPattern);
                if (!eyeColorMatch.Success)
                {
                    return false;
                }
            }

            // Validate passport id
            if (!string.IsNullOrWhiteSpace(passport.PassportId))
            {
                var passportIdMatch = Regex.Match(passport.PassportId, passportPolicy.PassportIdPattern);
                if (!passportIdMatch.Success)
                {
                    return false;
                }
            }

            // Validate country id

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
