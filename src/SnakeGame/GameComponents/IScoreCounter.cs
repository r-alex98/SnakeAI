using SnakeGame.Game;

namespace SnakeGame.GameComponents
{
    public interface IScoreCounter
    {
        float TotalScore { get; }
        void UpdateScore(GameState state);
    }
}