using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day04
{
    public class PassportPolicy
    {
        /// <summary>
        /// byr(Birth Year) 
        /// </summary>
        public bool IsRequiredBirthYear { get; set; } = true;

        /// <summary>
        /// iyr(Issue Year) 
        /// </summary>
        public bool IsRequiredIssueYear { get; set; } = true;

        /// <summary>
        /// eyr(Expiration Year) 
        /// </summary>
        public bool IsRequiredExpirationYear { get; set; } = true;

        /// <summary>
        /// hgt(Height)
        /// </summary>
        public bool IsRequiredHeight { get; set; } = true;

        /// <summary>
        /// hcl(Hair Color) 
        /// </summary>
        public bool IsRequiredHairColor { get; set; } = true;

        /// <summary>
        /// ecl(Eye Color) 
        /// </summary>
        public bool IsRequiredEyeColor { get; set; } = true;

        /// <summary>
        /// pid(Passport ID) 
        /// </summary>
        public bool IsRequiredPassportId { get; set; } = true;

        /// <summary>
        /// cid(Country ID) 
        /// </summary>
        public bool IsRequiredCountryId { get; set; } = true;

        public bool IsValidatingFields { get; set; } = true;

        // byr (Birth Year) - four digits; at least 1920 and at most 2002.
        public int BirthYearMin { get; set; } = 1920;
        public int BirthYearMax { get; set; } = 2002;

        // iyr(Issue Year) - four digits; at least 2010 and at most 2020.
        public int IssueYearMin { get; set; } = 2010;
        public int IssueYearMax { get; set; } = 2020;

        // eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
        public int ExpirationYearMin { get; set; } = 2020;
        public int ExpirationYearMax { get; set; } = 2030;

        // hgt(Height) - a number followed by either cm or in:
        // If cm, the number must be at least 150 and at most 193.
        // If in, the number must be at least 59 and at most 76.
        public string HeightPattern { get; set; } = @"^(\d+)(cm|in)$";
        public int HeightCmMin { get; set; } = 150;
        public int HeightCmMax { get; set; } = 193;
        public int HeightInMin { get; set; } = 59;
        public int HeightInMax { get; set; } = 76;

        // hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
        public string HairColorPattern { get; set; } = @"^#[a-f0-9]{6}$";

        // ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
        public string EyeColorPattern { get; set; } = @"^(amb|blu|brn|gry|grn|hzl|oth)$";

        // pid(Passport ID) - a nine-digit number, including leading zeroes.
        public string PassportIdPattern { get; set; } = @"^\d{9}$";

        // cid(Country ID) - ignored, missing or not.
        public string CountryIdPattern { get; set; } = @"^\d+$";
    }
}
