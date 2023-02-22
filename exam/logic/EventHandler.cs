using System;
using exam.data.repo;
using exam.data.json;
using exam.data.database;
using exam.ui;
using System.Linq;

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
            var randomRecipe = mainRepository.GetRandomCocktailRecipe().Result;
            Console.WriteLine("");
            Console.WriteLine(randomRecipe.ToString());
            SecondMenu(randomRecipe);
        }

        public void BrowseSavedRecipes()
        {
            // Load username
            var userData = new UserData();
            var userName = userData.Load().UserName;

            // Fetch all cocktails in DB
            List<CocktailRecipe> allCocktails = DatabaseHelper.GetAllCocktails();

            if (allCocktails != null)
            {
                // Print collection
                foreach (var cocktail in allCocktails)
                {
                    // -40 to format the string
                    Console.WriteLine($"{cocktail.strDrink,-40} (ID: {cocktail.idDrink})");
                }

                Console.WriteLine($"\nQuite a collection you've got there, {userName}!");

                // Loop to ensure valid ID
                while (true)
                {
                    Console.WriteLine("If you'd like to access one of your cocktails, write its ID");

                    var inputId = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputId))
                    {
                        Console.WriteLine($"\nI'm sorry {userName}, I need a valid ID");
                        continue; // goes back to start of loop until input equals a valid ID
                    }

                    // Define how to find cocktail
                    var chosenCocktail = allCocktails.FirstOrDefault(cocktail => cocktail.idDrink == inputId);

                    // Make sure coktail exist
                    if (chosenCocktail == null)
                    {
                        Console.WriteLine("\nNo cocktail matching your requested ID. Please try again\n");
                        continue; // goes back to start of loop until the cocktail is not null
                    }

                    // Loop to ensure valid commando (go/delete)
                    while (true)
                    {
                        Console.WriteLine($"\nI have your cocktail: {chosenCocktail.strDrink}");
                        Console.Write("Please write 'go' to see the recipe, or 'delete' to remove it from your collection\n");

                        var commando = Console.ReadLine();

                        if (string.IsNullOrEmpty(commando))
                        {
                            Console.WriteLine($"\nMy apologies {userName}, but I did not understand your command: '{commando}'"); // den er stuck her
                            continue; // goes back to start of sub-loop until input is not null or empty
                        }

                        // Print cocktail
                        if (commando.ToLower().Contains("go"))
                        {
                            Console.WriteLine(chosenCocktail.ToString());
                        }

                        // Delete cocktail
                        else if (commando.ToLower().Contains("delete"))
                        {
                            Console.WriteLine($"Removing {chosenCocktail.strDrink} from your collection...");

                            try
                            {
                                DatabaseHelper.DeleteCocktail(chosenCocktail);
                                Console.WriteLine("Success!\n");
                                
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"An error occurred while removing the cocktail: {e.Message}\n");
                            }
                        }

                        // Navigate further in the program
                        Console.WriteLine($"How may I be of service, {userName}?");
                        Console.WriteLine("1: Browse cocktail recipe collection");
                        Console.WriteLine("2: Go back to main menu\n");

                        while (true)
                        {
                            var choice = Console.ReadKey();

                            switch (choice.Key)
                            {
                                case ConsoleKey.D1:
                                    BrowseSavedRecipes();
                                    break;
                                case ConsoleKey.D2:
                                    InitialMenu();
                                    break;
                                default:
                                    Console.WriteLine("\nYour choice was not recognized: " + choice + "\n");
                                    continue;
                            }

                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"You have not saved any cocktail recipes yet, {userName}");
                Console.WriteLine("To do so you can choose a random recipe, search for one, or take a quiz.");
                Thread.Sleep(1000);
                InitialMenu();
            }
        }

        public void InitialMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    var searchLogic = new SearchLogic(); // spør hva som er forskjellen på måtene man lager ny instanse av en klasse
                    var quizLogic = new QuizLogic();
                    var userData = new UserData();
                    var userName = userData.Load().UserName;

                    displayMessages.PrintInitialMenu();
                    var choice = Console.ReadKey();

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
                            Console.WriteLine("\n==== Browse Your Collection ====\n");
                            BrowseSavedRecipes();
                            return;
                        case ConsoleKey.D5:
                            Console.WriteLine("\n==== Your Best Suited Cocktail Quiz ====\n");
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