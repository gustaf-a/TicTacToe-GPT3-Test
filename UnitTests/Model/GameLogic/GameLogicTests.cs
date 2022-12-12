using TicTacToe.Model.Data;
using Xunit;

namespace UnitTests.Model.GameLogic;

public class GameLogicTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void PlayerHasWon_ShouldReturnTrue_WhenPlayerHasWon_AcrossRows(int row)
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var player = 1;
        
        var gameState = new GameState(3);

        for (int i = 0; i < 3; i++)
            gameState.ApplyMove(new Move
            {
                Column = i,
                Row = row,
                Player = player
            });

        //Act
        var result = gameLogic.PlayerHasWon(gameState, player);

        //Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void PlayerHasWon_ShouldReturnTrue_WhenPlayerHasWon_AcrossColumns(int column)
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var player = 1;

        var gameState = new GameState(3);

        for (int i = 0; i < 3; i++)
            gameState.ApplyMove(new Move
            {
                Column = column,
                Row = i,
                Player = player
            });

        //Act
        var result = gameLogic.PlayerHasWon(gameState, player);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void PlayerHasWon_ShouldReturnTrue_WhenPlayerHasWon_Diagonally_LeftToRight_Down()
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var player = 1;

        var gameState = new GameState(3);

        for (int i = 0; i < 3; i++)
            gameState.ApplyMove(new Move
            {
                Column = i,
                Row = i,
                Player = player
            });

        //Act
        var result = gameLogic.PlayerHasWon(gameState, player);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void PlayerHasWon_ShouldReturnTrue_WhenPlayerHasWon_Diagonally_LeftToRight_Up()
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var player = 1;

        var gameState = new GameState(3);

        for (int i = 0; i < 3; i++)
            gameState.ApplyMove(new Move
            {
                Column = i,
                Row = 2-i,
                Player = player
            });

        //Act
        var result = gameLogic.PlayerHasWon(gameState, player);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void PlayerHasWon_ShouldReturnFalse_WhenPlayerHasNotWon()
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var player = 1;

        var gameState = new GameState(3);

        gameState.ApplyMove(new Move
        {
            Column = 0,
            Row = 0,
            Player = player
        });

        gameState.ApplyMove(new Move
        {
            Column = 0,
            Row = 1,
            Player = player
        });

        gameState.ApplyMove(new Move
        {
            Column = 1,
            Row = 1,
            Player = player
        });

        gameState.ApplyMove(new Move
        {
            Column = 2,
            Row = 0,
            Player = player
        });

        gameState.ApplyMove(new Move
        {
            Column = 1,
            Row = 2,
            Player = player
        });

        //Act
        var result = gameLogic.PlayerHasWon(gameState, player);

        //Assert
        Assert.False(result);
    }
}
