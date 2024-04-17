using System;
using System.Collections.Generic;
using Infrastructure.Curtain;
using Infrastructure.GameMachine.States;
using Infrastructure.SceneLoad;
using Infrastructure.Services.AutoProcessesControl;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.HeroesStorage;
using Infrastructure.Services.Factories.ItemFactory;
using Infrastructure.Services.Factories.UI;
using Infrastructure.Services.Factories.Wallet;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.UpgradeRegistry;
using Zenject;

namespace Infrastructure.GameMachine
{
    /// <summary>
    /// Game life cycle controller.
    /// </summary>
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, DiContainer diContainer, ICoroutineRunner coroutineRunner,
            LoadingCurtain loadingCurtain)
        {
            diContainer.Bind<ICoroutineRunner>().FromInstance(coroutineRunner);
            
            IItemFieldFactory itemFieldFactory = diContainer.Resolve<IItemFieldFactory>();
            IUpgradesRegistry upgradesRegistry = diContainer.Resolve<IUpgradesRegistry>();
            IHeroesStorageFactory heroesStorageFactory = diContainer.Resolve<IHeroesStorageFactory>();
            IWalletFactory walletFactory = diContainer.Resolve<IWalletFactory>();
            IUIMenuFactory uiMenuFactory = diContainer.Resolve<IUIMenuFactory>();
            IConfigLoader configLoader = diContainer.Resolve<IConfigLoader>();
            IProgressProvider progressProvider = diContainer.Resolve<IProgressProvider>();
            IAutoProcessesController autoProcessesController = diContainer.Resolve<IAutoProcessesController>();
            
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadGameState)] = new LoadGameState(this, itemFieldFactory, heroesStorageFactory,
                    uiMenuFactory, walletFactory, sceneLoader, loadingCurtain),
                [typeof(LoadDataState)] = new LoadDataState(this, progressProvider, configLoader,upgradesRegistry, loadingCurtain),
                [typeof(GameLoopState)] = new GameLoopState(progressProvider, autoProcessesController, coroutineRunner, configLoader),
                [typeof(GameStoppedState)] = new GameStoppedState(sceneLoader),
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