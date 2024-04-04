using Infrastructure.SceneLoad;

namespace Infrastructure.GameMachine.States
{
    public class GameStoppedState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private const int GameStopSceneIndex = 2;

        public GameStoppedState(SceneLoader sceneLoad)
        {
            _sceneLoader = sceneLoad;
        }

        public void Enter()
        {
            _sceneLoader.Load(GameStopSceneIndex);
        }

        public void Exit()
        {
           
        }
    }
}