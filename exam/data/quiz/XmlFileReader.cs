using System;
using System.Xml;

namespace exam.data.quiz
{
    public class XmlFileReader
    {
        public Dictionary<string, int> ReadAnswersFromXml(string filePath)
        {
            var answerCounts = new Dictionary<string, int>
            {
                { "a", 0 },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 }
            };

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

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

