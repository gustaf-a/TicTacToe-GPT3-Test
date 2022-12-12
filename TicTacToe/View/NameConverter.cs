using TicTacToe.Model.Data;

namespace TicTacToe.View;

public static class NameConverter
{
    public static Move GetMove(string name)
    {
        var splitName = name.Split("_");

        return new Move
        {
            Row = int.Parse(splitName[1]),
            Column = int.Parse(splitName[2]),
        };
    }

    public static string GetButtonName(Move move)
    {
        return GetButtonName(move.Row, move.Column);
    }

    public static string GetButtonName(int row, int column)
    {
        return $"Button_{row}_{column}";
    }
}
