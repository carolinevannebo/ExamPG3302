using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using exam.logic;
using exam.data.quiz;

namespace examTest.logic;

[TestFixture]
public class QuizLogicTests
{
    [Test]
    public void PrintQuiz_WritesQuestionsAndAnswersToConsole() // denne feiler, men programmet gjør som jeg vil
    {
        // Arrange
        StringWriter consoleOutput = new StringWriter(); // pga denne
        Console.SetOut(consoleOutput);

        // Act
        var quizLogic = new QuizLogic();
        quizLogic.PrintAndReadQuiz();
        var outputLines = consoleOutput.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        // Assert
        Assert.That(outputLines.Length, Is.EqualTo(5));
        Assert.That(outputLines[0], Is.EqualTo("1. Which word best describes you?"));
        Assert.That(outputLines[1], Is.EqualTo("a) Adventurous"));
        Assert.That(outputLines[2], Is.EqualTo("b) Calm"));
        Assert.That(outputLines[2], Is.EqualTo("c) Confident"));
        Assert.That(outputLines[2], Is.EqualTo("d) Fun-loving"));
    }

    [Test]
    public void GetQuiz_ReturnsListOfQuestionTemplates()
    {
        // Arrange
        var quizLogic = new QuizLogic();

        // Act
        var quizData = quizLogic.GetQuiz();

        // Assert
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
    public void GetResults_ReturnsCorrectOutput()
    {
        var answerCounts = new Dictionary<string, int>();
    }

}

