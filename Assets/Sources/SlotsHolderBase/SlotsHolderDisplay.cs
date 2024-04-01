using Infrastructure.Configurations.SlotsField;
using Infrastructure.Extensions.DataExtensions;
using Infrastructure.ProgressData.Field;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ProgressProvider;
using Sources.SlotBase;
using UnityEngine;

namespace Sources.SlotsHolderBase
{
    /// <summary>
    /// View model of slots holder, the main purpose is to build a grid and initialize the model.
    /// </summary>
    public class SlotsHolderDisplay : MonoBehaviour
    {
        [SerializeField, Tooltip("Where all the slots will be stored")]
        private Transform _slotStorage;

        [SerializeField, Tooltip("Transform for an item when it is being dragged")]
        private Transform _itemDraggingParent;

        private SlotsHolder _slotsHolderInstance;

        /// <summary>
        /// Constructor is called to build a grid from config
        /// </summary>
        public void Construct(IAssetProvider assetProvider, IProgressProvider progressProvider,
            SlotsFieldConfiguration slotsFieldConfig)
        {
            _slotsHolderInstance = new SlotsHolder(progressProvider, slotsFieldConfig);
            InstantiateGridAsNew(assetProvider, _slotsHolderInstance.Grid, slotsFieldConfig.UnlockedSlots);
        }

        /// <summary>
        /// Constructor is called to build a grid from save.
        /// </summary>
        public void Construct(IAssetProvider assetProvider, IProgressProvider progressProvider,
            FieldData data)
        {
            _slotsHolderInstance = new SlotsHolder(progressProvider, data);
            InstantiateGridAsReferenced(assetProvider, _slotsHolderInstance.Grid, data.FromSerializable(_slotsHolderInstance));
        }

        
        /// <summary>
        /// Instantiate a grid creating empty slots
        /// </summary>
        /// <param name="assetProvider"></param>
        /// <param name="grid">drawing grid</param>
        /// <param name="slotsToUnlock">Amount of unlocking slots</param>
        private void InstantiateGridAsNew(IAssetProvider assetProvider, Slot[] grid, int slotsToUnlock)
        {
            ClearStorage();

            int unlockedSlots = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                bool isSlotLocked = unlockedSlots >= slotsToUnlock;
                if (!isSlotLocked)
                    unlockedSlots++;
                
                SlotDisplay slotDisplay = assetProvider.Instantiate<SlotDisplay>(AssetPaths.SlotTemplate, _slotStorage);
                slotDisplay.Construct(assetProvider, _slotsHolderInstance, _itemDraggingParent, isSlotLocked);
                
                grid[i] = slotDisplay.SlotInstance;
            }
        }

        /// <summary>
        /// Instantiates a grid from a grid refernce
        /// </summary>
        /// <param name="assetProvider">Asset Provider Service</param>
        /// <param name="grid">Where to instantiate</param>
        /// <param name="gridReference">Instantiating reference</param>
        private void InstantiateGridAsReferenced(IAssetProvider assetProvider, Slot[] grid, Slot[] gridReference)
        {
            ClearStorage();

            for (int i = 0; i < gridReference.Length; i++)
            {
                SlotDisplay slotDisplay = assetProvider.Instantiate<SlotDisplay>(AssetPaths.SlotTemplate, _slotStorage);
                slotDisplay.Construct(assetProvider, _slotsHolderInstance, _itemDraggingParent, gridReference[i]);
                
                grid[i] = slotDisplay.SlotInstance;
            }
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