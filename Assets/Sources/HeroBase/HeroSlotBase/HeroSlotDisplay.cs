using System;
using Sources.ItemsBase.ItemBase;
using TMPro;
using UnityEngine;

namespace Sources.HeroBase.HeroSlotBase
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

        public void Store(Item item, out bool isSucceeded, IItemStorage previousStorage = null)
        {
            isSucceeded = false;
            
            _slotInstance.SetStoredItem(item, out isSucceeded, previousStorage);
            
            if (previousStorage != null && isSucceeded)
                previousStorage.ClearStorage();
            
        }

        public void Store(Item item, IItemStorage previousStorage = null)
        {
            _slotInstance.SetStoredItem(item, out bool isSucceeded, previousStorage);
            if (previousStorage != null && isSucceeded)
                previousStorage.ClearStorage();
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
        public Transform Storage => gameObject.transform;
    }
}