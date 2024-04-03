using Infrastructure.Curtain;
using Infrastructure.SceneLoad;
using Infrastructure.Services.Factories.Field;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// The state where all the game components are being instantiated.
    /// </summary>
    public class LoadGameComponentsState : IState 
    {
        private const int GameSceneIndex = 1;
        
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFieldFactory _gameFieldFactory;

        public LoadGameComponentsState(SceneLoader sceneLoader, GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain,
            IGameFieldFactory gameFieldFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _gameFieldFactory = gameFieldFactory;
        }

        public void Enter() => 
            _sceneLoader.Load(sceneIndex: GameSceneIndex, OnLoaded);

        /// <summary>
        /// Building the components of the game scene here.
        /// </summary>
        private void OnLoaded()
        {
            _loadingCurtain.ShowCurtain();
            
            _gameFieldFactory.CreateField();
            _gameFieldFactory.CreateFieldControl();
            _gameFieldFactory.CreateFieldExtensions();
            
            _gameStateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
           _loadingCurtain.HideCurtain();
        }
    }
}