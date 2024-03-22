using Sources.Infrastructure.SceneLoad;
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

        private const int BootstrapSceneIndex = 0;

        public BootstrapState(SceneLoader sceneLoader, GameStateMachine gameStateMachine, DiContainer diContainer)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _diContainer = diContainer;
            
            RegisterServices();
        }

        private void RegisterServices() // all the services are being registered to project context here.
        {
            IUIFactory uiFactory = new UIFactory();
            _diContainer.Bind<IUIFactory>().FromInstance(uiFactory).AsSingle().NonLazy();
            Debug.Log("Services registered");
        }

        public void Enter() => 
            _sceneLoader.Load(sceneIndex: BootstrapSceneIndex, OnSceneLoaded); // once bootstrapped, goes to GameState

        private void OnSceneLoaded() => 
            _gameStateMachine.Enter<GameState>();

        public void Exit()
        {
        }
    }
}