using System.Linq;
using SnakeGame.Game;

namespace SnakeGame.GameOverConditions
{
    public class CollisionWithBodyCondition : IGameOverCondition
    {
        public bool IsGameOver(GameState state)
        {
            return state.BodyPositions.Any(p => p.Equals(state.HeadPosition));
        }
    }
}
