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

        public static bool GetIsValidPasswordOldJob(string password, PasswordPolicy policy)
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

        public static bool GetIsValidPassword(string password, PasswordPolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }
            // According to the new rules, we interpret the MinOccurrencesRequiredPhrase
            // and MaxOccurrencesRequiredPhrase variables as positions within the string.
            // The required phrase must appear at exactly one of these positions, but not
            // both. We use xor to test that at the end.
            bool firstPositionContainsRequiredPhrase = false;
            bool secondPositionContainsRequiredPhrase = false;
            if (password.Length - policy.MinOccurrencesRequiredPhrase + 1 >= policy.RequiredPhrase.Length)
            {
                firstPositionContainsRequiredPhrase =
                    string.Equals(
                        policy.RequiredPhrase,
                        password.Substring(policy.MinOccurrencesRequiredPhrase - 1, policy.RequiredPhrase.Length));
            }
            if (password.Length - policy.MaxOccurrencesRequiredPhrase + 1 >= policy.RequiredPhrase.Length)
            {
                secondPositionContainsRequiredPhrase =
                    string.Equals(
                        policy.RequiredPhrase,
                        password.Substring(policy.MaxOccurrencesRequiredPhrase - 1, policy.RequiredPhrase.Length));
            }
            return firstPositionContainsRequiredPhrase ^ secondPositionContainsRequiredPhrase;
        }

        public static bool GetIsValidPasswordOldJob(PasswordWrapper passwordWrapper)
        {
            if (passwordWrapper == null)
            {
                throw new ArgumentNullException(nameof(passwordWrapper));
            }
            return GetIsValidPasswordOldJob(passwordWrapper.Password, passwordWrapper.Policy);
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
