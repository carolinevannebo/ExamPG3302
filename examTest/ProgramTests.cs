using System;
using exam.logic;
using exam.logic.commands;
using Microsoft.Extensions.DependencyInjection;

namespace examTest
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void ServicesShouldNotBeNull()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICommand, RandomCocktailCommand>().AddScoped<SearchLogic>()
                .AddSingleton<ICommand, SearchCocktailCommand>()
                .AddSingleton<ICommand, SearchIngredientCommand>()
                .AddSingleton<ICommand, BrowseSavedCocktailsCommand>().AddScoped<CollectionBrowserLogic>()
                .AddSingleton<ICommand, QuizCommand>().AddScoped<QuizLogic>()
                .BuildServiceProvider();

            var services = serviceProvider.GetServices<ICommand>().ToArray();

            foreach (var service in services)
            {
                Assert.That(service, Is.Not.Null);
            }
        }
    }
}

