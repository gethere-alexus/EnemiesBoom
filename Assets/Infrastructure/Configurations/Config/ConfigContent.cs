using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Hero;
using NorskaLib.Spreadsheets;

namespace Infrastructure.Configurations.Config
{
    [Serializable]
    public class ConfigContent
    {
        [SpreadsheetPage("Heroes")] public List<HeroData> Heroes;

        [SpreadsheetPage("ActiveHeroesHolder")] public ActiveHeroesConfig ActiveHeroesSlots;
        [SpreadsheetPage("SlotsUnlock")] public SlotsUnlockConfig SlotsUnlock;

        public HeroData GetHeroDataByID(int heroID) => 
            Heroes.FirstOrDefault(data => data.ID == heroID);
    }
}