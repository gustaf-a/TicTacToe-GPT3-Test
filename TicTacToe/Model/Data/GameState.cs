namespace TicTacToe.Model.Data;

public class GameState
{
    private readonly int[,] _board;

    private int _movesMade;
    private int _maxMovesToMake;

    public int BoardSize { get; private set; }

    public GameState(int boardSize)
    {
        BoardSize = boardSize;

        _board = new int[boardSize, boardSize];

        _movesMade = 0;
        _maxMovesToMake = BoardSize * BoardSize;
    }

    public void ApplyMove(Move move)
    {
        _board[move.Row, move.Column] = move.Player;

        _movesMade++;
    }

    public int[,] GetBoard()
           => _board;

    public bool IsMoreMovesPossible()
    {
        return _movesMade < _maxMovesToMake;
    }

    public override string ToString()
    {
        // Return a string representation of the game board
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
