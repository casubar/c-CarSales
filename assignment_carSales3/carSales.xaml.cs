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
using System.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace assignment_carSales3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    // Charlie Asubar
    // 18 November 2019
    // Assignment CarSales Part 3
    public sealed partial class carSales : Page
    {
        // declare the array list
        private ArrayList nameList = null;
        private ArrayList phoneNumList = null;
        private ArrayList vehicleList = null;


        //string[] nameList = { "Charlie", "", "", "", "", "", "", "", "Michael", "Scot" };
        //int[] phoneNumList = { , , , , , , , , ,  };

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

            //    1 year warranty has no charge.


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
        private void displayVehicleMakes()
        {

            string dispVehic = "";

            // call set vehicle arraylist
            vehicleList = setVehicleList();
            // sort the list
            vehicleList.Sort();

            for (int index = 0; index < vehicleList.Count; index++)
            {
                dispVehic = dispVehic + vehicleList[index] + "\n";
            }
            summaryMsgTextBlock.Text = "Vehicle List: \n" + dispVehic;
        }


        // binary search
        // search for the position of an element in the array

        //private int getCarMakeIndex(ArrayList myList, string itemToSearch)
        //{
        //    int searchItem, convertedMyList, temp;
        //    myList.Sort();

        //    int min = 0;
        //    int max = myList.Count - 1;
        //    int mid;

        //    // convert itemToSearch to int
        //    searchItem = int.Parse(itemToSearch);
        //    // convert array to int
        //    temp = myList[mid];
        //    convertedMyList = int.Parse(temp);

        //    do
        //    {
        //        mid = (min + max) / 2;


        //        if (myList[mid] == itemToSearch)
        //            return mid;
        //        if (string.Compare(myList[mid], itemToSearch), true)
        //    } while (min <= max);


            // set toUpper search item
            // searchItem = searchMakeTextBox.Text.ToUpper();



        //    // get index of item to search
        //    int index = myList.BinarySearch(itemToSearch);
            

        //    // when car is found then display a FOUND message and index from the list
        //    if (index > 0)
        //    {                
        //        return index;
        //    }
        //    // when car is NOT found then display a NOT FOUND message
        //    else
        //    {                
        //        return index;
        //    }
            
        //}

        // get the index of the car make from car list array
        private async void searchCarMake(ArrayList myList, int index)
        {
            myList.Sort();

            // when the search box is empty then prompt for a warning message and set focus back to search box
            if (searchMakeTextBox.Text == "")
            {
                var warningMsg = new MessageDialog("Search box is blank! ");
                await warningMsg.ShowAsync();
                searchMakeTextBox.Focus(FocusState.Programmatic);
                return;
            }
            // when car is found then display a FOUND message and index from the list


            // when car is NOT found then display a NOT FOUND message



        }


        //private int searchPosInArray(ArrayList listData, string itemToSearch)
        //{
        //    int min, max, mid;

        //    min = 0;
        //    max = listData.Count - 1;

        //    do
        //    {
        //        mid = (min + max) / 2;
        //        if (listData[mid] == itemToSearch) // when item is found then return the index mid
        //            return mid;

        //        if (itemToSearch > listData[mid].ToString()) // check if the item is in the top half of the search list
        //            min = mid + 1;
        //        else
        //            max = mid - 1;
        //    } while (min <= max);
        //}




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
        // set contents of Names and Phone Number in the page load event 
        private void pageLoaded(object sender, RoutedEventArgs e)
        {
            nameList = setNameList();
            phoneNumList = setPhoneNumList();

        }

        // display customer names
        private void DispAllCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            dispCustomers();
        }


        // sequential search for customers names
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
                if (nameToSearch == nameList[index].ToString().ToUpper())
                {
                    found = true;
                }
                else
                {
                    index++;
                }
            }
            if (found)
            {
                custDetCustNameTextBox.Text = nameList[index].ToString();
                custDetCustPhoneTextBox.Text = phoneNumList[index].ToString();
            }
            else
            {
                var errorMsg = new MessageDialog("No Record of name " + nameToSearch + " is found!");
                await errorMsg.ShowAsync();
            }
        }

        // DELETE BUTTON
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
            else
            {
                var warningMsg = new MessageDialog(nameToDelete + " does not EXIST! ");
                await warningMsg.ShowAsync();
                return;
            }

            
        }

        // display all vehicle makes
        private void DisplayAllMakeButton_Click(object sender, RoutedEventArgs e)
        {
            displayVehicleMakes();
        }

        // binary search vehicle make
        private async void SearchMakeButton_Click(object sender, RoutedEventArgs e)
        {
            // need item to string search
            // need arraylist
            int index = 0;
            string searchItem;

            vehicleList = setVehicleList();
            vehicleList.Sort();

            
            // set search item to Upper
            searchItem = searchMakeTextBox.Text.ToUpper();
           // index = getCarMakeIndex(vehicleList, searchItem);

            


            // when found then display a found message and the index from the array list
            if (index > 0)
            {
                // set index + 1
                index = index + 1;
                var foundMessage = new MessageDialog("Car FOUND at " + index);
                await foundMessage.ShowAsync();
                return;
            }
            else
            {
                var notFoundMessage = new MessageDialog("Car Not Found");
                await notFoundMessage.ShowAsync();
                return;
            }

        }

        

    }
}
