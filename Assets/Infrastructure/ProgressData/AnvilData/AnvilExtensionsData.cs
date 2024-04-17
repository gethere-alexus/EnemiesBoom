using System;

namespace Infrastructure.ProgressData.AnvilData
{
    [Serializable]
    public class AnvilExtensionsData
    {
        public AnvilAutoRefillerData AnvilAutoRefiller;
        public AnvilRefillData AnvilRefill;
        public AnvilAutoUseData AnvilAutoUse;
    }
}