// See https://aka.ms/new-console-template for more information
using exam.data.repo;
using exam.logic;
using exam.ui;

namespace exam
{
    public class Program
    {
        public static void Main()
        {
            //var randomCocktailRecipe = new MainRepository().GetRandomCocktailRecipe().Result;
            //Console.WriteLine(randomCocktailRecipe.ToString());

            var eventHandler = new logic.EventHandler();
            var displayMessages = new DisplayMessages();
            try
            {
                displayMessages.PrintInitialWelcome();
                eventHandler.InitialMenu();
            } catch (IOException e)
            {
               //todo string? stackTrace = e.StackTrace <--- print stacktrace
            }
        }
    }
}