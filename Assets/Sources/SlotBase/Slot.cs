using System;
using Sources.ItemBase;
using Sources.SlotsHolderBase;

namespace Sources.SlotBase
{
    /// <summary>
    /// A single cell where the item can be stored, being instantiated from SlotsHolder
    /// By default, cell doesn't contain any arrow and it is unlocked.
    /// </summary>
    public class Slot
    {
        private readonly SlotsHolder _slotHolder;
        
        private Item _storingItem;
        private bool _isLocked;
        public event Action SlotUpdated;
        public event Action StoredItemUpdated;

        public Slot(SlotsHolder slotHolder, bool isLocked)
        {
            _slotHolder = slotHolder;
            _isLocked = isLocked;
        }

        public Slot(SlotsHolder slotHolder, Item storingItem)
        {
            _slotHolder = slotHolder;
            _storingItem = storingItem;
            _isLocked = false;
        }

        /// <summary>
        /// Puts an item in the slot with operation results.
        /// </summary>
        /// <param name="isSucceeded"> Is placing operation succeeded</param>
        public void PutItem(Item itemToPlace, out bool isSucceeded) =>
            isSucceeded = TryStoreItem(itemToPlace);

        /// <summary>
        /// Puts an item in the slot without operation result
        /// </summary>
        public void PutItem(Item itemToPlace) =>
            TryStoreItem(itemToPlace);

        /// <summary>
        /// Replaces storing item, completely removing the old one.
        /// </summary>
        /// <param name="newItem">Replacing item</param>
        public void ReplaceItem(Item newItem)
        {
            RemoveStoringItem();
            PutItem(newItem);
        }
        
        /// <summary>
        /// If a slot stores an item - removes it.
        /// </summary>
        public void RemoveStoringItem()
        {
            _storingItem = null;
            OnSlotUpdated();
        }

        /// <summary>
        /// Unlocks slot, allowing it to store items.
        /// </summary>
        public void Unlock()
        {
            _isLocked = false;
            OnSlotUpdated();
        }

        /// <summary>
        /// Locks slot, preventing it from storing items.
        /// </summary>
        public void Lock()
        {
            _isLocked = true;
            OnSlotUpdated();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Stores item if slot is unlocked and it is empty or itemLevels are the same.
        /// </summary>
        /// <returns>Operation result</returns>
        private bool TryStoreItem(Item itemToPlace)
        {
            bool isSucceeded = false;
            if (!IsLocked)
            {
                if (IsEmpty)
                {
                    _storingItem = itemToPlace;
                    isSucceeded = true;
                }
                else if (itemToPlace.Level == _storingItem.Level)
                {
                    _storingItem.Upgrade();
                    isSucceeded = true;
                }
            }
            
            if (isSucceeded)
                OnStoringItemUpdated();
            
            return isSucceeded;
        }

        private void OnSlotUpdated()
        {
            _slotHolder.SaveProgress();
            SlotUpdated?.Invoke();
        }

        private void OnStoringItemUpdated()
        {
            _slotHolder.SaveProgress();
            StoredItemUpdated?.Invoke();
        }

        public Item StoringItem => _storingItem;
        public bool IsLocked => _isLocked;
        public bool IsEmpty => _storingItem == null;
    }
}