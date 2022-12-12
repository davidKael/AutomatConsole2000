using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console.Products
{
    internal interface IProduct
    {

        string Description();
        string Buy();
        string Use();
        bool Clone<T>(out T newProduct);
    }
}
