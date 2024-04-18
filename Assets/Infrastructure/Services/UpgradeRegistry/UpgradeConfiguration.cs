using System;

namespace Infrastructure.Services.UpgradeRegistry
{
    [Serializable]
    public class UpgradeConfiguration
    {
        public int UpgradeID;
        
        public string UpgradeName;
        public string UpgradeDescription;
        
        public int StartUpgradeStage, MaxUpgradeStage;
       
        public int InitialPrice;
        public float UpgradePriceMultiplication;
    }
}