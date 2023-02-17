using System;
using exam.data.repo;
using exam.ui;

namespace exam.logic
{
    public class EventHandler
    {
        public EventHandler()
        {
        }

        public void InitialMenu()
        {
            var mainRepository = new MainRepository();
            var displayMessages = new DisplayMessages();
            displayMessages.PrintInitialWelcome();

            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    displayMessages.PrintInitialMenu();
                    int choice = Console.Read();

                    switch (choice)
                    {
                        case 1:
                            var randomRecipe = mainRepository.GetRandomCocktailRecipe().Result;
                            Console.WriteLine(randomRecipe.ToString);
                            break;
                        case 2:
                            Console.WriteLine("Under construction...");
                            break;
                        case 3:
                            Console.WriteLine("Under construction...");
                            break;
                        case 4:
                            Console.WriteLine("Under construction...");
                            break;
                        case 5:
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Your choice was not recognized: " + choice);
                            continue; // usikker på denne løsningen
                    }
                } catch (IOException e)
                {
                    Console.WriteLine("An error occured, " + e.GetBaseException); // ?
                }
            }
        }
    }
}

