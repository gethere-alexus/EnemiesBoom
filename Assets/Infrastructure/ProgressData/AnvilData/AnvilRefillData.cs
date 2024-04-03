using System;

namespace Infrastructure.ProgressData.AnvilData
{
    [Serializable]
    public class AnvilRefillData : IProgressData
    {
        public int Charges;
    }
}