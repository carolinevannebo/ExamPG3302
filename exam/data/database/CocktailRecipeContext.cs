using System;
using exam.logic;
using Microsoft.EntityFrameworkCore;

namespace exam.data.database
{
    public class CocktailRecipeContext : DbContext
    {
        public DbSet<CocktailRecipe> CocktailRecipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=CocktailRecipes.db");
        }
    }
}

