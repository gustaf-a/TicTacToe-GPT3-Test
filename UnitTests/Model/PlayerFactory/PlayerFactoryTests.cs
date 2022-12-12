using Xunit;

namespace UnitTests.Model.PlayerFactory;

public class PlayerFactoryTests
{
    [Fact]
    public void TestCreatePlayers_ShouldCreateCorrectNumberOfPlayers()
    {
        // Arrange
        var numberOfPlayers = 2;
        var playerFactory = new TicTacToe.Model.PlayerFactory.PlayerFactory();

        // Act
        var players = playerFactory.CreatePlayers(numberOfPlayers);

        // Assert
        Assert.Equal(numberOfPlayers, players.Count);
    }

    [Fact]
    public void TestCreatePlayers_ShouldCreateCorrectPlayerNumbers()
    {
        // Arrange
        var numberOfPlayers = 2;
        var playerFactory = new TicTacToe.Model.PlayerFactory.PlayerFactory();

        // Act
        var players = playerFactory.CreatePlayers(numberOfPlayers);

        // Assert
        Assert.Equal(1, players[0].PlayerNumber);
        Assert.Equal(2, players[1].PlayerNumber);
    }

    [Fact]
    public void TestCreatePlayers_ShouldCreateHumanPlayer()
    {
        // Arrange
        var numberOfPlayers = 2;
        var playerFactory = new TicTacToe.Model.PlayerFactory.PlayerFactory();

        // Act
        var players = playerFactory.CreatePlayers(numberOfPlayers);

        // Assert
        var humanPlayer = players.First(player => player.IsHuman);
        Assert.NotNull(humanPlayer);
    }

    [Fact]
    public void TestCreatePlayers_ShouldCreateCorrectPlayerSymbols()
    {
        // Arrange
        var numberOfPlayers = 5;
        var playerFactory = new TicTacToe.Model.PlayerFactory.PlayerFactory();
        var expectedSymbols = new string[] { "X", "O" };

        // Act
        var players = playerFactory.CreatePlayers(numberOfPlayers);

        int i;

        Assert.Equal(numberOfPlayers, players.Count);

        // Assert
        for (i = 0; i < expectedSymbols.Length; i++)
            Assert.Equal(expectedSymbols[i], players[i].PlayerSymbol);

        for (int j = i; j < players.Count; j++)
        {
            var symbol = players[j].PlayerSymbol;

            Assert.False(string.IsNullOrWhiteSpace(symbol));
        };
    }

    [Fact]
    public void TestCreatePlayers_ShouldThrowExceptionForInvalidNumberOfPlayers()
    {
        // Arrange
        var numberOfPlayers = 1;
        var playerFactory = new TicTacToe.Model.PlayerFactory.PlayerFactory();

        // Act & Assert
        Assert.Throws<Exception>(() => playerFactory.CreatePlayers(numberOfPlayers));
    }
}

