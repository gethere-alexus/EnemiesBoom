using UnityEngine;

namespace Infrastructure.Configurations.SlotsField
{
    [CreateAssetMenu(menuName = ("ConfigDataBase"))]
    public class ConfigDataBase : ScriptableObject
    {
        public SlotsUnlockConfig SlotsUnlockConfig;
    }
}