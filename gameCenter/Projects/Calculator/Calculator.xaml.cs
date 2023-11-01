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
                Equals_Click(sender, e);
                currentNumber = double.Parse(currentInput);
                currentInput = string.Empty;
                //ResultTextBox.Text = string.Empty;
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
                    case "X":
                        currentNumber *= secondNumber;
                        break;
                    case "÷":
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

                currentInput = ResultTextBox.Text;
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

    }
}
