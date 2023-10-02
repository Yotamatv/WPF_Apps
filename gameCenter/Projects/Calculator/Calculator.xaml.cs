using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gameCenter.Projects.Calculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        private string currentInput = string.Empty;
        private double currentNumber = 0;
        private string selectedOperator = string.Empty;

        public Calculator()
        {
            InitializeComponent();
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            // Handle number button clicks
            Button button = (Button)sender;
            currentInput += button.Content.ToString();
            ResultTextBox.Text = currentInput;
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            // Handle operator button clicks
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentNumber = double.Parse(currentInput);
                currentInput = string.Empty;
                selectedOperator = ((Button)sender).Content.ToString();
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            // Handle equals button click to perform the calculation
            if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(selectedOperator))
            {
                double secondNumber = double.Parse(currentInput);
                switch (selectedOperator)
                {
                    case "+":
                        currentNumber += secondNumber;
                        break;
                    case "-":
                        currentNumber -= secondNumber;
                        break;
                    case "*":
                        currentNumber *= secondNumber;
                        break;
                    case "/":
                        if (secondNumber != 0)
                        {
                            currentNumber /= secondNumber;
                        }
                        else
                        {
                            MessageBox.Show("Cannot divide by zero.");
                        }
                        break;
                }
                ResultTextBox.Text = currentNumber.ToString();
                currentInput = string.Empty;
                selectedOperator = string.Empty;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Handle clear button click to reset the calculator
            currentInput = string.Empty;
            currentNumber = 0;
            selectedOperator = string.Empty;
            ResultTextBox.Text = string.Empty;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Do not allow non-numeric input
            }
        }

        private bool IsNumeric(string text)
        {
            // Use a regular expression to check if the input is numeric
            return Regex.IsMatch(text, "^[0-9]+$");
        }

        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentNumber = double.Parse(ResultTextBox.Text);
        }
    }
}
