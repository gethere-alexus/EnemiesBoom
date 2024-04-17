using System;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Hero;

namespace Infrastructure.ProgressData
{
    [Serializable]
    public class GameProgress
    {
        public ItemFieldData ItemField;
        public FieldExtensionsData FieldExtensions;
        public AnvilData.AnvilData Anvil;
        public UpgradesData UpgradesData;
        public AnvilExtensionsData AnvilExtensions;
        public HeroesData HeroesData;
        public WalletData WalletData;
    }

    [Serializable]
    public class UpgradesData
    {
        public UpgradeData AnvilUpgradeData;
        
    }
}