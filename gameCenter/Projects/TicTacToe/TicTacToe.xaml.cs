using gameCenter.Projects.TicTacToe.Models;
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

namespace gameCenter.Projects.TicTacToe
{
    
    public partial class TicTacToe : Window
    {
        GameTicTacToe game = new GameTicTacToe();

        public TicTacToe()
        {

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //updates the game board, checks for a win or draw,
            // and switches the current player for the next move.
            Button button = (Button)sender;
            if (button != null && string.IsNullOrEmpty(button.Content as string))
            {
                button.Content = game.CurrentPlayer.ToString();
                int row = Grid.GetRow(button);
                int column = Grid.GetColumn(button);
                game.GameBoard[row, column] = game.CurrentPlayer;

                if (game.CheckForWin())
                {
                    MessageBox.Show($"{game.CurrentPlayer} wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetGame();
                }
                else
                {
                    if (game.IsBoardFull())
                    {
                        MessageBox.Show("It's a draw!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetGame();
                    }
                    else
                    {
                        game.CurrentPlayer = game.CurrentPlayer == 'X' ? 'O' : 'X';
                    }
                }
            }
        }

        private void ResetGame()
        {
            game.CurrentPlayer = 'X';
            game.GameBoard = new char[3, 3];

            // Clear the game board
            foreach (Button button in MainGrid.Children)
            {
                button.Content = "";
            }
        }

    }
}
