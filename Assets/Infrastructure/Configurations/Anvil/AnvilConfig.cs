using UnityEngine;

namespace Infrastructure.Configurations.Anvil
{
    [CreateAssetMenu(menuName = ("Configurations/AnvilConfig"))]
    public class AnvilConfig : ScriptableObject , IConfiguration
    {
        public int MaxAnvilCharges;
        public int CraftingArrowLevel;
    }
}