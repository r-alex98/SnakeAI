using SnakeGame.Game;

namespace SnakeGame.GameOverConditions
{
    public interface IGameOverCondition
    {
        bool IsGameOver(GameState state);
    }
}
