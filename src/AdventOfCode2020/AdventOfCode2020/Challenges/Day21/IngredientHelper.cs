using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day21
{
    public static class IngredientHelper
    {
        public static string GetCanonicalDangerousIngredientList(
            IList<Tuple<string, string>> ingredientAllergens)
        {
            var result = string.Join(
                ",",
                ingredientAllergens
                    .OrderBy(t => t.Item2)
                    .Select(t => t.Item1));
            return result;
        }

        public static int GetNumberOfIngredientAppearances(
            IList<Tuple<IList<string>, IList<string>>> ingredientLists, 
            IList<string> ingredients)
        {
            var result = ingredients
                .SelectMany(i => ingredientLists
                    .Where(l => l.Item1.Contains(i))
                    .Select(l => 1))
                .Sum();
            return result;
        }

        public static bool TryGetIngredientAllergens(
            IList<Tuple<IList<string>, IList<string>>> ingredientLists,
            out IList<IList<Tuple<string, string>>> ingredientAllergenConfigurations,
            out IList<string> ingredientsWithNoAllergens)
        {
            ingredientAllergenConfigurations = new List<IList<Tuple<string, string>>>();
            ingredientsWithNoAllergens = new List<string>();
            var uniqueIngredients = ingredientLists.SelectMany(t => t.Item1).ToHashSet();
            var ingredientsWithValidAllergens = new HashSet<string>();

            var candidates = new Stack<
                Tuple<
                    // Remaining unassigned ingredients/allergens
                    IList<Tuple<HashSet<string>, HashSet<string>>>,
                    // Assigned ingredients/allergens
                    IList<Tuple<string, string>>
                    >>();
            var initialConfiguration = ingredientLists
                .Select(t => new Tuple<HashSet<string>, HashSet<string>>(
                    t.Item1.ToHashSet(),
                    t.Item2.ToHashSet()))
                .OrderBy(t => t.Item1.Count)
                .ToList();
            var initialAssignments = new List<Tuple<string, string>>();
            var initialCandidate = new Tuple<
                IList<Tuple<HashSet<string>, HashSet<string>>>, 
                IList<Tuple<string, string>>
                >(initialConfiguration, initialAssignments);
            candidates.Push(initialCandidate);
            while (candidates.Count > 0)
            {
                var candidate = candidates.Pop();

                if (candidate.Item1.Count == 0)
                {
                    throw new Exception($"Encountered candidate with empty configuration");
                }

                // Create new candidates
                var firstUnassignedRow = candidate.Item1[0];
                foreach (var ingredient in firstUnassignedRow.Item1)
                {
                    foreach (var allergen in firstUnassignedRow.Item2)
                    {
                        // Assign the current ingredient to the current allergen
                        // Remove that ingredient and allergen from each row
                        // If we encounter a row that has remaining allergens
                        // but no ingredients, then that's invalid
                        var newConfiguration = new List<Tuple<HashSet<string>, HashSet<string>>>();
                        var newAssignments = candidate.Item2.ToList();
                        var assignment = new Tuple<string, string>(ingredient, allergen);
                        newAssignments.Add(assignment);
                        bool isValid = true;
                        foreach (var configurationRow in candidate.Item1)
                        {
                            var newRowIngredients = configurationRow.Item1.ToHashSet();
                            var newRowAllergens = configurationRow.Item2.ToHashSet();

                            // We found a row where this ingredient isn't present
                            // but the allergen is
                            // That means this allergen assignment is invalid
                            if (!newRowIngredients.Contains(ingredient)
                                && newRowAllergens.Contains(allergen))
                            {
                                isValid = false;
                                break;
                            }

                            newRowIngredients.Remove(ingredient);
                            newRowAllergens.Remove(allergen);
                            
                            // We found a row where allergens remain but no ingredients remain
                            // Therefore, this isn't a valid configuration
                            if (newRowIngredients.Count == 0
                                && newRowAllergens.Count > 0)
                            {
                                isValid = false;
                                break;
                            }
                            
                            // Add this row if ingredients/allergens remain
                            // Don't add a row if there are only igredients but no allergens
                            if (newRowIngredients.Count > 0 && newRowAllergens.Count > 0)
                            {
                                var newConfigurationRow = new Tuple<HashSet<string>, HashSet<string>>(
                                    newRowIngredients,
                                    newRowAllergens);
                                newConfiguration.Add(newConfigurationRow);
                            }
                        }
                        if (isValid)
                        {
                            // If there are no rows left, we found a valid configuration
                            if (newConfiguration.Count == 0)
                            {
                                // Add the set of assigned ingredients
                                foreach (var validAssignment in newAssignments)
                                {
                                    ingredientsWithValidAllergens.Add(validAssignment.Item1);
                                }
                                ingredientAllergenConfigurations.Add(newAssignments);
                                break;
                            }
                            // Things remain to be assigned
                            // Push this into the stack
                            var newCandidate = new Tuple<
                                IList<Tuple<HashSet<string>, HashSet<string>>>,
                                IList<Tuple<string, string>>
                                >(newConfiguration, newAssignments);
                            candidates.Push(newCandidate);
                        }
                    }
                }
            }

            // For each ingredient, add it to the result if it was never assigned
            // a valid allergen
            foreach (var ingredient in uniqueIngredients)
            {
                if (!ingredientsWithValidAllergens.Contains(ingredient))
                {
                    ingredientsWithNoAllergens.Add(ingredient);
                }
            }

            if (ingredientAllergenConfigurations.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static Tuple<IList<string>, IList<string>> ParseInputLine(string inputLine)
        {
            var match = Regex.Match(inputLine, @"^([^\(]+)\(contains([^\)]+)\)\s*$");
            if (!match.Success)
            {
                throw new Exception($"Unrecognized pattern: {inputLine}");
            }
            var ingredientsString = match.Groups[1].Value;
            var allergensString = match.Groups[2].Value;
            var ingredientsMatches = Regex.Matches(ingredientsString, @"\w+");
            var allergensMatches = Regex.Matches(allergensString, @"\w+");
            var ingredients = ingredientsMatches.Select(m => m.Value).ToList();
            var allergens = allergensMatches.Select(m => m.Value).ToList();
            var result = new Tuple<IList<string>, IList<string>>(
                ingredients,
                allergens);
            return result;
        }
        public static IList<Tuple<IList<string>, IList<string>>> ParseInputLines(IList<string> inputLines)
        {
            var result = new List<Tuple<IList<string>, IList<string>>>();
            foreach (var inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    continue;
                }
                var ingredientsAndAllergensForLine = ParseInputLine(inputLine);
                result.Add(ingredientsAndAllergensForLine);
            }
            return result;
        }
    }
}
