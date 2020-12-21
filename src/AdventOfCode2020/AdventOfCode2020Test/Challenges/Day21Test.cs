using AdventOfCode2020.Challenges.Day21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2020Test.Challenges
{
    public class Day21Test
    {
        [Fact]
        public void ParseInputLinesTest()
        {
            var testData = new List<Tuple<IList<string>, IList<Tuple<IList<string>, IList<string>>>>>()
            {
                new Tuple<IList<string>, IList<Tuple<IList<string>, IList<string>>>>(
                    new List<string>()
                    {
                        "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
                        "trh fvjkl sbzzf mxmxvkd (contains dairy)",
                        "sqjhc fvjkl (contains soy)",
                        "sqjhc mxmxvkd sbzzf (contains fish)"
                    },
                    new List<Tuple<IList<string>, IList<string>>>()
                    {
                        new Tuple<IList<string>, IList<string>>(
                            new List<string>()
                            { "mxmxvkd", "kfcds", "sqjhc", "nhms" },
                            new List<string>()
                            { "dairy", "fish" }),
                        new Tuple<IList<string>, IList<string>>(
                            new List<string>()
                            { "trh", "fvjkl", "sbzzf", "mxmxvkd" },
                            new List<string>()
                            { "dairy" }),
                        new Tuple<IList<string>, IList<string>>(
                            new List<string>()
                            { "sqjhc", "fvjkl" },
                            new List<string>()
                            { "soy" }),
                        new Tuple<IList<string>, IList<string>>(
                            new List<string>()
                            { "sqjhc", "mxmxvkd", "sbzzf" },
                            new List<string>()
                            { "fish" })
                    })
            };

            foreach (var testExample in testData)
            {
                var actual = IngredientHelper.ParseInputLines(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }
        
        [Fact]
        public void TryGetIngredientAllergensTest()
        {
            var testData = new List<Tuple<IList<string>, IList<IList<Tuple<string, string>>>, IList<string>>>()
            {
                new Tuple<IList<string>, IList<IList<Tuple<string, string>>>, IList<string>>(
                    new List<string>()
                    {
                        "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
                        "trh fvjkl sbzzf mxmxvkd (contains dairy)",
                        "sqjhc fvjkl (contains soy)",
                        "sqjhc mxmxvkd sbzzf (contains fish)",
                    },
                    new List<IList<Tuple<string, string>>>()
                    {
                        new List<Tuple<string, string>>()
                        {
                            new Tuple<string, string>("mxmxvkd", "dairy"),
                            new Tuple<string, string>("sqjhc", "fish"),
                            new Tuple<string, string>("fvjkl", "soy")
                        }
                    },
                    new List<string>() { "kfcds", "nhms", "sbzzf", "trh" })
            };

            foreach (var testExample in testData)
            {
                var ingredientLists = IngredientHelper.ParseInputLines(testExample.Item1);
                var success = IngredientHelper.TryGetIngredientAllergens(
                    ingredientLists,
                    out IList<IList<Tuple<string, string>>> ingredientAllergenConfigurations,
                    out IList<string> ingredientsWithNoAllergens);
                Assert.True(success);
                var areEqualIngredientAllergenConfigurations =
                    (testExample.Item2.Count == ingredientAllergenConfigurations.Count)
                    && !ingredientAllergenConfigurations
                    .Where(c => !testExample.Item2
                        .Where(exc => c.Count == exc.Count 
                            && c.All(t => exc.Contains(t)))
                        .Any())
                    .Any();
                Assert.True(areEqualIngredientAllergenConfigurations);
                var areEqualIngredientsWithNoAllergens = (ingredientsWithNoAllergens.Count == testExample.Item3.Count)
                    && !ingredientsWithNoAllergens.Where(i => !testExample.Item3.Contains(i)).Any();
                Assert.True(areEqualIngredientsWithNoAllergens);
            }
        }

        [Fact]
        public void GetNumberOfIngredientAppearancesTest()
        {
            var testData = new List<Tuple<IList<string>, IList<string>, int>>()
            {
                new Tuple<IList<string>, IList<string>, int>(
                    new List<string>()
                    {
                        "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
                        "trh fvjkl sbzzf mxmxvkd (contains dairy)",
                        "sqjhc fvjkl (contains soy)",
                        "sqjhc mxmxvkd sbzzf (contains fish)",
                    },
                    new List<string>() { "kfcds", "nhms", "sbzzf", "trh" },
                    5)
            };

            foreach (var testExample in testData)
            {
                var ingredientLists = IngredientHelper.ParseInputLines(testExample.Item1);
                _ = IngredientHelper.TryGetIngredientAllergens(
                    ingredientLists,
                    out IList<IList<Tuple<string, string>>> ingredientAllergenConfigurations,
                    out IList<string> ingredientsWithNoAllergens);
                var actual = IngredientHelper.GetNumberOfIngredientAppearances(
                    ingredientLists, 
                    ingredientsWithNoAllergens);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetCanonicalDangerousIngredientListTest()
        {
            var testData = new List<Tuple<IList<Tuple<string, string>>, string>>()
            {
                new Tuple<IList<Tuple<string, string>>, string>(
                    new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("sqjhc", "fish"),
                        new Tuple<string, string>("mxmxvkd", "dairy"),
                        new Tuple<string, string>("fvjkl", "soy")
                    },
                    "mxmxvkd,sqjhc,fvjkl")
            };

            foreach (var testExample in testData)
            {
                var actual = IngredientHelper.GetCanonicalDangerousIngredientList(testExample.Item1);
                Assert.Equal(testExample.Item2, actual);
            }
        }

        [Fact]
        public void GetDay21Part01AnswerTest()
        {
            int expected = 1885;
            int actual = Day21.GetDay21Part01Answer();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetDay21Part02AnswerTest()
        {
            string expected = "fllssz,kgbzf,zcdcdf,pzmg,kpsdtv,fvvrc,dqbjj,qpxhfp";
            string actual = Day21.GetDay21Part02Answer();
            Assert.Equal(expected, actual);
        }
    }
}
