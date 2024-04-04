using System;
using System.Linq;
using Infrastructure.Extensions.DataExtensions;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Field.Slot;
using Infrastructure.Services.ProgressLoad;
using Infrastructure.Services.ProgressLoad.Connection;
using Sources.ItemBase;
using Sources.SlotBase;

namespace Sources.GameFieldBase
{
    public class GameField : IProgressWriter
    {
        private const int GridSize = 40;
        private readonly IProgressProvider _progressProvider;

        private readonly Slot[] _grid;
        
        public event Action SlotsMerged, StoredItemUpdated;
        
        public GameField(IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _grid = new Slot[GridSize];
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
            _grid.FirstOrDefault(slot => slot.IsLocked)?.Unlock();

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
                MergeSlots(firstSlot, secondSlot);
                SlotsMerged?.Invoke();
            }
        }

        /// <summary>
        /// Upgrades slot a, removes item from slot b.
        /// </summary>
        private void MergeSlots(Slot a, Slot b)
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


        public void OnStoredItemUpdated() => 
            StoredItemUpdated?.Invoke();

        public void SaveProgress(GameProgress progress) => 
            progress.GameField = _grid.ToSerializable();

        public void LoadProgress(GameProgress progress)
        {
            SlotData[] savedGrid = progress.GameField.Grid;
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

        public Slot[] Grid => _grid;
    }
}