using System;
using Infrastructure.ProgressData;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.ProgressLoad.Connection;
using Sources.ItemsBase.ItemBase;
using Sources.ItemsBase.ItemFieldBase;

namespace Sources.AnvilBase
{
    /// <summary>
    /// Creates and places the items on a grid.
    /// </summary>
    public class Anvil : IProgressWriter
    {
        private readonly ItemField _itemField;

        private int _maxCharges;
        private int _chargesLeft;

        private int _craftingItemLevel;
        public event Action ItemCrafted;
        public event Action ChargesUpdated;
        
        public Anvil(ItemField itemField) => 
            _itemField = itemField;

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
                _itemField.PlaceItem(new Item(_craftingItemLevel), out bool isSucceeded);

                if (isSucceeded)
                {
                    SpendCharge();
                    ItemCrafted?.Invoke();
                }
            }
        }

        public void AddCharge(int amount)
        {
            _chargesLeft += amount;
            ChargesUpdated?.Invoke();
        }

        public void LoadProgress(GameProgress progress)
        {
            _maxCharges = progress.Anvil.MaxCharges;
            _chargesLeft = progress.Anvil.ChargesLeft;
            _craftingItemLevel = progress.Anvil.CraftingItemLevel;
            
            ChargesUpdated?.Invoke();
        }


        public void SaveProgress(GameProgress progress)
        {
            progress.Anvil.MaxCharges = _maxCharges;
            progress.Anvil.ChargesLeft = _chargesLeft;
            progress.Anvil.CraftingItemLevel = _craftingItemLevel;
        }

        /// <summary>
        /// Spends one anvil charge.
        /// </summary>
        private void SpendCharge()
        {
            _chargesLeft--;
            ChargesUpdated?.Invoke();
        }

        public bool IsFullOfCharges => _chargesLeft >= _maxCharges;

        public bool IsCompletelyCharged => _chargesLeft >= _maxCharges;

        public int ChargesLeft => _chargesLeft;

        public int MaxCharges => _maxCharges;
    }
}