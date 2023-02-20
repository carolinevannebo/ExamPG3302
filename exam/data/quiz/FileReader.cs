using System.Collections.Generic;
using System.IO;

namespace exam.data.quiz
{
    public class FileReader
    {
        private readonly string filePath; //la til readonly

        public FileReader(string filePath)
        {
            // if (ValidateFilePath(filePath)... // Throw new exception)
            this.filePath = filePath;
        }

        public List<QuestionTemplate> ReadQuizFile()
        {
            var quizData = new List<QuestionTemplate>();

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found: {0}", filePath);
                return quizData;
            }

            // Read the contents of the file
            string fileContents;
            using (StreamReader reader = new StreamReader(filePath))
            {
                fileContents = reader.ReadToEnd();
            }

            // Parse the contents of the file
            string[] lines = fileContents.Split("\n\n");

            foreach (var line in lines)
            {
                if (line.StartsWith("What"))
                    continue;
                if (line.StartsWith("Results:") || string.IsNullOrWhiteSpace(line))
                    break;

                var currentQuestion = new QuestionTemplate(line);
                quizData.Add(currentQuestion);
            }

            return quizData;
        }

        public List<QuestionTemplate> ReadQuizFile1()
        {
            var quizData = new List<QuestionTemplate>();

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found: {0}", filePath);
                return quizData;
            }

            // Read the contents of the file
            string fileContents;
            using (StreamReader reader = new StreamReader(filePath))
            {
                fileContents = reader.ReadToEnd();
            }

            // Parse the contents of the file
            string[] lines = fileContents.Split('\n'); //hva med å splitte på space istedenfor
            string currentQuestion = null;
            string[] answerOptions = new string[4];

            foreach (var line in lines)
            {
                Console.WriteLine(line); //tester
                if (line.StartsWith("Results:") || string.IsNullOrWhiteSpace(line))
                {
                    // Stop parsing when we reach the results section or a blank line
                    break;
                }
                else if (line.StartsWith("What"))
                {
                    continue;
                }
                else if (line.StartsWith("\t") || line.StartsWith(" "))
                {
                    // Add the answer to the current question
                    answerOptions[line[0] - 'a'] = line.TrimStart()[2..]; //  'a' - 'a' = 0, 'b' - 'a' = 1, 'c' - 'a' = 2, 'd' - 'a' = 3
                }
                else
                {
                    // Add a new question
                    if (currentQuestion != null)
                    {
                        var question = new QuestionTemplate(currentQuestion, answerOptions[0], answerOptions[1], answerOptions[2], answerOptions[3]);
                        quizData.Add(question);
                    }

                    string[] parts = line.Split('.');
                    if (parts.Length >= 2)
                    {
                        currentQuestion = parts[1].Trim();
                        answerOptions = new string[4];
                    }
                }
            }

            // Add the last question to the quiz data
            if (currentQuestion != null)
            {
                var question = new QuestionTemplate(currentQuestion, answerOptions[0], answerOptions[1], answerOptions[2], answerOptions[3]);
                quizData.Add(question);
            }

            return quizData;
        }

    }
}