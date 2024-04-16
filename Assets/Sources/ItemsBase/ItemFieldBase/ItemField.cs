using System;
using System.Linq;
using Infrastructure.Extensions.DataExtensions;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Field.Slot;
using Infrastructure.Services.ProgressLoad.Connection;
using Sources.ItemsBase.ItemSlotBase;

namespace Sources.ItemsBase.ItemFieldBase
{
    public class ItemField : IProgressWriter
    {
        private const int GridSize = 40;

        private readonly  ItemSlot[] _grid;
        
        public event Action SlotsMerged, StoredItemUpdated;
        
        public ItemField() => 
            _grid = new ItemSlot[GridSize];

        /// <summary>
        /// Placing arrow on the first free slot
        /// </summary>
        public void PlaceItem(ItemBase.Item itemToPlace, out bool isOperationSucceeded)
        {
            ItemSlot storage = GetEmptySlot();

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
            _grid.FirstOrDefault(slot => slot.IsLocked)?.Unlock();

        public void TryMergeItems(ItemSlot firstItemSlot)
        {
            ItemSlot secondItemSlot = GetSlotWithItemLevel(firstItemSlot.StoringItem.Level, firstItemSlot);
            TryMergeItems(firstItemSlot, secondItemSlot);
        }

        /// <summary>
        /// Merges an item from two slots, returns upgraded to the first slot.
        /// </summary>
        public void TryMergeItems(ItemSlot firstItemSlot, ItemSlot secondItemSlot)
        {
            bool isTheSameItems = firstItemSlot == secondItemSlot;
            bool doesContainItems = firstItemSlot.StoringItem != null && secondItemSlot?.StoringItem != null;
            bool isTheSameLevel = firstItemSlot.StoringItem?.Level == secondItemSlot?.StoringItem?.Level;

            bool isAbleToMerge = !isTheSameItems && doesContainItems && isTheSameLevel;

            if (isAbleToMerge)
            {
                MergeSlots(firstItemSlot, secondItemSlot);
                SlotsMerged?.Invoke();
            }
        }

        /// <summary>
        /// Upgrades slot a, removes item from slot b.
        /// </summary>
        private void MergeSlots(ItemSlot a, ItemSlot b)
        {
            a.StoringItem?.Upgrade();
            b?.RemoveStoringItem();
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


        /// <summary>
        /// Iterate through all unlocked slots and searching for slot which has item with level
        /// </summary>
        /// <param name="storingItemLevel">searching item level</param>
        /// <param name="excludeFromSearch">excluded from search slot</param>
        /// <returns>Slot which stores an item with the certain level</returns>
        private ItemSlot GetSlotWithItemLevel(int storingItemLevel, ItemSlot excludeFromSearch = null)
        {
            if (_grid != null)
            {
                foreach (ItemSlot slot in _grid.Where(slot => !slot.IsLocked && !slot.IsEmpty))
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
        private ItemSlot GetEmptySlot()
        {
            if (_grid != null)
            {
                foreach (ItemSlot slot in _grid.Where(slot => !slot.IsLocked))
                {
                    if (slot.IsEmpty)
                        return slot;
                }
            }

            return null;
        }


        public void OnStoredItemUpdated() => 
            StoredItemUpdated?.Invoke();

        public void SaveProgress(GameProgress progress) => 
            progress.ItemField = _grid.ToSerializable();

        public void LoadProgress(GameProgress progress)
        {
            SlotData[] savedGrid = progress.ItemField.Grid;
            for (int i = 0; i < _grid.Length; i++)
            {
                _grid[i].RemoveStoringItem();
                _grid[i].Lock();

                if (!savedGrid[i].IsLocked)
                {
                    _grid[i].Unlock();
                    if (savedGrid[i].StoringItem?.Level > 0)
                    {
                        _grid[i].PutItem(savedGrid[i].StoringItem.FromSerializable());
                    }
                }
            }
        }

        public int InitialSlots => 
            _grid.Length;

        public int UnlockedSlots => 
            _grid.Where(slot => !slot.IsLocked).ToArray().Length;

        public ItemSlot FirstEmpty => 
            _grid.FirstOrDefault(slot => slot.IsEmpty);
        public ItemSlot[] Grid => _grid;
    }
}