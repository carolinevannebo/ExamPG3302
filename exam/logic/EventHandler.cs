using System;
using exam.data.repo;
using exam.data.json;
using exam.ui;

namespace exam.logic
{
    public class EventHandler
    {
        #region Properties

        readonly MainRepository mainRepository = new();
        readonly DisplayMessages displayMessages = new();

        #endregion

        #region Methods

        public void SecondMenu() {
            bool isRunning = true; // refactor
            while(isRunning)
            {
                try
                {
                    //hent brukernavn
                    var userData = new UserData();
                    var jsonUserName = userData.Load();
                    var userName = jsonUserName.UserName;

                    displayMessages.PrintSecondMenu();
                    var choice = Console.ReadKey();

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("Under construction...");
                            return;
                        case ConsoleKey.D2:
                            InitialMenu();
                            return;
                        case ConsoleKey.D3:
                            Console.WriteLine("");
                            Console.WriteLine($"Goodbye {userName}");
                            isRunning = false;
                            return;
                        default:
                            Console.WriteLine("");
                            Console.WriteLine("Your choice was not recognized: " + choice);
                            SecondMenu();
                            return;
                    }

                }
                catch (IOException e)
                {
                    Console.WriteLine("An error occured, " + e.GetBaseException); // ?
                }
            }
        }

        public void TriggerChoice1FromInitialMenu()
        {
            var randomRecipe = mainRepository.GetRandomCocktailRecipe().Result;
            Console.WriteLine("");
            Console.WriteLine(randomRecipe.ToString());
            SecondMenu();
        }

        public void TriggerChoice2FromInitialMenu()
        {
            //hent brukernavn
            var userData = new UserData();
            var jsonUserName = userData.Load();
            var userName = jsonUserName.UserName;

            displayMessages.PrintSearchMenu();
            var choice = Console.ReadKey();

            switch (choice.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("");
                    Console.WriteLine("Please type the recipe name you're looking for");
                    Console.WriteLine("");

                    var inputName = Console.ReadLine();
                    var recipeName = mainRepository.GetCocktailRecipeByName(inputName).Result; //må ha exception handling

                    Console.WriteLine("");
                    Console.WriteLine(recipeName.ToString());
                    SecondMenu();
                    break;

                case ConsoleKey.D2:
                    Console.WriteLine("");
                    Console.WriteLine($"I require a letter. Would you be so kind to give me one, {userName}?");
                    Console.WriteLine("");

                    var inputLetter = Console.ReadKey();
                    var recipeLetter = mainRepository.GetCocktailRecipeByFirstLetter(inputLetter.KeyChar.ToString()).Result; //må ha exception handling

                    Console.WriteLine("");
                    Console.WriteLine(recipeLetter.ToString());
                    SecondMenu();
                    break;

                default:
                    Console.WriteLine("");
                    Console.WriteLine("Your choice was not recognized: " + choice);
                    TriggerChoice2FromInitialMenu();
                    break;
            }
        }

        public void TriggerChoice3FromInitialMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Please let me know which ingredient you'd like to know about");
            Console.WriteLine("");

            var input = Console.ReadLine();
            var ingredient = mainRepository.GetIngredient(input!.ToString()).Result;

            Console.WriteLine("");
            Console.WriteLine(ingredient.ToString());
            SecondMenu(); // non-user friendly, shouldnt save an ingredient as recipe
        }

        public void InitialMenu()
        {
            bool isRunning = true; // refactor
            while (isRunning)
            {
                try
                {
                    //hent brukernavn
                    var userData = new UserData();
                    var jsonUserName = userData.Load();
                    var userName = jsonUserName.UserName;

                    displayMessages.PrintInitialMenu();
                    var choice = Console.ReadKey();

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("");
                            Console.WriteLine("==== Random Cocktail Recipe ====");
                            TriggerChoice1FromInitialMenu();
                            return;
                        case ConsoleKey.D2:
                            Console.WriteLine("");
                            Console.WriteLine("==== Search Cocktail Recipe ====");
                            TriggerChoice2FromInitialMenu();
                            return;
                        case ConsoleKey.D3:
                            Console.WriteLine("");
                            Console.WriteLine("==== Research Ingredient ====");
                            TriggerChoice3FromInitialMenu();
                            return;
                        case ConsoleKey.D4:
                            Console.WriteLine("");
                            Console.WriteLine("==== Browse Saved Recipes ====");
                            Console.WriteLine("");
                            Console.WriteLine("Under construction...");
                            SecondMenu();
                            return;
                        case ConsoleKey.D5:
                            Console.WriteLine("");
                            Console.WriteLine("==== Which Cocktail Should You Prepare Quiz ====");
                            Console.WriteLine("");
                            Console.WriteLine("Under construction...");
                            SecondMenu();
                            return;
                        case ConsoleKey.D6:
                            Console.WriteLine("");
                            Console.WriteLine("==== Quit Program ====");
                            Console.WriteLine("");
                            Console.WriteLine($"Goodbye {userName}");
                            isRunning = false;
                            return;
                        default:
                            Console.WriteLine("");
                            Console.WriteLine("Your choice was not recognized: " + choice);
                            return; // usikker på denne løsningen
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("An error occured, " + e.GetBaseException); // ?
                }
            }
        }
        #endregion
    }
}