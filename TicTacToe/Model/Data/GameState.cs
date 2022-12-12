using System;
using System.Collections.Generic;

namespace TicTacToe.Model.Data;

public class GameState
{
    private const int NoMoveMadeNumber = 0;

    private readonly int[,] _board;

    private int _movesMade;
    private readonly int _maxMovesToMake;

    public int BoardSize { get; private set; }

    private int _currentPlayer;
    private List<Player> _players;

    public GameState(int boardSize, List<Player> players)
    {
        if (boardSize < 2)
            throw new Exception("Invalid board size");

        BoardSize = boardSize;
        _board = new int[boardSize, boardSize];

        _movesMade = 0;
        _maxMovesToMake = BoardSize * BoardSize;

        _players = players;
        _currentPlayer = 0;
    }

    public void ApplyMove(Move move)
    {
        _board[move.Row, move.Column] = GetCurrentPlayer().PlayerNumber;

        _movesMade++;
    }

    public void RemoveMove(Move move)
    {
        _board[move.Row, move.Column] = NoMoveMadeNumber;

        _movesMade--;
    }

    public Player GetCurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    public void NextPlayer()
    {
        _currentPlayer++;

        if (_currentPlayer >= _players.Count)
            _currentPlayer = 0;
    }

    public void PreviousPlayer()
    {
        _currentPlayer--;

        if (_currentPlayer < 0)
            _currentPlayer = _players.Count - 1;
    }

    public int[,] GetBoard()
           => _board;

    public bool IsMoreMovesPossible()
    {
        return _movesMade < _maxMovesToMake;
    }

    public override string ToString()
    {
        var boardString = "";

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                boardString += $"{_board[row, col]}";
                if (col < BoardSize - 1)
                {
                    boardString += "|";
                }
            }

            boardString += "\n";
        }

        return boardString;
    }
}
