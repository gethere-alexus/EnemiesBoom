using System;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad.Connection;

namespace Infrastructure.Services.UpgradeRegistry.Upgrades
{
    public abstract class UpgradeBase : IConfigReader, IProgressWriter
    {
        public abstract event Action ConfigLoaded;
        
        public abstract void Upgrade(out bool isSucceeded);

        public virtual void Upgrade() => 
            Upgrade(out bool isSucceeded);

        public abstract void LoadUpgradeConfiguration(UpgradeConfiguration upgradeConfiguration);
        public abstract void LoadConfiguration(ConfigContent configContainer);
        public abstract void LoadProgress(GameProgress progress);
        public abstract void SaveProgress(GameProgress progress);

        public abstract int RequiredUpgradeID { get; }
        public abstract UpgradeConfiguration UpgradeConfiguration { get; }
        
        public abstract int CurrentUpgradeStage { get; }
        public abstract int UpgradePrice { get; }
        
        public abstract bool IsUpgradable { get; }
        public abstract bool IsCompletelyUpgraded { get; }
    }
}