using NorskaLib.Spreadsheets;
using UnityEngine;

namespace Infrastructure.Configurations.Config
{
    /// <summary>
    /// Stores game configuration, which is being loaded from Google Spreadsheets.
    /// </summary>
    [CreateAssetMenu(menuName = ("Containers/Config Container"))]
    public class ConfigContainer : SpreadsheetsContainerBase
    {
        [SpreadsheetContent] public ConfigContent ConfigContent;
    }
}