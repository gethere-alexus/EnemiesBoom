using Infrastructure.ProgressData;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.Factories.Field;
using Infrastructure.Services.ProgressLoad;
using Zenject;

namespace Infrastructure.GameMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameFieldFactory _gameFieldFactory;

        private readonly GameStateMachine _gameStateMachine;


        public LoadProgressState(GameStateMachine gameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameStateMachine;
            _progressProvider = diContainer.Resolve<IProgressProvider>();
        }

        public void Enter()
        {
            GameProgress progress = _progressProvider.GameProgress;
                    
            foreach (var reader in _progressProvider.ProgressReaders)
            {
                reader.LoadProgress(progress);
            }
            
            _gameStateMachine.Enter<GameLoopState>();
        }
        

        public void Exit()
        {
            _progressProvider.SaveProgress();
        }
    }
}