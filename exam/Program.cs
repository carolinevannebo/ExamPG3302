// See https://aka.ms/new-console-template for more information
using exam.data.repo;
using exam.logic;

namespace exam
{
    public class Program
    {
        public static void Main()
        {
            var randomCocktailRecipe = new MainRepository().GetRandomCocktailRecipe().Result;
            Console.WriteLine(randomCocktailRecipe.ToString());
        }
    }
}