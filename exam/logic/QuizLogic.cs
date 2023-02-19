using System;
using exam.data.quiz;

namespace exam.logic
{
    public class QuizLogic
    {
        public List<QuestionTemplate> GetQuiz()
        {
            // Create a new instance of the FileReader class
            FileReader fileReader = new FileReader("../../../data/quiz/quiz.txt"); // finner ikke fil

            // Read and parse the quiz file
            var quizData = fileReader.ReadQuizFile();
            var quizDataArray = quizData.ToArray();
            Console.WriteLine(quizDataArray[0].ToString());
            return quizData;
        }

        public void PrintQuiz()
        {
            var quizData = GetQuiz();

            // Print the questions and answer options
            foreach (var question in quizData)
            {
                Console.WriteLine(question.ToString());
            }
        }

    }
}

