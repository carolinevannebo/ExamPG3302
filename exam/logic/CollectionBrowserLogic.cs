using System;
using exam.data.database;
using exam.data.userData;

namespace exam.logic
{
    public class CollectionBrowserLogic
    {
        private readonly UserData _userData;
        private readonly EventHandler _eventHandler;

        public CollectionBrowserLogic()
        {
            _userData = new UserData();
            _eventHandler = new EventHandler();
        }

        // You need to refactor the shit out of this method
        public void BrowseSavedRecipes()
        {
            // Load username
            var userName = _userData.Load().UserName;

            // Fetch all cocktails in DB
            List<CocktailRecipe> allCocktails = DatabaseHelper.GetAllCocktails();

            if (allCocktails != null)
            {
                Console.WriteLine("");

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
                    Console.WriteLine("In order to go back, type 'main menu'");

                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine($"\nI'm sorry {userName}, I need a valid input to help you");
                        continue; // goes back to start of loop until input equals a valid ID
                    }

                    if (input.ToLower().Contains("main menu"))
                    {
                        _eventHandler.InitialMenu();
                        return;
                    }

                    // Define how to find cocktail
                    var chosenCocktail = allCocktails.FirstOrDefault(cocktail => cocktail.idDrink == input);

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
                                    return;
                                case ConsoleKey.D2:
                                    _eventHandler.InitialMenu();
                                    return;
                                default:
                                    Console.WriteLine("\nYour choice was not recognized: " + choice + "\n");
                                    continue;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"You have not saved any cocktail recipes yet, {userName}");
                Console.WriteLine("To do so you can choose a random recipe, search for one, or take a quiz.");
                Thread.Sleep(1500);
                _eventHandler.InitialMenu();
            }
        }
    }
}

