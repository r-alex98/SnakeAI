using SnakeGame.Game;

namespace SnakeGame.GameOverConditions
{
    public class CollisionWithBorderCondition : IGameOverCondition
    {
        private readonly int _boardSize;

        public CollisionWithBorderCondition(int boardSize)
        {
            _boardSize = boardSize;
        }

        public bool IsGameOver(GameState state)
        {
            return state.HeadPosition.IsOutOfPlane(_boardSize);
        }
    }
}
