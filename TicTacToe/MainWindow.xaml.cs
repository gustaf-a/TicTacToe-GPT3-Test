using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Model.Data;
using TicTacToe.Model.GameLogic;
using TicTacToe.Model.MoveMaker;
using TicTacToe.Model.PlayerFactory;
using TicTacToe.View;

namespace TicTacToe;

public partial class MainWindow : Window
{
    private readonly IPlayerFactory _playerFactory;
    private readonly IMoveMaker _moveMaker;
    private readonly GameLogic _gameLogic;

    private bool _waitingForComputerMove = false;
    private bool _gameIsOver = false;

    private const int BoardSize = 3;
    private const int NumberOfPlayers = 2;

    private GameState _gameState;
    private List<Player> _players;

    private Button[,] _boardButtons;

    public MainWindow()
    {
        InitializeComponent();

        InitializeBoard(BoardSize);

        _playerFactory = new PlayerFactory();

        _gameLogic = new GameLogic(3);

        _moveMaker = new RandomMoveMaker();

        NewGame();
    }

    private void InitializeBoard(int boardSize)
    {
        // Define the spBoard stack panel in the C# code
        StackPanel spBoard = new StackPanel();
        spBoard.Orientation = Orientation.Vertical;
        spBoard.Margin = new Thickness(0);

        // Add the spBoard stack panel to the grid
        Grid.SetRow(spBoard, 1);
        Grid.SetColumn(spBoard, 0);

        grdMain.Children.Add(spBoard);

        _boardButtons = new Button[boardSize, boardSize];

        for (int row = 0; row < boardSize; row++)
        {
            StackPanel rowStackPanel = new StackPanel();
            rowStackPanel.Orientation = Orientation.Horizontal;

            for (int col = 0; col < boardSize; col++)
            {
                Button button = new Button();

                button.Style = (Style)Resources["BoardButtonStyle"];
                button.Click += Button_Click;
                button.Name = $"Button_{row}_{col}";

                _boardButtons[row, col] = button;
                rowStackPanel.Children.Add(button);
            }

            spBoard.Children.Add(rowStackPanel);
        }
    }

    private void NewGame()
    {
        if(_players is null)
            _players = _playerFactory.CreatePlayers(NumberOfPlayers);

        _gameState = new GameState(BoardSize, _players);

        _gameIsOver = false;

        UpdateBoard(_gameState);
    }

    private void NewGameButton_Click(object sender, RoutedEventArgs e)
    {
        if (_waitingForComputerMove)
            return;

        NewGame();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (_waitingForComputerMove)
            return;

        if (_gameIsOver)
        {
            NewGame();
            return;
        }

        var currentPlayer = _gameState.GetCurrentPlayer();

        if (!currentPlayer.IsHuman)
            return;

        Button button = (Button)sender;
        var move = NameConverter.GetMove(button.Name, currentPlayer.PlayerNumber);

        if (!GameLogic.MoveIsValid(_gameState, move))
            return;

        MoveMade(move);
    }

    private void MoveMade(Move move)
    {
        _gameState.ApplyMove(move);

        UpdateBoard(_gameState);

        if (_gameLogic.PlayerHasWon(_gameState))
        {
            _gameIsOver = true;
            MessageBox.Show($"Player {move.Player} has won!");

            AddToPlayerScore(move.Player);

            return;
        }

        if (!_gameState.IsMoreMovesPossible())
        {
            _gameIsOver = true;
            MessageBox.Show($"Draw!");
            return;
        }

        _gameState.NextPlayer();

        if (!_gameState.GetCurrentPlayer().IsHuman)
            MakeComputerControlledMove();
    }

    private void AddToPlayerScore(int playerNumber)
    {
        var player = _players[playerNumber - 1];
        player.Score++;

        if (FindName($"txtPlayer{playerNumber}") is not TextBox scoreBoard)
            throw new Exception($"Couldn't find TextBox named txtPlayer{playerNumber}");

        scoreBoard.Text = player.ToString();
    }

    private void MakeComputerControlledMove()
    {
        _waitingForComputerMove = true;

        var move = _moveMaker.GetNextMove(_gameState);

        MoveMade(move);

        _waitingForComputerMove = false;
    }

    private void UpdateBoard(GameState gameState)
    {
        var boardSize = gameState.BoardSize;
        var board = gameState.GetBoard();

        for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
            {
                var button = _boardButtons[i, j];

                if (board[i, j] == 0)
                    button.Content = "";
                else
                    button.Content = _players[board[i, j] - 1].PlayerSymbol;
            }
    }
}
