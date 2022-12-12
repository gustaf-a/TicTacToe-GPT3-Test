using TicTacToe.Model.Data;

namespace PerformanceTests.MoveMaker;

public class MoveMakerBigBoardBenchMarkRunner : MoveMakerBenchmarkRunnerBase
{
    public MoveMakerBigBoardBenchMarkRunner()
        : base(
            new GameState(7, new List<Player>()
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
