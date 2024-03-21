using Sources.Infrastructure.SceneLoad;

namespace Sources.Infrastructure.GameMachine.States
{
    public class GameState : IState // state where the game is being played
    {
        private const int GameSceneIndex = 1;
        private readonly SceneLoader _sceneLoader;

        public GameState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(sceneIndex: GameSceneIndex);
        }

        public void Exit()
        {
           
        }
    }
}