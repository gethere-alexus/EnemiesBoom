using System;
using Infrastructure.ProgressData.Hero;
using Sources.ItemsBase.ItemBase;
using Sources.ItemsBase.ItemFieldBase;

namespace Sources.HeroBase.HeroSlotBase
{
    [Serializable]
    public class HeroSlot
    {
        private readonly ItemField _itemField;
        private HeroData _storedHero;
        private Item _storedItem;

        public event Action SlotInformationUpdated;

        public HeroSlot(ItemField itemField) => 
            _itemField = itemField;

        public void SetActiveHero(HeroData hero, bool setDefaultItem)
        {
            if (hero != null)
            {
                _storedHero = hero;
                
                if(setDefaultItem)
                    _storedItem ??= new Item();
                
                SlotInformationUpdated?.Invoke();                
            }
        }

        public void RemoveStoredItem()
        {
            if (IsHeroStored && !IsDefaultItemStored)
            {
                _itemField.FirstEmpty?.PutItem(_storedItem);
                _storedItem.SetDefaultLevel();
            }
        }

        public void SetStoredItem(Item item)
        { 
           SetStoredItem(item, out bool isSucceeded);
        }

        public void SetStoredItem(Item item, out bool isSucceeded, IItemStorage previousStorage = null)
        {
            isSucceeded = false;
            if (IsHeroStored)
            {
                if (IsItemStored)
                {
                    if (_storedItem.Level == item.Level)
                    {
                        _storedItem.Upgrade();
                    }
                    else if (!IsDefaultItemStored)
                    {
                        ReturnStoredItem(previousStorage);
                        _storedItem = item;
                    }
                    else
                    {
                        _storedItem = item;
                    }
                }
                else
                {
                    _storedItem = item;
                }
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

        public bool IsItemStored => _storedItem != null;
        public bool IsHeroStored => _storedHero != null;
        public bool IsDefaultItemStored => _storedItem.IsDefaultItemLevel;
        public HeroData StoredHero => _storedHero;

        public Item StoredItem => _storedItem;
    }
}