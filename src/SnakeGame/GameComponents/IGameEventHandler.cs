using System.Threading.Tasks;
using SnakeGame.Game;

namespace SnakeGame.GameComponents
{
    public interface IGameEventHandler
    {
        void OnMoveFinished(MoveResult moveResult);
    }
}
