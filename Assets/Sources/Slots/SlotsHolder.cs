using System.Linq;
using Sources.Infrastructure.Services.AssetsProvider;
using Sources.Items;
using UnityEngine;
using Zenject;

namespace Sources.Slots
{
    // Component is responsible for instantiating and operating the slots;
    public class SlotsHolder : MonoBehaviour 
    {
        [Header("Setting Up")]
        [SerializeField, Tooltip("Where all the slots are going to be stored")] private Transform _slotStorage;
        
        [Header("Configuration")]
        [SerializeField, Tooltip("Amount of initially created slots")] private int _initialSlots;
        [SerializeField, Tooltip("Amount of unlocked slots, can't be more than init. slots")] private int _unlockedSlots;

        private Slot[] _grid; // storing all the instantiated slots

        [Inject]
        public void Construct(IAssetProvider assetProvider) => 
            ConstructGrid(assetProvider);

        public void PlaceArrow(Arrow arrowToPlace, out bool isOperationSucceeded )
        {
            Slot storage = GetEmptySlot();
            
            if (storage != null)
            {
                storage.PutArrow(arrowToPlace);
                isOperationSucceeded = true;
            }
            else
            {
                isOperationSucceeded = false;
            }
        }

        // looping through the all unlocked slots, returning the first unlocked. If there is not such a slot - null is returned
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

        private void ConstructGrid(IAssetProvider assetProvider)
        {
            ClearStorage();
            CreateGrid(assetProvider);
        }

        private void CreateGrid(IAssetProvider assetProvider)
        {
            _grid = new Slot[_initialSlots];
            int unlockedSlots = 0;
            for (int i = 0; i < _initialSlots; i++)
            {
                SlotDisplay slotDisplay = assetProvider.Instantiate<SlotDisplay>(AssetPaths.SlotTemplate, _slotStorage);
                slotDisplay.Construct();
                
                Slot slot = slotDisplay.Slot;
                
                // Locking slots
                if (unlockedSlots <= _unlockedSlots)
                    unlockedSlots++;
                else
                    slot.Lock();
                
                _grid[i] = slot;
            }
        }

        private void ClearStorage() 
        {
            // Clears the slots storage if there is any existing slots
            if (_slotStorage.childCount != 0)
            {
                foreach (Transform child in _slotStorage)
                    Destroy(child.gameObject);
            }
        }

        private void OnValidate()
        {
            if (_unlockedSlots > _initialSlots) // Clarifying that there aren't more unlocked slots than available
                _unlockedSlots = _initialSlots;
        }
    }
}