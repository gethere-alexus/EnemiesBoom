using UnityEngine;

namespace Infrastructure.Configurations.Anvil
{
    /// <summary>
    /// Configuration for anvil auto-refilling extension.
    /// </summary>
    [CreateAssetMenu(menuName = ("Configurations/Anvil/AnvilAutoRefillConfig"))]
    public class AnvilAutoRefillConfig : ScriptableObject, IConfiguration
    {
        [Tooltip("Cooldown for auto refilling in secs")] public float RefillCoolDown;

        [Tooltip("Recharges added per iteration")] public int AmountChargesToAdd;
    }
}