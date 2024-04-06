using System;
using NorskaLib.Spreadsheets;

namespace Infrastructure.Configurations.Config
{
    [Serializable]
    public class ConfigContent
    {
        [SpreadsheetPage("SlotsUnlock")] public SlotsUnlockConfig SlotsUnlockConfig;
    }
}