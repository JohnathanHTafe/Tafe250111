using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TripCalc : Page
	{
		public TripCalc()
		{
			this.InitializeComponent();
		}

		private async void CalculateButton_Click(object sender, RoutedEventArgs e)
		{
			double ppd;
			double noOfDays;
			double totalCost;

			try
			{
				ppd = double.Parse(pricePerDayBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error please enter with only numbers. ");
				await dialogMessage.ShowAsync();
				pricePerDayBox.Focus(FocusState.Programmatic);
				pricePerDayBox.SelectAll();
				return;
			}

			try
			{
				noOfDays = int.Parse(numOfDayHiredBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error please enter with only numbers. ");
				await dialogMessage.ShowAsync();
				numOfDayHiredBox.Focus(FocusState.Programmatic);
				numOfDayHiredBox.SelectAll();
				return;
			}
			totalCost = noOfDays * ppd;

			amountToPayBox.Text = totalCost.ToString("C");


		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
