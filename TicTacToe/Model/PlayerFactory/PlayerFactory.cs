using System;
using System.Collections.Generic;
using TicTacToe.Model.Data;

namespace TicTacToe.Model.PlayerFactory;

public class PlayerFactory : IPlayerFactory
{
    private readonly string[] _playerSymbols = { "X", "O" };

    public List<Player> CreatePlayers(int numberOfPlayers)
    {
        if (numberOfPlayers < 2)
            throw new Exception("Invalid number of players. Must be at least 2 players.");

        var playerList = new List<Player>();

        for (var i = 0; i < numberOfPlayers; i++)
            playerList.Add(new Player
            {
                IsHuman = i == 0,
                PlayerNumber = i + 1,
                PlayerSymbol = GetPlayerSymbol(i),
                Score = 0
            });

        return playerList;
    }

    //TODO Ensure unique symbols
    private string GetPlayerSymbol(int playerNumber)
    {
        if (playerNumber < _playerSymbols.Length)
            return _playerSymbols[playerNumber];

        // Generate a random upper case letter
        var rnd = new Random();
        return ((char)(rnd.Next(26) + 'A')).ToString();
    }
}
