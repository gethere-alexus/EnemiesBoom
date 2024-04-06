using NorskaLib.Spreadsheets;
using UnityEngine;

namespace Infrastructure.Configurations.InitialProgress
{
    [CreateAssetMenu(menuName = ("Containers/Initial Progress Container"))]
    public class InitialProgressContainer : SpreadsheetsContainerBase
    {
        [SpreadsheetContent] public InitialProgressContent InitProgressContent;
    }
}