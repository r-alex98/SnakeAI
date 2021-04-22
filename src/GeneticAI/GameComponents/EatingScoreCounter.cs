using SnakeGame;
using SnakeGame.Game;
using SnakeGame.GameComponents;

namespace GeneticGame.GameComponents
{
    public class EatingScoreCounter : IScoreCounter
    {
        private readonly float _scoreForEatingFood;
        
        public float TotalScore { get; private set; }

        public EatingScoreCounter(float scoreForEatingFood)
        {
            _scoreForEatingFood = scoreForEatingFood;
        }

        public void UpdateScore(GameState state)
        {
            TotalScore = state.Points * _scoreForEatingFood;
        }
    }
}
