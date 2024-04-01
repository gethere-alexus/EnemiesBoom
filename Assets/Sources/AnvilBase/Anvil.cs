using System;
using Infrastructure.Configurations.Anvil;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Anvil;
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
        public Anvil(SlotsHolder slotsHolder, IProgressProvider progressProvider, AnvilData anvilData)
        {
            _slotsHolder = slotsHolder;
            _progressProvider = progressProvider;

            _maxCharges = anvilData.MaxCharges;
            _chargesLeft = anvilData.ChargesLeft;
            _craftingItemLevel = anvilData.CraftingItemLevel;
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
            AnvilData toSave = new AnvilData()
            {
                ChargesLeft = _chargesLeft,
                CraftingItemLevel = _craftingItemLevel,
                MaxCharges = _maxCharges,
            };
                
            _progressProvider.SaveProgress(toSave);
        }

        public void LoadProgress()
        {
            AnvilData loadedData = _progressProvider.LoadProgress<AnvilData>();

            _maxCharges = loadedData.MaxCharges;
            _chargesLeft = loadedData.ChargesLeft;
            _craftingItemLevel = loadedData.CraftingItemLevel;
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