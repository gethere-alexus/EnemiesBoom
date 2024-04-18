using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.ProgressData.Hero;
using Infrastructure.Services.UpgradeRegistry;
using Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilAutoUseUpgrade;
using Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilReloadSpeedUpgrade;
using Infrastructure.Services.UpgradeRegistry.Upgrades.CraftingItemLevel;
using Infrastructure.Services.UpgradeRegistry.Upgrades.SlotMergeUpgrade;
using NorskaLib.Spreadsheets;

namespace Infrastructure.Configurations.Config
{
    [Serializable]
    public class ConfigContent
    {
        [SpreadsheetPage("Heroes")] public List<HeroData> Heroes;
        [SpreadsheetPage("ActiveHeroesHolder")] public ActiveHeroesConfig ActiveHeroesSlots;
        [SpreadsheetPage("SlotsUnlock")] public SlotsUnlockConfig SlotsUnlock;

        [SpreadsheetPage("Upgrades")] public List<UpgradeConfiguration> UpgradeData;
        
        [SpreadsheetPage("AnvilUpgradeConfig")] public AnvilUpgradeConfig AnvilUpgradeCfg;

        [SpreadsheetPage("AnvilReloadUpgradeConfig")] public AnvilReloadUpgradeCfg AnvilReloadUpgrade;

        [SpreadsheetPage("AnvilAutoUseUpgradeConfig")] public AnvilAutoUseUpgradeCfg AnvilAutoUseUpgrade;
        [SpreadsheetPage("SlotsMergeUpgradeConfig")] public SlotsMergeUpgradeCfg SlotsMergeUpgrade;

        public HeroData GetHeroDataByID(int heroID) => 
            Heroes.FirstOrDefault(data => data.ID == heroID);
    }
}