using gameCenter.Projects.CarGame.Objects;
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
using System.Threading;
using System.Text.RegularExpressions;

namespace gameCenter.Projects.BlackJack
{
   
    public partial class BlackjackGame : Window
    {
        Hand PlayerHand;
        Hand DealerHand;
        double PlayerMoney = 1000;
        double BetAmount;
        public static Dictionary<string, string> CardsPaths = new Dictionary<string, string>
        {
           { "Ace", "Images/Ace.png" },
           { "Two", "Images/2.png" },
           { "Three", "Images/3.png" },
           { "Four", "Images/4.png" },
           { "Five", "Images/5.png" },
           { "Six", "Images/6.png" },
           { "Seven", "Images/7.png" },
           { "Eight", "Images/8.png" },
           { "Nine", "Images/9.png" },
           { "Ten", "Images/10.png" },
           { "Jack", "Images/Jack.png" },
           { "Queen", "Images/Queen.png" },
           { "King", "Images/King.png" }
        };
        public static Dictionary<string, int> BlackjackValues = new Dictionary<string, int>
        {
            { "Ace", 11 },   // Ace can also be 11, depending on the context
            { "Two", 2 },
            { "Three", 3 },
            { "Four", 4 },
            { "Five", 5 },
            { "Six", 6 },
            { "Seven", 7 },
            { "Eight", 8 },
            { "Nine", 9 },
            { "Ten", 10 },
            { "Jack", 10 },
            { "Queen", 10 },
            { "King", 10 }
        };
        private static Random random = new Random();
        public BlackjackGame()
        {
            InitializeComponent();
        }
        private static Card RandomCard()
        {
            int randomIndex = random.Next(CardsPaths.Keys.Count);
            return new Card(CardsPaths.Keys.ElementAt(randomIndex), BlackjackValues.Values.ElementAt(randomIndex), CardsPaths.Values.ElementAt(randomIndex)); 
        }
        public void DisplayHands()
        {
            DrawPlayerHand();
            DrawDealerHand();
        }
        // This function checks the outcome of the game and displays messages based on the result.
        // It compares the total value of the hands (PlayerHand and DealerHand) and updates PlayerMoney accordingly.
        // It also handles scenarios like draws, Black Jack wins, player wins, player losses, and game over if the player runs out of money.
        private void CheckWin()
        {
            if(DealerHand.cards.Count == 1)
            {
                while (DealerHand.HandTotal() < 17)
                {
                    DealerHand.AddCard(RandomCard());
                }
            }
            DisplayHands();
            if (PlayerHand.HandTotal() == DealerHand.HandTotal() ||
                PlayerHand.HandTotal() > 21 && DealerHand.HandTotal() > 21)
            {
                MessageBoxResult playAgain = MessageBox.Show($"You Drew With The Dealer", "Draw", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.No);
                PlayerMoney += BetAmount;
                ResetTable();
            }
            else if (PlayerHand.HandTotal() == 21)
            {
                MessageBoxResult playAgain = MessageBox.Show($"Black Jack You Won {1.5 * BetAmount}$", "You Win", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.No);
                PlayerMoney += 2.5 * BetAmount;
                ResetTable();
            }
            else if (PlayerHand.HandTotal() < 21 && (DealerHand.HandTotal() > 21 || PlayerHand.HandTotal() > DealerHand.HandTotal()))
            {
                MessageBoxResult playAgain = MessageBox.Show($"You Won {BetAmount}$", "You Win", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.No);
                PlayerMoney += 2 * BetAmount;
                ResetTable();
            }
            else if (PlayerHand.HandTotal() > 21 || DealerHand.HandTotal() > PlayerHand.HandTotal()) 
            {
                MessageBoxResult playAgain = MessageBox.Show($"You Lost {BetAmount}$", "You Lost", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.No);
                if (PlayerMoney == 0) 
                {
                    MessageBoxResult GameOver = MessageBox.Show("You Lost All Your Money, The Casino Thanks You.\nCome Again!", "Game Over", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.No);
                    this.Close();
                }
                ResetTable();
            }
        }
        // This function is called when the "Hit Me" button is clicked.
        // It adds a random card to the player's hand, displays the updated hands,
        // and checks for a win condition if the player's hand total is greater than or equal to 21.
        private void HitMe_Click(object sender, RoutedEventArgs e)
        {
            PlayerHand.AddCard(RandomCard());
            DisplayHands();
            if(PlayerHand.HandTotal() >= 21)
            {
                CheckWin();
            }
        }
        //after the ploayer stood, the diller will take cards until hitting value 17 - 21 or busting, at the end we check who won
        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            while(DealerHand.HandTotal()<17)
            {
                DealerHand.AddCard(RandomCard());
                DisplayHands();
            }
            
            CheckWin();
        }

        // These functions are responsible for drawing the player's and dealer's hands on the user interface.
        // They clear the respective panels and then loop through the cards in the hands, creating and displaying 
        private void DrawPlayerHand()
        {
            PlayerPannel.Children.Clear();
            foreach (Card card in PlayerHand.cards)
            {
                Image cardImage = new Image();
                cardImage.Source = new BitmapImage(new Uri(card.ImgSource, UriKind.RelativeOrAbsolute));
                cardImage.Width = 80;
                cardImage.Height = 150;
                PlayerPannel.Children.Add(cardImage);
            }
        }
        private void DrawDealerHand()
        {
            DealerPannel.Children.Clear();
            foreach (Card card in DealerHand.cards)
            {
                Image cardImage = new Image();
                cardImage.Source = new BitmapImage(new Uri(card.ImgSource, UriKind.RelativeOrAbsolute));
                cardImage.Width = 80;
                cardImage.Height = 150;
                DealerPannel.Children.Add(cardImage);
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            DisplayBetInput();
            StartGameButton.Visibility = Visibility.Collapsed;
        }
        private void InitGame()
        {
            PlayerHand = new Hand(RandomCard(), RandomCard());
            DealerHand = new Hand(RandomCard());
            if (PlayerHand.HandTotal() == 21)
            {
                CheckWin();
            }
            Stand.Visibility = Visibility.Visible;
            HitMe.Visibility = Visibility.Visible;
            DisplayHands();
        }

        //sets everything up to make the next bet, clears the previous one and displays cash amounts
        private void ResetTable()
        {
            BetAmount = 0;
            BetMoney.Content = $"Bet: {BetAmount}$";
            TotalMoney.Content = $"Total Money: {PlayerMoney}$";
            DealerHand.Clear();
            PlayerHand.Clear();
            DisplayHands();
            DisplayBetInput();
        }

        // This function is called when the "Submit Bet" button is clicked. It processes the bet input,
        // updates the player's money, collapses bet overlay and initiates the game if the player has enough money to place the bet.
        private void SubmitBetButton_Click(object sender, RoutedEventArgs e)
        {
            BetAmount= double.Parse(BetAmountTxt.Text);
            if(PlayerMoney>=BetAmount)
            {
                PlayerMoney -= BetAmount;
                BetMoney.Content = $"Bet: {BetAmount}$";
                TotalMoney.Content = $"Total Money: {PlayerMoney}$";
                BetLabel.Visibility = Visibility.Collapsed;
                BetAmountTxt.Visibility = Visibility.Collapsed;
                SubmitBetButton.Visibility = Visibility.Collapsed;
                InitGame();
            }
        }

        //loads bet overlay
        private void DisplayBetInput()
        {
            BetLabel.Visibility = Visibility.Visible;
            BetAmountTxt.Visibility = Visibility.Visible;
            SubmitBetButton.Visibility = Visibility.Visible;
            Stand.Visibility = Visibility.Collapsed;
            HitMe.Visibility = Visibility.Collapsed;
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
    }
}
