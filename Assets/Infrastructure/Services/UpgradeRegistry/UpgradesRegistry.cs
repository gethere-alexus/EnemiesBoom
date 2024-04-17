using System.Collections.Generic;
using Infrastructure.Configurations.Config;
using Infrastructure.PrefabPaths;
using Infrastructure.Services.PrefabLoad;

namespace Infrastructure.Services.UpgradeRegistry
{
    public class UpgradesRegistry : IUpgradesRegistry
    {
        public List<IUpgradable> Upgradeables { get; } = new ();
        
        private readonly List<UpgradeConfiguration> _upgradeData;

        public UpgradesRegistry(IPrefabLoader prefabLoader)
        {
            var content = prefabLoader.LoadPrefab<ConfigContainer>(PersistentDataPaths.GameConfig)
                .ConfigContent;

            _upgradeData = content.UpgradeData;
        }

        public void Register(IUpgradable toRegister)
        {
            if(!Upgradeables.Contains(toRegister))
                Upgradeables.Add(toRegister);
        }

        public void ClearRegistry() => 
            Upgradeables.Clear();

        public void LoadUpgradesData()
        {
            foreach (IUpgradable upgradable in Upgradeables)
            {
                UpgradeConfiguration toLoad = _upgradeData.Find(data => data.UpgradeID == upgradable.RequiredUpgradeID);
                
                upgradable.LoadUpgradeInformation(toLoad);
            }
        }
    }
}