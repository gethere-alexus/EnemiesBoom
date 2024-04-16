using System;
using Infrastructure.Configurations.Config;
using Infrastructure.Services.ConfigLoad;
using UnityEngine;

namespace Sources.Item.ItemFieldBase.Extensions.SlotsUnlock
{
    /// <summary>
    /// Unlocks slots if item on the grid is reached certain level
    /// </summary>
    public class SlotsUnlocker : MonoBehaviour, IConfigReader
    {
        private ItemField _itemField;

        private int _unlockingLevel;
        private int _unlockingStep;
        private int _unlockingSlotsPerStep;
        public event Action ConfigLoaded;


        public void Construct(ItemField itemField)
        {
            _itemField = itemField;
            _unlockingLevel = 5;
            _unlockingStep = 1;
            _unlockingSlotsPerStep = 1;
            _itemField.StoredItemUpdated += OnStoredItemUpdated;
        }

        private void OnStoredItemUpdated() => 
            UnlockSlots();

        private void UnlockSlots()
        {
            int highestItemLevel = _itemField.MaxStoredItemLevel();

            if (highestItemLevel - _unlockingLevel >= _unlockingStep)
            {
                MakeUnlockStep();
                UnlockSlots();
            }
        }

        private void MakeUnlockStep()
        {
            for (int i = 0; i < _unlockingSlotsPerStep; i++)
            {
                _itemField.UnlockSlot();
            }

            _unlockingLevel += _unlockingStep;
        }

        private void OnDisable() => 
            _itemField.StoredItemUpdated -= OnStoredItemUpdated;

        public void LoadConfiguration(ConfigContent configContainer)
        {
            _unlockingLevel = configContainer.SlotsUnlock.StartUnlockingLevel;
            _unlockingStep = configContainer.SlotsUnlock.UnlockStep;
            _unlockingSlotsPerStep = configContainer.SlotsUnlock.UnlockSlotsPerStep;
            ConfigLoaded?.Invoke();
        }
    }
}