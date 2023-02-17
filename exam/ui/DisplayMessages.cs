﻿using System;
namespace exam.ui
{
    public class DisplayMessages
    {
        #region
        public string userName;
        public string choice;
        #endregion

        public void PrintInitialWelcome()
        {
            Console.WriteLine("Welcome! Let me introduce myself: ");
            Console.WriteLine("My name is Quinton, your personal bartender assistant.");
            Console.WriteLine("I'm here to help you find your desired cocktail recipes.");
            Console.WriteLine("Would you be so kind to introduce yourself?");
            Console.WriteLine("");
            Console.WriteLine("What is your name?");
            Console.WriteLine("");
            userName = Console.ReadLine();
            Console.Clear();

            PrintSecondWelcome();
        }

        public void PrintSecondWelcome()
        {
            Console.WriteLine($"Nice to meet you {userName}!");
            Console.WriteLine("Are you ready to make some cocktails? (y/n)");
            Console.WriteLine("");
            var input = Console.ReadKey();

            if (input.Key == ConsoleKey.Y)
            {
                Console.WriteLine("");
                Console.WriteLine("Splendid! Let's start.");
            }
            else if (input.Key == ConsoleKey.N)
            {
                Console.WriteLine("");
                Console.WriteLine("Nonsense! I insist! Let's start.");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Neither a yes or no? I interpret that as a yes! Let's start.");
            }
            Thread.Sleep(3000); // tiny delay
            PrintInitialMenu();
        }

        public void PrintInitialMenu()
        {
            Console.Clear();
            Console.WriteLine($"How may I be of service today, {userName}?");
            Console.WriteLine("");
            Console.WriteLine("1: Random cocktail recipe");
            Console.WriteLine("2: Search for cocktail recipe");
            Console.WriteLine("3: Research ingredients");
            Console.WriteLine("4: Find coktail based on your current mood");
            Console.WriteLine("5: Quit");
        }
    }
}

