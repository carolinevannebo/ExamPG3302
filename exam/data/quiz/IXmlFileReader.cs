using System;
namespace exam.data.quiz
{
    public interface IXmlFileReader
    {
        Dictionary<string, int> ReadAnswersFromXml(string xmlFilePath);
    }
}

