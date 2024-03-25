using Infrastructure.Curtain;
using Infrastructure.SceneLoad;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.UIFactory;
using Infrastructure.Services.PrefabLoad;
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
        private readonly LoadingCurtain _loadingCurtain;

        private const int BootstrapSceneIndex = 0;

        public BootstrapState(SceneLoader sceneLoader, GameStateMachine gameStateMachine, DiContainer diContainer,
            LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _diContainer = diContainer;
            _loadingCurtain = loadingCurtain;

            RegisterServices();
        }

        /// <summary>
        /// Composition Root. All the services are being registered to project context here.
        /// </summary>
        private void RegisterServices()
        {
            // services instantiating
            IPrefabLoader prefabLoader = new PrefabLoader();
            IAssetProvider assetProvider = new AssetProvider(prefabLoader);
            IConfigLoader configLoader = new ConfigLoader(prefabLoader);
            IUIFactory uiFactory = new UIFactory(assetProvider, configLoader);

            // services are being registered to container
            _diContainer.Bind<IPrefabLoader>().FromInstance(prefabLoader).AsSingle().NonLazy();
            _diContainer.Bind<IConfigLoader>().FromInstance(configLoader).AsSingle().NonLazy();
            _diContainer.Bind<IAssetProvider>().FromInstance(assetProvider).AsSingle().NonLazy();
            _diContainer.Bind<IUIFactory>().FromInstance(uiFactory).AsSingle().NonLazy();
        }

        public void Enter() => 
            _sceneLoader.Load(sceneIndex: BootstrapSceneIndex, OnSceneLoaded); // once bootstrapped, goes to LoadGameState

        private void OnSceneLoaded() => 
            _gameStateMachine.Enter<LoadGameState>();

        public void Exit()
        {
            _loadingCurtain.ShowCurtain();
        }
    }
}