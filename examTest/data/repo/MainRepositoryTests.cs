﻿using NUnit.Framework;
using System.Threading.Tasks;
using exam.data.repo;

namespace examTest;

[TestFixture]
public class MainRepositoryTests
{
    private MainRepository _mainRepository;

    [SetUp]
    public void Setup()
    {
        _mainRepository = new MainRepository();
    }

    [Test]
    public async Task GetRandomCocktailRecipe_ReturnsData()
    {
        // Act
        var result = await _mainRepository.GetRandomCocktailRecipe();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task GetCocktailRecipeByName_ReturnsData()
    {
        // Arrange
        string cocktailName = "margarita";

        // Act
        var result = await _mainRepository.GetCocktailRecipeByName(cocktailName);

        //Assert
        Assert.That(result, Is.Not.Null);

    }

    [Test]
    public async Task GetCocktailRecipeByFirstLetter_ReturnsData()
    {
        // Arrange
        string firstLetter = "a";

        // Act
        var result = await _mainRepository.GetCocktailRecipeByFirstLetter(firstLetter);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task GetCocktailRecipeByIngredient_ReturnData()
    {
        // Arrange
        string ingredient = "vodka";

        // Act
        var result = await _mainRepository.GetCocktailRecipeByIngredient(ingredient);

        // Assert
        Assert.That(result, Is.Not.Null);
    }
}
