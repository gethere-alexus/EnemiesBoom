using UnityEngine;

namespace Infrastructure.Configurations.SlotsField
{
    /// <summary>
    /// Configuration for items auto-merging extension.
    /// </summary>
    [CreateAssetMenu(menuName = ("Configurations/Slots/AutoMergeConfig"))]
    public class AutoMergerConfig : ScriptableObject, IConfiguration
    {
        [Tooltip("In seconds")]public float UsageCoolDown;
    }
}