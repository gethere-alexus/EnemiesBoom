using Infrastructure.Services.AssetsProvider;
using Sources.ItemBase;
using Sources.SlotsHolderBase;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.SlotBase
{
    /// <summary>
    /// Represents a slot, instantiating and displaying it.
    /// </summary>
    public class SlotDisplay : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField, Tooltip("SlotInstance's background")] private Image _frame;

        private IAssetProvider _assetProvider;
        private SlotsHolder _slotsHolder;
        private Transform _itemDraggingParent;

        private Slot _slotInstance;

        public void Construct(IAssetProvider assetProvider, SlotsHolder slotsHolder, Transform itemDraggingParent)
        {
            _itemDraggingParent = itemDraggingParent;
            _assetProvider = assetProvider;
            _slotsHolder = slotsHolder;
            
            _slotInstance = new Slot();
            
            _slotInstance.SlotUpdated += UpdateView;
            _slotInstance.StoredItemUpdated += UpdateView;
        }

        /// <summary>
        /// Once a slot is changing its condition, view will be updated afterwards
        /// </summary>
        private void UpdateView()
        {
            if (_slotInstance.IsLocked)
                _frame.color =
                    Color.black; // simplified for not focusing on graphical part, but to highlight that the slot is locked
            else
                _frame.color = new Color(0.373f, 0.333f, 0.325f, 1.000f);
            
            ClearSlotStorage();
            DisplayStoredItem();
        }
        
        /// <summary>
        /// Deletes all the child of the display.
        /// </summary>
        private void ClearSlotStorage()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        /// <summary>
        /// If slot instance stores an item, it will display it.
        /// </summary>
        private void DisplayStoredItem()
        {
            if(IsItemDisplayed)
                ClearSlotStorage();
            
            if (!_slotInstance.IsEmpty)
            {
                ItemDisplay itemDisplay = _assetProvider.Instantiate<ItemDisplay>(AssetPaths.Item, transform);
                itemDisplay.Construct(_slotInstance.StoringItem ,_slotsHolder,this, _itemDraggingParent);
            }
        }

        private void OnDisable()
        {
            _slotInstance.SlotUpdated -= UpdateView;
            _slotInstance.StoredItemUpdated -= UpdateView;
        }

        private bool IsItemDisplayed => transform.childCount != 0;
        public Slot SlotInstance => _slotInstance;
    }
}