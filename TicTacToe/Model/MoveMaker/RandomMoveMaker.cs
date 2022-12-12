using System;
using System.Collections.Generic;
using TicTacToe.Model.Data;

namespace TicTacToe.Model.MoveMaker;

public class RandomMoveMaker : IMoveMaker
{
    private readonly Random _random;

    public RandomMoveMaker()
    {
        _random = new Random();
    }

    public Move GetNextMove(GameState gameState)
    {
        var possibleMoves = new List<Move>();

        var board = gameState.GetBoard();
        var playerNumber = gameState.GetCurrentPlayer().PlayerNumber;

        for (int row = 0; row < board.GetLength(0); row++)
            for (int col = 0; col < board.GetLength(1); col++)
                if (board[row, col] == 0)
                    possibleMoves.Add(new Move { Column = col, Row = row, Player = playerNumber});

        if (possibleMoves.Count == 0)
            return null;

        return possibleMoves[_random.Next(possibleMoves.Count)];
    }
}
