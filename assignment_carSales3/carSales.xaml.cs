﻿using System;
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
using System.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace assignment_carSales3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    // Charlie Asubar
    // 4 December 2019
    // Assignment CarSales Part 3
    public sealed partial class carSales : Page
    {
        // declare the array list
        private ArrayList nameList = null;
        private ArrayList phoneNumList = null;
        private ArrayList vehicleList = null;
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
        // set the contents of the name ArrayList
        private ArrayList setNameList()
        {
            ArrayList nameValue = new ArrayList();
            nameValue.Add("Charlie");
            nameValue.Add("Chris");
            nameValue.Add("Carlisle");
            nameValue.Add("Minerva");
            nameValue.Add("Carlo");
            nameValue.Add("John");
            nameValue.Add("Rey");
            nameValue.Add("Lito");
            nameValue.Add("Michael");
            nameValue.Add("Scot");
            return nameValue;            
        }

        // set the contents of the phone number ArrayList
        private ArrayList setPhoneNumList()
        {
            ArrayList phoneNumValue = new ArrayList();
            phoneNumValue.Add(421368501);
            phoneNumValue.Add(423368502);
            phoneNumValue.Add(423489503);
            phoneNumValue.Add(421658504);
            phoneNumValue.Add(423687505);
            phoneNumValue.Add(426456506);
            phoneNumValue.Add(425426507);
            phoneNumValue.Add(425456508);
            phoneNumValue.Add(421783509);
            phoneNumValue.Add(421657500);
            return phoneNumValue;
        }

        // set the contents of vehicle list
        private ArrayList setVehicleList()
        {
            ArrayList vehicleName = new ArrayList();
            vehicleName.Add("Toyota");
            vehicleName.Add("Holden");
            vehicleName.Add("Mitsubishi");
            vehicleName.Add("Ford");
            vehicleName.Add("BMW");
            vehicleName.Add("Mazda");
            vehicleName.Add("Volkswagen");
            vehicleName.Add("Mini");            
            return vehicleName;
        }


        // get vehicle warranty        
        private double getVehicWarranty(double vehicAmount)
        {
            double vehicCost;
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
            double totalOptExtras, winTint, ducoProtect, GPSNav, DluxSound;
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
            return totalOptExtras = winTint + ducoProtect + GPSNav + DluxSound;
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


        // display all customers
        private void dispCustomers()
        {
            string dispCustomer = "";
            for (int index = 0; index < nameList.Count; index++)
            {
                dispCustomer = dispCustomer + nameList[index] + " - " + phoneNumList[index] + "\n";
            }
            summaryMsgTextBlock.Text = "Customers and Phone Nos.:  \n" + dispCustomer;
        }

        // display all vehicle makes
        private void displayVehicleMakes(ArrayList listVehicle)
        {
            string dispVehic = "";          
            // sort the list
            listVehicle.Sort();
            for (int index = 0; index < listVehicle.Count; index++)
            {
                dispVehic = dispVehic + listVehicle[index] + "\n";
            }
            summaryMsgTextBlock.Text = "Vehicle List: \n" + dispVehic;
        }


        // binary search for vehicle make
        private int binarySearchVehicle(ArrayList myList, string itemToSearch)
        {
            int min = 0;
            int max = myList.Count - 1;
            int mid;
            do
            {
                mid = (min + max) / 2;
                // when itemToSearch is found then return the index where it was found i.e. [mid]
                if (myList[mid].ToString().ToUpper() == itemToSearch.ToUpper())
                    return mid;
                // check if itemToSearch is above the list
                if (itemToSearch.CompareTo(myList[mid].ToString().ToUpper()) > 0)
                    min = mid + 1; // set min = mid + 1
                // otherwise itemToSearch is below the list
                else
                    max = mid - 1; // set max = mid - 1
            } while (min <= max);
            return -1; // return - 1 when itemToSearch is not found
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
                "\n" + "Vehicle Price: $" + custDetCustVehiPriceTextBox.Text + "\n" + "Trade In Price: $" + custDetCustTradeInTextBox.Text + "\n" + "Vehicle Warranty: $" +
                vehicWarranty + "\n" + "Optional Extras: $" + optExtras + "\n" + "Vehicle Insurance: $" + accidentInsure + "\n" +
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


        // PAGE LOAD    
        // set contents of Names and Phone Number and vehicle list in the page load event 
        private void pageLoaded(object sender, RoutedEventArgs e)
        {
            nameList = setNameList();
            phoneNumList = setPhoneNumList();
            vehicleList = setVehicleList();
        }

        // DISPLAY CUSTOMER NAMES
        private void DispAllCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            dispCustomers();
        }


        // SEARCH SEQUENTIAL for customers names
        private async void SearchNameButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            bool found = false;
            // get input from Customer name text box
            string nameToSearch = custDetCustNameTextBox.Text.ToUpper();
            // when name is empty, then prompt for error and return focus to re-enter
            if (custDetCustNameTextBox.Text == "")
            {
                var errorMessage = new MessageDialog("Pls. Enter a Name! ");
                await errorMessage.ShowAsync();
                custDetCustNameTextBox.Focus(FocusState.Programmatic);
                return;
            }
            // process sequential search
            while (!found && index < nameList.Count)
            {
                // when nameToSearch is located then set found = true
                if (nameToSearch == nameList[index].ToString().ToUpper())
                {
                    found = true;
                }
                else
                {
                    index++;
                }
            }
            if (found) // when nameToSearch is found then display name and phone number in nameTextBox and phoneTextBox
            {
                custDetCustNameTextBox.Text = nameList[index].ToString();
                custDetCustPhoneTextBox.Text = phoneNumList[index].ToString();
            }
            else // when nameToSearch is not found then prompt message not found 
            {
                var errorMsg = new MessageDialog("No Record of name " + nameToSearch + " is found!");
                await errorMsg.ShowAsync();
            }
        }

        // DELETE NAME BUTTON
        // delete a name in the customer ArrayList
        private async void DeleteNameButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            bool found = false;
            string tempName, tempPhone;

            // check if input box is empty
            if (custDetCustNameTextBox.Text == "")
            {
                var errorMsg = new MessageDialog("Pls. enter a name to delete");
                await errorMsg.ShowAsync();
                custDetCustNameTextBox.Focus(FocusState.Programmatic);
                return;
            }
            // get input from user
            string nameToDelete = custDetCustNameTextBox.Text.ToUpper();
            // process sequential search
            while (!found && index < nameList.Count)
            {
                // when nameToDelete is located then set found = true
                if (nameToDelete == nameList[index].ToString().ToUpper())
                    found = true;
                else
                    index++;
            }
            // when found then delete from arraylist
            if (found)
            {
                // store phone and name to temp
                tempName = nameList[index].ToString().ToUpper();
                tempPhone = phoneNumList[index].ToString();
                // remove name and phone from arraylist
                nameList.RemoveAt(index);
                phoneNumList.RemoveAt(index);
                // display new arraylist
                dispCustomers();
                // display delete confirmation message
                var deleteSuccessMsg = new MessageDialog("Customer Name: " + tempName + "\n" + "Phone: " + tempPhone + "\n \n" + "Deleted Successfully! \n" + "New customer count is " + nameList.Count);
                await deleteSuccessMsg.ShowAsync();
                return;
            }
            else // when nameToDelete is not found then prompt name not found
            {
                var warningMsg = new MessageDialog(nameToDelete + " does not EXIST! ");
                await warningMsg.ShowAsync();
                return;
            }            
        }



        // DISPLAY ALL VEHICLE makes
        private void DisplayAllMakeButton_Click(object sender, RoutedEventArgs e)
        {
            displayVehicleMakes(vehicleList);
        }


        // SEARCH BINARY vehicle make
        private async void SearchMakeButton_Click(object sender, RoutedEventArgs e)
        {           
            int foundPos = 0;
            string searchItem;    
            // when searchbox is empty then promt empty search box
            if (searchMakeTextBox.Text == "")
            {
                var warningMsg = new MessageDialog("Pls. enter item to search");
                await warningMsg.ShowAsync();
                return;
            }
            // set search item to Upper case
            searchItem = searchMakeTextBox.Text.ToUpper();          
            vehicleList.Sort(); // make sure list is sorted
            // call the binarySearch()
            foundPos = binarySearchVehicle(vehicleList, searchItem);
            // when found then display a found message and the index from the array list
            if (foundPos == -1) // when not found then prompt not found message
            {
                var notFoundMessage = new MessageDialog(searchItem + " Car Not Found");
                await notFoundMessage.ShowAsync();
                return;                
            }
            else // when found then display message found and the index location from the list
            {
                foundPos = foundPos + 1;
                var foundMessage = new MessageDialog("Car FOUND at index " + foundPos);
                await foundMessage.ShowAsync();
                return;
            }
        }

       

        // INSERT VEHICLE make
        private async void InsertMakeButton_Click(object sender, RoutedEventArgs e)
        {
            int pos = 0;
            bool found = false;
            string itemToSearch = insertMakeTextBox.Text.ToUpper();  
            // when search box is empty then prompt for a warning and reset focus for re-entry
            if (insertMakeTextBox.Text == "")
            {
                var msgWarning = new MessageDialog("Pls. enter item to search! ");
                await msgWarning.ShowAsync();
                insertMakeTextBox.Focus(FocusState.Programmatic);
                return;
            }
            // do sequential search
            while (!found && pos < vehicleList.Count) //while not found and not end of array 
            {
                if (itemToSearch == vehicleList[pos].ToString().ToUpper()) // check if the name is found
                    found = true;
                else
                    pos++; // if no match move to next element in array
            }
            // when car is already in the list then don't add in the list and prompt warning msg
            if (pos < vehicleList.Count)
            {
                var warningMsg = new MessageDialog(itemToSearch + " Car already exist");
                await warningMsg.ShowAsync();
            }
            else // when car is not in the list then insert the car in the list and prompt insert success
            {                
                vehicleList.Add(itemToSearch); // add car make to the list
                displayVehicleMakes(vehicleList); // display full car make list
                var insertMsgSuccess = new MessageDialog(itemToSearch + " added to the list!");
                await insertMsgSuccess.ShowAsync();
            }

        }
    }
}
