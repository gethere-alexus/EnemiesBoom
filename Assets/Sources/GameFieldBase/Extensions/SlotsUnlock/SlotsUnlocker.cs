using UnityEngine;

namespace Sources.GameFieldBase.Extensions.SlotsUnlock
{
    /// <summary>
    /// Unlocks slots if item on the grid is reached certain level
    /// </summary>
    public class SlotsUnlocker : MonoBehaviour
    {
        private GameField _gameField;
        
        private int _unlockingLevel;
        private int _unlockingStep;
        private int _unlockingSlotsPerStep;

        public void Construct(GameField gameField)
        {
            _gameField = gameField;
            
            _unlockingLevel = 5;
            _unlockingStep = 1;
            _unlockingSlotsPerStep = 2;

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
    }
}