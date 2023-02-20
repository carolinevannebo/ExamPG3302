using System;
using System.Reflection;
using exam.data.quiz;

namespace examTest.data.quiz
{
    [TestFixture]
    public class TxtFileReaderTests
    {
        [Test]
        public void ReadQuizFile_ReturnsListOfQuestionTemplate()
        {
            // du må finne path, sensor bruker ikke din absolute path
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "quiz.txt");
            var filePath = "/Users/carolinevannebo/Desktop/IT/3-semester/SoftwareDesign/kont/exam/examTest/data/quiz/quiz.txt";
            TxtFileReader fileReader = new TxtFileReader(filePath);

            var collection = fileReader.ReadQuizFile();
            Assert.That(collection, Is.Not.Null);
            Assert.That(collection, Is.Not.Empty);
            Assert.That(collection.Count, Is.EqualTo(5));
        }
    }
}

