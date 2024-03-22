using Sources.Infrastructure.Curtain;
using Sources.Infrastructure.SceneLoad;
using Sources.Infrastructure.Services.AssetsProvider;
using Sources.Infrastructure.Services.Factories.UIFactory;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.GameMachine.States
{
    public class BootstrapState : IState // state where the services are being registered
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

        private void RegisterServices() // all the services are being registered to project context here. Composition root.
        {
            // services instantiating
            IAssetProvider assetProvider = new AssetProvider();
            IUIFactory uiFactory = new UIFactory(assetProvider, _diContainer);

            // services are being registered to container
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