using SnakeGame;
using SnakeGame.Game;
using SnakeGame.GameComponents;
using SnakeGame.GameOverConditions;

namespace GeneticGame.GameOverConditions
{
    public class MovesWithoutEatingCondition : IGameOverCondition, IGameEventHandler
    {
        private int _movesWithoutEating;
        private readonly int _maxMovesWithoutEating;
        
        public MovesWithoutEatingCondition(int maxMoves)
        {
            _maxMovesWithoutEating = maxMoves;
        }
        
        public bool IsGameOver(GameState state)
        {
            return _movesWithoutEating >= _maxMovesWithoutEating;
        }

        public void OnMoveFinished(MoveResult moveResult)
        {
            _movesWithoutEating = moveResult.IsFoodEaten ? 0 : _movesWithoutEating + 1;
        }
    }
}
