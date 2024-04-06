using System;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;

namespace Infrastructure.ProgressData
{
    /// <summary>
    /// Stores the game progress
    /// </summary>
    [Serializable]
    public class GameProgress
    {
        public GameFieldData GameField;
        public FieldExtensionsData FieldExtensions;
        public AnvilData.AnvilData Anvil;
        public AnvilExtensionsData AnvilExtensions;
        public int[] PurchasedHeroesID;
    }
}