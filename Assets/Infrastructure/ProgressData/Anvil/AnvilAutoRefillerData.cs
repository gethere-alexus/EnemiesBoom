using System;

namespace Infrastructure.ProgressData.Anvil
{
    [Serializable]
    public class AnvilAutoRefillerData
    {
        public float RefillCoolDown;
        public int AmountChargesToAdd;
    }
}