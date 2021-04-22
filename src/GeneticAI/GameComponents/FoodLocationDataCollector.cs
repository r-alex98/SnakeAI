using SnakeGame;
using SnakeGame.Game;
using SnakeGame.GameComponents;

namespace GeneticGame.GameComponents
{
    public class FoodLocationDataCollector : IDataCollector
    {
        public int DataSize { get; } = 4;
        
        public float[] CollectData(GameState state)
        {
            var data = new float[DataSize];
            data[0] = state.HeadPosition.Y < state.FoodPosition.Y ? 1 : 0;
            data[1] = state.HeadPosition.Y > state.FoodPosition.Y ? 1 : 0;
            data[2] = state.HeadPosition.X < state.FoodPosition.X ? 1 : 0;
            data[3] = state.HeadPosition.X > state.FoodPosition.X ? 1 : 0;
            return data; 
        }
    }
}
