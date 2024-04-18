using System;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Sources.AnvilBase.AnvilExtensions.AutoUse;
using Sources.Utils;
using Zenject;

namespace Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilAutoUseUpgrade
{
    public class AnvilAutoUseUpgrade : UpgradeBase
    {
        public const int UpgradeID = 2;
        
        private readonly AnvilAutoUse _anvilAutoUse;

        private float _stageAutoUseDecrease;
        private int _currentUpgradeStage;
        private int _upgradePrice;
        private UpgradeConfiguration _upgradeConfiguration;
        public override event Action ConfigLoaded;


        [Inject]
        public AnvilAutoUseUpgrade(AnvilAutoUse anvilAutoUse)
        {
            _anvilAutoUse = anvilAutoUse;
        }

        public override void Upgrade(out bool isSucceeded)
        {
            isSucceeded = false;
            if (IsUpgradable)
            {
                _anvilAutoUse.DecreaseUsageCoolDown(_stageAutoUseDecrease);
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
            _stageAutoUseDecrease = configContainer.AnvilAutoUseUpgrade.StageAutoUseCooldownDecrease;
            ConfigLoaded?.Invoke();
        }

        public override void LoadProgress(GameProgress progress)
        {
            _currentUpgradeStage = progress.UpgradesData.AnvilAutoUseUpgradesData.CurrentUpgradeStage;

            int initialPrice = _upgradeConfiguration.InitialPrice; 
            float stageMultiplication = _upgradeConfiguration.UpgradePriceMultiplication;
            int startStage = _upgradeConfiguration.StartUpgradeStage;
            
            _upgradePrice = UpgradeUtility.GetStagePrice(initialPrice, stageMultiplication,startStage, _currentUpgradeStage);
        }

        public override void SaveProgress(GameProgress progress)
        {
            progress.UpgradesData.AnvilAutoUseUpgradesData.CurrentUpgradeStage = _currentUpgradeStage;
        }

        public override int RequiredUpgradeID => UpgradeID;
        public override UpgradeConfiguration UpgradeConfiguration => _upgradeConfiguration;

        public override int CurrentUpgradeStage => _currentUpgradeStage;
        public override int UpgradePrice => _upgradePrice;

        public override bool IsUpgradable => _currentUpgradeStage < _upgradeConfiguration.MaxUpgradeStage;
        public override bool IsCompletelyUpgraded => _currentUpgradeStage == _upgradeConfiguration.MaxUpgradeStage;
        
    }
    
}