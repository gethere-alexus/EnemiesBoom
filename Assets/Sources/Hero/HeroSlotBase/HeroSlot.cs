using System;
using Infrastructure.ProgressData;
using Sources.Item.ItemBase;
using Sources.Item.ItemFieldBase;

namespace Sources.Hero.HeroSlotBase
{
    [Serializable]
    public class HeroSlot
    {
        private readonly ItemField _itemField;
        private HeroData _storedHero;
        private Item.ItemBase.Item _storedItem;

        public event Action SlotInformationUpdated;

        public HeroSlot(ItemField itemField) => 
            _itemField = itemField;

        public void SetActiveHero(HeroData hero)
        {
            _storedHero = hero;
            if (_storedItem == null)
            {
                SetStoredItem(new Item.ItemBase.Item(1));
            }
            SlotInformationUpdated?.Invoke();
        }

        public void RemoveStoredItem()
        {
            if (IsHeroStored && !IsDefaultItemStored)
            {
                _itemField.FirstEmpty?.PutItem(_storedItem);
                _storedItem.SetLevel(1);
            }
        }

        public void SetStoredItem(Item.ItemBase.Item item)
        {
            if (IsHeroStored)
            {
                _storedItem = item;
                SlotInformationUpdated?.Invoke();
            }
        }

        public void SetStoredItem(Item.ItemBase.Item item, out bool isSucceeded, IItemStorage previousStorage = null)
        {
            isSucceeded = false;
            if (IsHeroStored)
            {
                if (!IsDefaultItemStored)
                    ReturnStoredItem(previousStorage);
                
                _storedItem = item;
                isSucceeded = true;
                SlotInformationUpdated?.Invoke();
            }
        }

        private void ReturnStoredItem(IItemStorage previousStorage = null)
        {
            if (_itemField.FirstEmpty != null)
            {
                _itemField.FirstEmpty?.PutItem(_storedItem);
            }
            else if(previousStorage !=  null)
            {
                previousStorage.ClearStorage();
                previousStorage.Store(_storedItem);
            }
            
        }
        public bool IsDefaultItemStored => _storedItem?.Level == 1;
        public bool IsHeroStored => _storedHero != null;
        public HeroData StoredHero => _storedHero;

        public Item.ItemBase.Item StoredItem => _storedItem;
    }
}