using System.Collections.Generic;

namespace Infrastructure.Services.UpgradeRegistry
{
    public interface IUpgradesRegistry
    {
        void Register(IUpgradable toRegister);
        void LoadUpgradesData();
        void ClearRegistry();
        
        List<IUpgradable> Upgradeables { get; }
    }
}