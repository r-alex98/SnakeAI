using SnakeGame;
using SnakeGame.Game;
using SnakeGame.GameComponents;
using SnakeGame.Math2D;

namespace GeneticGame.GameComponents
{
    public class NearestCollisionsDataCollector : IDataCollector
    {
        private readonly int _boardSize;

        public int DataSize { get; } = 4;
        
        public NearestCollisionsDataCollector(int boardSize)
        {
            _boardSize = boardSize;
        }

        public float[] CollectData(GameState state)
        {
            Vector2D[] pointsAroundHead = 
            {
                state.HeadPosition + Vector2D.UpUnitVector,
                state.HeadPosition + Vector2D.RightUnitVector,
                state.HeadPosition + Vector2D.DownUnitVector,
                state.HeadPosition + Vector2D.LeftUnitVector
            };

            var data = new float[DataSize];
            
            for (int i = 0; i < pointsAroundHead.Length; i++)
            {
                if (pointsAroundHead[i].IsOutOfPlane(_boardSize))
                    data[i] = 1;
                for (int j = 0; j < state.BodyPositions.Count; j++)
                {
                    if (pointsAroundHead[i].Equals(state.BodyPositions[j]))
                    {
                        data[i] = 1;
                        break;
                    }
                }
            }

            return data;
        }
    }
}
