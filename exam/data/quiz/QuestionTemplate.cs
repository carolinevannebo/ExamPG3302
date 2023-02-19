using System;
namespace exam.data.quiz
{
    public class QuestionTemplate
    {
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }

        public QuestionTemplate(string question, string optionA, string optionB, string optionC, string optionD)
        {
            Question = question;
            OptionA = optionA;
            OptionB = optionB;
            OptionC = optionC;
            OptionD = optionD;
        }

        public override string ToString()
        {
            return $"{Question}\n\n{OptionA}\n{OptionB}\n{OptionC}\n{OptionD}";
        }
    }
}

