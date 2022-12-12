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

    /// <summary>
    /// Stores all user's coins 
    /// </summary>
    internal class Wallet
    {

        /// <summary>
        /// where the coins are stored
        /// </summary>
        private List<Coin> CoinList = new List<Coin>();

        

        public Wallet(int ones = 0, int fives = 0, int tens = 0)
        {
            FillWallet(ones, fives, tens);
        }


        /// <summary>
        /// Get wallet content as a string array
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// fills wallet by given number of coins
        /// </summary>
        /// <param name="ones"></param>
        /// <param name="fives"></param>
        /// <param name="tens"></param>
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


        /// <summary>
        /// Counts the diffeent types of coins inside wallet
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CountCoinTypeInWallet(Type type)
        {
            return CoinList.Count(coin => coin.GetType() == type);
        }


        /// <summary>
        /// returns all diffrent coin types currently inside the wallet 
        /// </summary>
        /// <returns></returns>
        public Type[] GetCoinTypesFoundInWallet()
        {
            List<Type> output = new List<Type>();

            var types = CoinList.GroupBy(item => new { Type = item.GetType() }).ToList();

            types.ForEach(item => output.Add(item.Key.Type));

            return output.ToArray();
        }


        /// <summary>
        /// tries to use a given coin type, and has a specific coin as output if success
        /// </summary>
        /// <param name="coinType"></param>
        /// <param name="coinToUse"></param>
        /// <returns></returns>
        public bool UseCoin(Type? coinType, out Coin? coinToUse)
        {
            if (coinType != null)
            {
                //checks if the coin type exists in wallet
                Coin? coinFound = CoinList.Find(coin => coin.GetType() == coinType);

                if (coinFound != null)
                {
                    //if coin is found it removes it from wallet and sets it as output
                    CoinList.Remove(coinFound);
                    coinToUse = coinFound;
                    return true;
                }


            }



            coinToUse = null;
            return false;
        }


        /// <summary>
        /// Adds given coin to wallet
        /// </summary>
        /// <param name="newCoin"></param>
        public void AddCoin(Coin newCoin)
        {
            CoinList.Add(newCoin);
        }



        /// <summary>
        /// returns a type at given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
