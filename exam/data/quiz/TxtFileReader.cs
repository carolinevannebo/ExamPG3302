using System.Collections.Generic;
using System.IO;

namespace exam.data.quiz
{
    public class TxtFileReader
    {
        private readonly string filePath; //la til readonly

        public TxtFileReader(string filePath)
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
            using (StreamReader reader = new (filePath))
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

    }
}