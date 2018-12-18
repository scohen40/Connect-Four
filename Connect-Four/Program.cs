using System;

namespace Connect_Four
{
    class Program
    {
        static int MAX_DEPTH = 4;
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].StartsWith("ON")) {
                Trace.ON = true;
            }
            Board gameBoard = new Board();
            bool gameOver = false;
            while (!gameOver)
            {
                Console.WriteLine("I am thinking about my move now");
                double highVal = -1.0;
                int bestMove = 0;
                double alfa = -1.0;
                double beta = 1.0;
                for (int col = 0; col < Board.NR_COLS; ++col) {
                    if (gameBoard.canMove(col)) {
                        Board nextPos = gameBoard.makeMove(Player.MAX, col);
                        double thisVal = AlphaBeta.Value(nextPos, MAX_DEPTH, alfa, beta, Player.MIN);
                        if (thisVal > highVal) {
                            bestMove = col; highVal = thisVal;
                        }
                    }
                }
                Console.WriteLine("My move is " + bestMove);
                gameBoard = gameBoard.makeMove(Player.MAX, bestMove);
                gameBoard.showBoard();
                if (gameBoard.isWin(Player.MAX)) {
                    Console.WriteLine("\n I win"); gameOver = true;
                }
                else {
                    Console.WriteLine("Your move");
                    int theirMove = UserInput.getInteger("Select column 1 - 7", 1, 7) - 1;
                    if (gameBoard.canMove(theirMove)) {
                        gameBoard = gameBoard.makeMove(Player.MIN, theirMove);
                        Console.WriteLine(""); gameBoard.showBoard();
                    }
                    if (gameBoard.isWin(Player.MIN)) {
                        Console.WriteLine("\n You win");
                        gameOver = true;
                    }
                }
            }
        }

    //    static int MAX_DEPTH = 4;
    //    static void Main(string[] args)
    //    {
    //        if (args.Length > 0 && args[0].StartsWith("ON")) { Trace.ON = true; }
    //        Board gameBoard = new Board(); bool gameOver = false; while (!gameOver)
    //        {
    //            Console.WriteLine("I am thinking about my move now"); double highVal = -1.0; int bestMove = 0; double alfa = -1.0; double beta = 1.0; for (int col = 0; col < Board.NR_COLS; ++col) { if (gameBoard.canMove(col)) { Board nextPos = gameBoard.makeMove(Player.MAX, col); double thisVal = AlphaBeta.Value(nextPos, MAX_DEPTH, alfa, beta, Player.MIN); if (thisVal > highVal) { bestMove = col; highVal = thisVal; } } }
    //            if (highVal == -1) { bestMove = DesperationMove(gameBoard); }
    //            Console.WriteLine($"My move is {(bestMove + 1)}    (subj. value {highVal})"); gameBoard = gameBoard.makeMove(Player.MAX, bestMove); gameBoard.showBoard();
    //            if (gameBoard.isWin(Player.MAX)) { Console.WriteLine("\n I win"); gameOver = true; } else { Console.WriteLine("Your move"); int theirMove = UserInput.getInteger("Select column 1 - 7", 1, 7) - 1; if (gameBoard.canMove(theirMove)) { gameBoard = gameBoard.makeMove(Player.MIN, theirMove); Console.WriteLine(""); gameBoard.showBoard(); } if (gameBoard.isWin(Player.MIN)) { Console.WriteLine("\n You win"); gameOver = true; } }
    //        }
    //    }
    //    private static int DesperationMove(Board gameBoard) { int ColumnPicked = 0; for (int col = 0; col < Board.NR_COLS; ++col) { if (gameBoard.canMove(col)) { Board nextPos = gameBoard.makeMove(Player.MIN, col); if (nextPos.isWin(Player.MIN)) { ColumnPicked = col; break; } } } return ColumnPicked; }
    //}
}
}

