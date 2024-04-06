using Infrastructure.ProgressData;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.Field;
using Infrastructure.Services.ProgressLoad;

namespace Infrastructure.GameMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameFieldFactory _gameFieldFactory;

        private readonly GameStateMachine _gameStateMachine;
        private readonly IConfigLoader _configLoader;


        public LoadProgressState(GameStateMachine gameStateMachine, IProgressProvider progressProvider, IConfigLoader configLoader)
        {
            _gameStateMachine = gameStateMachine;
            _progressProvider = progressProvider;
            _configLoader = configLoader;
        }

        public void Enter()
        {
            LoadConfiguration();
            LoadProgress();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void LoadConfiguration() => 
            _configLoader.LoadConfigs();

        private void LoadProgress()
        {
            GameProgress progress = _progressProvider.GameProgress;

            foreach (var reader in _progressProvider.ProgressReaders)
            {
                reader.LoadProgress(progress);
            }
        }


        public void Exit()
        {
            _progressProvider.SaveProgress();
        }
    }
}