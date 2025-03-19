using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.UI.Interop;

/// List of Known Issues ///
/// Error on button click: input string was not in a correct format
/// Exit Button doesn't work
/// Can't rename xaml/xaml.cs files to an appropriate name

namespace MortgageCalculator
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Constants //
            const int MONTHS_IN_YEAR = 12;
            
            // Variables //
            double principal;
            int years;
            int months;
            double annualInterestRate;
            int numberOfMonths;
            double monthlyRepaymentCalculationNumerator;
            double monthlyRepaymentCalculationDenominator;
            double monthlyRepayment;
            double monthlyInterestRate;

            // This section is for data entries and the try/catch for errors.
            try
            {
                principal = double.Parse(principalValueTextBox.Text);
            }
            catch (Exception exception) {
                var dialogMessage = new MessageDialog("Error!" + exception.Message);
                await dialogMessage.ShowAsync();
                principalValueTextBox.Focus(FocusState.Programmatic);
                principalValueTextBox.SelectAll();
                return;
            }

            try
            {
                years = int.Parse(yearsTextBox.Text);
            }
            catch (Exception exception)
            {
                var dialogMessage = new MessageDialog("Error!" + exception.Message);
                await dialogMessage.ShowAsync();
                yearsTextBox.Focus(FocusState.Programmatic);
                yearsTextBox.SelectAll();
                return;
            }

            try
            {
                months = int.Parse(monthsTextBox.Text);
            }
            catch (Exception exception)
            {
                var dialogMessage = new MessageDialog("Error!" + exception.Message);
                await dialogMessage.ShowAsync();
                monthsTextBox.Focus(FocusState.Programmatic);
                monthsTextBox.SelectAll();
                return;
            }

            try
            {
                annualInterestRate = double.Parse(yearInterestTextBox.Text);
            }
            catch (Exception exception)
            {
                var dialogMessage = new MessageDialog("Error!" + exception.Message);
                await dialogMessage.ShowAsync();
                yearInterestTextBox.Focus(FocusState.Programmatic);
                yearInterestTextBox.SelectAll();
                return;
            }

            // Calculation of the Monthly Interest Rate
            monthlyInterestRate = annualInterestRate / MONTHS_IN_YEAR;
            monthlyInterestRate = monthlyInterestRate * 0.01;
            monthInterestTextBox.Text = monthlyInterestRate.ToString();

            // Calculation of of the Number of Months
            numberOfMonths = (years * 12) + months;

            // Calculation Numeration of Monthly Repayment Calculation
            monthlyRepaymentCalculationNumerator = principal * (Math.Pow(1 + monthlyInterestRate, numberOfMonths)) * monthlyInterestRate;

            // Calculation Denominator of Monthly Repayment Calculation
            monthlyRepaymentCalculationDenominator = Math.Pow(1 + monthlyInterestRate, numberOfMonths) - 1;

            // Calculation of Monthly Repayment
            monthlyRepayment = monthlyRepaymentCalculationNumerator / monthlyRepaymentCalculationDenominator;
            monthlyRepayTextBox.Text = monthlyRepayment.ToString("C");
        }

        // Exit button to return to the main page of the calculator
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }
    }
}