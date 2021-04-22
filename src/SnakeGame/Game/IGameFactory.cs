using SnakeGame.Player;

namespace SnakeGame.Game
{
    public interface IGameFactory
    {
        ISnakeGame CreateGame();
        IPlayer CreatePlayer();
    }
}
