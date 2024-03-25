using Infrastructure.Services.AssetsProvider;
using Sources.ItemBase;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.SlotBase
{
    /// <summary>
    /// Represents a slot 
    /// </summary>
    public class SlotDisplay : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField, Tooltip("SlotInstance's background")] private Image _frame;

        private IAssetProvider _assetProvider;
        private Transform _itemDraggingParent;
        
        private Slot _slotInstance;

        public void Construct(IAssetProvider assetProvider, Transform itemDraggingParent)
        {
            _itemDraggingParent = itemDraggingParent;
            _assetProvider = assetProvider;
            
            _slotInstance = new Slot();
            _slotInstance.SlotConditionUpdated += UpdateView;
        }

        /// <summary>
        /// Once a slot is changing its condition, view will be updated afterwards
        /// </summary>
        private void UpdateView()
        {
            if(_slotInstance.IsLocked)
                _frame.color = Color.black; // simplified for not focusing on graphical part, but to highlight that the slot is locked
            
            ClearSlotStorage();
            DisplayStoredItem();
        }

        private void ClearSlotStorage()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        private void DisplayStoredItem()
        {
            if(IsItemDisplayed)
                ClearSlotStorage();
            
            if (!_slotInstance.IsEmpty)
            {
                ItemDisplay itemDisplay = _assetProvider.Instantiate<ItemDisplay>(AssetPaths.Item, transform);
                itemDisplay.Construct(_slotInstance.StoringItem ,this, _itemDraggingParent);
            }
        }

        private void OnDisable()
        {
            _slotInstance.SlotConditionUpdated -= UpdateView;
        }

        private bool IsItemDisplayed => transform.childCount != 0;
        public Slot SlotInstance => _slotInstance;
    }
}