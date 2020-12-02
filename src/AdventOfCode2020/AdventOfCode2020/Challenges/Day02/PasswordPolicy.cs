using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day02
{
    public class PasswordPolicy
    {
        public int MinOccurrencesRequiredPhrase { get; set; }
        public int MaxOccurrencesRequiredPhrase { get; set; }
        public string RequiredPhrase { get; set; }
        public PasswordPolicy(
            int minOccurrences, 
            int maxOccurrences, 
            string requiredPhrase)
        {
            MinOccurrencesRequiredPhrase = minOccurrences;
            MaxOccurrencesRequiredPhrase = maxOccurrences;
            RequiredPhrase = requiredPhrase ?? throw new ArgumentNullException(nameof(RequiredPhrase));
        }

        // Equals, GetHashCode, and ToString() adapted from Microsoft example here:
        // https://docs.microsoft.com/en-us/dotnet/api/system.object.equals?view=netcore-3.1
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                PasswordPolicy p = (PasswordPolicy)obj;
                return (MinOccurrencesRequiredPhrase == p.MinOccurrencesRequiredPhrase) 
                    && (MaxOccurrencesRequiredPhrase == p.MaxOccurrencesRequiredPhrase) 
                    && (string.Equals(RequiredPhrase, p.RequiredPhrase));
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(MinOccurrencesRequiredPhrase, MaxOccurrencesRequiredPhrase, RequiredPhrase);
            int hash = tuple.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return $"{MinOccurrencesRequiredPhrase}-{MaxOccurrencesRequiredPhrase} {RequiredPhrase}";
        }
    }
}
