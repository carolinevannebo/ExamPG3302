using System;

namespace exam.logic.commands
{
    public class BrowseSavedCocktailsCommand : ICommand
    {
        private readonly CollectionBrowserLogic _collectionBrowserLogic;

        public BrowseSavedCocktailsCommand(CollectionBrowserLogic collectionBrowserLogic)
        {
            _collectionBrowserLogic = collectionBrowserLogic;
        }

        public void Execute()
        {
            _collectionBrowserLogic.BrowseSavedRecipes();
        }
    }
}

