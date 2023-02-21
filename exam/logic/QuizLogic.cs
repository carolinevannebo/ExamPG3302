﻿using System;
using exam.data.quiz;

namespace exam.logic
{
    public class QuizLogic
    {
        private List<string> _answers = new List<string>();

        //private readonly IXmlFileReader _xmlFileReader;

        public QuizLogic() { }

        /*public QuizLogic(IXmlFileReader xmlFileReader)
        {
            _xmlFileReader = xmlFileReader;
        }*/

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

                    // Register the answers
                    var answer = Console.ReadKey();
                    var answerString = RegisterAnswer(question, answer);

                    // Save answers to List
                    _answers.Add(answerString);
                }

                // Send list to XML file
                var xmlFileWriter = new XmlFileWriter();
                xmlFileWriter.WriteAnswersToXml("answers.xml", _answers);

                // Calculate the results
                var result = GetResults();

                Console.WriteLine("");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Could not retrieve questions");

                // Fallback to main menu so program does not crash
                EventHandler eventHandler = new EventHandler();
                eventHandler.InitialMenu();
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

                    // Fallback so user can try again and program does not crash
                    var newAnswer = Console.ReadKey();

                    return RegisterAnswer(question, newAnswer);
            }
        }

        public string GetResults()
        {
            // New instance of file reader to retrieve the results
            var xmlFileReader = new XmlFileReader();
            var answerCounts = xmlFileReader.ReadAnswersFromXml("answers.xml");

            // Define each score
            int aCount = answerCounts["a"];
            int bCount = answerCounts["b"];
            int cCount = answerCounts["c"];
            int dCount = answerCounts["d"];

            // Return result based on scores -- Would this do better as switch case?
            if (aCount > bCount && aCount > cCount && aCount > dCount)
            {
                return "Based on your answers you should make a spicy Margarita!";
            }
            else if (bCount > aCount && bCount > cCount && bCount > dCount)
            {
                return "Based on your answers you should make a classic Gin and Tonic!";
            }
            else if (cCount > aCount && cCount > bCount && cCount > dCount)
            {
                return "Based on your answers you should make a Mojito!";
            }
            else if (dCount > aCount && dCount > bCount && dCount > cCount)
            {
                return "Based on your answers you should make an Old Fashioned!";
            }
            else // Default in case there is a 2-2-1 score. My favorite cocktail:)
            {
                return "Based on your answers you should make a Whiskey Sour!";
            }
        }

    }
}

