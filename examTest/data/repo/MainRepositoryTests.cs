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
        // Act
        var result = await MainRepository.GetRandomCocktailRecipe();

        // Assert
        Assert.That(result, Is.Not.Null);

        if (result != null)
            Assert.That(string.IsNullOrEmpty(result.idDrink), Is.False);
    }

    [Test]
    public async Task GetCocktailRecipeByName_ReturnsData()
    {
        // Arrange
        string cocktailName = "margarita";

        // Act
        var result = await MainRepository.GetCocktailRecipeByName(cocktailName);

        //Assert
        Assert.That(result, Is.Not.Null);

        if (result != null)
            Assert.That(string.IsNullOrEmpty(result.idDrink), Is.False);
    }

    [Test]
    public async Task GetCocktailRecipesByFirstLetter_ReturnsData()
    {
        // Arrange
        string firstLetter = "w";

        // Act
        var result = await MainRepository.GetCocktailRecipesByFirstLetter(firstLetter);

        // Assert
        Assert.That(result, Is.Not.Null);

        foreach (var recipe in result)
        {
            Assert.That(string.IsNullOrEmpty(recipe.idDrink), Is.False);
        }
    }

    [Test]
    public async Task GetCocktailRecipeByIngredient_ReturnsData()
    {
        // Arrange
        string ingredient = "vodka";

        // Act
        var result = await MainRepository.GetIngredient(ingredient);

        // Assert
        Assert.That(result, Is.Not.Null);

        if (result != null)
            Assert.That(string.IsNullOrEmpty(result.idIngredient), Is.False);
    }

}
