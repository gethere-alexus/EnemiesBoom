using System;
using System.Collections.Generic;
using NorskaLib.Spreadsheets;
using Sources.HeroBase;

namespace Infrastructure.Configurations.Config
{
    [Serializable]
    public class ConfigContent
    {
        [SpreadsheetPage("Heroes")] public List<HeroData> Heroes;

        [SpreadsheetPage("ActiveHeroesHolder")] public ActiveHeroesHolderConfig ActiveHeroesHolder;
        [SpreadsheetPage("SlotsUnlock")] public SlotsUnlockConfig SlotsUnlock;
    }
}