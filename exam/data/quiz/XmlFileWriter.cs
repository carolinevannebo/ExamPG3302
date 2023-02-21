using System;
using System.Xml;

namespace exam.data.quiz
{
    public class XmlFileWriter
    {
        public void WriteAnswersToXml(string file, List<string> answers)
        {
            // Defining the file path   <- todo ends up in bin/net7.0, needs fixing?
            var xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{file}");
            var settings = new XmlWriterSettings();

            // XML output is formatted with indentation for readability
            settings.Indent = true;

            // Ensuring that the writer is disposed of properly
            using (XmlWriter writer = XmlWriter.Create(xmlFilePath, settings))
            {
                // Root element
                writer.WriteStartElement("Answers");

                var answerCounts = new Dictionary<string, int>
                {
                    { "a", 0 },
                    { "b", 0 },
                    { "c", 0 },
                    { "d", 0 }
                };

                // Counting number of times each answer appears in the list
                foreach (var answer in answers)
                {
                    // Update dictionary and write to file
                    if (answerCounts.ContainsKey(answer))
                    {
                        answerCounts[answer]++;
                        writer.WriteElementString("Answer", answer);
                    }
                }

                // Added results section for readability
                writer.WriteStartElement("Results");

                foreach (var answerCount in answerCounts)
                {
                    writer.WriteElementString(answerCount.Key, answerCount.Value.ToString());
                }

                writer.WriteEndElement(); // Closing Results
                writer.WriteEndElement(); // Closing Answers
            }
        }
    }
}

