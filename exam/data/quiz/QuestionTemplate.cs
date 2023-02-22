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


            foreach (var line in contentLines)
            {
                var indicator = line.Substring(0, 1);

                switch (indicator)
                {
                    case "a":
                        OptionA = line;  // ikke egt en linje, men et element. du må endre navn
                        break;
                    case "b":
                        OptionB = line;
                        break;
                    case "c":
                        OptionC = line;
                        break;
                    case "d":
                        OptionD = line;
                        break;
                    default:
                        Question = line;
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

