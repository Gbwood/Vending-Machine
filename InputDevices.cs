//////////////////////////////////////////////////////////////////////
//      Vending Machine (Actuators.cs)                              //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010, 2011 //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    // For each class, you can (must) add fields and overriding constructors

    public class CoinInserter
    {
        // add a field to specify an object that CoinInserted() will firstvisit
        private VendingControl VendControl;
        private int CoinIndex;


        /// <summary>
        ///  // rewrite the following constructor with a constructor that takes an object
        /// ^These instructions contradict what the directions on canvas say
        /// </summary>

        // to be set to the above field
        public CoinInserter()
        {
            
        }

        /// <summary>
        /// override constructor that sends a pointer to the VendingControl
        /// </summary>
        /// <param name="VC"></param>
        /// <param name="Index"></param>
        public CoinInserter(VendingControl VC, int Index) 
        {
            VendControl = VC;
            CoinIndex = Index;
        }
        public void CoinInserted()
        {
            VendControl.CoinInserted(CoinIndex); //Calls the CoinInserted method of Vendcontrol
        }

    }

    public class PurchaseButton
    {
        
        private VendingControl VendControl;

        //the index of the purchase button being pushed
        private int buttonIndex;


        /// <summary>
        /// default constructor
        /// </summary>
        public PurchaseButton()
        {

        }

        /// <summary>
        /// override constructor that sets the pointer to vendingcontrol and the index associated with the purchase button
        /// </summary>
        /// <param name="vc"></param>
        /// <param name="index"></param>
        public PurchaseButton(VendingControl vc, int index)
        {
            VendControl = vc;
            buttonIndex = index;
        }

        /// <summary>
        /// method that handles when a purchase button is pressed
        /// </summary>
        public void ButtonPressed()
        {
            VendControl.PurchaseButtonPushed(buttonIndex); //calls the purchasebuttonpushed method in VendControl
        }
    }

    /// <summary>
    /// handles when the coin return button is pushed by a user
    /// </summary>
    public class CoinReturnButton
    {
        private VendingControl VendController;
        
        /// <summary>
        /// default constructor
        /// </summary>
        public CoinReturnButton()
        {

        }
        /// <summary>
        /// overload constructor that sets a pointer to vending control
        /// </summary>
        /// <param name="VC"></param>
        public CoinReturnButton(VendingControl VC)
        {
            VendController = VC;
        }

        //handles when the button is pressed
        public void ButtonPressed()
        {
            VendController.ReturnChange(); //calls the method to return all the change that has been entered into the system
        }
    }
}
