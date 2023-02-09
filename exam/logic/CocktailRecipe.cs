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
        public string[] strIngredients { get; set; } // alt som inneholder strIngredient i name, inkluder null pga index
        public string[] strMeasurements { get; set; } // du må sjekke at det er lik index ˆ

        #endregion

        #region Methods

        public override string ToString()
        {
            var ingredients = string.Join(", ", strIngredients);
            var steps = string.Join("\n", strMeasurements.Select((s, i) => $"{i + 1}. {s}"));

            return $"Name: {strDrink}\n Category: {strAlcoholic}\n Alcoholic: {strAlcoholic}\n Glass: {strGlass}\n Instructions: {strInstructions}\n Ingredients: {ingredients}\n Measurements:\n {steps}";
        }
        #endregion
    }
}

