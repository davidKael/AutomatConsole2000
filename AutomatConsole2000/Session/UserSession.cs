
using Automat_Console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console
{
    /// <summary>
    /// Static class with data the whole system needs to have access to
    /// </summary>
    internal static class UserSession
    {
        static public Wallet SessionWallet;
        static public Store SessionStore;


        /// <summary>
        /// List of products collected by user
        /// </summary>
        static public List<Product> UserProductBag { get; private set; } = new List<Product>();


        /// <summary>
        /// adds given product to user bag
        /// </summary>
        /// <param name="product"></param>
        static public void AddProductToUserBag(Product? product)
        {
            if(product != null) UserProductBag.Add(product);

        }

        /// <summary>
        /// initializing a wallet and a store for the user session
        /// </summary>
        public static void SetUpSession()
        {
            SessionWallet = new Wallet(10,10,10);
            SessionStore = new Store();
        }
    }
}

