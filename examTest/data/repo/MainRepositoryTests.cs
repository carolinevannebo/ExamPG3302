using NUnit.Framework;
using System.Threading.Tasks;
using exam.data.repo;

namespace examTest;

[TestFixture]
public class MainRepositoryTests
{
    [Test]
    public async Task GetRandomCocktailRecipe_ReturnsData()
    {
        var result = await MainRepository.GetRandomCocktailRecipe();

        Assert.That(result, Is.Not.Null);

        if (result != null)
            Assert.That(string.IsNullOrEmpty(result.idDrink), Is.False);
    }

    [Test]
    public async Task GetCocktailRecipeByName_ReturnsData()
    {
        string cocktailName = "margarita";

        var result = await MainRepository.GetCocktailRecipeByName(cocktailName);

        Assert.That(result, Is.Not.Null);

        if (result != null)
            Assert.That(string.IsNullOrEmpty(result.idDrink), Is.False);
    }

    [Test]
    public async Task GetCocktailRecipesByFirstLetter_ReturnsData()
    {
        string firstLetter = "w";

        var result = await MainRepository.GetCocktailRecipesByFirstLetter(firstLetter);

        Assert.That(result, Is.Not.Null);

        foreach (var recipe in result)
        {
            Assert.That(string.IsNullOrEmpty(recipe.idDrink), Is.False);
        }
    }

    [Test]
    public async Task GetCocktailRecipeByIngredient_ReturnsData()
    {
        string ingredient = "vodka";

        var result = await MainRepository.GetIngredient(ingredient);

        Assert.That(result, Is.Not.Null);

        if (result != null)
            Assert.That(string.IsNullOrEmpty(result.idIngredient), Is.False);
    }

}
