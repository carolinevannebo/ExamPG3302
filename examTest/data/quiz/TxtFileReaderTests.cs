using System;
using System.Reflection;
using exam.data.quiz;

namespace examTest.data.quiz
{
    [TestFixture]
    public class TxtFileReaderTests
    {
        private TxtFileReader _txtFileReader;
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "quiz.txt");
            _txtFileReader = new TxtFileReader(_filePath);
        }

        [Test]
        public void ReadQuizFile_ReturnsListOfQuestionTemplate()
        {
            var collection = _txtFileReader.ReadQuizFile();

            Assert.That(collection, Is.Not.Null);
            Assert.That(collection, Is.Not.Empty);
            Assert.That(collection.Count, Is.EqualTo(5));
        }
    }
}

