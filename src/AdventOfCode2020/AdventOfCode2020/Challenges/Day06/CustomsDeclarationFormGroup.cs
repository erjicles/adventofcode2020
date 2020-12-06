using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day06
{
    public class CustomsDeclarationFormGroup
    {
        public IList<CustomsDeclarationForm> CustomsDeclarationForms { get; } = new List<CustomsDeclarationForm>();

        public CustomsDeclarationFormGroup(IList<CustomsDeclarationForm> customsDeclarationForms)
        {
            CustomsDeclarationForms = customsDeclarationForms;
        }
    }
}
