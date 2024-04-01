using System;
using Infrastructure.Configurations.Anvil;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Anvil;
using Infrastructure.Services.ProgressProvider;

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
        
        public event Action RefillChargeSpent;
        
        /// <summary>
        /// Loads data from config
        /// </summary>
        public AnvilChargesRefill(Anvil anvil, IProgressProvider progressProvider, AnvilRefillConfig anvilRefillConfig, Action onConstructed = null)
        {
            _anvil = anvil;
            _progressProvider = progressProvider;
            _charges = anvilRefillConfig.RefillCharges;
            onConstructed?.Invoke();
        }

        /// <summary>
        /// Loads data from save 
        /// </summary>
        public AnvilChargesRefill(Anvil anvil, IProgressProvider progressProvider, AnvilRefillData data, Action onConstructed = null)
        {
            _anvil = anvil;
            _progressProvider = progressProvider;
            _charges = data.ChargesLeft;
            onConstructed?.Invoke();
        }

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
                SaveProgress();
            }
        }

        public void LoadProgress()
        {
            AnvilRefillData save = _progressProvider.LoadProgress<AnvilRefillData>();
            _charges = save.ChargesLeft;
        }

        public void SaveProgress()
        {
            AnvilRefillData toSave = new AnvilRefillData()
            {
                ChargesLeft = _charges,
            };
            
            _progressProvider.SaveProgress(toSave);
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