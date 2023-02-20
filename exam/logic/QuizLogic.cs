using System;
using exam.data.quiz;

namespace exam.logic
{
    public class QuizLogic
    {
        public List<QuestionTemplate> GetQuiz()
        {
            // Create a new instance of the FileReader class
            FileReader fileReader = new FileReader("../../../data/quiz/quiz.txt");

            // Read and parse the quiz file
            var quizData = fileReader.ReadQuizFile();
            //var quizDataArray = quizData.ToArray();
            //Console.WriteLine(quizDataArray[0].ToString());
            return quizData;
        }

        public void PrintQuiz()
        {
            var quizData = GetQuiz();
            if (quizData != null)
            {
                Console.WriteLine(quizData.Count);
                // Print the questions and answer options
                foreach (var question in quizData)
                {
                    Console.WriteLine(question.ToString());
                }
            }
            else
            {
                Console.WriteLine("list of questions is null");
            }
            
        }

    }
}

