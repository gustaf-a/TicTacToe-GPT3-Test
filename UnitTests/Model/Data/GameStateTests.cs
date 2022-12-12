using Xunit;
using TicTacToe.Model.Data;

namespace UnitTests.Model.Data;

public class GameStateTests
{
    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(99)]
    public void GameState_ShouldSetCorrectSize_WhenCreated(int boardSize)
    {
        var gameState = new GameState(boardSize);

        Assert.Equal(boardSize, gameState.BoardSize);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(-1)]
    public void GameState_ShouldThrowException_WhenCreatedWithInvalidBoardSize(int boardSize)
    {
        var gameState = new GameState(boardSize);

        Assert.Equal(boardSize, gameState.BoardSize);
    }
}
