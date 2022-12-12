using Xunit;
using TicTacToe.Model.Data;

namespace UnitTests.Model.Data;

public class GameStateTests
{
    private List<Player> _players;

    public GameStateTests()
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
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(99)]
    public void GameState_ShouldSetCorrectSize_WhenCreated(int boardSize)
    {
        var gameState = new GameState(boardSize, _players);

        Assert.Equal(boardSize, gameState.BoardSize);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    public void GameState_ShouldThrowException_WhenCreatedWithInvalidBoardSize(int boardSize)
    {
        Assert.Throws<Exception>(() => new GameState(boardSize, _players));
    }

    [Fact]
    public void GameState_ShouldToggleBetweenPlayers_WhenNextPlayerIsCalled()
    {
        var gameState = new GameState(3, _players);

        var firstPlayer = gameState.GetCurrentPlayer();
        gameState.NextPlayer();
        var secondPlayer = gameState.GetCurrentPlayer();
        gameState.NextPlayer();
        var firstPlayerAgain = gameState.GetCurrentPlayer();

        Assert.Equal(firstPlayer.PlayerNumber + 1, secondPlayer.PlayerNumber);
        Assert.Equal(firstPlayer.PlayerNumber, firstPlayerAgain.PlayerNumber);
    }

    [Fact]
    public void GameState_ShouldToggleBetweenPlayers_WhenPreviousPlayerIsCalled()
    {
        _players.Add(new Player
        {
            IsHuman = true,
            PlayerNumber = 3,
            PlayerSymbol = "M"
        });

        var gameState = new GameState(3, _players);

        var firstPlayer = gameState.GetCurrentPlayer();
        gameState.PreviousPlayer();
        var thirdPlayer = gameState.GetCurrentPlayer();
        gameState.PreviousPlayer();
        var secondPlayer = gameState.GetCurrentPlayer();
        gameState.PreviousPlayer();
        var firstPlayerAgain = gameState.GetCurrentPlayer();

        Assert.Equal(firstPlayer.PlayerNumber + 1, secondPlayer.PlayerNumber);
        Assert.Equal(firstPlayer.PlayerNumber + 2, thirdPlayer.PlayerNumber);
        Assert.Equal(firstPlayer.PlayerNumber, firstPlayerAgain.PlayerNumber);
    }
}
