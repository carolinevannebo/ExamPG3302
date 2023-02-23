using System;
using exam.data.repo;
using exam.data.userData;
using exam.data.database;
using exam.ui;
using System.Linq;

namespace exam.logic
{
    public class EventHandler
    {
        #region Properties

        //readonly MainRepository mainRepository = new();
        readonly DisplayMessages displayMessages = new();

        #endregion

        #region Methods

        public void SecondMenu(CocktailRecipe cocktail) {
            bool isRunning = true;
            while(isRunning)
            {
                try
                {
                    //hent brukernavn
                    //var userData = new UserData();
                    var userName = UserData.Load().UserName;

                    displayMessages.PrintSecondMenu();
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
                                InitialMenu();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"An error occurred while saving the cocktail: {e.Message}");
                                InitialMenu();
                            }
                            return;
                        case ConsoleKey.D2:
                            InitialMenu();
                            return;
                        case ConsoleKey.D3:
                            Console.WriteLine($"\nGoodbye {userName}");
                            isRunning = false;
                            return;
                        default:
                            Console.WriteLine("\nYour choice was not recognized: " + choice);
                            SecondMenu(cocktail);
                            return;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occured, " + e.Message); // ?
                }
            }
        }

        public void GetRandomCocktailRecipe()
        {
            var randomRecipe = MainRepository.GetRandomCocktailRecipe().Result;
            Console.WriteLine(randomRecipe.ToString());
            SecondMenu(randomRecipe);
        }

        public void InitialMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    var searchLogic = new SearchLogic(); // finn ut hva som er forskjellen på måtene man lager ny instanse av en klasse
                    var quizLogic = new QuizLogic();
                    //var userData = new UserData();
                    var collectionBrowserLogic = new CollectionBrowserLogic();

                    var userName = UserData.Load().UserName;

                    displayMessages.PrintInitialMenu();
                    var choice = Console.ReadKey();

                    Console.WriteLine("\n");

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("==== Random Cocktail Recipe ====\n");
                            GetRandomCocktailRecipe();
                            return;
                        case ConsoleKey.D2:
                            Console.WriteLine("==== Search Cocktail Recipe ====\n");
                            searchLogic.SearchCocktailRecipesFromApi();
                            return;
                        case ConsoleKey.D3:
                            Console.WriteLine("==== Research Ingredient ====\n");
                            searchLogic.SearchIngredientsFromApi();
                            return;
                        case ConsoleKey.D4:
                            Console.WriteLine("==== Browse Your Collection ====\n");
                            collectionBrowserLogic.BrowseSavedRecipes();
                            return;
                        case ConsoleKey.D5:
                            Console.WriteLine("==== Your Best Suited Cocktail Quiz ====\n");
                            quizLogic.PrintAndReadQuiz();
                            quizLogic.PresentCocktailBasedOnResult();
                            return;
                        case ConsoleKey.D6:
                            Console.WriteLine("==== Quit Program ====\n");
                            Console.WriteLine($"Goodbye {userName}");
                            isRunning = false;
                            return;
                        default:
                            Console.WriteLine("Your choice was not recognized: " + choice + "\n");
                            InitialMenu();
                            return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occured, " + e.Message);
                }
            }
        }

        #endregion
    }
}