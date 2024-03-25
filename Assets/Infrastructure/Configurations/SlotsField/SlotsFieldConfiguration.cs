using UnityEngine;

namespace Infrastructure.Configurations
{
    [CreateAssetMenu(menuName = ("Configurations/SlotsHolderConfig"))]
    public class SlotsFieldConfiguration : ScriptableObject, IConfiguration
    {
        public int InitialSlots = 40;
        public int UnlockedSlots = 20;

        private void OnValidate()
        {
            if (UnlockedSlots > InitialSlots)
                UnlockedSlots = InitialSlots;
        }
    }
}