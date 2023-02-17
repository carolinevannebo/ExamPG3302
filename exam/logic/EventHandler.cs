using System;
using exam.data.repo;
using exam.ui;

namespace exam.logic
{
    public class EventHandler
    {
        MainRepository mainRepository = new MainRepository();
        DisplayMessages displayMessages = new DisplayMessages();

        public EventHandler()
        {
        }

        public void SecondMenu() {
            bool isRunning = true; // refactor
            while(isRunning)
            {
                try
                {
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
                            Console.WriteLine($"Goodbye {displayMessages.userName}");
                            isRunning = false;
                            return;
                        default:
                            Console.WriteLine("");
                            Console.WriteLine("Your choice was not recognized: " + choice);
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

        public void InitialMenu()
        {
            bool isRunning = true; // refactor
            while (isRunning)
            {
                try
                {
                    displayMessages.PrintInitialMenu();
                    var choice = Console.ReadKey();

                    switch (choice.Key)
                    {
                        case ConsoleKey.D1:
                            TriggerChoice1FromInitialMenu();
                            return;
                        case ConsoleKey.D2:
                            Console.WriteLine("");
                            Console.WriteLine("Under construction...");
                            return;
                        case ConsoleKey.D3:
                            Console.WriteLine("");
                            Console.WriteLine("Under construction...");
                            return;
                        case ConsoleKey.D4:
                            Console.WriteLine("");
                            Console.WriteLine("Under construction...");
                            return;
                        case ConsoleKey.D5:
                            Console.WriteLine("");
                            Console.WriteLine("Under construction...");
                            return;
                        case ConsoleKey.D6:
                            Console.WriteLine("");
                            Console.WriteLine($"Goodbye {displayMessages.userName}");
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
    }
}

