using System.Collections.Generic;
using System.IO;

namespace exam.data.quiz
{
    public class FileReader
    {
        private readonly string filePath; //la til readonly

        public FileReader(string filePath)
        {
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
            string[] lines = fileContents.Split('\n');
            string currentQuestion = null;
            string[] answerOptions = new string[4];

            foreach (var line in lines)
            {
                if (line.StartsWith("Results:") || string.IsNullOrWhiteSpace(line))
                {
                    // Stop parsing when we reach the results section or a blank line
                    break;
                }
                else if (line.StartsWith("\t") || line.StartsWith(" ")) // write test on this
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

        /*public Dictionary<string, List<string>> ReadQuizFile()
        {
            var quizData = new Dictionary<string, List<string>>();
            //var lines = File.ReadAllLines(filePath); // la til denne

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
            string[] lines = fileContents.Split('\n');
            List<string> answerOptions = new List<string>();

            string currentQuestion = null;

            foreach (var line in lines) // erstattet string med var
            {
                if (line.StartsWith("Results:")) // la til string.IsNullOrWhiteSpace(line) ||
                {
                    break; // Stop parsing when we reach the results section
                }
                else if (line.StartsWith("\t") || line.StartsWith(" "))
                {
                    // Add the answer to the current question
                    //answerOptions.Add(line.TrimStart());
                    continue;
                }
                else
                {
                    // Add a new question
                    if (currentQuestion != null)
                    {
                        if (!quizData.ContainsKey(currentQuestion))
                            quizData.Add(currentQuestion, answerOptions);
                    }

                    string[] parts = line.Split('.');
                    if (parts.Length >= 2) // check if parts array has at least 2 elements before accessing second element
                    currentQuestion = parts[1].Trim();
                }
            }

            // Add the last question to the quiz data
            if (currentQuestion != null)
            {
                if (!quizData.ContainsKey(currentQuestion))
                quizData.Add(currentQuestion, answerOptions);
            }

            return quizData;
        }*/
    }
}