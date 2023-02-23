using System;
using Microsoft.Extensions.DependencyInjection;

namespace exam.logic.events
{
    public class RandomCocktailCommand : ICommand
    {
        private readonly SearchLogic _searchLogic;

        public RandomCocktailCommand(SearchLogic searchLogic)
        {
            _searchLogic = searchLogic;
        }

        public void Execute()
        {
            _searchLogic.GetRandomCocktailRecipe();
        }
    }
}

