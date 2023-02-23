using System;
using exam.logic;

namespace exam.logic.commands
{
    public class SearchCocktailCommand : ICommand
    {
        private readonly SearchLogic _searchLogic;

        public SearchCocktailCommand(SearchLogic searchLogic)
        {
            _searchLogic = searchLogic;
        }

        public void Execute()
        {
            _searchLogic.SearchCocktailRecipesFromApi();
        }
    }
}

