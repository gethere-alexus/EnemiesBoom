namespace Sources.WalletBase
{
    public static class WalletUtility
    {
        public static string ConvertToPrettyFormat(int value, char dividingChar)
        {
            string stringToReturn = value.ToString();

            if (value > 999)
            {
                int charsAmount = 0;
                for (int i = stringToReturn.Length - 1; i >= 0; i--)
                {
                    if (stringToReturn[i] != dividingChar && (i - 1 != 0 || i != 0)) charsAmount++;
                    if (charsAmount == 3 && i != 0)
                    {
                        stringToReturn = stringToReturn.Substring(0, i) + dividingChar + stringToReturn.Substring(i);
                        charsAmount = 0;
                    }
                }
            }

            return stringToReturn;
        }
    }
}