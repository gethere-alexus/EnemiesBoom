using System;
using System.Linq;
using Infrastructure.Configurations.SlotsField;
using Sources.ItemBase;
using Sources.SlotBase;

namespace Sources.SlotsHolderBase
{
    /// <summary>
    /// Holds all the slots from firstSlot game grid
    /// </summary>
    public class SlotsHolder
    {
        private readonly int _initialSlots;
        private readonly Slot[] _grid;
        
        private int _unlockedSlots;

        public event Action StorageInformationUpdated;

        public SlotsHolder(SlotsFieldConfiguration config)
        {
            _initialSlots = config.InitialSlots;
            _unlockedSlots = config.UnlockedSlots;

            _grid = new Slot[_initialSlots];
        }

        /// <summary>
        /// Placing arrow on the first free slot
        /// </summary>
        public void PlaceItem(Item itemToPlace, out bool isOperationSucceeded)
        {
            Slot storage = GetEmptySlot();
            
            isOperationSucceeded = false;
            if (storage != null)
            {
                storage.PutItem(itemToPlace);
                isOperationSucceeded = true;
            }
        }

        /// <summary>
        /// Unlocks first locked slot in the grid.
        /// </summary>
        public void UnlockSlot() => 
            _grid.First(slot => slot.IsLocked)?.Unlock();

        /// <summary>
        /// Merges an item from two slots, returns upgraded to the first slot.
        /// </summary>
        public void TryMergeSlotsItems(Slot firstSlot, Slot secondSlot = null)
        {
            secondSlot ??= GetSlotWithItemLevel(firstSlot.StoringItem.Level, firstSlot);
            
            bool isTheSameItems = firstSlot == secondSlot;
            bool doesContainItems = firstSlot.StoringItem != null && secondSlot?.StoringItem != null;
            bool isTheSameLevel = firstSlot.StoringItem?.Level == secondSlot?.StoringItem?.Level;

            bool isAbleToMerge = !isTheSameItems && doesContainItems && isTheSameLevel;
            
            if (isAbleToMerge)
            {
                firstSlot.StoringItem.Upgrade();
                secondSlot.RemoveStoringItem();
            }
        }
        
        /// <summary>
        /// Iterates through all unlocked slots, returning maximum level of stored item
        /// </summary>
        /// <returns>maximum level of stored item in grid</returns>
        public int MaxStoredItemLevel()
        {
            int maxLevel = 0;
            
            if (_grid != null)
            {
                foreach (var slot in _grid.Where(slot => !slot.IsLocked && !slot.IsEmpty))
                {
                    int storedLevel = slot.StoringItem.Level;
                    maxLevel = storedLevel > maxLevel ? storedLevel : maxLevel;
                }
            }

            return maxLevel;
        }
        
        public void OnGridBuild()
        {
            foreach (var slot in _grid)
            {
                slot.ItemInformationUpdated += () => StorageInformationUpdated?.Invoke();
            }
        }

        /// <summary>
        /// Iterate through all unlocked slots and searching for slot which has item with level
        /// </summary>
        /// <param name="storingItemLevel">searching item level</param>
        /// <param name="excludeFromSearch">excluded from search slot</param>
        /// <returns>Slot which stores an item with the certain level</returns>
        private Slot GetSlotWithItemLevel(int storingItemLevel, Slot excludeFromSearch = null)
        {
            if (_grid != null)
            {
                foreach (Slot slot in _grid.Where(slot => !slot.IsLocked && !slot.IsEmpty))
                {
                    if (slot != excludeFromSearch && slot.StoringItem.Level == storingItemLevel)
                        return slot;
                }
            }
            return null;
        }

        /// <summary>
        /// Looping through the all unlocked slots, looking for first empty slot
        /// </summary>
        /// <returns>returns the first unlocked slot. Null if there is no free ones</returns>
        private Slot GetEmptySlot()
        {
            if (_grid != null)
            {
                foreach (Slot slot in _grid.Where(slot => !slot.IsLocked))
                {
                    if (slot.IsEmpty)
                        return slot;
                }
            }
            return null;
        }

        public int InitialSlots => _initialSlots;
        public int UnlockedSlots => _unlockedSlots;

        public Slot[] Grid => _grid;
    }
}