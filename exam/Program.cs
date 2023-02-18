// See https://aka.ms/new-console-template for more information
using exam.data.json;
using exam.data.repo;
using exam.logic;
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
                displayMessages.PrintInitialWelcome();
                var userData = new UserData();
                eventHandler.InitialMenu();
            } catch (IOException e)
            {
               //todo string? stackTrace = e.StackTrace <--- print stacktrace
            }
        }
    }
}