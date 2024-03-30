using System;
using Infrastructure.Configurations.Anvil;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.Services.ProgressProvider;
using Sources.ItemBase;
using Sources.SlotsHolderBase;

namespace Sources.AnvilBase
{
    /// <summary>
    /// Creates and places the items on a grid.
    /// Parametrized by AnvilConfig.
    /// </summary>
    public class Anvil : IProgressWriter
    {
        private readonly SlotsHolder _slotsHolder;
        private readonly IProgressProvider _progressProvider;

        private int _maxCharges;
        private int _chargesLeft;

        private int _craftingItemLevel;
        public event Action ItemCrafted;
        public event Action ChargesUpdated;

        /// <summary>
        /// Constructs if save file is not found
        /// </summary>
        public Anvil(SlotsHolder slotsHolder, IProgressProvider progressProvider, AnvilConfig anvilConfig)
        {
            _slotsHolder = slotsHolder;
            _progressProvider = progressProvider;

            _maxCharges = anvilConfig.MaxAnvilCharges;
            _chargesLeft = _maxCharges;
            _craftingItemLevel = anvilConfig.CraftingItemLevel;
        }

        /// <summary>
        /// Constructs with information from save file
        /// </summary>
        public Anvil(SlotsHolder slotsHolder, IProgressProvider progressProvider, AnvilProgress anvilProgress)
        {
            _slotsHolder = slotsHolder;
            _progressProvider = progressProvider;

            _maxCharges = anvilProgress.MaxCharges;
            _chargesLeft = anvilProgress.ChargesLeft;
            _craftingItemLevel = anvilProgress.CraftingItemLevel;
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
                {
                    SpendCharge();
                    ItemCrafted?.Invoke();
                }
            }
        }

        /// <param name="amount">amount of charges to add</param>
        public void AddCharge(int amount)
        {
            _chargesLeft += amount;
            SaveProgress();
            ChargesUpdated?.Invoke();
        }

        public void SaveProgress()
        {
            AnvilProgress toSave = new AnvilProgress()
            {
                ChargesLeft = _chargesLeft,
                CraftingItemLevel = _craftingItemLevel,
                MaxCharges = _maxCharges,
            };
                
            _progressProvider.SaveProgress(toSave);
        }

        public void LoadProgress()
        {
            AnvilProgress loadedProgress = _progressProvider.LoadProgress<AnvilProgress>();

            _maxCharges = loadedProgress.MaxCharges;
            _chargesLeft = loadedProgress.ChargesLeft;
            _craftingItemLevel = loadedProgress.CraftingItemLevel;
        }

        /// <summary>
        /// Spends one anvil charge.
        /// </summary>
        private void SpendCharge()
        {
            _chargesLeft--;
            SaveProgress();
            ChargesUpdated?.Invoke();
        }

        public bool IsFullOfCharges => _chargesLeft >= _maxCharges;

        public bool IsCompletelyCharged => _chargesLeft >= _maxCharges;

        public int ChargesLeft => _chargesLeft;

        public int MaxCharges => _maxCharges;
    }
}