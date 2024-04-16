using NorskaLib.Spreadsheets;
using UnityEngine;

namespace Infrastructure.Configurations.InitialProgress
{
    /// <summary>
    /// Stores the initial game progress.
    /// </summary>
    [CreateAssetMenu(menuName = ("Containers/Initial Progress Container"))]
    public class InitialProgressContainer : SpreadsheetsContainerBase
    {
        [SpreadsheetContent] public InitialProgressContent InitProgressContent;
    }
}