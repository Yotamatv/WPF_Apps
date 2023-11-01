using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.TicTacToe.Models
{
    internal class GameTicTacToe
    {
        public char[,] GameBoard { get; set; }
        public char CurrentPlayer { get; set; }
        public GameTicTacToe()
        {
            GameBoard = new char[3, 3];
            CurrentPlayer = 'X';
        }
        public bool CheckForWin()
        {
            //checks for a win in a tic-tac-toe game by examining the current state of the game board.
            // It checks rows, columns, and diagonals for matching symbols ('X' or 'O') and returns true if a win is found.
            for (int i = 0; i < 3; i++)
            {
                if (GameBoard[i, 0] == CurrentPlayer && GameBoard[i, 1] == CurrentPlayer && GameBoard[i, 2] == CurrentPlayer)
                    return true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (GameBoard[0, i] == CurrentPlayer && GameBoard[1, i] == CurrentPlayer && GameBoard[2, i] == CurrentPlayer)
                    return true;
            }

            if (GameBoard[0, 0] == CurrentPlayer && GameBoard[1, 1] == CurrentPlayer && GameBoard[2, 2] == CurrentPlayer)
                return true;

            if (GameBoard[0, 2] == CurrentPlayer && GameBoard[1, 1] == CurrentPlayer && GameBoard[2, 0] == CurrentPlayer)
                return true;

            return false;
        }
        public bool IsBoardFull()
        {
            //retun true if board is full, false if not
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (GameBoard[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
