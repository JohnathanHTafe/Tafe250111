using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class MainConverter : Page
	{
		private Dictionary<string, double> exchangeRates = new Dictionary<string, double>()
		{
			{ "USD_EUR", 0.85189982 },
			{ "USD_GBP", 0.72872436 },
			{ "USD_INR", 74.257327 },

			{ "EUR_USD", 1.1739732 },
			{ "EUR_GBP", 0.8556672 },
			{ "EUR_INR", 87.00755 },

			{ "GBP_USD", 1.371907 },
			{ "GBP_EUR", 1.1686692 },
			{ "GBP_INR", 101.68635 },

			{ "INR_USD", 0.011492628 },
			{ "INR_EUR", 0.013492774 },
			{ "INR_GBP", 0.0098339397 }
		};

		public MainConverter()
		{
			this.InitializeComponent();
		}

		private void ConvertCurrency_Click(object sender, RoutedEventArgs e)
		{
			double amount;
			if (!double.TryParse(AmountTextBox.Text, out amount) || amount < 0)
			{
				ResultTextBlock.Text = "Invalid amount!";
				ExchangeRateTextBlock.Text = "";
				return;
			}

			string fromCurrency = ((ComboBoxItem)FromCurrencyComboBox.SelectedItem).Content.ToString().Split(' ')[0];
			string toCurrency = ((ComboBoxItem)ToCurrencyComboBox.SelectedItem).Content.ToString().Split(' ')[0];

			string key = $"{fromCurrency}_{toCurrency}";
			string reverseKey = $"{toCurrency}_{fromCurrency}";

			if (exchangeRates.ContainsKey(key))
			{
				double rate = exchangeRates[key];
				double convertedAmount = amount * rate;

				// Hiển thị kết quả chuyển đổi tiền
				ResultTextBlock.Text = $"{amount} {fromCurrency} = {convertedAmount} {toCurrency}";

				//Hiển thị tỷ giá hối đoái cho cả 2 chiều
				if (exchangeRates.ContainsKey(reverseKey))
				{
					double reverseRate = exchangeRates[reverseKey];
					ExchangeRateTextBlock.Text =
						$"1 {fromCurrency} = {rate} {toCurrency}\n" +
						$"1 {toCurrency} = {reverseRate} {fromCurrency}";
				}
			}
			else if (fromCurrency == toCurrency)
			{
				ResultTextBlock.Text = $"{amount} {fromCurrency} = {amount} {toCurrency}";
				ExchangeRateTextBlock.Text = "";
			}
			else
			{
				ResultTextBlock.Text = "Exchange rate not available!";
				ExchangeRateTextBlock.Text = "";
			}
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
