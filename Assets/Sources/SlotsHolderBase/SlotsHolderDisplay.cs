using Infrastructure.Configurations;
using Infrastructure.Configurations.SlotsField;
using Infrastructure.Services.AssetsProvider;
using Sources.SlotBase;
using UnityEngine;

namespace Sources.SlotsHolderBase
{
    /// <summary>
    /// View model of slots holder, the main purpose is to build a grid and initialize the model.
    /// </summary>
    public class SlotsHolderDisplay : MonoBehaviour
    {
        [SerializeField, Tooltip("Where all the slots will be stored")] private Transform _slotStorage;
        [SerializeField, Tooltip("Transform for an item when it is being dragged")] private Transform _itemDraggingParent;
        
        private SlotsHolder _slotsHolderInstance;
        
        public void Construct(IAssetProvider assetProvider, SlotsFieldConfiguration slotsFieldConfig)
        {
            _slotsHolderInstance = new SlotsHolder(slotsFieldConfig);
            InstantiateGrid(assetProvider, _slotsHolderInstance.Grid, _slotsHolderInstance.UnlockedSlots);
        }
        
        /// <param name="grid">drawing grid</param>
        /// <param name="unlockedSlots">amount of initially unlocked slots</param>
        private void InstantiateGrid(IAssetProvider assetProvider, Slot[] grid, int unlockedSlots)
        {
            ClearStorage();
            
            int alreadyUnlockedSlots = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                SlotDisplay slotDisplay = assetProvider.Instantiate<SlotDisplay>(AssetPaths.SlotTemplate, _slotStorage);
                slotDisplay.Construct(assetProvider,_slotsHolderInstance, _itemDraggingParent);
                
                Slot slot = slotDisplay.SlotInstance;
                // Locking slots
                if (alreadyUnlockedSlots <= unlockedSlots)
                    alreadyUnlockedSlots++;
                else
                    slot.Lock();
                
                grid[i] = slot;
            }
             
            _slotsHolderInstance.OnGridBuild();
        }

        /// <summary>
        /// Clears grid storage if there is some game objects.
        /// </summary>
        private void ClearStorage() 
        {
            // Clears the slots storage if there is any existing slots
            if (_slotStorage.childCount != 0)
            {
                foreach (Transform child in _slotStorage)
                    Destroy(child.gameObject);
            }
        }

        public SlotsHolder SlotsHolderInstance => _slotsHolderInstance;
    }
}