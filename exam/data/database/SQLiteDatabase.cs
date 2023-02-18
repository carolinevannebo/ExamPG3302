﻿using System.Collections.Generic;
using System.Data.SQLite;
using exam.logic;

namespace exam.data.database
{
    public sealed class SQLiteDatabase : IDatabase
    {
        private static readonly SQLiteDatabase instance = new SQLiteDatabase();

        static SQLiteDatabase() { }

        private SQLiteDatabase() { }

        public static SQLiteDatabase Instance
        {
            get
            {
                return instance;
            }
        }

        public void InsertCocktail(CocktailRecipe cocktail)
        {
            using (var connection = new SQLiteConnection("Data Source=cocktails.db"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO cocktails (id, name, category, alcoholic, glass, instructions, ingredients, measurements) VALUES (@id, @name, @category, @alcoholic, @glass, @instructions, @ingredients, @measurements)";
                    command.Parameters.AddWithValue("@id", cocktail.idDrink);
                    command.Parameters.AddWithValue("@name", cocktail.strDrink);
                    command.Parameters.AddWithValue("@category", cocktail.strCategory);
                    command.Parameters.AddWithValue("@alcoholic", cocktail.strAlcoholic);
                    command.Parameters.AddWithValue("@glass", cocktail.strGlass);
                    command.Parameters.AddWithValue("@instructions", cocktail.strInstructions);
                    command.Parameters.AddWithValue("@ingredients", string.Join(",", cocktail.strIngredients));
                    command.Parameters.AddWithValue("@measurements", string.Join(",", cocktail.strMeasurements));
                    command.ExecuteNonQuery();
                }
            }
        }

        public CocktailRecipe GetCocktailById(string id)
        {
            using (var connection = new SQLiteConnection("Data Source=cocktails.db"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT id, name, category, alcoholic, glass, instructions, ingredients, measurements FROM cocktails WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CocktailRecipe
                            {
                                idDrink = reader.GetString(0),
                                strDrink = reader.GetString(1),
                                strCategory = reader.GetString(2),
                                strAlcoholic = reader.GetString(3),
                                strGlass = reader.GetString(4),
                                strInstructions = reader.GetString(5),
                                strIngredients = reader.GetString(6).Split(','),
                                strMeasurements = reader.GetString(7).Split(',')
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<CocktailRecipe> GetAllCocktails()
        {
            var cocktails = new List<CocktailRecipe>();
            using (var connection = new SQLiteConnection("Data Source=cocktails.db"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT id, name, category, alcoholic, glass, instructions, ingredients, measurements FROM cocktails";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cocktails.Add(new CocktailRecipe
                            {
                                idDrink = reader.GetString(0),
                                strDrink = reader.GetString(1),
                                strCategory = reader.GetString(2),
                                strAlcoholic = reader.GetString(3),
                                strGlass = reader.GetString(4),
                                strInstructions = reader.GetString(5),
                                strIngredients = reader.GetString(6).Split(','),
                                strMeasurements = reader.GetString(7).Split(',')
                            });
                        }
                    }
                }
            }
            return cocktails;
        }

    }
}

