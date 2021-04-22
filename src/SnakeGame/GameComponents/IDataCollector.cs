using SnakeGame.Game;

namespace SnakeGame.GameComponents
{
    public interface IDataCollector
    {
        int DataSize { get; }
        float[] CollectData(GameState state);
    }
}
