using System;
using System.Collections.Generic;
using Sources.Infrastructure.Curtain;
using Sources.Infrastructure.GameMachine.States;
using Sources.Infrastructure.SceneLoad;
using Sources.Infrastructure.Services.Factories.UIFactory;
using Zenject;

namespace Sources.Infrastructure.GameMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, DiContainer diContainer, LoadingCurtain loadingCurtain)
        {
            //Bootstrap state is the state where all the services are being registered
            //LoadGameState is the state where all the game components are being loaded.
            //GameLoopState is the state which goes after LoadGameState, once all the components are prepared
            
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(sceneLoader, this, diContainer, loadingCurtain),
                [typeof(LoadGameState)] = new LoadGameState(sceneLoader,this, loadingCurtain, diContainer.Resolve<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
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