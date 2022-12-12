using TicTacToe.Model.Data;
using Xunit;

namespace UnitTests.Model.GameLogic;

public class GameLogicTests
{
    private readonly List<Player> _players;

    public GameLogicTests()
    {
        _players = new()
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

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    [InlineData(5, 4)]
    [InlineData(7, 5)]
    [InlineData(9, 5)]
    public void PlayerHasWon_ShouldReturnTrue_WhenDifferentBoardSizes_AcrossRows(int boardSize, int movesInRowToWin)
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(movesInRowToWin);

        var gameState = new GameState(boardSize, _players);

        for (int row = 0; row < boardSize; row++)
        {
            for (int i = 0; i < movesInRowToWin; i++)
                gameState.ApplyMove(new Move
                {
                    Column = i,
                    Row = row
                });

            //Act
            var result = gameLogic.PlayerHasWon(gameState);

            //Assert
            Assert.True(result);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void PlayerHasWon_ShouldReturnTrue_WhenPlayerHasWon_AcrossColumns(int column)
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        
        var gameState = new GameState(3, _players);

        for (int i = 0; i < 3; i++)
            gameState.ApplyMove(new Move
            {
                Column = column,
                Row = i
            });

        //Act
        var result = gameLogic.PlayerHasWon(gameState);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void PlayerHasWon_ShouldReturnTrue_WhenPlayerHasWon_Diagonally_LeftToRight_Down()
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);

        var gameState = new GameState(3, _players);

        for (int i = 0; i < 3; i++)
            gameState.ApplyMove(new Move
            {
                Column = i,
                Row = i
            });

        //Act
        var result = gameLogic.PlayerHasWon(gameState);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void PlayerHasWon_ShouldReturnTrue_WhenPlayerHasWon_Diagonally_LeftToRight_Up()
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);

        var gameState = new GameState(3, _players);

        for (int i = 0; i < 3; i++)
            gameState.ApplyMove(new Move
            {
                Column = i,
                Row = 2-i
            });

        //Act
        var result = gameLogic.PlayerHasWon(gameState);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void PlayerHasWon_ShouldReturnFalse_WhenPlayerHasNotWon()
    {
        //Arrange
        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);

        var gameState = new GameState(3, _players);

        gameState.ApplyMove(new Move
        {
            Column = 0,
            Row = 0
        });

        gameState.ApplyMove(new Move
        {
            Column = 0,
            Row = 1
        });

        gameState.ApplyMove(new Move
        {
            Column = 1,
            Row = 1
        });

        gameState.ApplyMove(new Move
        {
            Column = 2,
            Row = 0
        });

        gameState.ApplyMove(new Move
        {
            Column = 1,
            Row = 2
        });

        //Act
        var result = gameLogic.PlayerHasWon(gameState);

        //Assert
        Assert.False(result);
    }
}
