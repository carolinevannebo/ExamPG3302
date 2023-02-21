using System;

namespace exam.logic
{
    public class CocktailRecipe
    {
        #region Properties
        public string idDrink { get; set; }
        public string strDrink { get; set; }
        public string strCategory { get; set; }
        public string strAlcoholic { get; set; }
        public string strGlass { get; set; }
        public string strInstructions { get; set; }
        public string[] strIngredients { get; set; }
        public string[] strMeasurements { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            var ingredients = string.Join("\n", strIngredients.Select((s, i) => $" {i + 1}. {s}"));
            var steps = string.Join("\n", strMeasurements.Select((s, i) => $" {i + 1}. {s}"));

            return $"\nName: {strDrink}\nCategory: {strAlcoholic}\nAlcoholic: {strAlcoholic}\nGlass: {strGlass}\n\nIngredients:\n{ingredients}\n\nMeasurements:\n{steps}\n\nInstructions:\n{strInstructions}\n";
        }
        #endregion
    }
}