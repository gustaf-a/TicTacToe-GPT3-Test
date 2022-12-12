using TicTacToe.Model.Data;

namespace TicTacToe.Model.MoveMaker;

public interface IMoveMaker
{
    public Move GetNextMove(int[,] board, int playerNumber);
}
