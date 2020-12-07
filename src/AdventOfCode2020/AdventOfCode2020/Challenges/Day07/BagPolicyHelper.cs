using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day07
{
    public static class BagPolicyHelper
    {
        public const string BagPolicyPattern = @"^((\w+\s)+)bags contain((\s\d+ (\w+\s)+bags?,?)+| no other bags)\.$";

        public static int GetNumberOfOutermostBagsThatCanContainTargetBag(IList<BagPolicy> bagPolicies, string targetBagType)
        {
            var bagPolicyDictionary = bagPolicies.ToDictionary(bp => bp.BagType);
            var result = bagPolicies.Select(bp => bp.BagType)
                .Where(t => GetIsBagAbleToContainTargetBag(t, targetBagType, bagPolicyDictionary))
                .Count();
            return result;
        }

        public static bool GetIsBagAbleToContainTargetBag(
            string outerBagType, 
            string targetBagType, 
            IDictionary<string, BagPolicy> bagPolicyDictionary)
        {
            var policiesToCheck = new Stack<BagPolicy>();
            var initialPolicy = bagPolicyDictionary[outerBagType];
            foreach (var requirement in initialPolicy.ContentsRequirements)
            {
                policiesToCheck.Push(bagPolicyDictionary[requirement.Item1]);
            }
            while (policiesToCheck.Count > 0)
            {
                var currentPolicy = policiesToCheck.Pop();
                if (targetBagType.Equals(currentPolicy.BagType))
                {
                    return true;
                }
                foreach (var contentRequirement in currentPolicy.ContentsRequirements)
                {
                    policiesToCheck.Push(bagPolicyDictionary[contentRequirement.Item1]);
                }
            }
            return false;
        }

        public static int GetNumberOfBagsRequiredInsideBag(IList<BagPolicy> bagPolicies, string outerBagType)
        {
            var result = 0;
            var bagPolicyDictionary = bagPolicies.ToDictionary(bp => bp.BagType);
            var policiesToCheck = new Stack<BagPolicy>();
            var initialPolicy = bagPolicyDictionary[outerBagType];
            foreach (var requirement in initialPolicy.ContentsRequirements)
            {
                var policyForRequirement = bagPolicyDictionary[requirement.Item1];
                for (int i = 0; i < requirement.Item2; i++)
                {
                    policiesToCheck.Push(policyForRequirement);
                }
                result += requirement.Item2;
            }
            while (policiesToCheck.Count > 0)
            {
                var currentPolicy = policiesToCheck.Pop();
                foreach (var contentRequirement in currentPolicy.ContentsRequirements)
                {
                    var policyForRequirement = bagPolicyDictionary[contentRequirement.Item1];
                    for (int i = 0; i < contentRequirement.Item2; i++)
                    {
                        policiesToCheck.Push(policyForRequirement);
                    }
                    result += contentRequirement.Item2;
                }
            }
            return result;
        }

        public static BagPolicy ParseBagPolicy(string bagPolicyDefinition)
        {
            var match = Regex.Match(bagPolicyDefinition, BagPolicyPattern);
            if (!match.Success)
            {
                throw new Exception($"Unexpected bag policy input: {bagPolicyDefinition}");
            }
            var bagType = match.Groups[1].Value.Trim();
            var contentsRequirements = new List<Tuple<string, int>>();
            var contentsRequirementLines = bagPolicyDefinition.Split("contain")[1]
                .Split(",");
            foreach (var contentsRequirementLine in contentsRequirementLines)
            {
                if ("no other bags.".Equals(contentsRequirementLine.Trim()))
                {
                    break;
                }
                var contentsRequirementMatch = Regex.Match(contentsRequirementLine, @"^\s?(\d+) ((\w+\s)+)bags?\.?$");
                if (!contentsRequirementMatch.Success)
                {
                    throw new Exception($"Unexpected contents requirement line: {contentsRequirementLine}");
                }
                var amount = int.Parse(contentsRequirementMatch.Groups[1].Value.Trim());
                var type = contentsRequirementMatch.Groups[2].Value.Trim();
                var requirement = new Tuple<string, int>(type, amount);
                contentsRequirements.Add(requirement);
            }
            return new BagPolicy(bagType, contentsRequirements);
        }

        public static IList<BagPolicy> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<BagPolicy>();
            foreach (var inputLine in inputLines)
            {
                var policy = ParseBagPolicy(inputLine);
                result.Add(policy);
            }
            return result;
        }
    }
}
