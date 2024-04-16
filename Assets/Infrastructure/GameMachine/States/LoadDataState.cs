using Infrastructure.Curtain;
using Infrastructure.ProgressData;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.Factories.FieldFactory;
using Infrastructure.Services.ProgressLoad;

namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// State is loading data for instantiated components
    /// </summary>
    public class LoadDataState : IState
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly IItemFieldFactory _itemFieldFactory;

        private readonly GameStateMachine _gameStateMachine;
        private readonly IConfigLoader _configLoader;
        private readonly LoadingCurtain _loadingCurtain;


        public LoadDataState(GameStateMachine gameStateMachine, IProgressProvider progressProvider, 
            IConfigLoader configLoader, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _progressProvider = progressProvider;
            _configLoader = configLoader;
            _loadingCurtain = loadingCurtain;
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
            _loadingCurtain.HideCurtain();
        }
    }
}