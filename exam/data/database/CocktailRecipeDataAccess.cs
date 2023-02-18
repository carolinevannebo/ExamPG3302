using System;
using exam.logic;

namespace exam.data.database
{
    public class CocktailRecipDataAccess
    {
        private readonly CocktailRecipeContext _context;

        private CocktailRecipDataAccess()
        {
            _context = new CocktailRecipeContext();
            _context.Database.EnsureCreated();
        }

        private static CocktailRecipDataAccess _instance;
        public static CocktailRecipDataAccess Instance => _instance ??= new CocktailRecipDataAccess();

        public void Add(CocktailRecipe recipe)
        {
            _context.CocktailRecipes.Add(recipe);
            _context.SaveChanges();
        }

        public List<CocktailRecipe> GetAll()
        {
            return _context.CocktailRecipes.ToList();
        }
    }
}

