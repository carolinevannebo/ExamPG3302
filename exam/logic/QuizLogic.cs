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
            TxtFileReader fileReader = new TxtFileReader("../../../data/quiz/quiz.txt");

            // Read and parse the quiz file
            var quizData = fileReader.ReadQuizFile();

            return quizData;
        }

        public void PrintAndReadQuiz()
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
                xmlFileWriter.WriteAnswersToXml("answers.xml", answers); //"../../data/quiz/answers.xml"
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

        public void GetResults() //Kommer ikke til å funke
        {
            var xmlFileReader = new XmlFileReader();
            var answerCounts = xmlFileReader.ReadAnswersFromXml("../../data/quiz/answers.xml");

            // Determine the user's preferred cocktail based on their answers
            var maxCount = 0;
            var preferredCocktail = "";

            foreach (var answerCount in answerCounts)
            {
                if (answerCount.Value > maxCount)
                {
                    maxCount = answerCount.Value;

                    switch (answerCount.Key)
                    {
                        case "a":
                            preferredCocktail = "Spicy Margarita";
                            break;
                        case "b":
                            preferredCocktail = "Classic Gin and Tonic";
                            break;
                        case "c":
                            preferredCocktail = "Mojito";
                            break;
                        case "d":
                            preferredCocktail = "Old Fashioned";
                            break;
                    }
                }
            }

            // Print the results
            Console.WriteLine("Results:");
            Console.WriteLine($"If you mostly answered A's, you're a {preferredCocktail}!");
        }

    }
}

