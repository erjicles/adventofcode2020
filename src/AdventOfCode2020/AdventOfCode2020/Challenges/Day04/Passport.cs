using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace AdventOfCode2020.Challenges.Day04
{
    public class Passport
    {
        /// <summary>
        /// byr(Birth Year) 
        /// </summary>
        public string? BirthYear { get; set; }
        
        /// <summary>
        /// iyr(Issue Year) 
        /// </summary>
        public string? IssueYear { get; set; }

        /// <summary>
        /// eyr(Expiration Year) 
        /// </summary>
        public string? ExpirationYear { get; set; }
        
        /// <summary>
        /// hgt(Height)
        /// </summary>
        public string? Height { get; set; }

        /// <summary>
        /// hcl(Hair Color) 
        /// </summary>
        public string? HairColor { get; set; }

        /// <summary>
        /// ecl(Eye Color) 
        /// </summary>
        public string? EyeColor { get; set; }

        /// <summary>
        /// pid(Passport ID) 
        /// </summary>
        public string? PassportId { get; set; }
        
        /// <summary>
        /// cid(Country ID) 
        /// </summary>
        public string? CountryId { get; set; }
    }
}
