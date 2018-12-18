using System;
using System.Collections.Generic;
using System.Text;

namespace Connect_Four
{
    class AlphaBeta
    {
        public static double Value(Board board, int depth, double alfa, double beta, bool player)
        {
            Trace.println("Enter alphabeta d = " + depth + " a = " + alfa + " b = " + beta + " P = " + player);
            double value = 0.0;

            if (depth == 0) {
                value = board.heuristicValue();
            } else if (board.isFull()) {
                value = board.heuristicValue();
            } else {
                bool opponent = player == Player.MAX ? Player.MIN : Player.MAX;
                if (player == Player.MAX){
                    for (int col = 0; col < Board.NR_COLS; ++col){
                        if (board.canMove(col)){
                            Board nextPos = board.makeMove(Player.MAX, col);
                            double thisVal = Value(nextPos, depth - 1, alfa, beta, opponent);
                            if (thisVal > alfa)
                            {
                                alfa = thisVal;
                            } if (beta <= alfa)
                            {
                                break;
                            }
                        }
                    }
                    value = alfa;
                }
                else
                // player == Player.MIN
                {
                    for (int col = 0; col < Board.NR_COLS; ++col)
                    {
                        if (board.canMove(col))
                        {
                            Board nextPos = board.makeMove(Player.MIN, col);
                            double thisVal = Value(nextPos, depth - 1, alfa, beta, opponent);
                            if (thisVal < beta)
                            {
                                beta = thisVal;
                            }
                            if (beta <= alfa)
                            {
                                break;
                            }
                        }
                    }
                    value = beta;
                }
            }
            Trace.println("Exit alfabeta value = " + value);
            return value;
    
        }
    }
}
