

using Sources.Infrastructure.SceneLoad;

namespace Sources.Infrastructure.GameMachine.States
{
    public class BootstrapState : IState // state where the services are being registered
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        
        private const int BootstrapSceneIndex = 0;

        public BootstrapState(SceneLoader sceneLoader, GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _sceneLoader.Load(sceneIndex: BootstrapSceneIndex, OnSceneLoaded);
        }

        private void OnSceneLoaded() => 
            _gameStateMachine.Enter<GameState>();

        public void Exit()
        {
        }
    }
}