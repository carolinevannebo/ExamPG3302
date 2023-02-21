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

            var xmlDocument = new XmlDocument();
            var xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{file}");
            xmlDocument.Load(xmlFilePath);

            var answerNodes = xmlDocument.SelectNodes("/Answers/Answer");

            foreach (XmlNode answerNode in answerNodes)
            {
                var answer = answerNode.InnerText.ToLower();
                if (answerCounts.ContainsKey(answer))
                {
                    answerCounts[answer]++;
                }
            }

            return answerCounts;
        }
    }
}

