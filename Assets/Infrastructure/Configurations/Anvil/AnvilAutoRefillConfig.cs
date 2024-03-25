﻿using UnityEngine;

namespace Infrastructure.Configurations.Anvil
{
    [CreateAssetMenu(menuName = ("Configurations/AnvilAutoRefillConfig"))]
    public class AnvilAutoRefillConfig : ScriptableObject, IConfiguration
    {
        [Tooltip("Cooldown for auto refilling in secs")] public float RefillCoolDown;

        [Tooltip("Recharges added per iteration")] public int AmountChargesToAdd;
    }
}