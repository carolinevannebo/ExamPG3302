// See https://aka.ms/new-console-template for more information
using exam.logic;
using exam.logic.events;
using exam.ui;
using Microsoft.Extensions.DependencyInjection;

namespace exam
{
    public class Program
    {
        public static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICommand, RandomCocktailCommand>().AddScoped<SearchLogic>()
                .AddSingleton<ICommand, SearchCocktailCommand>()
                .AddSingleton<ICommand, SearchIngredientCommand>()
                .AddSingleton<ICommand, BrowseSavedCocktailsCommand>().AddScoped<CollectionBrowserLogic>()
                .AddSingleton<ICommand, QuizCommand>().AddScoped<QuizLogic>()
                .BuildServiceProvider();

            var commands = serviceProvider.GetServices<ICommand>().ToArray();

            var displayMessages = new DisplayMessages();
            var eventHandler = new logic.EventHandler(commands);

            try
            {
                Console.WriteLine("Starting the program...");
                Console.WriteLine("");

                displayMessages.PrintInitialWelcome();
                eventHandler.InitialMenu();

            } catch (Exception e)
            {
                Console.WriteLine($"An error occurred while starting the program: {e.Message}");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}