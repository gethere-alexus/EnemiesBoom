using System.Collections.Generic;
using Infrastructure.Configurations.Config;
using Infrastructure.PrefabPaths;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.UpgradeRegistry.Upgrades;
using Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilAutoUseUpgrade;
using Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilReloadSpeedUpgrade;
using Infrastructure.Services.UpgradeRegistry.Upgrades.CraftingItemLevel;
using Infrastructure.Services.UpgradeRegistry.Upgrades.SlotMergeUpgrade;
using Zenject;

namespace Infrastructure.Services.UpgradeRegistry
{
    public class UpgradesRegistry : IUpgradesRegistry
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _container;
        private UpgradeBase[] _upgrades;
        
        private readonly List<UpgradeConfiguration> _upgradeData;
        
        public UpgradesRegistry(IProgressProvider progressProvider, IConfigProvider configProvider, IPrefabLoader prefabLoader, DiContainer container)
        {
            _progressProvider = progressProvider;
            _configProvider = configProvider;
            _container = container;
            
            var content = prefabLoader.LoadPrefab<ConfigContainer>(PersistentDataPaths.GameConfig)
                .ConfigContent;

            _upgradeData = content.UpgradeData;
        }
        
        public void LoadUpgradesData()
        {
            foreach (UpgradeBase upgradable in Upgrades)
            {
                UpgradeConfiguration toLoad = _upgradeData.Find(data => data.UpgradeID == upgradable.RequiredUpgradeID);
                
                upgradable.LoadUpgradeConfiguration(toLoad);
            }
        }

        public void CreateUpgrades()
        {
            _upgrades = new UpgradeBase[]
            {
                _container.Instantiate<AnvilCraftingItemUpgrade>(),
                _container.Instantiate<AnvilReloadSpeedUpgrade>(),
                _container.Instantiate<AnvilAutoUseUpgrade>(),
                _container.Instantiate<SlotsMergeUpgrade>(),
            };

            foreach (var upgrade in _upgrades)
            {
                _progressProvider.RegisterObserver(upgrade);
                _configProvider.RegisterLoader(upgrade);
            }
        }
        public UpgradeBase[] Upgrades => _upgrades;
    }
}