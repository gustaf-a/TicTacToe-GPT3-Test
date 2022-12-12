using TicTacToe.Model.Data;
using TicTacToe.Model.MoveMaker;
using Xunit;

namespace UnitTests.Model.MoveMaker;

public class RandomMoveMakerTests
{
    private List<Player> _players;

    public RandomMoveMakerTests()
    {
        _players = new List<Player>()
        {
            new Player
            {
                IsHuman = true,
                PlayerNumber = 1,
                PlayerSymbol = "X"
            },
            new Player
            {
                IsHuman = true,
                PlayerNumber = 2,
                PlayerSymbol = "O"
            }
        };

    }

    [Fact]
    public void GetNextMove_ShouldReturnNull_WhenFullBoard()
    {
        //Arrange
        var boardSize = 3;
        var gameState = new GameState(boardSize, _players);

        for (int row = 0; row < boardSize; row++)
            for (int column = 0; column < boardSize; column++)
                gameState.ApplyMove(new Move { Row = row, Column = column, Player = 1 });

        var moveMaker = new RandomMoveMaker();

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        Assert.Null(move);
    }

    [Fact]
    public void GetNextMove_ShouldReturnOnlyMovePossible_WhenAlmostFullBoard()
    {
        //Arrange
        var boardSize = 3;
        var gameState = new GameState(boardSize, _players);

        for (int row = 0; row < boardSize; row++)
            for (int column = 0; column < boardSize; column++)
                gameState.ApplyMove(new Move { Row = row, Column = column, Player = 1 });

        gameState.RemoveMove(new Move { Row = 2, Column = 2, Player = 1 });

        var moveMaker = new RandomMoveMaker();

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        Assert.Equal(1, move.Player);
        Assert.Equal(2, move.Row);
        Assert.Equal(2, move.Column);
    }
}
