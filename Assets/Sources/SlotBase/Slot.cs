using System;
using Sources.ItemBase;

namespace Sources.SlotBase
{
    /// <summary>
    /// A single cell where the item can be stored, being instantiated from SlotsHolder
    /// By default, cell doesn't contain any arrow and it is unlocked.
    /// </summary>
    public class Slot
    {
        private Item _storingItem;
        private bool _isLocked; 
        public event Action SlotConditionUpdated;

        public void PutItem(Item itemToPlace, out bool isSucceeded) =>
            isSucceeded = TryStoreItem(itemToPlace);

        public void PutItem(Item itemToPlace) =>
            TryStoreItem(itemToPlace);

        public void RemoveStoringItem()
        {
            _storingItem = null;
            SlotConditionUpdated?.Invoke();
        }

        public void Unlock()
        {
            _isLocked = false;
            SlotConditionUpdated?.Invoke();
        }

        public void Lock()
        {
            _isLocked = true;
            SlotConditionUpdated?.Invoke();
        }

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
                else
                {
                    _storingItem.TryMerge(itemToPlace, out isSucceeded);
                }
            }
            
            if(isSucceeded)
                SlotConditionUpdated?.Invoke();
            
            return isSucceeded;
        }

        public Item StoringItem => _storingItem;

        public bool IsLocked => _isLocked;

        public bool IsEmpty => _storingItem == null;
    }
}