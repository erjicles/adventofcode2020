using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day06
{
    public static class CustomsDeclarationFormHelper
    {
        public static CustomsDeclarationForm ParseCustomsDeclarationForm(string input)
        {
            var result = new CustomsDeclarationForm(input);
            return result;
        }

        public static CustomsDeclarationFormGroup ParseCustomsDeclarationFormGroup(IList<string> groupInputLines)
        {
            var forms = new List<CustomsDeclarationForm>();
            foreach (var inputLine in groupInputLines)
            {
                var form = ParseCustomsDeclarationForm(inputLine);
                forms.Add(form);
            }
            var result = new CustomsDeclarationFormGroup(forms);
            return result;
        }

        public static IList<CustomsDeclarationFormGroup> ParseInput(IList<string> inputLines)
        {
            var result = new List<CustomsDeclarationFormGroup>();

            var groups = new List<List<string>>();
            var currentGroup = new List<string>();
            foreach (var inputLine in inputLines)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    groups.Add(currentGroup);
                    currentGroup = new List<string>();
                }
                else
                {
                    currentGroup.Add(inputLine);
                }
            }
            if (currentGroup.Count > 0)
            {
                groups.Add(currentGroup);
            }
            foreach (var groupLines in groups)
            {
                var group = ParseCustomsDeclarationFormGroup(groupLines);
                result.Add(group);
            }
            return result;
        }

        public static int GetNumberOfUniqueAnswersInGroup(CustomsDeclarationFormGroup group)
        {
            var uniqueAnswers = new HashSet<string>();
            foreach (var form in group.CustomsDeclarationForms)
            {
                uniqueAnswers.UnionWith(form.Answers);
            }
            return uniqueAnswers.Count;
        }
    }
}
