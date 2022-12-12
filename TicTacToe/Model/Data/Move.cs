using System;

namespace TicTacToe.Model.Data;

public class Move : IEquatable<Move>
{
    public int Row;
    public int Column;

    public bool Equals(Move? other)
    {
        if (other == null)
            return false;

        return this.Row == other.Row
            && this.Column == other.Column;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Move);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 7;

            hash *= 13 + this.Row;
            hash *= 13 + this.Column;

            return hash;
        }
    }
}
