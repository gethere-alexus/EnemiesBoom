using UnityEngine;

namespace Infrastructure.Configurations.Anvil
{
    [CreateAssetMenu(menuName = ("Configurations/AnvilRefillConfig"))]
    public class AnvilRefillConfig : ScriptableObject, IConfiguration
    {
        public int RefillCharges;
    }
}