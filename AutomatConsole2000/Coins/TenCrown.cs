using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Coins
{
    internal class TenCrown : Coin
    {
        public override string Name { get; } = "Ten Crowner";
        public override int Value { get; } = 10;
    }
}
