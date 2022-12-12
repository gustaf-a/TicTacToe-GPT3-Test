using System.Collections.Generic;
using System.Linq;
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
    /// Finds the next winning move or a uses the fallback MoveMaker.
    /// Only works with 2 players.
    /// </summary>
    public Move GetNextMove(GameState gameState)
    {
        var playerNumber = gameState.GetCurrentPlayer().PlayerNumber;

        var possibleMoves = GetPossibleMoves(gameState);

        Move move = null;

        if (WinningMoveFound(gameState, possibleMoves, out move))
            return move;

        if (BlockingOpponentWinningMoveFound(gameState, possibleMoves, out move))
            return move;

        if (ForkMoveFound(gameState, possibleMoves, out move))
            return move;

        if (BlockOpponentForkMoveFound(gameState, possibleMoves, out move))
            return move;

        if (ForcingMoveFound(gameState, possibleMoves, out move))
            return move;

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
                    possibleMoves.Add(new Move { Row = row, Column = column });

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
    }

    /// <summary>
    /// Returns true and returns the move if a move can produce a fork, that is a move that will create two winning moves
    /// </summary>
    private bool ForkMoveFound(GameState gameState, List<Move> possibleMoves, out Move move)
    {
        move = new Move();

        foreach (var possibleMove in possibleMoves)
        {
            gameState.ApplyMove(possibleMove);

            var possibleFollowUpMoves = possibleMoves.Except(new List<Move> { possibleMove }).ToList();
            if (!WinningMoveFound(gameState, possibleFollowUpMoves, out Move winningFollowUpMove))
            {
                gameState.RemoveMove(possibleMove);
                continue;
            }

            possibleFollowUpMoves.Remove(winningFollowUpMove);
            if (!WinningMoveFound(gameState, possibleFollowUpMoves, out Move winningFollowUpMoveTwo))
            {
                gameState.RemoveMove(possibleMove);
                continue;
            }

            gameState.RemoveMove(possibleMove);

            move = possibleMove;

            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns true and returns the move if a move can block the opponent from producing a fork, that is a move that will create two winning moves
    /// </summary>
    private bool BlockOpponentForkMoveFound(GameState gameState, List<Move> possibleMoves, out Move move)
    {
        gameState.NextPlayer();

        var result = ForkMoveFound(gameState, possibleMoves, out move);

        gameState.PreviousPlayer();

        return result;
    }

    /// <summary>
    /// Returns true and returns the move if a move can setup a situation where the next move can win the game
    /// </summary>
    private bool ForcingMoveFound(GameState gameState, List<Move> possibleMoves, out Move move)
    {
        move = new Move();

        foreach (var possibleMove in possibleMoves)
        {
            gameState.ApplyMove(possibleMove);

            var possibleFollowUpMoves = possibleMoves.Except(new List<Move> { possibleMove }).ToList();
            if (!WinningMoveFound(gameState, possibleFollowUpMoves, out Move winningFollowUpMove))
            {
                gameState.RemoveMove(possibleMove);
                continue;
            }

            gameState.RemoveMove(possibleMove);

            move = possibleMove;

            return true;
        }

        return false;
    }
}
