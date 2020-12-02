using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day02
{
    public static class PasswordHelper
    {
        public static PasswordWrapper ParsePassword(string passwordLine)
        {
            if (passwordLine == null)
            {
                throw new ArgumentNullException(nameof(passwordLine));
            }
            string pattern = @"(\d+)\s?-\s?(\d+)\s(\w+):\s(\w+)";
            var match = Regex.Match(passwordLine, pattern);
            if (match.Success)
            {
                var minOccurrences = int.Parse(match.Groups[1].Value);
                var maxOccurrences = int.Parse(match.Groups[2].Value);
                var requiredPhrase = match.Groups[3].Value;
                var password = match.Groups[4].Value;
                var policy = new PasswordPolicy(minOccurrences, maxOccurrences, requiredPhrase);
                var passwordWrapper = new PasswordWrapper(password, policy);
                return passwordWrapper;
            }
            return null;
        }

        public static bool GetIsValidPassword(string password, PasswordPolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }
            var occurrencesOfRequiredPhrase = Regex.Matches(password, Regex.Escape(policy.RequiredPhrase)).Count;
            if (policy.MinOccurrencesRequiredPhrase <= occurrencesOfRequiredPhrase
                && policy.MaxOccurrencesRequiredPhrase >= occurrencesOfRequiredPhrase)
            {
                return true;
            }
            return false;
        }

        public static bool GetIsValidPassword(PasswordWrapper passwordWrapper)
        {
            if (passwordWrapper == null)
            {
                throw new ArgumentNullException(nameof(passwordWrapper));
            }
            return GetIsValidPassword(passwordWrapper.Password, passwordWrapper.Policy);
        }
    }
}
