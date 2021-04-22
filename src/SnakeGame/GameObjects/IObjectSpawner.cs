using System.Collections.Generic;
using SnakeGame.Math2D;

namespace SnakeGame.GameObjects
{
    public interface IObjectSpawner
    {
        Snake SpawnSnake(int snakeSize);
        Food SpawnFood();
    }
}
