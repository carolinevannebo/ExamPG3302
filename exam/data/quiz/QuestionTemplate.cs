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

        public QuestionTemplate(string content)
        {
            string[] contentLines = content.Split('\n');


            foreach (var element in contentLines)
            {
                var indicator = element.Substring(0, 1);

                switch (indicator)
                {
                    case "a":
                        OptionA = element;
                        break;
                    case "b":
                        OptionB = element;
                        break;
                    case "c":
                        OptionC = element;
                        break;
                    case "d":
                        OptionD = element;
                        break;
                    default:
                        Question = element;
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"\n{Question}\n\n{OptionA}\n{OptionB}\n{OptionC}\n{OptionD}";
        }
    }
}

