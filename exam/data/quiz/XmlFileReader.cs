using System;
using System.Xml;

namespace exam.data.quiz
{
    public class XmlFileReader : IXmlFileReader
    {
        public Dictionary<string, int> ReadAnswersFromXml(string file)
        {
            var answerCounts = new Dictionary<string, int>
            {
                { "a", 0 },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 }
            };

            // New instance of xmlDocument, and defining the path
            var xmlDocument = new XmlDocument();
            var xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{file}");

            // Loading document from specified stream
            xmlDocument.Load(xmlFilePath);

            // Selecting the desired list of nodes matching the Xpath expression
            var answerNodes = xmlDocument.SelectNodes("/Answers/Answer");

            // Checks how many of each letter there are
            foreach (XmlNode answerNode in answerNodes)
            {
                var answer = answerNode.InnerText.ToLower();
                if (answerCounts.ContainsKey(answer))
                {
                    answerCounts[answer]++;
                }
            }

            //Returns a Dictionary of the answers
            return answerCounts;
        }
    }
}

