using System;
using System.Collections.Generic;
using System.Text;

namespace Connect_Four
{
    class Board
    {
        public static readonly int NR_ROWS = 6;
        public static readonly int NR_COLS = 7;  // has to be odd > 1

        public enum Cell { RED, BLACK, EMPTY };
        private Cell[,] board = new Cell[NR_ROWS, NR_COLS];

        #region  Constructors

        public Board()
        {
            for (int row = NR_ROWS - 1; row >= 0; --row)
            {
                for (int col = 0; col < NR_COLS; ++col)
                {
                    board[row, col] = Cell.EMPTY;
                }
            }
        }

        public Board(Cell[,] other)
        {
            for (int row = NR_ROWS - 1; row >= 0; --row)
            {
                for (int col = 0;
                    col < NR_COLS; ++col)
                {
                    board[row, col] = other[row, col];
                }
            }
        }

        #endregion

        /***  display the current status of the board */
        public void showBoard()
        {
            for (int row = NR_ROWS - 1; row >= 0; --row)
            {
                Console.Write("|");
                for (int col = 0; col < NR_COLS; ++col)
                {
                    switch (board[row, col])
                    {
                        case Cell.RED:
                            Console.Write("R|");
                            break;
                        case Cell.BLACK:
                            Console.Write("B|");
                            break;
                        default:
                            Console.Write(" |");
                            break;
                    }
                }
                Console.WriteLine();
                for (int col = 0; col < NR_COLS; ++col)
                {
                    Console.Write("--");
                }
                Console.WriteLine("-");
            }
            for (int col = 0; col < NR_COLS; ++col)
            {
                Console.Write($" {(col + 1)}");
            }
            Console.WriteLine();
        }

        /***  make the specified move**  
         * @param Player who - MAX or MIN
         * @param int column - into which column was the next piece dropped
         * @return Board - new board configuration after the move was made
         */
        public Board makeMove(bool who, int column)
        {
            Board newBoard = new Board(board);
            int row = 0;
            while (board[row, column] != Cell.EMPTY && row < NR_ROWS)
            {
                ++row;
            }
            if (row < NR_ROWS)
            {
                newBoard.board[row, column] = who == Player.MAX ? Cell.RED : Cell.BLACK;
            }
            else
            {
                // this cannot happen
                Console.WriteLine("Booga booga!");
            }
            return newBoard;
        }

        public bool canMove(int col)
        {
            //        Console.WriteLine("Enter canMove col = " + col);
            bool retVal = false;
            if (col >= 0 && col < NR_COLS && board[NR_ROWS - 1, col] == Cell.EMPTY)
            {
                retVal = true;
            }
            //        Console.WriteLine("exiting canMove with " + retVal);
            return retVal;
        }
        //<editor-fold defaultstate="collapsed" desc="Check if the current board contains connected 4">
        /***  is the given player the winner**  
         * @param Player player: MAX or MIN*  
         * @return bool decision
         */
        public bool isWin(bool player)
        {
            bool retVal = false;
            Cell color = player == Player.MAX ? Cell.RED : Cell.BLACK;
            // for every cell check if it is a first
            // cell in a winning configuration
            for (int row = 0; row < NR_ROWS; ++row)
            {
                for (int col = 0; col < NR_COLS; ++col)
                {
                    if (isWinConfiguration(color, row, col))
                    {
                        retVal = true;
                        goto _end_search;
                    }
                }
            }
            _end_search: return retVal;
        }


        /**
        * check if the given cell is a start of four consecutive cells of color
        * @param Cell color - which color to check
        * @param int row - which row to start
        * @param int col - which column to start
        * @return bool success or failure
        */
        private bool isWinConfiguration(Cell color, int row, int col)
        {
            bool retVal = false;
            if (board[row, col] == color)
            {
                // then check up, UR diag, right, LR diag
                retVal =
                    checkUpward(color, row, col)
                    || checkUpDiag(color, row, col)
                    || checkToRite(color, row, col)
                    || checkDnDiag(color, row, col);
            }
            return retVal;
        }

        private bool checkUpward(Cell color, int row, int col)
        {
            bool retVal = false;
            if (row < NR_ROWS - 3)
            {
                retVal =
                    color == board[row + 1, col]
                    && color == board[row + 2, col]
                    && color == board[row + 3, col];
            }
            return retVal;
        }

        private bool checkUpDiag(Cell color, int row, int col)
        {
            bool retVal = false;
            if (row < NR_ROWS - 3 && col < NR_COLS - 3)
            {
                retVal =
                    color == board[row + 1, col + 1]
                    && color == board[row + 2, col + 2]
                    && color == board[row + 3, col + 3];
            }
            return retVal;
        }

        private bool checkToRite(Cell color, int row, int col)
        {
            bool retVal = false;
            if (col < NR_COLS - 3)
            {
                retVal =
                    color == board[row, col + 1]
                    && color == board[row, col + 2]
                    && color == board[row, col + 3];
            }
            return retVal;
        }

        private bool checkDnDiag(Cell color, int row, int col)
        {
            bool retVal = false;
            if (row > 2 && col < NR_COLS - 3)
            {
                retVal =
                    color == board[row - 1, col + 1]
                    && color == board[row - 2, col + 2]
                    && color == board[row - 3, col + 3];
            }
            return retVal;
        }
        //</editor-fold>
        /**  
        * is the entire board filled?
        * @return
        */
        public bool isFull()
        {
            bool fullStatus = true;
            for (int row = NR_ROWS - 1; row >= 0; --row)
            {
                for (int col = 0; col < NR_COLS; ++col)
                {
                    if (board[row, col] == Cell.EMPTY)
                    {
                        fullStatus = false;
                        goto _end_search;
                    }
                }
            }
            _end_search: return fullStatus;
        }

        public double heuristicValue()
        {
            double retVal = 0.0;
            if (isWin(Player.MAX))
            {
                retVal = 1.0;
            }
            else if (isWin(Player.MIN))
            {
                retVal = -1.0;
            }
            else
            {
                retVal = heuristicColumsCount();
            }
            return retVal;
        }

        private double heuristicColumsCount()
        {
            double value = 0.0;
            int midColumn = NR_COLS / 2;
            for (int row = 0; row < NR_ROWS; ++row)
            {
                for (int col = 1; col <= midColumn; ++col)
                {
                    if (board[row, col] == Cell.RED)
                    {
                        value += col * 0.05;
                    }
                    else if (board[row, col] == Cell.BLACK)
                    {
                        value -= col * 0.05;
                    }
                }
                for (int col = NR_COLS - 2; col > midColumn; --col)
                {
                    if (board[row, col] == Cell.RED)
                    {
                        value += (NR_COLS - col) * 0.05;
                    }
                    else if (board[row, col] == Cell.BLACK)
                    {
                        value -= (NR_COLS - col) * 0.05;
                    }
                }
            }
            return value;
        }
    }
}

