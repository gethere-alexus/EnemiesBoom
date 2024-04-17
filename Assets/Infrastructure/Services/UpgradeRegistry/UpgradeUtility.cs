using System;

namespace Infrastructure.Services.UpgradeRegistry
{
    public static class UpgradeUtility
    {
        public static int GetStagePrice(int initialPrice, int stageMultiplication, int startUpgradeStage, int currentUpgradeStage)
        {
            bool isFirstUpgradeStage = currentUpgradeStage == startUpgradeStage;
            int multiplication = isFirstUpgradeStage ? 1 : stageMultiplication * (currentUpgradeStage - 1);
            int price = (initialPrice * multiplication);
            return price;
        }
    }
}