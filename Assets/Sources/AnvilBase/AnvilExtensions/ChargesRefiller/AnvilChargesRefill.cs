using System;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Anvil;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.ProgressLoad.Connection;

namespace Sources.AnvilBase.AnvilExtensions.ChargesRefiller
{
    /// <summary>
    /// Refills the anvil
    /// </summary>
    public class AnvilChargesRefill : IProgressWriter
    {
        private readonly Anvil _anvil;
        private readonly IProgressProvider _progressProvider;
        private int _charges;

        public event Action RefillChargesUpdated;

        public AnvilChargesRefill(Anvil anvil) =>
            _anvil = anvil;

        /// <summary>
        /// If anvil is not full - refills it.
        /// </summary>
        public void RefillAnvil()
        {
            if (IsRefillable)
            {
                _anvil.RefillCharges(out bool isOperationSucceeded);
                if (isOperationSucceeded)
                    SpendRefillingCharge();
            }
        }

        public AnvilRefillData SaveProgress() => new()
        {
            Charges = _charges,
        };

        public void LoadProgress(GameProgress progress)
        {
            _charges = progress.AnvilExtensions.AnvilRefill.Charges;
            RefillChargesUpdated?.Invoke();
        }
        

        public void SaveProgress(GameProgress progress) => 
            progress.AnvilExtensions.AnvilRefill.Charges = _charges;

        private void SpendRefillingCharge()
        {
            _charges--;
            RefillChargesUpdated?.Invoke();
        }

        public bool IsRefillable => _charges > 0 && !_anvil.IsFullOfCharges;

        public int Charges => _charges;
    }
}