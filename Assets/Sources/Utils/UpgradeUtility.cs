namespace Sources.Utils
{
    public static class UpgradeUtility
    {
        public static int GetStagePrice(int initialPrice, float stageMultiplication, int startUpgradeStage, int currentUpgradeStage)
        {
            bool isFirstUpgradeStage = currentUpgradeStage == startUpgradeStage;
            float multiplication = isFirstUpgradeStage ? 1 : stageMultiplication * (currentUpgradeStage - 1);
            int price = (int)(initialPrice * multiplication);
            return price;
        }
    }
}