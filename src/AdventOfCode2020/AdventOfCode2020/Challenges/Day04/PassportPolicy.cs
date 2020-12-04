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
    }
}
