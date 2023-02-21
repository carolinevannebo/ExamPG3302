using System;
using System.Text;
using exam.ui;

namespace examTest.ui
{
    public class DisplayMessagesTests
    {
        private DisplayMessages _displayMessages;

        [SetUp]
        public void Setup()
        {
            _displayMessages = new DisplayMessages();
        }

        [Test]
        public void PrintSecondWelcome_ShouldPrintExpectedOutput()
        {
            // Arrange
            var input = "ja";
            var expectedOutput = "Supert! La oss komme i gang.";

            // Mock console input
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Redirect console output to a string builder
            var output = new StringBuilder();
            var outputWriter = new StringWriter(output);
            Console.SetOut(outputWriter);

            // Set username
            _displayMessages.userName = "Ola Nordmann";

            // Act
            _displayMessages.PrintSecondWelcome();

            // Assert
            Assert.That(output.ToString(), Does.Contain($"Nice to meet you Ola Nordmann!"));
            Assert.That(output.ToString(), Does.Contain($"Are you ready to make some cocktails?"));
            Assert.That(output.ToString(), Does.Contain(expectedOutput));
        }
    }
}

