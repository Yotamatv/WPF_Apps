using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gameCenter.Projects.CurrencyConvertorView
{
  
    public partial class CurrencyConvertorView : Window
    {
        private Projects.CurrencyConvertor.Services.CurrencyService _currency_Service;
        private Dictionary<string, double> _exchangeRates;
        public CurrencyConvertorView()
        {
            InitializeComponent();
            _currency_Service = new Projects.CurrencyConvertor.Services.CurrencyService();
            LoadCurrencies();
        }

        private async void LoadCurrencies()
        {
            //loads exchange rates from API
            try
            {
                _exchangeRates = await _currency_Service.GetExchangeRateAsync();
                string[] strings = _exchangeRates.Keys.ToArray();
                FromCurrencyComboBox.ItemsSource = strings;
                ToCurrencyComboBox.ItemsSource = strings;
            }
            catch
            {
                throw new InvalidOperationException("Failed to run GetExchangeRateAsync");
            }
        }

        private async void OnConvertClick(object sender, RoutedEventArgs e)
        {
            // This function is called when the "Convert" button is clicked. It performs a currency conversion
            // based on the selected currencies and the amount entered, and displays the result.
            string fromCurrency = FromCurrencyComboBox.SelectedItem.ToString();
            string toCurrency = ToCurrencyComboBox.SelectedItem.ToString();
            double amount = double.Parse(AmountTextBox.Text);
            txtResult.Text = $"{AmountTextBox.Text} {FromCurrencyComboBox.SelectedItem.ToString()} is equal to {(amount * (_exchangeRates[toCurrency] / _exchangeRates[fromCurrency])).ToString()} {ToCurrencyComboBox.SelectedItem.ToString()} ";
        }
    }
}
