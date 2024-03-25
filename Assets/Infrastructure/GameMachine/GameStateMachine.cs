using System;
using System.Collections.Generic;
using Infrastructure.Curtain;
using Infrastructure.GameMachine.States;
using Infrastructure.SceneLoad;
using Infrastructure.Services.Factories.UIFactory;
using Zenject;

namespace Infrastructure.GameMachine
{
    /// <summary>
    /// Controlling the game loop flow.
    /// </summary>
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states; 
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, DiContainer diContainer, LoadingCurtain loadingCurtain)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(sceneLoader, this, diContainer, loadingCurtain),
                [typeof(LoadGameState)] = new LoadGameState(sceneLoader,this, loadingCurtain, diContainer.Resolve<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        /// <summary>
        /// Enter the new state, leaving from the previous one.
        /// </summary>
        /// <typeparam name="TState">IState</typeparam>
        public void Enter<TState>() where TState : IState =>
            ChangeState<TState>();

        /// <summary>
        /// Changing active state - leave old state, switch to the new one, enter it.
        /// </summary>
        /// <typeparam name="TState">IState</typeparam>
        private void ChangeState<TState>() where TState : IState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.Enter();
        }
        
    }
}