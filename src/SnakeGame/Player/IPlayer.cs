using SnakeGame.Math2D;

namespace SnakeGame.Player
{
    public interface IPlayer
    {
        Vector2D GetMovingDirection(float[] data);
    }
}
