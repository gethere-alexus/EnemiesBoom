using UnityEngine;

namespace Infrastructure.Configurations.Anvil
{
    /// <summary>
    /// Configuration for anvil auto-using extension.
    /// </summary>
    [CreateAssetMenu(menuName = ("Configurations/Anvil/AnvilAutoUseConfig"))]
    public class AnvilAutoUseConfig : ScriptableObject, IConfiguration
    {
        [Tooltip("In seconds")] public float UsingCooldown;
    }
}