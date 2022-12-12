using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products.Stones
{
    internal class AlienStone : Product
    {
        public override ProductCategories Category { get; } = ProductCategories.Stone;

        public AlienStone()
        {
            this.Cost = 99.9;
            this.Name = "Alien Stone";
            this._description = "An oval stone smooth as silk. Many experts say this stone belonged to an anicent alien civilation and that it has the power of infinite life. Although a few haters claims it's just a stone..";
            this._useStr = "Splash!.... It sank like a stone... of course...";
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
