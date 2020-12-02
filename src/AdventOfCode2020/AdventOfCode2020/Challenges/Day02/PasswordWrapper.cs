using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day02
{
    public class PasswordWrapper
    {
        public string Password { get; set; }
        public PasswordPolicy Policy { get; set; }
        public PasswordWrapper(string password, PasswordPolicy policy)
        {
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        public bool IsValidOldJob { 
            get 
            {
                return PasswordHelper.GetIsValidPasswordOldJob(Password, Policy);
            } 
        }

        public bool IsValid
        {
            get
            {
                return PasswordHelper.GetIsValidPassword(Password, Policy);
            }
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
                PasswordWrapper p = (PasswordWrapper)obj;
                return (string.Equals(Password, p.Password))
                    && (Policy.Equals(p.Policy));
            }
        }

        public override int GetHashCode()
        {
            var tuple = Tuple.Create(Policy, Password);
            int hash = tuple.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return $"{Policy}: {Password}";
        }
    }
}
