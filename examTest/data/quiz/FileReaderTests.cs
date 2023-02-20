﻿using System;
using System.Reflection;
using exam.data.quiz;

namespace examTest.data.quiz
{
    [TestFixture]
    public class FileReaderTests
    {
        [Test]
        public void ReadQuizFile2_ReturnsListOfQuestionTemplate()
        {
            // du må finne path, sensor bruker ikke absolute path
            var filePath = "/Users/carolinevannebo/Desktop/IT/3-semester/SoftwareDesign/kont/exam/examTest/data/quiz/quiz.txt";
            FileReader fileReader = new FileReader(filePath);

            var collection = fileReader.ReadQuizFile2();
            Assert.That(collection, Is.Not.Null);
            Assert.That(collection, Is.Not.Empty);
            Assert.That(collection.Count, Is.EqualTo(5));
        }
    }
}
