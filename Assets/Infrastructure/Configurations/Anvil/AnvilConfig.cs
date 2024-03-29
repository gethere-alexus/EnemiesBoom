using UnityEngine;

namespace Infrastructure.Configurations.Anvil
{
    /// <summary>
    /// Configuration for anvil.
    /// </summary>
    [CreateAssetMenu(menuName = ("Configurations/Anvil/AnvilConfig"))]
    public class AnvilConfig : ScriptableObject , IConfiguration
    {
        public int MaxAnvilCharges;
        public int CraftingItemLevel;
    }
}