using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Stones
{
    internal class ThunderStone : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Stone;

        public ThunderStone()
        {
            this.Cost = 21;
            this.Name = "Thunder Stone";
            this._description = "A green transparent stone with a yellow lightning inside! Could be used at some specific creatures..";
            this._useStr = "Congratulations! Your Pikachu evolved into Raichu!";

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
