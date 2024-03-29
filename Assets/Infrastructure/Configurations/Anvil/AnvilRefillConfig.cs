using UnityEngine;

namespace Infrastructure.Configurations.Anvil
{
    /// <summary>
    /// Configuration for anvil refiller extension.
    /// </summary>
    [CreateAssetMenu(menuName = ("Configurations/Anvil/AnvilRefillConfig"))]
    public class AnvilRefillConfig : ScriptableObject, IConfiguration
    {
        [Tooltip("Initially available amount of refill charges")]public int RefillCharges;
    }
}