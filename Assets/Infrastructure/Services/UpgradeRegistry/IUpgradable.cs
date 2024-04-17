namespace Infrastructure.Services.UpgradeRegistry
{
    public interface IUpgradable
    {
        void Upgrade(out bool isSucceeded);
        void Upgrade();
        int UpgradePrice { get; }
        int CurrentUpgradeStage { get; }
        bool IsUpgradable { get; }
        bool IsCompletelyUpgraded { get; }

        void LoadUpgradeInformation(UpgradeConfiguration upgradeConfiguration);
        UpgradeConfiguration UpgradeInformation { get; }
        int RequiredUpgradeID { get; }
    }
}