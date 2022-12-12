using System;
using TicTacToe.Model.Data;

namespace TicTacToe.Model.GameLogic;

public class GameLogic
{
    public int MovesInARowToWin { get; private set; }

    public GameLogic(int movesInARowToWin)
    {
        MovesInARowToWin = movesInARowToWin;
    }

    public bool IsValidBoard(GameState gameState)
    {
        if (gameState.BoardSize < MovesInARowToWin)
            return false;

        return true;
    }

    public bool PlayerHasWon(GameState gameState)
    {
        var boardSize = gameState.BoardSize;
        var player = gameState.GetCurrentPlayer().PlayerNumber;

        //Check all rows
        for (int row = 0; row < boardSize; row++)
            if (CheckIfWinningDirection(gameState, player, row, 0, 0, 1))
                return true;

        //Check all columns
        for (int column = 0; column < boardSize; column++)
            if (CheckIfWinningDirection(gameState, player, 0, column, 1, 0))
                return true;

        //Check diagonally from top row both directions
        for (int topRowStartingPoint = 0; topRowStartingPoint < boardSize; topRowStartingPoint++)
        {
            if (CheckIfWinningDirection(gameState, player, 0, topRowStartingPoint, 1, 1))
                return true;

            if (CheckIfWinningDirection(gameState, player, 0, topRowStartingPoint, 1, -1))
                return true;
        }

        //Check diagonals from first column. First index checked by previous loop.
        for (int firstColumnStartingPoint = 0; firstColumnStartingPoint < boardSize; firstColumnStartingPoint++)
            if (CheckIfWinningDirection(gameState, player, 0, firstColumnStartingPoint, 1, 1))
                return true;

        //Check diagonals last column. First index checked by previous loop.
        for (int lastColumnStartingPoint = 0; lastColumnStartingPoint < boardSize; lastColumnStartingPoint++)
            if (CheckIfWinningDirection(gameState, player, 0, lastColumnStartingPoint, 1, -1))
                return true;

        //No winning condition found
        return false;
    }

    private bool CheckIfWinningDirection(GameState gameState, int player, int startRow, int startColumn, int changeRows, int changeColumns)
    {
        var boardSize = gameState.BoardSize;
        var board = gameState.GetBoard();

        // Keep track of the number of winning moves in a row for the current direction
        var winningMovesInARow = 0;

        // Keep track of the current position on the board
        var row = startRow;
        var column = startColumn;

        //Assumes a square board
        while (row < boardSize && column < boardSize
            && row >= 0 && column >= 0)
        {
            if (board[row, column] == player)
                winningMovesInARow++;
            else
                winningMovesInARow = 0;

            row += changeRows;
            column += changeColumns;
        }

        return winningMovesInARow >= MovesInARowToWin;
    }

    internal static bool MoveIsValid(GameState gameState, Move move)
    {
        var board = gameState.GetBoard();

        if (board[move.Row, move.Column] != 0)
            return false;

        return true;
    }
}
