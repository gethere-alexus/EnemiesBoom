using System;

namespace Infrastructure.ProgressData
{
    [Serializable]
    public class UpgradesData
    {
        public UpgradeData AnvilItemUpgradeData;
        public UpgradeData AnvilReloadUpgradesData;
        public UpgradeData AnvilAutoUseUpgradesData;
        public UpgradeData SlotsMergeUpgradeData;
    }
}