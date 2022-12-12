using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Stones
{
    internal class FakeStone : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Stone;

        public FakeStone()
        {
            this.Cost = 3;
            this.Name = "Fake Stone";
            this._description = "Not as bad as it sounds! Try it! Could be very useful in specific situations..";
            this._useStr = "ssshhpsd.... Hm.. this is just a piece of styrofoam painted gray..";

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
