using System;
using exam.data.json;

namespace exam.ui
{
    public class DisplayMessages
    {
        #region
        public string? userName;
        #endregion


        public void PrintInitialWelcome()
        {
            Console.Clear();
            Console.WriteLine("Welcome! Let me introduce myself: ");
            Console.WriteLine("My name is Quinton, your personal bartender assistant.");
            Console.WriteLine("I'm here to help you find your desired cocktail recipes.");
            Console.WriteLine("Would you be so kind to introduce yourself?");
            Console.WriteLine("");
            Console.WriteLine("What is your name?");
            Console.WriteLine("");

            userName = Console.ReadLine();

            // Save the username to a JSON file
            var userData = new UserData();
            if (userName != null)
            {
                var userDataModel = new UserDataModel(userName);
                userData.Save(userDataModel);
                Console.Clear();
                PrintSecondWelcome();
            }
        }

        public void PrintSecondWelcome()
        {
            Console.WriteLine($"Nice to meet you {userName}!");
            Console.WriteLine("Are you ready to make some cocktails?");
            Console.WriteLine("");

            var input = Console.ReadLine();
            var answer = input!.ToLower();

            var responses = new Dictionary<string, string>
            {
                { "yes", "Splendid! Let's start." },
                { "ja", "Supert! La oss komme i gang." },
                { "si", "¡Espléndido! Empecemos." },
                { "da", "Minunat! Să începem." },
                { "oui", "Splendide! Commençons." },
                { "no", "Nonsense! I insist! Let's start." },
                { "nei", "Tullball! Jeg insisterer! La oss begynne." },
                { "nu", "Prostii! Insist! Să începem." },
                { "non", "Absurdité! J'insiste! Commençons." },
                { "nein", "Unsinn! Ich bestehe darauf! Lasst uns beginnen." }
            };

            Console.WriteLine(responses.GetValueOrDefault(answer, "I interpret that as a yes! Let's start."));
            Thread.Sleep(2000); // tiny delay
            Console.Clear();
        }

        public void PrintInitialMenu()
        {
            //hent brukernavn
            var userData = new UserData();
            var jsonUserName = userData.Load();
            userName = jsonUserName.UserName;

            Console.WriteLine($"How may I be of service today, {userName}?");
            Console.WriteLine("");
            Console.WriteLine("1: Random cocktail recipe");
            Console.WriteLine("2: Search for cocktail recipe");
            Console.WriteLine("3: Research ingredients");
            Console.WriteLine("4: Browse your saved recipes");
            Console.WriteLine("5: Find cocktail based on your current mood");
            Console.WriteLine("6: Quit program");
            Console.WriteLine("");
        }

        public void PrintSecondMenu()
        {
            //hent brukernavn
            var userData = new UserData();
            var jsonUserName = userData.Load();
            userName = jsonUserName.UserName;

            Console.WriteLine("");
            Console.WriteLine($"Is there anything else I can do for you, {userName}?");
            Console.WriteLine("");
            Console.WriteLine("1: Save this recipe");
            Console.WriteLine("2: Go back to main menu");
            Console.WriteLine("3: Quit program");
            Console.WriteLine("");
        }

        public void PrintSearchMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("1: Search by name");
            Console.WriteLine("2: Search by first letter");
            Console.WriteLine("");
        }
    }
}

