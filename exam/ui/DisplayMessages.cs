using System;
using exam.data.userData;

namespace exam.ui
{
    public class DisplayMessages
    {
        #region
        public string? userName;
        public UserData userData;
        #endregion

        public DisplayMessages()
        {
            userData = new UserData();
        }

        public void PrintInitialWelcome()
        {
            Console.Clear();
            Console.WriteLine("Welcome! Let me introduce myself: ");
            Console.WriteLine("My name is Quinton, your personal bartender assistant.");
            Console.WriteLine("I'm here to help you find your desired cocktail recipes.");
            Console.WriteLine("Would you be so kind to introduce yourself?");
            Console.WriteLine("\nWhat is your name?\n");

            userName = Console.ReadLine();

            // Save the username to a JSON file
            if (userName != null)
            {
                // Ensuring the username is never empty
                if (userName == "" || userName == null || userName == "\n" || userName == "\t")
                    userName = "mystery user";
                
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

            Console.WriteLine("\n");
            Console.WriteLine(responses.GetValueOrDefault(answer, "I interpret that as a yes! Let's start."));
            Thread.Sleep(1500); // tiny delay
            Console.Clear();
        }

        public void PrintInitialMenu()
        {
            userName = userData.Load().UserName;

            Console.WriteLine("\n");
            Console.WriteLine($"How may I be of service today, {userName}?\n");
            Console.WriteLine("1: Random cocktail recipe");
            Console.WriteLine("2: Search for cocktail recipe");
            Console.WriteLine("3: Research ingredients");
            Console.WriteLine("4: Browse your saved recipes");
            Console.WriteLine("5: Take a quiz");
            Console.WriteLine("6: Quit program\n");
        }

        public void PrintSecondMenu()
        {
            userName = userData.Load().UserName;

            Console.WriteLine($"\nIs there anything else I can do for you, {userName}?\n");
            Console.WriteLine("1: Save this recipe");
            Console.WriteLine("2: Go back to main menu");
            Console.WriteLine("3: Quit program\n");
        }

        public void PrintSearchMenu()
        {
            Console.WriteLine("1: Search by name");
            Console.WriteLine("2: Search by first letter\n");
        }
    }
}

