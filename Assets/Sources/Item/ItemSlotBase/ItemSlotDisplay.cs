﻿using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Sources.Item.ItemBase;
using Sources.Item.ItemFieldBase;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Item.ItemSlotBase
{
    /// <summary>
    /// Represents a slot, instantiating and displaying it.
    /// </summary>
    public class ItemSlotDisplay : MonoBehaviour, IItemStorage
    {
        [Header("Configuration")]
        [SerializeField, Tooltip("SlotInstance's background")] private Image _frame;

        private IAssetProvider _assetProvider;
        private ItemDrag _itemDrag;

        private ItemSlot _itemSlotInstance;

        public void Construct(IAssetProvider assetProvider, ItemField itemField, ItemDrag itemDrag)
        {
            _itemDrag = itemDrag;
            _assetProvider = assetProvider;
            _itemSlotInstance = new ItemSlot(itemField);
            
            _itemSlotInstance.SlotUpdated += UpdateView;
            _itemSlotInstance.StoredItemUpdated += UpdateView;
            
            UpdateView();
        }

        public void Store(ItemBase.Item item, out bool isSucceeded, IItemStorage previousStorage = null)
        {
            isSucceeded = false;
            if (previousStorage != null)
                previousStorage.ClearStorage();
            _itemSlotInstance?.PutItem(item, out isSucceeded);
        }

        public void Store(ItemBase.Item item, IItemStorage previousStorage = null)
        {
            if (previousStorage != null)
                previousStorage.ClearStorage();
            _itemSlotInstance?.PutItem(item, out bool isSucceeded);
        }

        public void ClearStorage() => 
            _itemSlotInstance.RemoveStoringItem();

        /// <summary>
        /// Once a slot is changing its condition, view will be updated afterwards
        /// </summary>
        private void UpdateView()
        {
            if (_itemSlotInstance.IsLocked)
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
            
            if (!_itemSlotInstance.IsEmpty)
            {
                ItemDisplay itemDisplay = _assetProvider.Instantiate<ItemDisplay>(ItemPaths.Item, transform);
                itemDisplay.Construct(_itemSlotInstance.StoringItem ,this, _itemDrag);
            }
        }

        private void OnDisable()
        {
            _itemSlotInstance.SlotUpdated -= UpdateView;
            _itemSlotInstance.StoredItemUpdated -= UpdateView;
        }

        private bool IsItemDisplayed => transform.childCount != 0;

        public ItemSlot ItemSlotInstance => _itemSlotInstance;
    }
}