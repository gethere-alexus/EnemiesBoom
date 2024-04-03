using System;

namespace Infrastructure.ProgressData.AnvilData
{
    [Serializable]
    public class AnvilData : IProgressData
    {
        public int MaxCharges, ChargesLeft;
        public int CraftingItemLevel;
    }
}