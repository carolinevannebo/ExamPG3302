using System;
using System.Xml;

namespace exam.data.quiz
{
    public class XmlFileWriter
    {
        public void WriteAnswersToXml(List<string> answers) // dette kan refaktoreres til egen filskriver klasse
        {
            using (XmlWriter writer = XmlWriter.Create("../../data/quiz/answers.xml"))
            {
                writer.WriteStartElement("Answers");

                int numA = 0;
                int numB = 0;
                int numC = 0;
                int numD = 0;

                foreach (var answer in answers)
                {
                    switch (answer)
                    {
                        case "a":
                            numA++;
                            break;
                        case "b":
                            numB++;
                            break;
                        case "c":
                            numC++;
                            break;
                        case "d":
                            numD++;
                            break;
                    }
                    writer.WriteElementString("Answer", answer);
                }

                writer.WriteStartElement("Results");
                writer.WriteElementString("A", numA.ToString());
                writer.WriteElementString("B", numB.ToString());
                writer.WriteElementString("C", numC.ToString());
                writer.WriteElementString("D", numD.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }
}

