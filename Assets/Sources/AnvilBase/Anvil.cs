using System;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad.Connection;
using Infrastructure.Services.UpgradeRegistry;
using Sources.ItemsBase.ItemBase;
using Sources.ItemsBase.ItemFieldBase;

namespace Sources.AnvilBase
{
    /// <summary>
    /// Creates and places the items on a grid.
    /// </summary>
    public class Anvil : IProgressWriter, IUpgradable, IConfigReader
    {
        private readonly ItemField _itemField;

        private int _maxCharges;
        private int _chargesLeft;

        private int _craftingItemLevel;

        public const int UpgradeID = 0;

        private int _itemLevelStageUpgrade;
        private int _currentUpgradeStage;
        private int _upgradePrice;

        private UpgradeConfiguration _upgradeConfiguration;
        public event Action ItemCrafted;
        public event Action ConfigLoaded;
        public event Action ChargesUpdated;


        public Anvil(ItemField itemField) => 
            _itemField = itemField;

        /// <summary>
        /// Adds max-charges to already existing amount of charges, if it is not full
        /// </summary>
        public void RefillCharges(out bool isSucceeded)
        {
            isSucceeded = false;
            if (!IsFullOfCharges)
            {
                isSucceeded = true;
                AddCharge(_maxCharges);
            }
        }

        /// <summary>
        /// Places an item on a grid (if there is a free slot) and spends one charge.
        /// </summary>
        public void CraftItem()
        {
            if (_chargesLeft > 0)
            {
                _itemField.PlaceItem(new Item(_craftingItemLevel), out bool isSucceeded);

                if (isSucceeded)
                {
                    SpendCharge();
                    ItemCrafted?.Invoke();
                }
            }
        }

        public void AddCharge(int amount)
        {
            _chargesLeft += amount;
            ChargesUpdated?.Invoke();
        }

        /// <summary>
        /// Spends one anvil charge.
        /// </summary>
        private void SpendCharge()
        {
            _chargesLeft--;
            ChargesUpdated?.Invoke();
        }

        public void Upgrade(out bool isSucceeded)
        {
            isSucceeded = false;
            if (IsUpgradable)
            {
                _craftingItemLevel += _itemLevelStageUpgrade;
                _currentUpgradeStage++;

                int initialPrice = _upgradeConfiguration.InitialPrice;
                int stageMultiplication = _upgradeConfiguration.UpgradePriceMultiplication;
                int startStage = _upgradeConfiguration.StartUpgradeStage;
                
                _upgradePrice = UpgradeUtility.GetStagePrice(initialPrice, stageMultiplication, startStage, _currentUpgradeStage);
                isSucceeded = true;
            }
        }

        public void Upgrade() => 
            Upgrade(out bool isSucceeded);

        public void SaveProgress(GameProgress progress)
        {
            progress.Anvil.MaxCharges = _maxCharges;
            progress.Anvil.ChargesLeft = _chargesLeft;
            progress.Anvil.CraftingItemLevel = _craftingItemLevel;

            progress.UpgradesData.AnvilUpgradeData.CurrentUpgradeStage = _currentUpgradeStage;
        }

        public void LoadProgress(GameProgress progress)
        {
            _maxCharges = progress.Anvil.MaxCharges;
            _chargesLeft = progress.Anvil.ChargesLeft;
            _craftingItemLevel = progress.Anvil.CraftingItemLevel;

            _currentUpgradeStage = progress.UpgradesData.AnvilUpgradeData.CurrentUpgradeStage;

            int initialPrice = _upgradeConfiguration.InitialPrice; 
            int stageMultiplication = _upgradeConfiguration.UpgradePriceMultiplication;
            int startStage = _upgradeConfiguration.StartUpgradeStage;
            
            _upgradePrice = UpgradeUtility.GetStagePrice(initialPrice, stageMultiplication,startStage, _currentUpgradeStage);
                
            ChargesUpdated?.Invoke();
        }

        public void LoadConfiguration(ConfigContent configContainer)
        {
            _itemLevelStageUpgrade = configContainer.AnvilUpgradeCfg.ItemLevelStageUpgrade;
            ConfigLoaded?.Invoke();
        }


        public void LoadUpgradeInformation(UpgradeConfiguration upgradeConfiguration) => 
            _upgradeConfiguration = upgradeConfiguration;


        public bool IsFullOfCharges => _chargesLeft >= _maxCharges;
        public bool IsCompletelyCharged => _chargesLeft >= _maxCharges;
        public int ChargesLeft => _chargesLeft;
        public int MaxCharges => _maxCharges;
        public UpgradeConfiguration UpgradeInformation => _upgradeConfiguration;
        public int RequiredUpgradeID => UpgradeID;
        public int CurrentUpgradeStage => _currentUpgradeStage;
        public int UpgradePrice => _upgradePrice;

        public bool IsUpgradable => 
            _currentUpgradeStage < _upgradeConfiguration.MaxUpgradeStage;
        public bool IsCompletelyUpgraded => 
            _currentUpgradeStage == _upgradeConfiguration.MaxUpgradeStage;
    }
}