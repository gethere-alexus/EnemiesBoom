using System;

namespace Infrastructure.ProgressData.AnvilData
{
    [Serializable]
    public class AnvilAutoUseData : IProgressData
    {
        public float UsingCoolDown;
    }
}