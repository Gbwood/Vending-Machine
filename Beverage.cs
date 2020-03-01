using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// Class to manage beverage objects
    /// </summary>
    public class Beverage
    {
        private int price;
        private int quantityAvailable;
        private Light purchaseableLight;
        private Light SoldOutLight;
        private CanDispenser canDispenser;

        /// <summary>
        /// public getter for the price price field. Returns the value of the price of the beverage
        /// </summary>
        public int Price
        {
            get
            {
                return price;
            }
        }

#if DEBUG
        public int Quantity
        {
            get
            {
                return quantityAvailable;
            }
        }
#endif



        /// <summary>
        /// Beverage Constructor
        /// </summary>
        /// <param name="price"></param>
        /// <param name="quantityAvailable"></param>
        /// <param name="purchLight"></param>
        /// <param name="SoldOutLight"></param>
        /// <param name="canDispenser"></param>
        public Beverage(int price, int quantityAvailable, Light purchLight, Light SoldOutLight, CanDispenser canDispenser)
        {
            this.price = price;
            this.quantityAvailable = quantityAvailable;
            this.purchaseableLight = purchLight;
            this.SoldOutLight = SoldOutLight;
            this.canDispenser = canDispenser;
        }

        /// <summary>
        /// checks if a beverage is purchaseable
        /// </summary>
        /// <returns></returns>
        public bool IsPurchaseable()
        {
            return purchaseableLight.IsOn();
        }
        /// <summary>
        /// Purchase method that dispenses can and ajdusts inventory
        /// </summary>
        public void Purchase()
        {
            quantityAvailable--;
            canDispenser.Actuate(); 
        }

        /// <summary>
        /// method to update all the lights associated with beverages. Will check if can is sold out,or if it is affordable.
        /// </summary>
        /// <param name="credit"></param>
        public void UpdateLights(int credit)
        {
            if (quantityAvailable == 0) { SoldOutLight.TurnOn(); purchaseableLight.TurnOff(); return; }
            if (credit >= price) { purchaseableLight.TurnOn(); return; }
            purchaseableLight.TurnOff();
        }
    }
}
