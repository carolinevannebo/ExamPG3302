using System;
using exam.data.quiz;

namespace exam.logic
{
    public class QuizLogic
    {
        private List<string> answers = new List<string>();

        public List<QuestionTemplate> GetQuiz()
        {
            // Create a new instance of the FileReader class
            FileReader fileReader = new FileReader("../../../data/quiz/quiz.txt");

            // Read and parse the quiz file
            var quizData = fileReader.ReadQuizFile();

            return quizData;
        }

        public void PrintQuiz() // endre til print and read
        {
            var quizData = GetQuiz();

            if (quizData != null)
            {
                Console.WriteLine(quizData.Count);

                // Print the questions and answer options
                foreach (var question in quizData)
                {
                    Console.WriteLine(question.ToString());

                    var answer = Console.ReadKey();
                    var answerString = RegisterAnswer(question, answer);

                    answers.Add(answerString);
                }

                var xmlFileWriter = new XmlFileWriter();
                xmlFileWriter.WriteAnswersToXml(answers);
            }
            else
            {
                Console.WriteLine("List of questions is null");
            }
        }

        public string RegisterAnswer(QuestionTemplate question, ConsoleKeyInfo answer)
        {
            switch (answer.Key)
            {
                case ConsoleKey.A:
                    Console.WriteLine("");
                    Console.WriteLine($"You answered: {question.OptionA}");
                    return "a";
                case ConsoleKey.B:
                    Console.WriteLine("");
                    Console.WriteLine($"You answered: {question.OptionB}");
                    return "b";
                case ConsoleKey.C:
                    Console.WriteLine("");
                    Console.WriteLine($"You answered: {question.OptionC}");
                    return "c";
                case ConsoleKey.D:
                    Console.WriteLine("");
                    Console.WriteLine($"You answered: {question.OptionD}");
                    return "d";
                default:
                    Console.WriteLine("");
                    Console.WriteLine("Your choice was not recognized: " + answer);
                    var newAnswer = Console.ReadKey();
                    return RegisterAnswer(question, newAnswer);
            }
        }

    }
}

