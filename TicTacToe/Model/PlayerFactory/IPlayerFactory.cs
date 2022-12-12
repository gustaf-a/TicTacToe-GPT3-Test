using System;
using System.Collections.Generic;
using TicTacToe.Model.Data;

namespace TicTacToe.Model.PlayerFactory;

public interface IPlayerFactory
{
    public List<Player> CreatePlayers(int numberOfPlayers);
}
