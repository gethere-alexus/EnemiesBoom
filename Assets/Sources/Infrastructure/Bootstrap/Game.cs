using Sources.Infrastructure.GameMachine;
using Sources.Infrastructure.GameMachine.States;
using Sources.Infrastructure.SceneLoad;

namespace Sources.Infrastructure.Bootstrap
{
    public class Game
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _sceneLoader = new SceneLoader(coroutineRunner);
            _gameStateMachine = new GameStateMachine(_sceneLoader);
            _gameStateMachine.Enter<GameState>();
        }
    }
}