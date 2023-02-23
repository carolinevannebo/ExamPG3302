using System;

namespace exam.logic.commands
{
    public class QuizCommand : ICommand
    {
        private readonly QuizLogic _quizLogic;

        public QuizCommand(QuizLogic quizLogic)
        {
            _quizLogic = quizLogic;
        }

        public void Execute()
        {
            _quizLogic.PrintAndReadQuiz();
            _quizLogic.PresentCocktailBasedOnResult();
        }
    }
}

