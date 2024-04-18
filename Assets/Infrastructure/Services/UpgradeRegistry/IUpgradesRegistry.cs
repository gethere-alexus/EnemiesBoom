using System.Collections.Generic;
using Infrastructure.Services.UpgradeRegistry.Upgrades;

namespace Infrastructure.Services.UpgradeRegistry
{
    public interface IUpgradesRegistry
    {
        void LoadUpgradesData();
        UpgradeBase[] Upgrades { get; }
        void CreateUpgrades();
    }
}