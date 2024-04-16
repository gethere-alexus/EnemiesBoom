using System;
using Sources.Item.ItemBase;
using TMPro;
using UnityEngine;

namespace Sources.Hero.HeroSlotBase
{
    public class HeroSlotDisplay : MonoBehaviour, IItemStorage
    {
        [SerializeField] private TMP_Text _storedHeroIDText;
        [SerializeField] private TMP_Text _storedItemLevelText;
        
        private HeroSlot _slotInstance;

        public void Construct(HeroSlot presenter)
        {
            _slotInstance = presenter;
            _slotInstance.SlotInformationUpdated += UpdateView;
            
            UpdateView();
        }

        public void Store(Item.ItemBase.Item item, out bool isSucceeded, IItemStorage previousStorage = null)
        {
            isSucceeded = false;
            if (previousStorage != null)
                previousStorage.ClearStorage();
            _slotInstance.SetStoredItem(item, out isSucceeded, previousStorage);
            
        }

        public void Store(Item.ItemBase.Item item, IItemStorage previousStorage = null)
        {
            if (previousStorage != null)
                previousStorage.ClearStorage();
            
            _slotInstance.SetStoredItem(item, out bool isSucceeded, previousStorage);
        }

        public void ClearStorage()
        {
            _slotInstance.RemoveStoredItem();
        }

        private void UpdateView()
        {
            _storedHeroIDText.text = _slotInstance.StoredHero?.ID.ToString() ?? String.Empty;
            _storedItemLevelText.text = _slotInstance.StoredItem?.Level.ToString() ?? String.Empty;
        }

        private void OnDisable()
        {
            _slotInstance.SlotInformationUpdated -= UpdateView;
        }

        public HeroSlot SlotInstance => _slotInstance;
    }
}