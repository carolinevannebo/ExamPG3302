using System;
using exam.data.repo;
using exam.data.json;
using exam.data.database;
using exam.ui;

namespace exam.logic
{
    public class EventHandler //todo bruk solid til å dele opp denne klassen, er ganske mange funksjoner, kanskje hvert valg er en egen klasse?
    {
        #region Properties

        readonly MainRepository mainRepository = new();
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
                    var userData = new UserData();
                    var userName = userData.Load().UserName;

                    displayMessages.PrintSecondMenu();
                    var choice = Console.ReadKey();

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("");
                            Console.WriteLine("Saving recipe...");
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
            var randomRecipe = mainRepository.GetRandomCocktailRecipe().Result;
            Console.WriteLine("");
            Console.WriteLine(randomRecipe.ToString());
            SecondMenu(randomRecipe);
        }

        public void BrowseSavedRecipes()
        {
            var userData = new UserData();
            var userName = userData.Load().UserName;

            List<CocktailRecipe> allCocktails = DatabaseHelper.GetAllCocktails();
            if (allCocktails != null)
            {
                foreach (var cocktail in allCocktails)
                {
                    Console.WriteLine(cocktail.strDrink);
                    // todo gi valg å gå inn i oppskrift
                }
            }
            else if (allCocktails == null)
            {
                Console.WriteLine($"You have not saved any cocktail recipes yet, {userName}.\n");
            }
            InitialMenu();
        }

        public void InitialMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    //hent brukernavn
                    var userData = new UserData();
                    var userName = userData.Load().UserName;

                    displayMessages.PrintInitialMenu();
                    var choice = Console.ReadKey();

                    var searchLogic = new SearchLogic();

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("\n==== Random Cocktail Recipe ====\n");
                            GetRandomCocktailRecipe();
                            return;
                        case ConsoleKey.D2:
                            Console.WriteLine("\n==== Search Cocktail Recipe ====\n");
                            searchLogic.SearchCocktailRecipesFromApi();
                            return;
                        case ConsoleKey.D3:
                            Console.WriteLine("\n==== Research Ingredient ====\n");
                            searchLogic.SearchIngredientsFromApi();
                            return;
                        case ConsoleKey.D4:
                            Console.WriteLine("\n==== Browse Saved Recipes ====\n");
                            BrowseSavedRecipes();
                            return;
                        case ConsoleKey.D5:
                            Console.WriteLine("\n==== Which Cocktail Should You Prepare Quiz ====\n");
                            QuizLogic quizLogic = new QuizLogic();
                            quizLogic.PrintAndReadQuiz();
                            quizLogic.PresentCocktailBasedOnResult();
                            return;
                        case ConsoleKey.D6:
                            Console.WriteLine("\n==== Quit Program ====\n");
                            Console.WriteLine($"Goodbye {userName}");
                            isRunning = false;
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine("\nYour choice was not recognized: " + choice + "\n");
                            InitialMenu();
                            return; // usikker på denne løsningen
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