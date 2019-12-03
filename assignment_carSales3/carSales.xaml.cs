using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace assignment_carSales3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    // Charlie Asubar
    // 4 December 2019
    // Assignment CarSales Part2 
    public sealed partial class carSales : Page
    {
       
        // insurance
        const double AGE_25_UNDER = 0.20;       
        const double AGE_25_OVER = 0.10;

        // optional extras
        const double WINDOW_TINT = 150;
        const double DUCO_PROTECT = 180;
        const double GPS_NAV = 320;
        const double DLUX_SOUND = 350;

        // GST
        const double GST_RATE = 0.1;

        // warranty
        const double WARRANTY_TWO_YEARS = 0.05;
        const double WARRANTY_THREE_YEARS = 0.10;
        const double WARRANTY_FIVE_YEARS = 0.20;


        // ------------- Method Declarations ---------------------


        // get vehicle warranty        
        private double getVehicWarranty(double vehicAmount)
        {
            double vehicCost ;
            // when no warranty is selected - set default warranty to "1 Year no charge"
            if (warrantyComboBox.SelectedItem == null)
            {
                warrantyComboBox.SelectedValue = "1 Year no charge";
            }            
            // process vehiCost when a warranty is selected
            if (warrantyComboBox.SelectedValue.ToString() == "1 Year no charge")
            {
                return 0; // no additional cost for 1 year warranty
            }
            if (warrantyComboBox.SelectedValue.ToString() == "2 Years at 5% of Vehicle Cost")
            {
                // get total vehicCost by getting the sum of (0.05 * vehicAmount) and vehicAmount
                vehicCost = vehicAmount + (vehicAmount * WARRANTY_TWO_YEARS);
                return vehicCost;
            }
            if (warrantyComboBox.SelectedValue.ToString() == "3 Years at 10% of Vehicle Cost")
            {
                // get total vehicCost by getting the sum of (0.10 * vehicAmount) and vehicAmount
                vehicCost = vehicAmount + (vehicAmount * WARRANTY_THREE_YEARS);
                return vehicCost;
            }
            if (warrantyComboBox.SelectedValue.ToString() == "5 Years at 20% of Vehicle Cost")
            {
                // get total vehicCost by getting the sum of (0.20 * vehicAmount) and vehicAmount
                vehicCost = vehicAmount + (vehicAmount * WARRANTY_FIVE_YEARS);
                return vehicCost;
            }
            return 0;            
        }


        //Get Optional Extras:       
        private double getOptionalExtras(double vehicAmount)
        {
            double totalOptExtras, winTint, ducoProtect, GPSNav,DluxSound;
            winTint = 0;
            ducoProtect = 0;
            GPSNav = 0;
            DluxSound = 0;
            //  Window Tinting($150), 
            //  Duco Protection($180), 
            //  GPS Navigational System($320), and/or
            //  Deluxe Sound System($350).
            if (windowCheckBox.IsChecked == true)
            {
               winTint = WINDOW_TINT; //set window tint cost
            }
            if (ducoCheckBox.IsChecked == true)
            {
                ducoProtect = DUCO_PROTECT; //set duco cost
            }
            if (GPSCheckBox.IsChecked == true)
            {
                GPSNav = GPS_NAV; //set gps cost
            }
            if (soundCheckBox.IsChecked == true)
            {
                DluxSound = DLUX_SOUND; //set sound cost
            }
            // get the total of extras by adding them together and store them in totalOptExtras
            return totalOptExtras = winTint + ducoProtect + GPSNav + DluxSound ;            
        }


        // get accidental insurance
        private double getAccidentInsurance(double vehicPrice, double optnlExtras)
        {
            double accidentInsure, totalVehicPriceOptnlExtras;
            totalVehicPriceOptnlExtras = vehicPrice + optnlExtras;            
            // drivers under age 25 
            if (ageUnder25RadioButton.IsChecked == true)
            {
                // calculate insurance by getting the total of (0.20 * totalVehicPriceOptnlExtras) + totalVehicPriceOptnlExtras
                accidentInsure = totalVehicPriceOptnlExtras + (totalVehicPriceOptnlExtras * AGE_25_UNDER);
                return accidentInsure;
            }
            // drivers age 25 and over)
            if (age25AndOverRadioButton.IsChecked == true)
            {
                // calculate insurance by getting the total of (0.10 * totalVehicPriceOptnlExtras) + totalVehicPriceOptnlExtras
                accidentInsure = totalVehicPriceOptnlExtras + (totalVehicPriceOptnlExtras * AGE_25_OVER);
                return accidentInsure;
            }
            // if toggle switch is off          
            return 0;            
        }

        // --------- End Method Declarations -----------------

        public carSales()
        {
            this.InitializeComponent();
        }

        // SAVE Button Click
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            /*Save button is pressed, disable the customer details text boxes,
         * and set focus to the vehicle price text box, ready for data entry.*/

            // check if customer name text box is empty
            if (custDetCustNameTextBox.Text == "")
            {
                var nameTextBoxPopUpError = new MessageDialog("Enter your name PLS!");
                await nameTextBoxPopUpError.ShowAsync();
                custDetCustNameTextBox.Focus(FocusState.Programmatic);
                custDetCustNameTextBox.SelectAll();
                return;
            }
            // check if customer phone text box is empty
            if (custDetCustPhoneTextBox.Text == "")
            {
                var phoneTextBoxPopUpError = new MessageDialog("Enter your phone PLS!");
                await phoneTextBoxPopUpError.ShowAsync();
                custDetCustPhoneTextBox.Focus(FocusState.Programmatic);
                custDetCustPhoneTextBox.SelectAll();
                return;
            }
            // disable text box for customer name and customer phone
            // set cursor at vehicle price
            custDetCustNameTextBox.IsEnabled = false;            
            custDetCustPhoneTextBox.IsEnabled = false;
            custDetCustVehiPriceTextBox.Focus(FocusState.Programmatic);
        }

        // RESET Button Click
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            /*the Reset button is pressed, 
         * clear all fields and return focus to the customer name field*/

            custDetCustNameTextBox.Text = "";
            custDetCustPhoneTextBox.Text = "";
            custDetCustVehiPriceTextBox.Text = "";
            custDetCustTradeInTextBox.Text = "";
            subAmountTextBox.Text = "";
            GSTAmountTextBox.Text = "";
            finAmountTextBox.Text = "";

            warrantyComboBox.SelectedValue = null;
            windowCheckBox.IsChecked = false;
            ducoCheckBox.IsChecked = false;
            GPSCheckBox.IsChecked = false;
            soundCheckBox.IsChecked = false;
            insuranceToggleSwitch.IsOn = false;
            age25AndOverRadioButton.IsEnabled = false;
            ageUnder25RadioButton.IsEnabled = false;
            custDetCustPhoneTextBox.IsEnabled = true;
            custDetCustNameTextBox.IsEnabled = true;
            summaryMsgTextBlock.Text = "**** Transaction Summary Displayed Here ****";
            custDetCustNameTextBox.Focus(FocusState.Programmatic);
        }


        // SUMMARY Button Click 
        private async void ButtonCalc_Click(object sender, RoutedEventArgs e)
        {                      
            double vehicAmount, subAmount, gstAmount;
            double vehiclePrice, tradeInPrice, vehicWarranty, optExtras, accidentInsure;
            int initVehiclePrice = 0;

            // try catch vehicle price
            // input double as vehicle price
            // otherwise display error prompt
            try
            {
                vehiclePrice = double.Parse(custDetCustVehiPriceTextBox.Text);
            }
            catch (Exception errorMsg)
            {
                var popUpError = new MessageDialog("Invalid input in Vehicle Price. " + errorMsg.Message);
                await popUpError.ShowAsync();
                custDetCustVehiPriceTextBox.Focus(FocusState.Programmatic);
                custDetCustVehiPriceTextBox.SelectAll();
                return;
            }

            // if trade value in text box is empty set trade in value to 0
            if (custDetCustTradeInTextBox.Text == "")
            {
                custDetCustTradeInTextBox.Text = initVehiclePrice.ToString();
            }

            // try catch trade in price
            // input double as trade in price
            // otherwise display error prompt
            try
            {
                tradeInPrice = double.Parse(custDetCustTradeInTextBox.Text);
            }
            catch (Exception errorMsg)
            {
                var popUpError = new MessageDialog("Invalid input in Trade In Price. " + errorMsg.Message);
                await popUpError.ShowAsync();
                custDetCustTradeInTextBox.Focus(FocusState.Programmatic);
                custDetCustTradeInTextBox.SelectAll();                
                return;
            }


            //check that vehicle price is > 0, the tradeIn price is NOT < 0 and the vehicle price is greater than tradeIn price
            if (vehiclePrice <= 0)
            {
                var vehicPriceLessZero = new MessageDialog("Vehicle Price should not be less than or equal to ZERO!");
                await vehicPriceLessZero.ShowAsync();
                custDetCustVehiPriceTextBox.Focus(FocusState.Programmatic);
                custDetCustVehiPriceTextBox.SelectAll();
                return;
            }
            if (tradeInPrice < 0)
            {
                var tradeInPriceLessZero = new MessageDialog("Trade in price should be more than zero");
                await tradeInPriceLessZero.ShowAsync();
                custDetCustTradeInTextBox.Focus(FocusState.Programmatic);
                custDetCustTradeInTextBox.SelectAll();
                return;
            }
            if (vehiclePrice <= tradeInPrice)
            {
                var vehicPriceEqTrdInPrice = new MessageDialog("Vehicle Price should be more than Trade in price.");
                await vehicPriceEqTrdInPrice.ShowAsync();
                custDetCustVehiPriceTextBox.Focus(FocusState.Programmatic);
                custDetCustVehiPriceTextBox.SelectAll();
                return;
               
            }

            /*
             * Sub Amount as cost of all purchases (vehicle cost + warranty + extras + insurance - tradeIn)
             * gstAmount = subAmount*GST_RATE
             * vehicAmount = subAmount + gstAmount
             */

            // process calculations

            // get the value for vehicle warranty
            vehicWarranty = getVehicWarranty(vehiclePrice);
            // get the value for optional extras
            optExtras = getOptionalExtras(vehiclePrice);
            // get the value for accident insurace
            accidentInsure = getAccidentInsurance(vehiclePrice, optExtras);

            // set age radio button to false when insurance policy is set to NO
            if (insuranceToggleSwitch.IsOn == false)
            {
                accidentInsure = 0;
            }

            // the Sub Amount as cost of all purchases (vehicle cost + warranty + extras + insurance - tradeIn).             
            subAmount = vehiclePrice + vehicWarranty + optExtras + accidentInsure - tradeInPrice;
            subAmountTextBox.Text = subAmount.ToString("C");

            gstAmount = subAmount * GST_RATE;
            GSTAmountTextBox.Text = gstAmount.ToString("C");
                                  
            // process final amount
            // final amount = subAmount + gstAmount;
            vehicAmount = subAmount + gstAmount;
            finAmountTextBox.Text = vehicAmount.ToString("C");

            // display transaction summary
            summaryMsgTextBlock.Text = "**** Transaction Summary Displayed Here ****" + "\n" + "Customer Name: " + custDetCustNameTextBox.Text + "\n" + "Phone: " + custDetCustPhoneTextBox.Text + 
                "\n"+ "Vehicle Price: $" + custDetCustVehiPriceTextBox.Text + "\n" + "Trade In Price: $"+ custDetCustTradeInTextBox.Text + "\n" + "Vehicle Warranty: $" + 
                vehicWarranty + "\n" + "Optional Extras: $"+ optExtras + "\n" + "Vehicle Insurance: $" + accidentInsure + "\n" +
                "Final Amount: $" + vehicAmount;
        }

        // INSURANCE Toggle Switch Toggled
        private void InsuranceToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {                       
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            
            // The radio buttons should be disabled when the insurance toggleSwitch is off
            if (toggleSwitch.IsOn == false)
            {                                
                age25AndOverRadioButton.IsEnabled = false;
                ageUnder25RadioButton.IsEnabled = false;
                //insuranceToggleSwitch.Visibility = Visibility.Visible;
            }            
            // the radio buttons enabled when the insurance toggleSwitch is ON
            else
            {
                age25AndOverRadioButton.IsEnabled = true;
                ageUnder25RadioButton.IsEnabled = true;
                // The under 25 radio button should be set as default when toggleSwitch is on. 
                ageUnder25RadioButton.IsChecked = true; 
                //insuranceToggleSwitch.Visibility = Visibility.Visible;
            }
        }
    }
}
