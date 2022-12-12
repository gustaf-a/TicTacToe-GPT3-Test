using TicTacToe.Model.Data;

namespace PerformanceTests.MoveMaker;

public class MoveMakerSmallBoardBenchMarkRunner : MoveMakerBenchmarkRunnerBase
{
    public MoveMakerSmallBoardBenchMarkRunner()
                : base(
            new GameState(3, new List<Player>()
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
            }))
    {
    }
}
