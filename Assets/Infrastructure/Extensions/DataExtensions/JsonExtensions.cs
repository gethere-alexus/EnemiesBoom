using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Field.Slot;
using Infrastructure.ProgressData.Item;
using Sources.Hero.HeroSlotBase;
using Sources.Item.ItemBase;
using Sources.Item.ItemFieldBase;
using Sources.Item.ItemSlotBase;

namespace Infrastructure.Extensions.DataExtensions
{
    /// <summary>
    /// Exposing API for converting data to/from serializable format.
    /// </summary>
    public static class JsonExtensions
    {
        public static ActiveHeroData[] ToSerializable(this HeroSlot[] slots)
        {
            ActiveHeroData[] toReturn = new ActiveHeroData[slots.Length];
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = slots[i].ToSerializable();
            }

            return toReturn;
        }

        private static ActiveHeroData ToSerializable(this HeroSlot hero)
        {
            const int nullHeroID = -1;
            ActiveHeroData toReturn = new ActiveHeroData()
            {
                HeroID = hero.StoredHero?.ID ?? nullHeroID,
                StoredItem = hero.StoredItem.ToSerializable(),
            };
            return toReturn;
        }

        public static HeroSlot[] FromSerializable(this ActiveHeroData[] heroesData, HeroData[] gameHeroes)
        {
            HeroSlot[] toReturn = new HeroSlot[heroesData.Length];
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i].SetActiveHero(gameHeroes.GetHeroByID(heroesData[i].HeroID));
            }

            return toReturn;
        }

        #region Slot

        /// <summary>
        /// Converts Slot to JSON convertable format
        /// </summary>
        private static SlotData ToSerializable(this ItemSlot itemSlot)
        {
            SlotData toReturn = new SlotData
            {
                StoringItem = itemSlot.StoringItem?.ToSerializable(),
                IsLocked = itemSlot.IsLocked
            };

            return toReturn;
        }

        /// <summary>
        /// Converts Slot from JSON convertable format.
        /// </summary>
        public static ItemSlot FromSerializable(this SlotData slot, ItemField itemField)
        {
            ItemSlot toReturn;

            if (!slot.IsLocked && slot.StoringItem.Level != 0)
            {
                toReturn = new ItemSlot(itemField);
                toReturn.PutItem(slot.StoringItem.FromSerializable());
            }
            else
            {
                toReturn = new ItemSlot(itemField);
                if (!slot.IsLocked)
                    toReturn.Unlock();
            }

            return toReturn;
        }

        #endregion

        #region Item

        /// <summary>
        /// Converts Item from JSON convertable format.
        /// </summary>
        public static Item FromSerializable(this ItemData item) =>
            new Item(item.Level);

        /// <summary>
        /// Converts Item to JSON convertable format
        /// </summary>
        public static ItemData ToSerializable(this Item item)
        {
            const int nullItemLevel = 0;
            
            ItemData toReturn = new ItemData()
            {
                Level = item?.Level ?? nullItemLevel,
            };

            return toReturn;
        }

        #endregion

        #region GameField

        /// <summary>
        /// Converts Grid data from JSON convertable format.
        /// </summary>
        public static ItemSlot[] FromSerializable(this GameFieldData serializableGameField, ItemField itemField)
        {
            ItemSlot[] toReturn = new ItemSlot[serializableGameField.Grid.Length];

            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = serializableGameField.Grid[i].FromSerializable(itemField);
            }

            return toReturn;
        }

        /// <summary>
        /// Converts Slots array to JSON convertable format
        /// </summary>
        public static GameFieldData ToSerializable(this ItemSlot[] grid)
        {
            SlotData[] serializableField = new SlotData[grid.Length];

            for (int i = 0; i < serializableField.Length; i++)
            {
                serializableField[i] = grid[i].ToSerializable();
            }

            GameFieldData toReturn = new GameFieldData()
            {
                Grid = serializableField,
            };

            return toReturn;
        }

        #endregion
    }
}