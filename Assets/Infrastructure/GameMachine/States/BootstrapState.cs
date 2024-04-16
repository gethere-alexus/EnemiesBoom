﻿using Infrastructure.SceneLoad;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// State where the services are being registered
    /// </summary>
    public class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameStateMachine _gameStateMachine;

        private const int BootstrapSceneIndex = 0;

        
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _sceneLoader.Load(sceneIndex: BootstrapSceneIndex, OnSceneLoaded);
        }

        public void Exit()
        {
            
        }
        

        private void OnSceneLoaded() =>
            _gameStateMachine.Enter<LoadGameState>();
    }
}