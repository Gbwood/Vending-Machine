using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Coin
    {
        /// <summary>
        /// Value of coin
        /// </summary>
        protected int coinValue;

        /// <summary>
        /// number of coins the vending machine has of a specific type of coin
        /// </summary>
        private int numCoins;

        /// <summary>
        /// CoinDispenser that will be used to dispense coins
        /// </summary>
        private CoinDispenser coinDispenser;

        /// <summary>
        /// keeps track of the number of each coin to return when the time comes
        /// </summary>
        private int NumCoinsToReturn;


        /// <summary>
        /// public getter for the value of a coin
        /// </summary>
       public int Value
        {
            get
            {
                return coinValue;
            }
        }        


        /// <summary>
        /// public getter for quantity
        /// </summary>
        public int Quantity
        {
            get
            {
                return numCoins;
            }
        }

        /// <summary>
        /// constructor for coin that initializes it's fields and sets numcoinstoreturn to 0
        /// </summary>
        /// <param name="coinValue"></param>
        /// <param name="numCoins"></param>
        /// <param name="coinDispenser"></param>
        public Coin(int coinValue, int numCoins, CoinDispenser coinDispenser)
        {
            this.coinValue = coinValue;
            this.numCoins = numCoins;
            this.coinDispenser = coinDispenser;
            NumCoinsToReturn = 0;
        }

        /// <summary>
        /// Adds an additional coin
        /// </summary>
        public void AddCoin()
        {
            numCoins++;
        }

        /// <summary>
        /// calculates the maximum number of a certain coin that it can dispense given the change needed
        /// </summary>
        /// <param name="Change"></param>
        /// <returns></returns>
        public int GetMaxReturn(int Change)
        {

            int CoinsDesired = Change / Value; //calculates the ideal/max number of coins to make change

            if (CoinsDesired > numCoins) NumCoinsToReturn = numCoins; //if there are not enough coins in the machine to meet the ideal/max number it will output all the ones the machine has
            else NumCoinsToReturn = CoinsDesired; //if the machine does have enough it will output the desired amount
            return NumCoinsToReturn * Value; //returns the value of the coins that will be dispensed if there is enough change
        }

        /// <summary>
        /// dispenses coins based on the number of coins to return calculated in getmaxReturn method and adjusts the number of coins and coins to return
        /// </summary>
        public void DispenceCoins()
        {
            coinDispenser.Actuate(NumCoinsToReturn);
            numCoins -= NumCoinsToReturn;
            NumCoinsToReturn = 0;
        }
    }
}
