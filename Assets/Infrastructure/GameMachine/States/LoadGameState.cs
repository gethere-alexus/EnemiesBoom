using Infrastructure.Curtain;
using Infrastructure.SceneLoad;
using Infrastructure.Services.ConnectionCheck;
using Infrastructure.Services.Factories.FieldFactory;
using Infrastructure.Services.Factories.HeroesStorage;
using Infrastructure.Services.Factories.UI;
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
        private readonly IUIFactory _uiFactory;

        public LoadGameState(GameStateMachine gameStateMachine, IGameFieldFactory gameFieldFactory, SceneLoader sceneLoader, 
            IConnectionChecker connectionChecker, LoadingCurtain loadingCurtain, IHeroesStorageFactory heroesStorageFactory, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _heroesStorageFactory = heroesStorageFactory;
            _uiFactory = uiFactory;
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
                _gameStateMachine.Enter<LoadDataState>();
            }
            else
            {
                _gameStateMachine.Enter<GameStoppedState>();
            }
        }

        private void CreateGameComponents()
        {
            _uiFactory.CreateBottomMenu();
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