using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Meals
{
    internal class Burrito : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Meal;

        public Burrito()
        {
            this.Cost = 87;
            this.Name = "Burrito";
            this._description = "Secret suprise toy inside!!";
            this._useStr = "Munch, munch... OUch! there is a plastic donald duck toy inside this burrito!? I guess I missed the description..";

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
