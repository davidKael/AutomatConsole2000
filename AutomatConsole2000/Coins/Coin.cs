using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console
{
    /// <summary>
    /// Used as currency in Application
    /// </summary>
    internal abstract class Coin
    {
        public abstract string Name { get; }
        public abstract int Value { get; }

    }
}
