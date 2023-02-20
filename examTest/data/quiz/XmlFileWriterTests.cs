using System;
using exam.data.quiz;
using System.Xml;

namespace examTest.data.quiz
{
    [TestFixture]
    public class XmlFileWriterTests
    {
        [Test]
        public void WriteAnswersToXml_SavesAnswersToXmlFile()
        {
            // Arrange
            var xmlFileWriter = new XmlFileWriter();
            var answers = new List<string> { "a", "b", "c", "d" };
            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Answers><Answer>a</Answer><Answer>b</Answer><Answer>c</Answer><Answer>d</Answer><Results><a>1</a><b>1</b><c>1</c><d>1</d></Results></Answers>";
            var xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "test.xml");

            // Act
            xmlFileWriter.WriteAnswersToXml(xmlFilePath, answers);

            // Assert
            Assert.That(File.Exists(xmlFilePath), Is.True);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);
            var actualXml = xmlDocument.OuterXml;

            Assert.That(actualXml, Is.EqualTo(expectedXml));
        }
    }
}

