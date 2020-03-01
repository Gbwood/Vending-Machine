using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// class to Control the program and manage the amount for the vending machine
    /// </summary>
    public class VendingControl
    {
        /// <summary>
        /// An array of all the different coins
        /// </summary>
        private Coin[] CoinArray;

        /// <summary>
        /// an array of all the different beverage objects in the machine
        /// </summary>
        private Beverage[] BeverageArray;

        /// <summary>
        /// keeps track of the money inputted by a user
        /// </summary>
        private int credit;

        /// <summary>
        /// saves the index of which beverage button was pushed
        /// </summary>
        private int BeverageIndex;

        /// <summary>
        /// object that manages the NoChangeLight
        /// </summary>
        private TimerLight NoChangeLight;

        /// <summary>
        /// object that controls the amount display control
        /// </summary>
        private AmountDisplay AmtDisplay;

        /// <summary>
        /// constructor for Vending Control
        /// </summary>
        /// <param name="CoinArray"></param>
        /// <param name="BeverageArray"></param>
        /// <param name="NoChangeLight"></param>
        /// <param name="AmntDisplay"></param>
        public VendingControl(Coin[] CoinArray, Beverage[] BeverageArray, TimerLight NoChangeLight, AmountDisplay AmntDisplay)
        {
            this.CoinArray = CoinArray;
            this.BeverageArray = BeverageArray;
            this.NoChangeLight = NoChangeLight;
            this.AmtDisplay = AmntDisplay;
            credit = 0;
            
        }

        /// <summary>
        /// method to update the lights associated with beverages on the UI
        /// </summary>
        private void UpdateLights()
        {
            foreach (Beverage bev in BeverageArray)
            {
                bev.UpdateLights(credit);
            }
        }


        /// <summary>
        /// checks to see if change can be made and if so, calculates what coins will be used by calling the getMaxReturn method in the coin class
        /// </summary>
        /// <param name="price"></param>
        /// <returns>true if change has been calculated successfully, false if unable to calculate</returns>
        private bool TryGetChange(int price)
        {
            //ChangeNeeded is used to keep track of how much change is needed or remaining in order to return the proper amount of change to the user
            int ChangeNeeded = credit - price;


            if (ChangeNeeded < 0) return false; // if change needed is less than 0 than the program cannot calculate the change

            for (int i = CoinArray.Length-1; i >= 0; i--) //loops through each coin object and calculates how many coins of that value will be needed to return the proper amount of change
            {
                ChangeNeeded = ChangeNeeded - CoinArray[i].GetMaxReturn(ChangeNeeded); 
                if (ChangeNeeded == 0) { credit = 0; return true; }
            }
            return false; //if the proper amount of coins needed to make change is not available then the method will return false
        }

        /// <summary>
        /// Method to handle a coin being inserted into the machine.
        /// </summary>
        /// <param name="index"></param>
        public void CoinInserted(int index)
        {
            CoinArray[index].AddCoin(); //adds a coin to the appropriate coin object

            credit += CoinArray[index].Value; //adjusts the total credit the user has inserted so far
            AmtDisplay.DisplayAmount(credit); //updates the amount displayed on the machine
            UpdateLights(); //updates the lights and checks if anything has become purchaseable
        }

        /// <summary>
        /// Method to hand a purchase buttong being pushed on the machine
        /// </summary>
        /// <param name="index"></param>
        public void PurchaseButtonPushed(int index)
        {
            BeverageIndex = index; //sets the index of the beverage selected by the user

            if (!BeverageArray[BeverageIndex].IsPurchaseable()) return; //Checks if beverage is purchaseable or not

            if (!TryGetChange(BeverageArray[BeverageIndex].Price)) { NoChangeLight.TurnOn3Sec(); return; } //checks to see if the appropriate amount of change can be made if a certain beverage is to be purchased

            BeverageArray[BeverageIndex].Purchase(); //purchases the beverage 

            for (int i = 0; i < CoinArray.Length; i++) CoinArray[i].DispenceCoins(); //dispenses the appropriate mixture of coins needed for change


            UpdateLights(); //updates all the lights on the machine
            AmtDisplay.DisplayAmount(credit); //updates the display amount of credits in the machine
        }

        /// <summary>
        /// method called when the return change button is pushed
        /// </summary>
        public void ReturnChange()
        {
            TryGetChange(0); //checks to see if proper amount of change can be returned
            for (int i = 0; i < CoinArray.Length; i++) CoinArray[i].DispenceCoins(); //dispenses coins
            UpdateLights(); //updates all the lights on the machine
            AmtDisplay.DisplayAmount(credit); //updates the display on the machine that displays how much credit has been entered
        }
    }
}
