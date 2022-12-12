using System;
using System.Collections.Generic;
using System.Data.Common;
using TicTacToe.Model.Data;

namespace TicTacToe.Model.MoveMaker;

public class WinningMoveMaker : IMoveMaker
{
    private readonly GameLogic.GameLogic _gameLogic;
    private readonly IMoveMaker _fallBackMoveMaker;

    private const int NoMoveMadeNumber = 0;

    public WinningMoveMaker(GameLogic.GameLogic gameLogic, IMoveMaker fallBackMoveMaker)
    {
        _gameLogic = gameLogic;
        _fallBackMoveMaker = fallBackMoveMaker;
    }

    /// <summary>
    /// Finds the next winning move or a random move.
    /// Only works with 2 players.
    /// </summary>
    public Move GetNextMove(GameState gameState)
    {
        //TODO check number of moves made

        var playerNumber = gameState.GetCurrentPlayer().PlayerNumber;

        var possibleMoves = GetPossibleMoves(gameState);

        Move move = null;

        if (WinningMoveFound(gameState, possibleMoves, out move))
            return move;

        if (BlockingOpponentWinningMoveFound(gameState, possibleMoves, out move))
            return move;

        //if (ForkMoveFound(gameState, playerNumber, out move))
        //    return move;

        //if (BlockOpponentForkMoveFound(gameState, playerNumber, out move))
        //    return move;

        return _fallBackMoveMaker.GetNextMove(gameState);
    }

    /// <summary>
    /// Finds all possible moves.
    /// </summary>
    private static List<Move> GetPossibleMoves(GameState gameState)
    {
        var possibleMoves = new List<Move>();
        var board = gameState.GetBoard();

        for (int row = 0; row < gameState.BoardSize; row++)
            for (int column = 0; column < gameState.BoardSize; column++)
                if (board[row, column] == NoMoveMadeNumber)
                    possibleMoves.Add(new Move { Row = row, Column = column});

        return possibleMoves;
    }

    /// <summary>
    /// Returns true and returns the move if there's a move immediately winning the game 
    /// </summary>
    private bool WinningMoveFound(GameState gameState, List<Move> possibleMoves, out Move move)
    {
        move = new Move();

        foreach (var possibleMove in possibleMoves)
        {
            gameState.ApplyMove(possibleMove);

            var isWinningMove = _gameLogic.PlayerHasWon(gameState);

            gameState.RemoveMove(possibleMove);

            if (isWinningMove)
            {
                move = possibleMove;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns true and returns the move if the opponent can immediately win the game by playing a move
    /// </summary>
    private bool BlockingOpponentWinningMoveFound(GameState gameState, List<Move> possibleMoves, out Move move)
    {
        gameState.NextPlayer();

        var result = WinningMoveFound(gameState, possibleMoves, out move);

        gameState.PreviousPlayer();

        return result;

        //var nextPlayer = gameState.GetCurrentPlayer().PlayerNumber;

        //foreach (var possibleMove in possibleMoves)
        //{
        //    gameState.ApplyMove(possibleMove, nextPlayer);

        //    var isWinningMove = _gameLogic.PlayerHasWon(gameState);

        //    gameState.RemoveMove(possibleMove);

        //    if (isWinningMove)
        //    {
        //        move = possibleMove;
        //        break;
        //    }
        //}

        //gameState.PreviousPlayer();

        //return move != null;
    }

    /// <summary>
    /// Returns true and returns the move if a move can produce a fork, that is a move that will create two winning moves
    /// </summary>
    private bool ForkMoveFound(GameState gameState, int playerNumber, out Move move)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns true and returns the move if a move can block the opponent from producing a fork, that is a move that will create two winning moves
    /// </summary>
    private bool BlockOpponentForkMoveFound(GameState gameState, int playerNumber, out Move move)
    {
        throw new NotImplementedException();
    }
}
