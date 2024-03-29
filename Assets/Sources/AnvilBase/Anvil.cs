using System;
using Infrastructure.Configurations.Anvil;
using Sources.ItemBase;
using Sources.SlotsHolderBase;

namespace Sources.AnvilBase
{
    /// <summary>
    /// Creates and places the items on a grid.
    /// Parametrized by AnvilConfig.
    /// </summary>
    public class Anvil 
    {
        private readonly SlotsHolder _slotsHolder;

        private int _maxCharges;
        private int _chargesLeft;

        private int _craftingItemLevel;
        public event Action AnvilUsed;

        public Anvil(SlotsHolder slotsHolder, AnvilConfig anvilConfig)
        {
            _slotsHolder = slotsHolder;
            _maxCharges = anvilConfig.MaxAnvilCharges;
            _craftingItemLevel = anvilConfig.CraftingItemLevel;
            _chargesLeft = _maxCharges;
        }

        /// <summary>
        /// Adds max-charges to already existing amount of charges, if it is not full
        /// </summary>
        public void RefillCharges(out bool isSucceeded)
        {
            isSucceeded = false;
            if (!IsFullOfCharges)
            {
                isSucceeded = true;
                AddCharge(_maxCharges);
            }
        }

        /// <summary>
        /// Places an item on a grid (if there is a free slot) and spends one charge.
        /// </summary>
        public void CraftItem()
        {
            if (_chargesLeft > 0)
            {
                _slotsHolder.PlaceItem(new Item(_craftingItemLevel), out bool isSucceeded);
                if (isSucceeded)
                    SpendCharge();
            }
        }
        
        /// <param name="amount">amount of charges to add</param>
        public void AddCharge(int amount)
        {
            _chargesLeft += amount;
            AnvilUsed?.Invoke();
        }

        /// <summary>
        /// Spends one anvil charge.
        /// </summary>
        private void SpendCharge()
        {
            _chargesLeft--;
            AnvilUsed?.Invoke();
        }

        public bool IsFullOfCharges => _chargesLeft >= _maxCharges;
        public bool IsCompletelyCharged => _chargesLeft >= _maxCharges;
        public int ChargesLeft => _chargesLeft;
        public int MaxCharges => _maxCharges;
    }
}