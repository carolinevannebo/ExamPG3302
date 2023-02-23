using System;

namespace exam.logic.factory
{
    public class Ingredient
    {
        public string idIngredient { get; set; }
        public string strIngredient { get; set; }
        public string strDescription { get; set; }
        public string strType { get; set; }
        public string strAlcohol { get; set; }
        public string strABV { get; set; } // Alcohol By Volume

        public override string ToString()
        {
            if (strType == null)
                strType = "Unknown";
            if (strABV == null)
                strABV = "0";

            return $"Name: {strIngredient}\nType: {strType}\nAlcohol: {strAlcohol}\nAlcohol by volume: {strABV}%\n\nDescription: {strDescription}";
        }
    }
}