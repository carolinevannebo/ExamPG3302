using Newtonsoft.Json;
using System.Text.RegularExpressions;
using exam.logic.factory;

namespace exam.data.repo
{
    public partial class MainRepository
    {

        #region Properties
        private const string _searchEndpoint = "https://www.thecocktaildb.com/api/json/v1/1/search.php?";
        private const string _searchEndpointByName = "s="; //margarita
        private const string _searchEndpointByFirstLetter = "f="; //a
        private const string _searchEndpointByIngredient = "i="; //vodka
        private const string _randomCocktailEndpoint = "https://www.thecocktaildb.com/api/json/v1/1/random.php";

        #endregion
        public MainRepository()
        {
        }

        #region Methods
        public static async Task<CocktailRecipe> GetRandomCocktailRecipe()
        {
            var data = await GetJsonFromServer(_randomCocktailEndpoint);
            return ConvertJsonToCocktailRecipe(data, 0);
        }

        public static async Task<CocktailRecipe> GetCocktailRecipeByName(string input)
        {
            var data = await GetJsonFromServer(_searchEndpoint + _searchEndpointByName + input);
            return ConvertJsonToCocktailRecipe(data, 0);
        }

        public static async Task<List<CocktailRecipe>> GetCocktailRecipesByFirstLetter(string input)
        {
            if (!MyRegex().IsMatch(input))
            {
                throw new ArgumentException("Input must be a single letter");
            }

            var data = await GetJsonFromServer(_searchEndpoint + _searchEndpointByFirstLetter + input);

            return ConvertJsonToCocktailRecipeList(data);
        }

        public static async Task<Ingredient> GetIngredient(string input)
        {
            var data = await GetJsonFromServer(_searchEndpoint + _searchEndpointByIngredient + input);
            return ConvertJsonToIngredient(data);
        }

        private static async Task<RootObject> GetJsonFromServer(string endpoint)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(endpoint);
            if (response == null)
            {
                throw new NullReferenceException("Response from API is null");
            }

            var data = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(data))
            {
                throw new NullReferenceException("Data from API is null or empty");
            }

            // parse the JSON data
            var json = JsonConvert.DeserializeObject<RootObject>(data);

            return json!;
        }

        private static List<CocktailRecipe> ConvertJsonToCocktailRecipeList(RootObject json)
        {
            var list = json.drinks;
            var convertedList = new List<CocktailRecipe>();
            int index = 0;

            foreach (var cocktail in list)
            {
                var currentCocktailRecipe = ConvertJsonToCocktailRecipe(json, index);
                convertedList.Add(currentCocktailRecipe);
                index++;
            }

            return convertedList;
        }

        private static CocktailRecipe ConvertJsonToCocktailRecipe(RootObject json, int index)
        {
            var jsonDrink = json.drinks[index];

            // create a new CocktailRecipe object
            var recipe = new CocktailRecipe
            {
                idDrink = jsonDrink.idDrink,
                strDrink = jsonDrink.strDrink,
                strCategory = jsonDrink.strCategory,
                strAlcoholic = jsonDrink.strAlcoholic,
                strGlass = jsonDrink.strGlass,
                strInstructions = jsonDrink.strInstructions
            };

            // create arrays to store the ingredients and measurements
            var ingredients = new List<string>();
            var measurements = new List<string>();

            // loop through the ingredients and add them to the arrays
            for (int i = 1; i <= 15; i++)
            {
                var ingredient = jsonDrink.GetType().GetProperty("strIngredient" + i)?.GetValue(jsonDrink, null)?.ToString();
                if (ingredient != null)
                {
                    ingredients.Add(ingredient);
                    var measurement = jsonDrink.GetType().GetProperty("strMeasure" + i)?.GetValue(jsonDrink, null)?.ToString();
                    measurements.Add(measurement ?? string.Empty);
                }
            }

            // assign the arrays to the CocktailRecipe object
            recipe.strIngredients = ingredients.ToArray();
            recipe.strMeasurements = measurements.ToArray();

            return recipe;
        }

        private static Ingredient ConvertJsonToIngredient(RootObject json)
        {
            var jsonIngredient = json.ingredients[0];

            var ingredient = new Ingredient
            {
                idIngredient = jsonIngredient.idIngredient,
                strIngredient = jsonIngredient.strIngredient,
                strDescription = jsonIngredient.strDescription,
                strType = jsonIngredient.strType,
                strAlcohol = jsonIngredient.strAlcohol,
                strABV = jsonIngredient.strABV
            };

            return ingredient;
        }

        [GeneratedRegex("^[a-zA-Z]$")]
        private static partial Regex MyRegex();

        #endregion
    }
}