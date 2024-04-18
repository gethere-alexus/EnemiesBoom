using Infrastructure.Curtain;
using Infrastructure.Factories.HeroesStorage;
using Infrastructure.Factories.ItemFactory;
using Infrastructure.Factories.UI;
using Infrastructure.Factories.Wallet;
using Infrastructure.SceneLoad;
using Infrastructure.Services.ConnectionCheck;
using Infrastructure.Services.UpgradeRegistry;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// The state where all the game components are being instantiated.
    /// </summary>
    public class LoadGameState : IState
    {
        private const int GameSceneIndex = 1;
        
        private readonly IHeroesStorageFactory _heroesStorageFactory;
        private readonly IItemFieldFactory _itemFieldFactory;
        private readonly IConnectionChecker _connectionChecker;
        private readonly IUIMenuFactory _uiMenuFactory;
        private readonly IWalletFactory _walletFactory;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUpgradesRegistry _upgradesRegistry;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadGameState(GameStateMachine gameStateMachine, IUpgradesRegistry upgradesRegistry, IItemFieldFactory itemFieldFactory, IHeroesStorageFactory heroesStorageFactory
            , IUIMenuFactory uiMenuFactory, IWalletFactory walletFactory, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _upgradesRegistry = upgradesRegistry;
            _loadingCurtain = loadingCurtain;
            _heroesStorageFactory = heroesStorageFactory;
            _uiMenuFactory = uiMenuFactory;
            _walletFactory = walletFactory;
            _itemFieldFactory = itemFieldFactory;
        }

        public void Enter() =>
            _sceneLoader.Load(sceneIndex: GameSceneIndex, OnLoaded);

        /// <summary>
        /// Building the components of the game scene here.
        /// </summary>
        private void OnLoaded()
        {
            _loadingCurtain.ShowCurtain();

            CreateGameComponents();
            _upgradesRegistry.CreateUpgrades();

            _gameStateMachine.Enter<LoadDataState>();
        }

        private void CreateGameComponents()
        {
            _uiMenuFactory.CreateBottomMenu();
            _walletFactory.CreateWalletDisplay();
            _itemFieldFactory.CreateItemField();
            _heroesStorageFactory.CreateHeroesField();
        }
        
        public void Exit()
        {
        }
    }
}