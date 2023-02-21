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
        private string _xmlFilePath;

        [SetUp]
        public void Setup()
        {
            _xmlFileWriter = new XmlFileWriter();
            _xmlDocument = new XmlDocument();
            _xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "test.xml");
        }

        [Test]
        public void WriteAnswersToXml_SavesAnswersToXmlFile()
        {
            // Arrange
            var answers = new List<string> { "a", "b", "c", "d", "c" };
            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Answers><Answer>a</Answer><Answer>b</Answer><Answer>c</Answer><Answer>d</Answer><Answer>c</Answer><Results><a>1</a><b>1</b><c>2</c><d>1</d></Results></Answers>";

            // Act
            _xmlFileWriter.WriteAnswersToXml(_xmlFilePath, answers);

            // Assert
            Assert.That(File.Exists(_xmlFilePath), Is.True);

            _xmlDocument.Load(_xmlFilePath);
            var actualXml = _xmlDocument.OuterXml;

            Assert.That(actualXml, Is.EqualTo(expectedXml));
        }
    }
}

