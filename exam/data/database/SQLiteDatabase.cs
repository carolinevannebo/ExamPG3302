using System.Collections.Generic;
using System.Data.SQLite;
using exam.logic.factory;

namespace exam.data.database
{
    public sealed class SQLiteDatabase : IDatabase
    {

        private static readonly Lazy<SQLiteDatabase> lazy = new(() => new SQLiteDatabase());
        private readonly SQLiteConnection connection;

        private SQLiteDatabase()
        {

            connection = new SQLiteConnection("Data Source=cocktails.db");

            // Create the cocktails table if it does not exist
            connection.Open();
            using (SQLiteCommand command = new("CREATE TABLE IF NOT EXISTS cocktails (idDrink TEXT PRIMARY KEY, strDrink TEXT, strCategory TEXT, strAlcoholic TEXT, strGlass TEXT, strInstructions TEXT, strIngredients TEXT, strMeasurements TEXT)", connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public static SQLiteDatabase Instance => lazy.Value;

        public void InsertCocktail(CocktailRecipe cocktail)
        {
            
            if (GetCocktailById(cocktail.idDrink) == null)
            {
                connection.Open();

                using (SQLiteCommand command = new("INSERT INTO cocktails (idDrink, strDrink, strCategory, strAlcoholic, strGlass, strInstructions, strIngredients, strMeasurements) VALUES (@idDrink, @strDrink, @strCategory, @strAlcoholic, @strGlass, @strInstructions, @strIngredients, @strMeasurements)", connection))
                {
                    command.Parameters.AddWithValue("@idDrink", cocktail.idDrink);
                    command.Parameters.AddWithValue("@strDrink", cocktail.strDrink);
                    command.Parameters.AddWithValue("@strCategory", cocktail.strCategory);
                    command.Parameters.AddWithValue("@strAlcoholic", cocktail.strAlcoholic);
                    command.Parameters.AddWithValue("@strGlass", cocktail.strGlass);
                    command.Parameters.AddWithValue("@strInstructions", cocktail.strInstructions);
                    command.Parameters.AddWithValue("@strIngredients", string.Join(",", cocktail.strIngredients));
                    command.Parameters.AddWithValue("@strMeasurements", string.Join(",", cocktail.strMeasurements));

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("This cocktail is already saved");
            }
            
        }

        public void DeleteCocktail(CocktailRecipe cocktail)
        {
            if (GetCocktailById(cocktail.idDrink) != null)
            {
                connection.Open();

                using (SQLiteCommand command = new("DELETE FROM cocktails WHERE idDrink=@idDrink", connection))
                {
                    command.Parameters.AddWithValue("@idDrink", cocktail.idDrink);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Cocktail is not saved, no need to delete it");
            }
        }

        public CocktailRecipe GetCocktailById(string idDrink)
        {
            connection.Open();

            using (SQLiteCommand command = new("SELECT * FROM cocktails WHERE idDrink=@idDrink", connection))
            {
                command.Parameters.AddWithValue("@idDrink", idDrink);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CocktailRecipe cocktail = new CocktailRecipe()
                        {
                            idDrink = reader["idDrink"].ToString()!,
                            strDrink = reader["strDrink"].ToString()!,
                            strCategory = reader["strCategory"].ToString(),
                            strAlcoholic = reader["strAlcoholic"].ToString(),
                            strGlass = reader["strGlass"].ToString(),
                            strInstructions = reader["strInstructions"].ToString(),
                            strIngredients = reader["strIngredients"].ToString().Split(','),
                            strMeasurements = reader["strMeasurements"].ToString().Split(',')
                        };

                        connection.Close();
                        return cocktail;
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }
            }
        }

        public List<CocktailRecipe> GetAllCocktails()
        {
            List<CocktailRecipe> cocktails = new();

            connection.Open();

            using (SQLiteCommand command = new("SELECT * FROM cocktails", connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CocktailRecipe cocktail = new()
                        {
                            idDrink = reader["idDrink"].ToString()!,
                            strDrink = reader["strDrink"].ToString()!,
                            strCategory = reader["strCategory"].ToString(),
                            strAlcoholic = reader["strAlcoholic"].ToString(),
                            strGlass = reader["strGlass"].ToString(),
                            strInstructions = reader["strInstructions"].ToString(),
                            strIngredients = reader["strIngredients"].ToString().Split(','),
                            strMeasurements = reader["strMeasurements"].ToString().Split(',')
                        };

                        cocktails.Add(cocktail);
                    }
                }
            }

            connection.Close();
            return cocktails;
        }
    }
}

