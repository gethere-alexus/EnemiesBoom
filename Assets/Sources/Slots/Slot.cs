using System;
using Sources.Items;

namespace Sources.Slots
{
    // a single cell where the item can be stored, being instantiated from SlotsHolder
    // By default, cell doesn't contain any arrow and it is unlocked.
    public class Slot
    {
        private readonly SlotDisplay _slotRepresenter; 
        
        private bool _isLocked; // is interaction with the slot locked
        private bool _isEmpty; // does the slot contains an item inside

        public event Action SlotConditionUpdated;

        public Slot(SlotDisplay slotDisplay)
        {
            _slotRepresenter = slotDisplay;
            _isEmpty = true;
            _isLocked = false;
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
        

        public bool IsLocked => _isLocked;
        public bool IsEmpty => _isEmpty;

        public void PutArrow(Arrow arrowToPlace)
        {
            _isEmpty = false;
            SlotConditionUpdated?.Invoke();
        }
    }
}