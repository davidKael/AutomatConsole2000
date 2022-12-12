using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Meals
{
    internal class Ramen : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Meal;

        public Ramen()
        {
            this.Cost = 100;
            this.Name = "Ramen";
            this._description = "Pork ramen, you wont regret it!";
            this._useStr = "Slurp slurp... Where is all the pork? Oh.. Maybe I ate it all...?";

        }

        public override string Buy()
        {
            return $"{Name} was put in your bag..";
        }

        public override string Use()
        {
            return _useStr;
        }

        public override string Description()
        {
            return _description;
        }

    }
}
