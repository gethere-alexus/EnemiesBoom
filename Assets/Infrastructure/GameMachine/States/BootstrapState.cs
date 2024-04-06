using Infrastructure.SceneLoad;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ConnectionCheck;
using Infrastructure.Services.Factories.Field;
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
        private readonly DiContainer _diContainer;
        private readonly ICoroutineRunner _coroutineRunner;
        
        private const int BootstrapSceneIndex = 0;

        public BootstrapState(GameStateMachine gameStateMachine, DiContainer diContainer, SceneLoader sceneLoader,
            ICoroutineRunner coroutineRunner)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _diContainer = diContainer;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        /// <summary>
        /// Composition Root. All the services are being registered to project context here.
        /// </summary>
        private void RegisterServices()
        {
            // services instantiating
            IPrefabLoader prefabLoader = new PrefabLoader();
            IConfigLoader configLoader = new ConfigLoader(prefabLoader);
            IAssetProvider assetProvider = new AssetProvider(prefabLoader);
            IWindowsProvider windowsProvider = new WindowsProvider(prefabLoader);
            IConnectionChecker connectionChecker = new ConnectionChecker(_gameStateMachine,windowsProvider,_coroutineRunner);
            IProgressProvider progressProvider = new ProgressProvider();
            IAutoProcessesController autoProcessesController = new AutoProcessesController();
            IUIFactory uiFactory = new UIFactory(assetProvider);
            IGameFieldFactory gameFieldFactory = new GameFieldFactory(assetProvider, autoProcessesController, uiFactory, progressProvider, configLoader);

            // services are being registered to container
            _diContainer.Bind<IConnectionChecker>().FromInstance(connectionChecker).AsSingle().NonLazy();
            _diContainer.Bind<IPrefabLoader>().FromInstance(prefabLoader).AsSingle().NonLazy();
            _diContainer.Bind<IConfigLoader>().FromInstance(configLoader).AsSingle().NonLazy();
            _diContainer.Bind<IAssetProvider>().FromInstance(assetProvider).AsSingle().NonLazy();
            _diContainer.Bind<IWindowsProvider>().FromInstance(windowsProvider).AsCached().NonLazy();
            _diContainer.Bind<IProgressProvider>().FromInstance(progressProvider).AsSingle().NonLazy();
            _diContainer.Bind<IUIFactory>().FromInstance(uiFactory).AsSingle().NonLazy();
            _diContainer.Bind<IGameFieldFactory>().FromInstance(gameFieldFactory).AsSingle().NonLazy();
            _diContainer.Bind<IAutoProcessesController>().FromInstance(autoProcessesController).AsSingle().NonLazy();
        }

        public void Enter()
        {
            _sceneLoader.Load(sceneIndex: BootstrapSceneIndex, OnSceneLoaded); 
        }
        

        private void OnSceneLoaded() =>
            _gameStateMachine.Enter<LoadGameState>();

        public void Exit()
        {
            
        }
    }
}