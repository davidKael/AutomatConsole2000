
using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console
{
    internal static class UserSession
    {
        static public Wallet SessionWallet;
        static public Store SessionStore;
        static public List<Product> UserProductBag { get; private set; } = new List<Product>();



        static public void AddProductToUserBag(Product? product)
        {
            if(product != null) UserProductBag.Add(product);

        }

        public static void SetUpSession()
        {
            SessionWallet = new Wallet(10,10,10);
            SessionStore = new Store();
        }
    }
}

