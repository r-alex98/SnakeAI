using SnakeGame;
using SnakeGame.Game;
using SnakeGame.GameComponents;
using SnakeGame.Math2D;

namespace GeneticGame.GameComponents
{
    public class DistanceToFoodScoreCounter : IScoreCounter
    {
        private readonly float _scoreForCorrectWay;
        private readonly float _scoreForWrongWay;
        
        private float _prevDistanceToFood;
        
        public float TotalScore { get; private set; }

        public DistanceToFoodScoreCounter(float scoreForCorrectWay, float scoreForWrongWay)
        {
            _scoreForCorrectWay = scoreForCorrectWay;
            _scoreForWrongWay = scoreForWrongWay;
        }

        public void UpdateScore(GameState state)
        {
            var distanceToFood = Vector2D.CalcDistance(state.FoodPosition, state.HeadPosition);
            var scoreForDistance = distanceToFood > _prevDistanceToFood ? _scoreForWrongWay : _scoreForCorrectWay;
            TotalScore += scoreForDistance;
            _prevDistanceToFood = distanceToFood;
        }
    }
}
