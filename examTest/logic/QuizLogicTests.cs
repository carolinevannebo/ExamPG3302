using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Moq;
using exam.logic;
using exam.data.quiz;

namespace examTest.logic;

[TestFixture]
public class QuizLogicTests
{
    private XmlFileWriter _xmlFileWriter;

    [SetUp]
    public void Setup()
    {
        _xmlFileWriter = new XmlFileWriter();
    }

    [Test]
    public void GetQuiz_ReturnsListOfQuestionTemplates()
    {
        var quizData = QuizLogic.GetQuiz();

        Assert.That(quizData, Is.Not.Null);
        Assert.That(quizData, Is.InstanceOf<List<QuestionTemplate>>());

        var question = quizData.First();

        Assert.That(question.Question, Is.EqualTo("1. Which word best describes you?"));
        Assert.That(question.OptionA, Is.EqualTo("a) Adventurous"));
        Assert.That(question.OptionB, Is.EqualTo("b) Calm"));
        Assert.That(question.OptionC, Is.EqualTo("c) Confident"));
        Assert.That(question.OptionD, Is.EqualTo("d) Fun-loving"));
    }

    [Test]
    public void GetResults_ReturnsExpectedResult()
    {
        var xmlFilePath = "answers.xml";

        var answerCounts = new Dictionary<string, int>
        {
            { "a", 1 },
            { "b", 1 },
            { "c", 1 },
            { "d", 3 }
        };

        // Creating a List<string> of answers from the answerCounts dictionary using LINQ
        var answers = answerCounts.SelectMany(x => Enumerable.Repeat(x.Key, x.Value)).ToList();

        // Write the answer counts to the XML file
        _xmlFileWriter.WriteAnswersToXml(xmlFilePath, answers);

        var expected = "Based on your answers you should make an Old Fashioned!";

        // Act
        var actual = QuizLogic.GetResults();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));

        // Cleanup
        File.Delete(xmlFilePath);
    }
}