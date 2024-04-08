using Infrastructure.Extensions;
using Infrastructure.SceneLoad;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ConnectionCheck;
using Infrastructure.Services.Factories.FieldFactory;
using Infrastructure.Services.Factories.HeroesStorage;
using Infrastructure.Services.Factories.UI;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.WindowProvider;
using Zenject;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// State where the services are being registered
    /// </summary>
    public class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;

        private const int BootstrapSceneIndex = 0;

        public BootstrapState(GameStateMachine gameStateMachine, DiContainer diContainer, SceneLoader sceneLoader,
            ICoroutineRunner coroutineRunner)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            RegisterServices(diContainer, coroutineRunner);
        }

        public void Enter()
        {
            _sceneLoader.Load(sceneIndex: BootstrapSceneIndex, OnSceneLoaded);
        }

        public void Exit()
        {
            
        }

        /// <summary>
        /// Composition Root. All the services are being registered to project context here.
        /// </summary>
        private void RegisterServices(DiContainer container, ICoroutineRunner coroutineRunner)
        {
            IPrefabLoader prefabLoader = 
                new PrefabLoader().RegisterService<IPrefabLoader>(container);
            
            IConfigLoader configLoader = 
                new ConfigLoader(prefabLoader).RegisterService<IConfigLoader>(container);
            
            IAssetProvider assetProvider = 
                new AssetProvider(prefabLoader).RegisterService<IAssetProvider>(container);
            
            IUIFactory uiFactory = 
                new UIFactory(container, assetProvider).RegisterService<IUIFactory>(container);
            
            IWindowsProvider windowsProvider =
                new WindowsProvider(uiFactory, prefabLoader).RegisterService<IWindowsProvider>(container);
            
            IConnectionChecker connectionChecker =
                new ConnectionChecker(_gameStateMachine, windowsProvider, coroutineRunner).RegisterService<IConnectionChecker>(container);
            
            IProgressProvider progressProvider = 
                new ProgressProvider().RegisterService<IProgressProvider>(container);
            
            IAutoProcessesController autoProcessesController =
                new AutoProcessesController().RegisterService<IAutoProcessesController>(container);
            
            IGameFieldFactory gameFieldFactory =
                new GameFieldFactory(assetProvider, autoProcessesController, uiFactory, progressProvider, configLoader).RegisterService<IGameFieldFactory>(container);
            
            IHeroesStorageFactory heroesStorageFactory =
                new HeroesStorageFactory(uiFactory, assetProvider, configLoader).RegisterService<IHeroesStorageFactory>(container);
        }
        private void OnSceneLoaded() =>
            _gameStateMachine.Enter<LoadGameState>();
    }
}