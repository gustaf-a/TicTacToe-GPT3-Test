using BenchmarkDotNet.Attributes;
using TicTacToe.Model.Data;
using TicTacToe.Model.GameLogic;
using TicTacToe.Model.MoveMaker;

namespace PerformanceTests.MoveMaker;

public abstract class MoveMakerBenchmarkRunnerBase
{
    private readonly IMoveMaker _randomMoveMaker;
    private readonly IMoveMaker _winningMoveMaker;

    private readonly GameState _gameState;

    public MoveMakerBenchmarkRunnerBase(GameState gameState)
    {
        _gameState = gameState;

        var gameLogic = new GameLogic(3);

        _randomMoveMaker = new RandomMoveMaker();

        _winningMoveMaker = new WinningMoveMaker(gameLogic, _randomMoveMaker);
    }

    [Benchmark]
    public void RandomMoveMaker()
    {
        var move = _randomMoveMaker.GetNextMove(_gameState);
    }

    [Benchmark]
    public void WinningMoveMaker()
    {
        var move = _winningMoveMaker.GetNextMove(_gameState);
    }
}
