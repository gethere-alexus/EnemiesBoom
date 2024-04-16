using System;

namespace Infrastructure.ProgressData.Anvil
{
    [Serializable]
    public class AnvilData
    {
        public int MaxCharges;
        public int ChargesLeft;
        public int CraftingItemLevel;
    }
}