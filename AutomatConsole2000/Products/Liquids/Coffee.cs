using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Liquids
{
    internal class Coffee : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Liquid;

        public Coffee()
        {
            this.Cost = 50;
            this.Name = "Cup of coffee";
            this._description = "A warm cup of coffee smooth as a summerbreeze in june";
            this._useStr = "Slurp, slurp... hmm not as good as advertised";
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
