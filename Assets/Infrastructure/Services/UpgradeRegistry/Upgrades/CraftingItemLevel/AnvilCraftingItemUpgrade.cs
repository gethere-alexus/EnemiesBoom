using System;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Sources.AnvilBase;
using Sources.Utils;
using Zenject;

namespace Infrastructure.Services.UpgradeRegistry.Upgrades.CraftingItemLevel
{
    public class AnvilCraftingItemUpgrade : UpgradeBase
    {
        public const int UpgradeID = 0;
        
        private int _upgradePrice;
        private int _itemLevelStageUpgrade;
        private int _currentUpgradeStage;

        private UpgradeConfiguration _upgradeConfiguration;
        private readonly Anvil _anvilInstance;
        
        public override event Action ConfigLoaded;


        [Inject]
        public AnvilCraftingItemUpgrade(Anvil anvilInstance)
        {
            _anvilInstance = anvilInstance;
        }

        public override void Upgrade(out bool isSucceeded)
        {
            isSucceeded = false;
            if (IsUpgradable)
            {
                _anvilInstance.IncreaseCraftingItemLevel(_itemLevelStageUpgrade);
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
            _itemLevelStageUpgrade = configContainer.AnvilUpgradeCfg.ItemLevelStageUpgrade;
            ConfigLoaded?.Invoke();
        }

        public override void LoadProgress(GameProgress progress)
        {
            _currentUpgradeStage = progress.UpgradesData.AnvilItemUpgradeData.CurrentUpgradeStage;

            int initialPrice = _upgradeConfiguration.InitialPrice; 
            float stageMultiplication = _upgradeConfiguration.UpgradePriceMultiplication;
            int startStage = _upgradeConfiguration.StartUpgradeStage;
            
            _upgradePrice = UpgradeUtility.GetStagePrice(initialPrice, stageMultiplication,startStage, _currentUpgradeStage);
        }

        public override void SaveProgress(GameProgress progress)
        {
            progress.UpgradesData.AnvilItemUpgradeData.CurrentUpgradeStage = _currentUpgradeStage;
        }

        public override int RequiredUpgradeID => UpgradeID;
        public override UpgradeConfiguration UpgradeConfiguration => _upgradeConfiguration;

        public override int CurrentUpgradeStage => _currentUpgradeStage;
        public override int UpgradePrice => _upgradePrice;

        public override bool IsUpgradable => _currentUpgradeStage < _upgradeConfiguration.MaxUpgradeStage;
        public override bool IsCompletelyUpgraded => _currentUpgradeStage == _upgradeConfiguration.MaxUpgradeStage;
    }
}