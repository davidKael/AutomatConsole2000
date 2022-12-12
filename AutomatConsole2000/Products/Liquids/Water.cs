using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Liquids
{
    internal class Water : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Liquid;

        public Water()
        {
            this.Cost = 24;
            this.Name = "Water";
            this._description = "Just water...";
            this._useStr = "Slurp, slurp... Hydration is very important..";

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
