using Automat_Console.Products;
using AutomatConsole2000.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Items
{

    /// <summary>
    /// Base for all the products in application
    /// </summary>
    internal abstract class Product : IProduct
    {

        public string Name { get; set; } = "[No Name]";
        public double Cost { get; set; }
        protected string _description = "[No Description]";

        /// <summary>
        /// What happens when product is used
        /// </summary>
        protected string _useStr = "[No sound]";

        /// <summary>
        /// Products category
        /// </summary>
        public abstract ProductCategories Category { get; }

        public abstract string Description();

        public abstract string Buy();

        public abstract string Use();

        /// <summary>
        /// to Clone given product to a new identical
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        public bool Clone<T>(out T newProduct)
        {
            newProduct = (T)this.MemberwiseClone();
            return newProduct != null;
        }

        /// <summary>
        /// Returns products data as a string
        /// </summary>
        /// <param name="detailed">more data</param>
        /// <param name="withPrice"></param>
        /// <returns></returns>
        string GetData(bool detailed = false, bool withPrice = true)
        {
            string text = $"";

            if (withPrice)
            {
                text += $"{this.Cost} :-";
            }
            if (detailed)
            {
                text += "\n\n"
                    + $"    Category: {this.Category}" + "\n"
                    + $"    Description: {this.Description()}" + "\n\n";
            }

            return text;
        }


        /// <summary>
        /// Returns products data as a list of strings formated to be more readable for the user
        /// </summary>
        /// <param name="products"></param>
        /// <param name="highlighted"></param>
        /// <param name="withPrice"></param>
        /// <returns></returns>
        public static List<string> GetPrintableData(List<Product> products, Product? highlighted, bool withPrice = true)
        {

            
            List<string> output = new List<string>();

            //Not the best solution but it works
            string[] formatedNames = ConsoleHelper.EvenSpacesRight(products.Select(p => p.Name).ToArray(), 10);

            for(int i = 0; i< formatedNames.Length; i++)
            {
                //check if this product should show more details
                bool detailed = products[i] == highlighted;
                output.Add(formatedNames[i] + products[i].GetData(detailed: detailed, withPrice: withPrice));
            }

            return output;
        }

        public enum ProductCategories
        {
            Liquid, Meal, Stone
        }
    }
}
