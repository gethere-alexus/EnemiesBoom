using Infrastructure.Configurations.SlotsField;
using UnityEngine;

namespace Sources.SlotsHolderBase.Extensions.SlotsUnlock
{
    /// <summary>
    /// Unlocks slots if item on the grid is reached certain level
    /// </summary>
    public class SlotsUnlocker : MonoBehaviour
    {
        private SlotsHolder _slotsHolder;
        
        private int _unlockingLevel;
        private int _unlockingStep;
        private int _unlockingSlotsPerStep;

        public void Construct(SlotsHolder slotsHolder, SlotsUnlockConfig config)
        {
            _unlockingLevel = config.StartUnlockingLevel;
            _unlockingStep = config.UnlockStep;
            _unlockingSlotsPerStep = config.UnlockSlotsPerStep;

            foreach (var slot in slotsHolder.Grid)
            {
                slot.StoredItemUpdated += OnStoredItemUpdated;
            }

            _slotsHolder = slotsHolder;
        }

        private void OnStoredItemUpdated() => 
            UnlockSlots();

        private void UnlockSlots()
        {
            int highestItemLevel = _slotsHolder.MaxStoredItemLevel();

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
                _slotsHolder.UnlockSlot();
                Debug.Log("Unlocked");
            }

            _unlockingLevel += _unlockingStep;
        }

        private void OnDisable()
        {
            foreach (var slot in _slotsHolder.Grid)
            {
                slot.StoredItemUpdated -= OnStoredItemUpdated;
            }
        }
    }
}