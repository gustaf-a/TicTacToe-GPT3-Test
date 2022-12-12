namespace TicTacToe.Model.Data;

public class Player
{
    public int PlayerNumber;
    public string PlayerSymbol;
    public int Score;

    public bool IsHuman;

    public override string ToString()
    {
        return $"Player {PlayerNumber}, {PlayerSymbol}: {Score}";
    }
}
