using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.ProgressData.Hero;
using Infrastructure.Services.UpgradeRegistry;
using Infrastructure.Services.UpgradeRegistry.UpgradingParams;
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

        public HeroData GetHeroDataByID(int heroID) => 
            Heroes.FirstOrDefault(data => data.ID == heroID);
    }
}