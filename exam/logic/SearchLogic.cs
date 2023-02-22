using System;
using exam.data.userData;
using exam.data.repo;
using exam.ui;

namespace exam.logic
{
    public class SearchLogic
    {
        private readonly UserData _userData;
        private readonly DisplayMessages _displayMessages;
        private readonly MainRepository _mainRepository;
        private readonly EventHandler _eventHandler;

        public SearchLogic()
        {
            _userData = new UserData();
            _displayMessages = new DisplayMessages();
            _mainRepository = new MainRepository();
            _eventHandler = new EventHandler();
        }

        public void SearchIngredientsFromApi()
        {
            Console.WriteLine("\nPlease let me know which ingredient you'd like to know about\n");

            var input = Console.ReadLine();
            var ingredient = _mainRepository.GetIngredient(input!.ToString()).Result;

            Console.WriteLine("\n" + ingredient.ToString());
            _eventHandler.InitialMenu();
        }

        public void SearchCocktailRecipesFromApi()
        {
            var userName = _userData.Load().UserName;

            // Print menu
            _displayMessages.PrintSearchMenu();

            // Define choice
            var choice = Console.ReadKey();

            switch (choice.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("\nPlease type the recipe name you're looking for\n");

                    var inputName = Console.ReadLine();
                    var recipeByName = _mainRepository.GetCocktailRecipeByName(inputName).Result; //må ha exception handling

                    Console.WriteLine("\n" + recipeByName.ToString());

                    // Next option
                    _eventHandler.SecondMenu(recipeByName);
                    break;

                case ConsoleKey.D2:
                    Console.WriteLine($"\nI require a letter. Would you be so kind to give me one, {userName}?\n");

                    var inputLetter = Console.ReadKey();
                    var recipeByLetter = _mainRepository.GetCocktailRecipeByFirstLetter(inputLetter.KeyChar.ToString()).Result; //må ha exception handling

                    Console.WriteLine("\n" + recipeByLetter.ToString());

                    // Next option
                    _eventHandler.SecondMenu(recipeByLetter);
                    break;

                default:
                    Console.WriteLine("\nYour choice was not recognized: " + choice);
                    SearchCocktailRecipesFromApi();
                    break;
            }
        }
    }
}

