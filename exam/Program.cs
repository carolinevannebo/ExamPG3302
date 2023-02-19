// See https://aka.ms/new-console-template for more information
using exam.ui;

namespace exam
{
    public class Program
    {
        public static void Main()
        {
            var eventHandler = new logic.EventHandler();
            var displayMessages = new DisplayMessages();
            try
            {
                Console.WriteLine("Starting the program...");
                Console.WriteLine("");

                displayMessages.PrintInitialWelcome();
                eventHandler.InitialMenu();

            } catch (Exception e)
            {
                Console.WriteLine($"An error occurred while starting the program: {e.Message}");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}