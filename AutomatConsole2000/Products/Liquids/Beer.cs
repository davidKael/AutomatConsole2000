using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Liquids
{
    internal class Beer : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Liquid;

        public Beer()
        {
            this.Cost = 65;
            this.Name = "Beer";
            this._description = "A Beer served ice cold, perfect for the small moments in life";
            this._useStr = "Slurp, slurp... I guess it really is one of those beer moments";

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
