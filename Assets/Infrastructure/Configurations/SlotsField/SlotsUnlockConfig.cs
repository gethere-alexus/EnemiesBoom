using UnityEngine;

namespace Infrastructure.Configurations.SlotsField
{
    [CreateAssetMenu(menuName = ("Configurations/Slots/SlotsUnlockConfig"))]
    public class SlotsUnlockConfig : ScriptableObject, IConfiguration
    {
        [Tooltip("Starts slot unlocking at level")]
        public int StartUnlockingLevel;

        [Tooltip("Unlock a slot every x level record")]
        public int UnlockStep;

        [Tooltip("Amount of unlocking slots per step")]
        public int UnlockSlotsPerStep;
    }
}