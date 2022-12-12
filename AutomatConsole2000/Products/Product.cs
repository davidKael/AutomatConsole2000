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
    internal abstract class Product : IProduct
    {

        public string Name { get; set; } = "[No Name]";
        public double Cost { get; set; }
        protected string _description = "[No Description]";
        protected string _useStr = "[No sound]";


        public abstract ProductCategories Category { get; }

        public abstract string Description();

        public abstract string Buy();

        public abstract string Use();


        public bool Clone<T>(out T newProduct)
        {
            newProduct = (T)this.MemberwiseClone();
            return newProduct != null;
        }


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



        public static List<string> GetPrintableData(List<Product> products, Product? highlighted, bool withPrice = true)
        {
            List<string> output = new List<string>();

            string[] formatedNames = ConsoleHelper.EvenSpacesRight(products.Select(p => p.Name).ToArray(), 10);

            for(int i = 0; i< formatedNames.Length; i++)
            {
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
