using NorskaLib.Spreadsheets;
using UnityEngine;

namespace Infrastructure.Configurations.Config
{
    [CreateAssetMenu(menuName = ("Containers/Config Container"))]
    public class ConfigContainer : SpreadsheetsContainerBase
    {
        [SpreadsheetContent] public ConfigContent ConfigContent;
    }
}