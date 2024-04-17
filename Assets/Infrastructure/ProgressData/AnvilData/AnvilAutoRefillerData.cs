using System;

namespace Infrastructure.ProgressData.AnvilData
{
    [Serializable]
    public class AnvilAutoRefillerData
    {
        public float RefillCoolDown;
        public int AmountChargesToAdd;
    }
}