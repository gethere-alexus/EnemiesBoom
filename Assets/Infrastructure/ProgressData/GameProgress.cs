using System;
using Infrastructure.ProgressData.Anvil;
using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Hero;

namespace Infrastructure.ProgressData
{
    [Serializable]
    public class GameProgress
    {
        public ItemFieldData ItemField;
        public FieldExtensionsData FieldExtensions;
        public AnvilData Anvil;
        public AnvilExtensionsData AnvilExtensions;
        public HeroesData HeroesData;
        public WalletData WalletData;
    }
}