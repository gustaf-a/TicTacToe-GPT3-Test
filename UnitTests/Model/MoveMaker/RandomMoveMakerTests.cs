using TicTacToe.Model.MoveMaker;
using Xunit;

namespace UnitTests.Model.MoveMaker;

public class RandomMoveMakerTests
{
    [Fact]
    public void GetNextMove_ShouldReturnNull_WhenFullBoard()
    {
        //Arrange
        int[,] board = { { 1, 2, 1 }, { 2, 2, 1 }, { 1, 2, 2 } };

        var moveMaker = new RandomMoveMaker();

        //Act
        var move = moveMaker.GetNextMove(board, 2);

        //Assert
        Assert.Null(move);
    }

    [Fact]
    public void GetNextMove_ShouldReturnOnlyMovePossible_WhenAlmostFullBoard()
    {
        //Arrange
        int[,] board = { { 1, 2, 1 }, { 2, 2, 0 }, {1, 2, 2 } };

        var moveMaker = new RandomMoveMaker();

        //Act
        var move = moveMaker.GetNextMove(board, 1);

        //Assert
        Assert.Equal(1, move.Player);
        Assert.Equal(1, move.Row);
        Assert.Equal(2, move.Column);
    }
}
