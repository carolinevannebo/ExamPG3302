using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
        public async Task<object> GetRandomCocktailRecipe()
        {
            return await GetDataFromServer(_randomCocktailEndpoint);
        }

        public async Task<object> GetCocktailRecipeByName(string input)
        {
            return await GetDataFromServer(_searchEndpoint + _searchEndpointByName + input);
        }

        public async Task<object> GetCocktailRecipeByFirstLetter(string input)
        {
            if (!Regex.IsMatch(input, @"^[a-zA-Z]$"))
            {
                throw new ArgumentException("Input must be a single letter");
            }

            return await GetDataFromServer(_searchEndpoint + _searchEndpointByFirstLetter + input);
        }

        public async Task<object> GetCocktailRecipeByIngredient(string input)
        {
            return await GetDataFromServer(_searchEndpoint + _searchEndpointByIngredient + input);
        }

        private async Task<object> GetDataFromServer(string endpoint)
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

                return JsonConvert.DeserializeObject(data);
            }
        }
        #endregion
    }
}

