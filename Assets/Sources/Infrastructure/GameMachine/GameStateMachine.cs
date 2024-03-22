using System;
using System.Collections.Generic;
using Sources.Infrastructure.GameMachine.States;
using Sources.Infrastructure.SceneLoad;
using Zenject;

namespace Sources.Infrastructure.GameMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, DiContainer diContainer)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(sceneLoader, this, diContainer),
                [typeof(GameState)] = new GameState(sceneLoader),
            };
        }

        public void Enter<TState>() where TState : IState =>
            ChangeState<TState>();

        private void ChangeState<TState>() where TState : IState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.Enter();
        }
        
    }
}