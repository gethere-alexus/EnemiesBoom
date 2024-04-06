using System;

namespace Infrastructure.Configurations
{
    [Serializable]
    public class SlotsUnlockConfig 
    {
        public int StartUnlockingLevel;
        public int UnlockStep;
        public int UnlockSlotsPerStep;
    }
}