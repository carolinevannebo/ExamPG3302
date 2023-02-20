using System;
using System.Xml;

namespace exam.data.quiz
{
    public class XmlFileWriter
    {
        public void WriteAnswersToXml(string file, List<string> answers)
        {
            var xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{file}"); // dupliserer du path i test?
            var settings = new XmlWriterSettings();

            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(xmlFilePath, settings))
            {
                writer.WriteStartElement("Answers");

                var answerCounts = new Dictionary<string, int>
                {
                    { "a", 0 },
                    { "b", 0 },
                    { "c", 0 },
                    { "d", 0 }
                };

                foreach (var answer in answers)
                {
                    if (answerCounts.ContainsKey(answer))
                    {
                        answerCounts[answer]++;
                        writer.WriteElementString("Answer", answer);
                    }
                }

                writer.WriteStartElement("Results");
                foreach (var answerCount in answerCounts)
                {
                    writer.WriteElementString(answerCount.Key, answerCount.Value.ToString());
                    Console.WriteLine($"for each answerCount in answerCounts: this {answerCount.Key} has been chosen {answerCount.Value}"); //test
                }
                writer.WriteEndElement(); // Results

                writer.WriteEndElement(); // Answers
            }
        }
    }
}

