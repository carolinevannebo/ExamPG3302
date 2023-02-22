using System;
using exam.data.userData;
using exam.data.repo;
using exam.ui;

namespace exam.logic
{
    public class SearchLogic
    {
        //private readonly UserData _userData;
        private readonly DisplayMessages _displayMessages;
        private readonly MainRepository _mainRepository;
        private readonly EventHandler _eventHandler;

        public SearchLogic()
        {
            //_userData = new UserData();
            _displayMessages = new DisplayMessages();
            _mainRepository = new MainRepository();
            _eventHandler = new EventHandler();
        }

        public void SearchIngredientsFromApi()
        {
            Console.WriteLine("\nPlease let me know which ingredient you'd like to know about\n");

            var input = Console.ReadLine();
            var ingredient = MainRepository.GetIngredient(input!.ToString()).Result;

            Console.WriteLine("\n");
            Console.WriteLine(ingredient.ToString());
            _eventHandler.InitialMenu();
        }

        private void SearchByName()
        {
            Console.WriteLine("\nPlease type the recipe name you're looking for\n");

            var inputName = Console.ReadLine();

            try
            {
                var recipeByName = MainRepository.GetCocktailRecipeByName(inputName!.ToString()).Result;

                if (recipeByName != null)
                {
                    Console.WriteLine("\n" + recipeByName.ToString());
                    _eventHandler.SecondMenu(recipeByName);
                    return;
                }
                else
                {
                    Console.WriteLine("I can't seem to find the recipe you are looking for, my apologies");
                    SearchCocktailRecipesFromApi();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: " + e.Message);
                SearchCocktailRecipesFromApi();
            }
        }

        private void SearchByLetter()
        {
            var userName = UserData.Load().UserName;

            Console.WriteLine("\n");
            Console.WriteLine($"I require a letter. Would you be so kind to give me one, {userName}?\n");

            var inputLetter = Console.ReadKey();
            var recipesByLetter = MainRepository.GetCocktailRecipesByFirstLetter(inputLetter.KeyChar.ToString()).Result; //må ha exception handling

            Console.WriteLine("\n");

            foreach (var recipe in recipesByLetter)
            {
                Console.WriteLine($"{recipe.strDrink,-40} Category: {recipe.strCategory}");
            }

            Console.WriteLine("\n");
            Console.WriteLine($"Would you like to try one of these, {userName}?");
            Console.WriteLine("Type the cocktail name to open recipe, or 'main menu' to go back");

            while (true)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"Sorry {userName}, I need a little more than that to work with than that");
                    continue;
                }

                if (input.ToLower().Contains("main menu"))
                {
                    _eventHandler.InitialMenu();
                    return;
                }

                var chosenCocktail = MainRepository.GetCocktailRecipeByName(input.ToLower()).Result;

                if (chosenCocktail == null)
                {
                    Console.WriteLine($"Sorry to disappoint {userName}, but your cocktail is no where to be found. Try again");
                    continue;
                }

                Console.WriteLine(chosenCocktail.ToString());
                _eventHandler.SecondMenu(chosenCocktail);
                return;
            }
        }

        public void SearchCocktailRecipesFromApi()
        {
            // Print menu
            _displayMessages.PrintSearchMenu();

            while (true)
            {
                // Define choice
                var choice = Console.ReadKey();

                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        SearchByName();
                        return;

                    case ConsoleKey.D2:
                        SearchByLetter();
                        return;

                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("Your choice was not recognized: " + choice);
                        continue;
                }
            }
        }
    }
}

