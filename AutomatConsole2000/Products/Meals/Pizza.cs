using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Meals
{
    internal class Pizza : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Meal;

        public Pizza()
        {
            this.Cost = 120;
            this.Name = "Pizza";
            this._description = "One decimetre of melted cheese, a bite is like a volcanic eruption";
            this._useStr = "Much much... OUCH!!!! I burned my tounge on the melted cheese... and also... too salty.";

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
