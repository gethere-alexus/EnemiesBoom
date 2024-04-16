using System;

namespace Infrastructure.ProgressData.Anvil
{
    [Serializable]
    public class AnvilExtensionsData
    {
        public AnvilAutoRefillerData AnvilAutoRefiller;
        public AnvilRefillData AnvilRefill;
        public AnvilAutoUseData AnvilAutoUse;
    }
}