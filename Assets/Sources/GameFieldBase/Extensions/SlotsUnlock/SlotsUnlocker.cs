using System;
using Infrastructure.Configurations.Config;
using Infrastructure.Services.ConfigLoad;
using UnityEngine;

namespace Sources.GameFieldBase.Extensions.SlotsUnlock
{
    /// <summary>
    /// Unlocks slots if item on the grid is reached certain level
    /// </summary>
    public class SlotsUnlocker : MonoBehaviour, IConfigReader
    {
        private GameField _gameField;

        private int _unlockingLevel;
        private int _unlockingStep;
        private int _unlockingSlotsPerStep;
        public event Action ConfigLoaded;


        public void Construct(GameField gameField)
        {
            _gameField = gameField;
            _unlockingLevel = 5;
            _unlockingStep = 1;
            _unlockingSlotsPerStep = 1;
            _gameField.StoredItemUpdated += OnStoredItemUpdated;
        }

        private void OnStoredItemUpdated() => 
            UnlockSlots();

        private void UnlockSlots()
        {
            int highestItemLevel = _gameField.MaxStoredItemLevel();

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
                _gameField.UnlockSlot();
            }

            _unlockingLevel += _unlockingStep;
        }

        private void OnDisable() => 
            _gameField.StoredItemUpdated -= OnStoredItemUpdated;

        public void LoadConfiguration(ConfigContent configContainer)
        {
            _unlockingLevel = configContainer.SlotsUnlock.StartUnlockingLevel;
            _unlockingStep = configContainer.SlotsUnlock.UnlockStep;
            _unlockingSlotsPerStep = configContainer.SlotsUnlock.UnlockSlotsPerStep;
            ConfigLoaded?.Invoke();
        }
    }
}