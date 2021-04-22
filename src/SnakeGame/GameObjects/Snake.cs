using System.Collections.Generic;
using SnakeGame.Math2D;

namespace SnakeGame.GameObjects
{
    public class Snake
    {
        public Vector2D HeadPosition { get; private set; }
        public List<Vector2D> BodyPositions { get; private set; }
        
        public int Length => BodyPositions.Count + 1;
        public int FoodEaten { get; private set; }
        
        public Snake(Vector2D headPosition, List<Vector2D> bodyPositions)
        {
            HeadPosition = headPosition;
            BodyPositions = bodyPositions;
        }
        
        public void Move(Vector2D direction)
        {
            for (int i = BodyPositions.Count - 1; i > 0; i--)
            {
                BodyPositions[i] = BodyPositions[i - 1];
            }
            BodyPositions[0] = HeadPosition;
            HeadPosition += direction;
        }
        
        public void EatFood()
        {
            BodyPositions.Add((-1, -1));
            FoodEaten++;
        }
        
    }
}
