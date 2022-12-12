using Automat_Console.Coins;
using Automat_Console.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Automat_Console
{
    internal class Wallet
    {


        private List<Coin> CoinList = new List<Coin>();

        

        public Wallet(int ones = 0, int fives = 0, int tens = 0)
        {
            FillWallet(ones, fives, tens);
        }

        public string[] GetWalletContent()
        {
            List<string> output = new List<string>();

            Type[] coinTypesInWallet = GetCoinTypesFoundInWallet();

            foreach (Type coinType in coinTypesInWallet)
            {
                int count = CoinList.Count(coin => coin.GetType() == coinType);

                output.Add($" {coinType.Name} x{count}");

            }

            return output.ToArray();

        }

        public void FillWallet(int ones =0, int fives=0, int tens=0)
        {
            for (int i = 0; i < ones; i++)
            {
                CoinList.Add(new OneCrown());
            }
            for (int i = 0; i < fives; i++)
            {
                CoinList.Add(new FiveCrown());
            }
            for (int i = 0; i < tens; i++)
            {
                CoinList.Add(new TenCrown());
            }
        }

        public int CountCoinTypeInWallet(Type type)
        {
            return CoinList.Count(coin => coin.GetType() == type);
        }

        public Type[] GetCoinTypesFoundInWallet()
        {
            List<Type> output = new List<Type>();

            var types = CoinList.GroupBy(item => new { Type = item.GetType() }).ToList();

            types.ForEach(item => output.Add(item.Key.Type));

            return output.ToArray();
        }

        public bool UseCoin(Type? coinType, out Coin? coinToUse)
        {
            if (coinType != null)
            {

                Coin? coinFound = CoinList.Find(coin => coin.GetType() == coinType);

                if (coinFound != null)
                {

                    CoinList.Remove(coinFound);
                    coinToUse = coinFound;
                    return true;
                }


            }



            coinToUse = null;
            return false;
        }


        public void AddCoin(Coin newCoin)
        {
            CoinList.Add(newCoin);
        }


        public Type? CoinTypeByIndex(int index)
        {
            Type[] coinTypes = GetCoinTypesFoundInWallet();

            if(coinTypes.Length > index && index >= 0)
            {
                return coinTypes[index];
            }
            else{
                return null;
            }
            
        }
    }
}
