using Infrastructure.Curtain;
using Infrastructure.SceneLoad;
using Infrastructure.Services.ConnectionCheck;
using Infrastructure.Services.Factories.Field;
using Infrastructure.Services.Factories.HeroesStorage;
using Infrastructure.Services.WindowProvider;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// The state where all the game components are being instantiated.
    /// </summary>
    public class LoadGameState : IState
    {
        private const int GameSceneIndex = 1;

        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFieldFactory _gameFieldFactory;
        private readonly IConnectionChecker _connectionChecker;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IHeroesStorageFactory _heroesStorageFactory;

        public LoadGameState(GameStateMachine gameStateMachine, IGameFieldFactory gameFieldFactory,
            SceneLoader sceneLoader, IConnectionChecker connectionChecker, LoadingCurtain loadingCurtain, IHeroesStorageFactory heroesStorageFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _heroesStorageFactory = heroesStorageFactory;
            _gameFieldFactory = gameFieldFactory;
            _connectionChecker = connectionChecker;
        }

        public void Enter() =>
            _sceneLoader.Load(sceneIndex: GameSceneIndex, OnLoaded);

        /// <summary>
        /// Building the components of the game scene here.
        /// </summary>
        private void OnLoaded()
        {
            _loadingCurtain.ShowCurtain();
            
            if (_connectionChecker.IsNetworkConnected)
            {
                CreateGameComponents();
                _gameStateMachine.Enter<LoadProgressState>();
            }
            else
            {
                _gameStateMachine.Enter<GameStoppedState>();
            }
        }

        private void CreateGameComponents()
        {
            CreateGameField();
            _heroesStorageFactory.CreateActiveHeroesStorage();
        }

        private void CreateGameField()
        {
            _gameFieldFactory.CreateField();
            _gameFieldFactory.CreateFieldControl();
            _gameFieldFactory.CreateFieldExtensions();
        }

        public void Exit()
        {
            _loadingCurtain.HideCurtain();
        }
    }
}