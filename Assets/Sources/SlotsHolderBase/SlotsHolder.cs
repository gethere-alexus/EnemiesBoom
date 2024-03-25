using System.Linq;
using Infrastructure.Configurations;
using Sources.ItemBase;
using Sources.SlotBase;

namespace Sources.SlotsHolderBase
{
    public class SlotsHolder
    {
        private readonly int _initialSlots;
        private readonly Slot[] _grid;
        
        private int _unlockedSlots;

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
            
            if (storage != null)
            {
                storage.PutItem(itemToPlace);
                isOperationSucceeded = true;
            }
            else
            {
                isOperationSucceeded = false;
            }
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