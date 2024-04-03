using System;

namespace Infrastructure.ProgressData.AnvilData
{
    [Serializable]
    public class AnvilAutoRefillerData : IProgressData
    {
        public float RefillCoolDown;
        public int AmountChargesToAdd;
    }
}