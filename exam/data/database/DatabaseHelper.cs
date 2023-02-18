using System;
using exam.logic;

namespace exam.data.database
{
    public class DatabaseHelper
    {
        private static readonly IDatabase database = SQLiteDatabase.Instance;

        public static void InsertCocktail(CocktailRecipe cocktail)
        {
            database.InsertCocktail(cocktail);
        }

        public static CocktailRecipe GetCocktailById(string id)
        {
            return database.GetCocktailById(id);
        }

        public static List<CocktailRecipe> GetAllCocktails()
        {
            return database.GetAllCocktails();
        }
    }
}

