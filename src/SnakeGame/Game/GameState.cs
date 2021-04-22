using System.Collections.Generic;
using SnakeGame.Math2D;

namespace SnakeGame.Game
{
    public class GameState
    {
        public int SnakeLength { get; init; }
        public int Points { get; init; }
        public Vector2D HeadPosition { get; init; }
        public Vector2D FoodPosition { get; init; }
        public List<Vector2D> BodyPositions { get; init; }
    }
}
