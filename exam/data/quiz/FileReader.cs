using System.Collections.Generic;
using System.IO;

namespace exam.data.quiz
{
    public class FileReader
    {
        private string filePath;

        public FileReader(string filePath)
        {
            this.filePath = filePath;
        }

        public Dictionary<string, List<string>> ReadQuizFile()
        {
            Dictionary<string, List<string>> quizData = new Dictionary<string, List<string>>();

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
            foreach (string line in lines)
            {
                if (line.StartsWith("Results:"))
                {
                    break; // Stop parsing when we reach the results section
                }
                else if (line.StartsWith("\t"))
                {
                    // Add the answer to the current question
                    answerOptions.Add(line.TrimStart());
                }
                else if (line.StartsWith(" "))
                {
                    // Add the answer to the current question
                    answerOptions.Add(line.TrimStart());
                }
                else
                {
                    // Add a new question
                    if (currentQuestion != null)
                    {
                        if (!quizData.ContainsKey(currentQuestion))
                        quizData.Add(currentQuestion, answerOptions);
                        answerOptions = new List<string>();
                    }
                    string[] parts = line.Split('.');
                    if (parts.Length >= 2) // check if parts array has at least 2 elements before accessing second element
                    currentQuestion = parts[1].Trim(); // error here
                }
            }
            if (currentQuestion != null)
            {
                //todo koble svaralternativer til spørsmål
                quizData.Add(currentQuestion, answerOptions);
            }

            return quizData;
        }
    }
}