using Automat_Console.Coins;
using Automat_Console.Items;
using Automat_Console.Products.Liquids;
using Automat_Console.Products.Meals;
using Automat_Console.Products.Stones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Automat_Console
{
    internal class Store
    {

       private List<Product> _stock = new List<Product>();

      

        public double MachineBalance { get; private set; }



        public Store() 
        {
            //test
            LoadProducts();
        }



        public void AddProductToStore(Product newProduct)
        {
            _stock.Add(newProduct);
        }

        public Product[] GetProductsRows(string category = "")
        {
            List<Product> result = new List<Product>();

            for (int i = 0; i < _stock.Count; i++)
            {
                Product product = _stock[i];

                if (category == "" || product.Category.ToString() == category)
                {

                    result.Add(product);
                }
            }

            return result.ToArray();
        }




        public bool TryDeposit(Coin insertedCoin)
        {
            if(insertedCoin != null) {

                
                MachineBalance += insertedCoin.Value;
                return true;
            }
            else
            {
              return false;
            }
        }

        public bool MakePurchase(Wallet wallet, Product product, out Product? boughtProduct, out string message)
        {
            if (product.Cost > MachineBalance)
            {
                message = "Seems like you don't have enough money, try insert some coins to the machine..";
                boughtProduct = null;
                return false;
            }

            if (product.Cost <= MachineBalance && product.Clone(out Product newProduct))
            {
                MachineBalance -= product.Cost;
                boughtProduct = newProduct;
                message = boughtProduct.Buy();
                TryWidthdraw(wallet);
                return true;
            }
            else
            {
                message = "Seems like something is wrong with the machine.. someone should fix this...";
                boughtProduct = null;
                return false;

            }
        }

        bool TryWidthdraw(Wallet wallet)
        {

            double tempBalance = MachineBalance;


            int tensCount = (int)Math.Floor(tempBalance / 10);
            tempBalance -= tensCount * 10;
            int fivesCount = (int)Math.Floor(tempBalance / 5);
            tempBalance -= fivesCount * 5;
            int onesCount = (int)Math.Floor(tempBalance);
            tempBalance -= onesCount;


            double diff = MachineBalance - tempBalance;

            if (diff > 0)
            {
                wallet.FillWallet(onesCount, fivesCount, tensCount);
                MachineBalance = tempBalance;
                return true;
            }
            else
            {
                return false;
            }



        }

        public string[] GetProductCategoryNames()
        {
            List<string> output = new List<string>();

            var categories = _stock.GroupBy(item => new { Name = item.Category }).ToList();

            categories.ForEach(item => output.Add(item.Key.Name.ToString()));

            return output.ToArray();
        }

        public Product? GetProduct(string name)
        {
            Product? outProduct = _stock.Find(p => p.Name == name);
            return outProduct;
        }

        //test-------------------------------------------------------------------------------------------------------------------------------------------------
        void LoadProducts()
        {

            AddProductToStore(new Ramen());
            AddProductToStore(new Pizza());
            AddProductToStore(new Burrito());
            AddProductToStore(new Water());
            AddProductToStore(new Beer());
            AddProductToStore(new Coffee());
            AddProductToStore(new AlienStone());
            AddProductToStore(new FakeStone());
            AddProductToStore(new ThunderStone());
        }
    }
}
