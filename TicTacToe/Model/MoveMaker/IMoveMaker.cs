using TicTacToe.Model.Data;

namespace TicTacToe.Model.MoveMaker;

public interface IMoveMaker
{
    public Move GetNextMove(GameState gameState);
}
