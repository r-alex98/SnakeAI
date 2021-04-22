using System;
using System.Threading.Tasks;
using SnakeGame;
using SnakeGame.Game;

namespace Web.Simulators
{
    public interface ISimulator
    {
        Task SimulateAsync();
        void StopSimulation();
        
        event Func<int, GameState, Task> GameStateUpdated;
        event Func<SimulatorState, Task> IterationCompleted;
    }
}
