using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day06
{
    public class CustomsDeclarationForm
    {
        public string AnswerString { get; set; } = string.Empty;
        public HashSet<string> Answers { get; } = new HashSet<string>();

        public CustomsDeclarationForm(string answerString)
        {
            AnswerString = answerString;
            foreach (var answer in answerString)
            {
                if (!Answers.Contains(answer.ToString()))
                {
                    Answers.Add(answer.ToString());
                }
            }
        }
    }
}
