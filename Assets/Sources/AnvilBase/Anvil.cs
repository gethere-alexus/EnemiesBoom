using System;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Anvil;
using Sources.ItemBase;
using Sources.SlotsHolderBase;

namespace Sources.AnvilBase
{
    public class Anvil 
    {
        private readonly SlotsHolder _slotsHolder;

        private int _maxCharges;
        private int _chargesLeft;

        private int _craftingArrowLevel;
        public event Action AnvilUsed;

        public Anvil(SlotsHolder slotsHolder, AnvilConfig anvilConfig)
        {
            _slotsHolder = slotsHolder;
            _maxCharges = anvilConfig.MaxAnvilCharges;
            _craftingArrowLevel = anvilConfig.CraftingArrowLevel;
            _chargesLeft = _maxCharges;
        }

        public void RefillCharges(out bool isSucceeded)
        {
            isSucceeded = false;
            if (!IsFullOfCharges)
            {
                isSucceeded = true;
                AddCharge(_maxCharges);
            }
        }

        public void CraftArrow()
        {
            if (_chargesLeft > 0)
            {
                _slotsHolder.PlaceItem(new Item(_craftingArrowLevel), out bool isSucceeded);
                if (isSucceeded)
                    SpendCharge();
            }
        }

        public void AddCharge(int amount)
        {
            _chargesLeft += amount;
            AnvilUsed?.Invoke();
        }

        private void SpendCharge()
        {
            _chargesLeft--;
            AnvilUsed?.Invoke();
        }

        public bool IsFullOfCharges => _chargesLeft >= _maxCharges;


        public int ChargesLeft => _chargesLeft;
        public int MaxCharges => _maxCharges;
        public bool IsCompletelyCharged => _chargesLeft >= _maxCharges;
    }
}