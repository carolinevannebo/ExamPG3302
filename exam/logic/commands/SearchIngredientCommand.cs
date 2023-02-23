using System;

namespace exam.logic.commands
{
    public class SearchIngredientCommand : ICommand
    {
        private readonly SearchLogic _searchLogic;

        public SearchIngredientCommand(SearchLogic searchLogic)
        {
            _searchLogic = searchLogic;
        }

        public void Execute()
        {
            _searchLogic.SearchIngredientsFromApi();
        }
    }
}

