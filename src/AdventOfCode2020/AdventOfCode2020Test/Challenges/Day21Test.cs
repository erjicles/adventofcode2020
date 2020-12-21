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
        public void GetIngredientsWithNoAllergensTest()
        {
            var testData = new List<Tuple<IList<string>, IList<string>>>()
            {
                new Tuple<IList<string>, IList<string>>(
                    new List<string>()
                    {
                        "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
                        "trh fvjkl sbzzf mxmxvkd (contains dairy)",
                        "sqjhc fvjkl (contains soy)",
                        "sqjhc mxmxvkd sbzzf (contains fish)",
                    },
                    new List<string>() { "kfcds", "nhms", "sbzzf", "trh" })
            };

            foreach (var testExample in testData)
            {
                var ingredientLists = IngredientHelper.ParseInputLines(testExample.Item1);
                var actual = IngredientHelper.GetIngredientsWithNoAllergens(ingredientLists);
                var areEqual = (actual.Count == testExample.Item2.Count)
                    && !actual.Where(i => !testExample.Item2.Contains(i)).Any();
                Assert.True(areEqual);
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
                var ingredients = IngredientHelper.GetIngredientsWithNoAllergens(ingredientLists);
                var actual = IngredientHelper.GetNumberOfIngredientAppearances(ingredientLists, ingredients);
                Assert.Equal(testExample.Item3, actual);
            }
        }

        [Fact]
        public void GetDay21Part01AnswerTest()
        {
            int expected = 1885;
            int actual = Day21.GetDay21Part01Answer();
            Assert.Equal(expected, actual);
        }
    }
}
