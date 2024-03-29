using UnityEngine;

namespace Infrastructure.Configurations.SlotsField
{
    /// <summary>
    /// Configuration for grid initializing.
    /// </summary>
    [CreateAssetMenu(menuName = ("Configurations/Slots/SlotsHolderConfig"))]
    public class SlotsFieldConfiguration : ScriptableObject, IConfiguration
    {
        [Tooltip("Created amount of slots")] public int InitialSlots = 40;
        [Tooltip("Initially unlocked slots")] public int UnlockedSlots = 20;

        private void OnValidate()
        {
            if (UnlockedSlots > InitialSlots)
                UnlockedSlots = InitialSlots;
        }
    }
}