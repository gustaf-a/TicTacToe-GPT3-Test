using TicTacToe.Model.Data;
using TicTacToe.Model.MoveMaker;
using Xunit;

namespace UnitTests.Model.MoveMaker;

public class WinningMoveMakerTests
{
    private List<Player> _players;

    public WinningMoveMakerTests()
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
                gameState.ApplyMove(new Move { Row = row, Column = column });

        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var fallbackMoveMaker = new RandomMoveMaker();
        var moveMaker = new WinningMoveMaker(gameLogic, fallbackMoveMaker);

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
                gameState.ApplyMove(new Move { Row = row, Column = column });

        gameState.RemoveMove(new Move { Row = 2, Column = 2 });

        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var fallbackMoveMaker = new RandomMoveMaker();
        var moveMaker = new WinningMoveMaker(gameLogic, fallbackMoveMaker);

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        Assert.Equal(2, move.Row);
        Assert.Equal(2, move.Column);
    }

    [Fact]
    public void GetNextMove_ShouldReturnWinningMove_WhenPossible()
    {
        //Arrange
        var boardSize = 3;
        var gameState = new GameState(boardSize, _players);

        gameState.ApplyMove(new Move { Row = 0, Column = 0 });
        gameState.ApplyMove(new Move { Row = 0, Column = 1 });

        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var fallbackMoveMaker = new RandomMoveMaker();
        var moveMaker = new WinningMoveMaker(gameLogic, fallbackMoveMaker);

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        Assert.Equal(0, move.Row);
        Assert.Equal(2, move.Column);
    }

    [Fact]
    public void GetNextMove_ShouldApplyMoveToGameState_BeforeReturning()
    {
        //Arrange
        var boardSize = 3;
        var gameState = new GameState(boardSize, _players);

        gameState.ApplyMove(new Move { Row = 0, Column = 0 });
        gameState.ApplyMove(new Move { Row = 0, Column = 1 });

        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var fallbackMoveMaker = new RandomMoveMaker();
        var moveMaker = new WinningMoveMaker(gameLogic, fallbackMoveMaker);

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        var board = gameState.GetBoard();

        Assert.Equal(0, board[move.Row, move.Column]);
    }

    [Fact]
    public void GetNextMove_ShouldReturnBlockingWinningMove_WhenNotPossibleToWinButPossibleToBlock()
    {
        //Arrange
        var boardSize = 3;
        var gameState = new GameState(boardSize, _players);

        gameState.ApplyMove(new Move { Row = 0, Column = 0 });
        gameState.ApplyMove(new Move { Row = 1, Column = 1 });

        gameState.NextPlayer();

        gameState.ApplyMove(new Move { Row = 0, Column = 1 });

        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(3);
        var fallbackMoveMaker = new RandomMoveMaker();
        var moveMaker = new WinningMoveMaker(gameLogic, fallbackMoveMaker);

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        Assert.Equal(2, move.Row);
        Assert.Equal(2, move.Column);
    }

    [Fact]
    public void GetNextMove_ShouldReturnForkMove_WhenPossible()
    {
        //Arrange
        var boardSize = 5;
        var gameState = new GameState(boardSize, _players);

        gameState.ApplyMove(new Move { Row = 1, Column = 1 });
        gameState.ApplyMove(new Move { Row = 1, Column = 2 });

        gameState.NextPlayer();

        gameState.ApplyMove(new Move { Row = 4, Column = 1 });
        gameState.ApplyMove(new Move { Row = 4, Column = 2 });

        gameState.NextPlayer();

        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(4);
        var fallbackMoveMaker = new RandomMoveMaker();
        var moveMaker = new WinningMoveMaker(gameLogic, fallbackMoveMaker);

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        Assert.Equal(1, move.Row);
        Assert.Equal(3, move.Column);
    }

    [Fact]
    public void GetNextMove_ShouldReturnBlockingForkMove_WhenPossible()
    {
        //Arrange
        var boardSize = 5;
        var gameState = new GameState(boardSize, _players);

        gameState.ApplyMove(new Move { Row = 1, Column = 1 });
        gameState.ApplyMove(new Move { Row = 3, Column = 2 });

        gameState.NextPlayer();

        gameState.ApplyMove(new Move { Row = 4, Column = 1 });
        gameState.ApplyMove(new Move { Row = 4, Column = 2 });

        gameState.NextPlayer();

        var gameLogic = new TicTacToe.Model.GameLogic.GameLogic(4);
        var fallbackMoveMaker = new RandomMoveMaker();
        var moveMaker = new WinningMoveMaker(gameLogic, fallbackMoveMaker);

        //Act
        var move = moveMaker.GetNextMove(gameState);

        //Assert
        Assert.Equal(4, move.Row);
        Assert.Equal(3, move.Column);
    }
}
