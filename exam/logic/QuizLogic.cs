using System;
using exam.data.quiz;

namespace exam.logic
{
    public class QuizLogic
    {
        private List<string> answers = new List<string>();

        private readonly IXmlFileReader _xmlFileReader;

        public QuizLogic() { }

        public QuizLogic(IXmlFileReader xmlFileReader)
        {
            _xmlFileReader = xmlFileReader;
        }

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

                var xmlFileReader = new XmlFileReader();
                var filePath = "/Users/carolinevannebo/Desktop/IT/3-semester/SoftwareDesign/kont/exam/examTest/data/quiz/answers.xml";
                var answerCounts = xmlFileReader.ReadAnswersFromXml(filePath);
                var result = GetResults(answerCounts);

                Console.WriteLine("");
                Console.WriteLine(result);
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

        public string GetResults(Dictionary<string, int> answerCounts) //Kommer ikke til å funke
        {
            //var xmlFileReader = new XmlFileReader();
            //var answerCounts = xmlFileReader.ReadAnswersFromXml("../../data/quiz/answers.xml"); // feil path :( "/Users/carolinevannebo/Desktop/IT/3-semester/SoftwareDesign/kont/exam/examTest/data/quiz/answers.xml"
            //DENNE   ->      var answerCounts = xmlFileReader.ReadAnswersFromXml("/Users/carolinevannebo/Desktop/IT/3-semester/SoftwareDesign/kont/exam/examTest/data/quiz/answers.xml");
            //answerCounts = xmlFileReader.ReadAnswersFromXml(filePath);


            int aCount = answerCounts["a"];
            int bCount = answerCounts["b"];
            int cCount = answerCounts["c"];
            int dCount = answerCounts["d"];

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
            else
            {
                return "Based on your answers you should make a Whiskey Sour!";
            }
        }

    }
}

