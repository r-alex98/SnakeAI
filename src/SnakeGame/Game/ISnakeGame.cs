using System;
using System.Threading.Tasks;
using SnakeGame.Player;

namespace SnakeGame.Game
{
    public interface ISnakeGame
    {
        Task Run(IPlayer player);
        float Score { get; }
        
        event Func<IPlayer, GameState, Task> GameStateUpdated;
        event Action<MoveResult> MoveFinished;
    }
}
