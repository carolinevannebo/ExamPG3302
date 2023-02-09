using Newtonsoft.Json;
using System.Text.RegularExpressions;
using exam.logic;

namespace exam.data.repo
{
    public class MainRepository
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
        public async Task<CocktailRecipe> GetRandomCocktailRecipe()
        {
            var data = await GetJsonFromServer(_randomCocktailEndpoint);
            return ConvertJsonToCocktailRecipe(data);
            //return await GetDataFromServer(_randomCocktailEndpoint);
        }

        public async Task<CocktailRecipe> GetCocktailRecipeByName(string input)
        {
            var data = await GetJsonFromServer(_searchEndpoint + _searchEndpointByName + input);
            return ConvertJsonToCocktailRecipe(data);
            //return await GetDataFromServer(_searchEndpoint + _searchEndpointByName + input);
        }

        public async Task<CocktailRecipe> GetCocktailRecipeByFirstLetter(string input)
        {
            if (!Regex.IsMatch(input, @"^[a-zA-Z]$"))
            {
                throw new ArgumentException("Input must be a single letter");
            }

            var data = await GetJsonFromServer(_searchEndpoint + _searchEndpointByFirstLetter + input);
            return ConvertJsonToCocktailRecipe(data);
            //return await GetDataFromServer(_searchEndpoint + _searchEndpointByFirstLetter + input);
        }

        public async Task<Ingredient> GetCocktailRecipeByIngredient(string input)
        {
            var data = await GetJsonFromServer(_searchEndpoint + _searchEndpointByIngredient + input);
            return ConvertJsonToIngredient(data);
            //return await GetDataFromServer(_searchEndpoint + _searchEndpointByIngredient + input);
        }

        private static async Task<RootObject> GetJsonFromServer(string endpoint)
        {
            using (var httpClient = new HttpClient())
            {
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
        }

        private static CocktailRecipe ConvertJsonToCocktailRecipe(RootObject json)
        {
            var jsonDrink = json.drinks[0];

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




        /*private static async Task<CocktailRecipe> GetDataFromServer(string endpoint)
        {
            using (var httpClient = new HttpClient())
            {
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

                var jsonDrink = json!.drinks[0];

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
        }*/
        #endregion
    }
}

/*
 * var jsonDrink = json!.drinks[0];

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
 */

/*
 var jsonIngredient = json.ingredients[0];

 if (jsonIngredient != null)
                {
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
                }*/