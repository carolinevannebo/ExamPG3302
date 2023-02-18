using System;
using exam.logic;

namespace exam.data.database
{
    public interface IDatabase
    {
        void InsertCocktail(CocktailRecipe cocktail);
        CocktailRecipe GetCocktailById(string id);
        List<CocktailRecipe> GetAllCocktails();
    }
}

