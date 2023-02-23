using System;
using exam.data.repo;
using exam.data.userData;
using exam.data.database;
using exam.logic.events;
using exam.logic.factory;
using exam.ui;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace exam.logic
{
    public class EventHandler
    {
        private readonly ICommand[] _commands;
        readonly DisplayMessages _displayMessages = new();

        public EventHandler(ICommand[] commands)
        {
            _commands = commands;
        }

        public EventHandler() { }

        public void SecondMenu(CocktailRecipe cocktail) {
            while(true)
            {
                try
                {
                    _displayMessages.PrintSecondMenu();
                    var choice = Console.ReadKey();

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("");
                            Console.WriteLine($"Saving {cocktail.strDrink} to your collection...");
                            try
                            {
                                // Insert the cocktail into the database
                                DatabaseHelper.InsertCocktail(cocktail);
                                Console.WriteLine("Success!\n");
                                //InitialMenu();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"An error occurred while saving the cocktail: {e.Message}");
                                //InitialMenu();
                            }
                            return;
                        case ConsoleKey.D2:
                            return;
                        case ConsoleKey.D3:
                            Console.WriteLine($"Goodbye {UserData.Load().UserName}");
                            Environment.Exit(0);
                            return;
                        default:
                            Console.WriteLine("\nYour choice was not recognized: " + choice);
                            continue;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occured, " + e.Message); // ?
                }
            }
        }

        public void InitialMenu()
        {
            while (true)
            {
                try
                {
                    _displayMessages.PrintInitialMenu();
                    var choice = Console.ReadKey();

                    Console.WriteLine("\n");

                    ICommand? command = null;

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("==== Random Cocktail Recipe ====\n");
                            command = _commands.OfType<RandomCocktailCommand>().SingleOrDefault();
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine("==== Search Cocktail Recipe ====\n");
                            command = _commands.OfType<SearchCocktailCommand>().SingleOrDefault();
                            break;
                        case ConsoleKey.D3:
                            Console.WriteLine("==== Research Ingredient ====\n");
                            command = _commands.OfType<SearchIngredientCommand>().SingleOrDefault();
                            break;
                        case ConsoleKey.D4:
                            Console.WriteLine("==== Browse Your Collection ====\n");
                            command = _commands.OfType<BrowseSavedCocktailsCommand>().SingleOrDefault();
                            break;
                        case ConsoleKey.D5:
                            Console.WriteLine("==== Your Best Suited Cocktail Quiz ====\n");
                            command = _commands.OfType<QuizCommand>().SingleOrDefault();
                            break;
                        case ConsoleKey.D6:
                            Console.WriteLine("==== Quit Program ====\n");
                            Console.WriteLine($"Goodbye {UserData.Load().UserName}");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Your choice was not recognized: " + choice + "\n");
                            continue;
                    }

                    if (command!= null)
                    {
                        command.Execute();
                    }
                    else
                    {
                        Console.WriteLine("Command not found");
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occured, " + e.Message);
                }
            }
        }

    }
}