using System;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Anvil;
using Sources.AnvilBase;

namespace Sources.AnvilChargesRefillerBase
{
    /// <summary>
    /// Refills the anvil
    /// </summary>
    public class AnvilChargesRefill
    {
        private readonly Anvil _anvil;
        private int _charges;
        
        public event Action RefillChargeSpent;
        public AnvilChargesRefill(Anvil anvil, AnvilRefillConfig anvilRefillConfig, Action onConstructed = null)
        {
            _anvil = anvil;
            _charges = anvilRefillConfig.RefillCharges;
            onConstructed?.Invoke();
        }

        public void RefillAnvil()
        {
            if (IsRefillable)
            {
                _anvil.RefillCharges(out bool isOperationSucceeded);
                if(isOperationSucceeded)
                    SpendRefillingCharge();
            }
        }

        private void SpendRefillingCharge()
        {
            _charges--;
            RefillChargeSpent?.Invoke();
        }

        public bool IsRefillable => _charges > 0 && !_anvil.IsFullOfCharges;
        public int Charges => _charges;
    }
}