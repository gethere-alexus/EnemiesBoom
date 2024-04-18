using System;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Sources.AnvilBase.AnvilExtensions.AutoRefiller;
using Sources.Utils;
using Zenject;

namespace Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilReloadSpeedUpgrade
{
    public class AnvilReloadSpeedUpgrade : UpgradeBase
    {
        public const int UpgradeID = 1;
        private readonly AnvilAutoRefiller _anvilRefiller;

        private float _stageRefillDecrease;
        private int _currentUpgradeStage;
        private int _upgradePrice;
        private UpgradeConfiguration _upgradeConfiguration;
        public override event Action ConfigLoaded;


        [Inject]
        public AnvilReloadSpeedUpgrade(AnvilAutoRefiller anvilAutoRefiller)
        {
            _anvilRefiller = anvilAutoRefiller;
        }

        public override void Upgrade(out bool isSucceeded)
        {
            isSucceeded = false;
            if (IsUpgradable)
            {
                _anvilRefiller.DecreaseRefillCoolDown(_stageRefillDecrease);
                _currentUpgradeStage++;
                
                int initialPrice = _upgradeConfiguration.InitialPrice;
                float stageMultiplication = _upgradeConfiguration.UpgradePriceMultiplication;
                int startStage = _upgradeConfiguration.StartUpgradeStage;
                
                _upgradePrice = UpgradeUtility.GetStagePrice(initialPrice, stageMultiplication, startStage, _currentUpgradeStage);
                isSucceeded = true;
            }
        }

        public override void LoadUpgradeConfiguration(UpgradeConfiguration upgradeConfiguration) => 
            _upgradeConfiguration = upgradeConfiguration;

        public override void LoadConfiguration(ConfigContent configContainer)
        {
            _stageRefillDecrease = configContainer.AnvilReloadUpgrade.StageRefillSpeedUpgrade;
            ConfigLoaded?.Invoke();
        }

        public override void LoadProgress(GameProgress progress)
        {
            _currentUpgradeStage = progress.UpgradesData.AnvilReloadUpgradesData.CurrentUpgradeStage;

            int initialPrice = _upgradeConfiguration.InitialPrice; 
            float stageMultiplication = _upgradeConfiguration.UpgradePriceMultiplication;
            int startStage = _upgradeConfiguration.StartUpgradeStage;
            
            _upgradePrice = UpgradeUtility.GetStagePrice(initialPrice, stageMultiplication,startStage, _currentUpgradeStage);
        }

        public override void SaveProgress(GameProgress progress)
        {
            progress.UpgradesData.AnvilReloadUpgradesData.CurrentUpgradeStage = _currentUpgradeStage;
        }

        public override int RequiredUpgradeID => UpgradeID;
        public override UpgradeConfiguration UpgradeConfiguration => _upgradeConfiguration;

        public override int CurrentUpgradeStage => _currentUpgradeStage;
        public override int UpgradePrice => _upgradePrice;

        public override bool IsUpgradable => _currentUpgradeStage < _upgradeConfiguration.MaxUpgradeStage;
        public override bool IsCompletelyUpgraded => _currentUpgradeStage == _upgradeConfiguration.MaxUpgradeStage;
        
    }
}