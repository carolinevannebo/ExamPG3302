using System;
using exam.data.quiz;
using System.Xml;

namespace examTest.data.quiz
{
    [TestFixture]
    public class XmlFileWriterTests
    {
        private XmlFileWriter _xmlFileWriter;
        private XmlDocument _xmlDocument;

        [SetUp]
        public void Setup()
        {
            _xmlFileWriter = new XmlFileWriter();
            _xmlDocument = new XmlDocument();
        }

        [Test]
        public void WriteAnswersToXml_SavesAnswersToXmlFile()
        {
            // Arrange
            var answers = new List<string> { "a", "b", "c", "d", "c" };
            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Answers><Answer>a</Answer><Answer>b</Answer><Answer>c</Answer><Answer>d</Answer><Answer>c</Answer><Results><a>1</a><b>1</b><c>2</c><d>1</d></Results></Answers>";
            var xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "test.xml");

            // Act
            _xmlFileWriter.WriteAnswersToXml(xmlFilePath, answers);

            // Assert
            Assert.That(File.Exists(xmlFilePath), Is.True);

            _xmlDocument.Load(xmlFilePath);
            var actualXml = _xmlDocument.OuterXml;

            Assert.That(actualXml, Is.EqualTo(expectedXml));
        }
    }
}

