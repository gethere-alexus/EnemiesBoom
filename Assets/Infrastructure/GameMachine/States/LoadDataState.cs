using Infrastructure.Curtain;
using Infrastructure.Factories.ItemFactory;
using Infrastructure.ProgressData;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.UpgradeRegistry;
using Zenject;

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
        private readonly IConfigProvider _configProvider;
        private readonly IUpgradesRegistry _upgradesRegistry;
        private readonly LoadingCurtain _loadingCurtain;


        public LoadDataState(GameStateMachine gameStateMachine, IProgressProvider progressProvider, 
            IConfigProvider configProvider, IUpgradesRegistry upgradesRegistry, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _progressProvider = progressProvider;
            _configProvider = configProvider;
            _upgradesRegistry = upgradesRegistry;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            LoadConfiguration();
            LoadUpgradesData();
            LoadProgress();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void LoadUpgradesData() =>
            _upgradesRegistry.LoadUpgradesData();

        private void LoadConfiguration() => 
            _configProvider.LoadConfigs();

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