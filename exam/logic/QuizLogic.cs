using System;
using exam.data.quiz;

namespace exam.logic
{
    public class QuizLogic
    {
        private Dictionary<string, List<string>> GetQuiz()
        {
            // Create a new instance of the FileReader class
            FileReader fileReader = new FileReader("../../../data/quiz/quiz.txt"); // finner ikke fil

            // Read and parse the quiz file
            Dictionary<string, List<string>> quizData = fileReader.ReadQuizFile();

            return quizData;
        }

        public void PrintQuiz()
        {
            var quizData = GetQuiz();

            // Print the questions and answer options
            foreach (KeyValuePair<string, List<string>> entry in quizData)
            {
                Console.WriteLine(entry.Key);
                foreach (string answer in entry.Value)
                {
                    Console.WriteLine("- " + answer);
                }
            }
        }
    }
}

