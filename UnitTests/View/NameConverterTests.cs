using TicTacToe.Model.Data;
using TicTacToe.View;
using Xunit;

namespace UnitTests.View;

public class NameConverterTests
{
    [Fact]
    public void GetMove_ShouldFindCorrectCoordinates_WhenValidButtonName()
    {
        //Arrange
        var buttonName = "Button_1_2";
        var expectedRow = 1;
        var expectedColumn = 2;

        //Act
        var move = NameConverter.GetMove(buttonName, 1);

        //Assert
        Assert.Equal(1, move.Player);
        Assert.Equal(expectedRow, move.Row);
        Assert.Equal(expectedColumn, move.Column);
    }

    [Fact]
    public void GetButtonName_ShouldReturnCorrectName_WhenValidMove()
    {
        //Arrange
        var expectedButtonName = "Button_2_3";

        var move = new Move()
        {
            Player = 1,
            Row = 2,
            Column = 3
        };

        //Act
        var name = NameConverter.GetButtonName(move);

        //Assert
        Assert.Equal(expectedButtonName, name);
    }
}
