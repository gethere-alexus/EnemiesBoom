using Sources.Infrastructure.Curtain;
using Sources.Infrastructure.SceneLoad;
using Sources.Infrastructure.Services.Factories.UIFactory;
using UnityEngine;

namespace Sources.Infrastructure.GameMachine.States
{
    // the state where all the game components are being loaded.
    public class LoadGameState : IState 
    {
        private const int GameSceneIndex = 1;
        
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IUIFactory _uiFactory;

        public LoadGameState(SceneLoader sceneLoader, GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
        }

        public void Enter() => 
            _sceneLoader.Load(sceneIndex: GameSceneIndex, OnLoaded);

        private void OnLoaded()
        {
            _loadingCurtain.ShowCurtain();
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateSlotsUI();
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
           _loadingCurtain.HideCurtain();
        }
    }
}