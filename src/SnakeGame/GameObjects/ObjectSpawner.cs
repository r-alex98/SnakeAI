using System;
using System.Collections.Generic;
using SnakeGame.Math2D;

namespace SnakeGame.GameObjects
{
    public class ObjectSpawner : IObjectSpawner
    {
        private static readonly Random Random = new ();
        
        private readonly int _boardSize;

        public ObjectSpawner(int boardSize)
        {
            _boardSize = boardSize;
        }

        public Snake SpawnSnake(int snakeSize)
        {
            var centralPoint = new Vector2D(_boardSize / 2, _boardSize / 2);
            var headPosition = centralPoint;
            
            List<Vector2D> bodyPositions = new List<Vector2D>();
            for (int i = 1; i < snakeSize; i++)
            {
                var bodyPartPosition = new Vector2D(headPosition.X, headPosition.Y + i);
                bodyPositions.Add(bodyPartPosition);
            }

            return new Snake(headPosition, bodyPositions);
        }

        public Food SpawnFood()
        {
            var foodPosition = new Vector2D
            {
                X = Random.Next(0, _boardSize),
                Y = Random.Next(0, _boardSize)
            };
            return new Food
            {
                Position = foodPosition
            };
        }
    }
}
