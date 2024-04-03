using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Field.Slot;
using Infrastructure.ProgressData.Item;
using Sources.GameFieldBase;
using Sources.ItemBase;
using Sources.SlotBase;

namespace Infrastructure.Extensions.DataExtensions
{
    /// <summary>
    /// Serializes data back and forth.
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts Slot to JSON convertable format
        /// </summary>
        public static SlotData ToSerializable(this Slot slot)
        {
            SlotData toReturn = new SlotData
            {
                StoringItem = slot.StoringItem?.ToSerializable(),
                IsLocked = slot.IsLocked
            };

            return toReturn;
        }

        /// <summary>
        /// Converts Slots array to JSON convertable format
        /// </summary>
        public static GameFieldData ToSerializable(this Slot[] grid)
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

        /// <summary>
        /// Converts Item to JSON convertable format
        /// </summary>
        public static ItemData ToSerializable(this Item item)
        {
            ItemData toReturn = new ItemData()
            {
                Level = item.Level,
            };

            return toReturn;
        }

        /// <summary>
        /// Converts Slot from JSON convertable format.
        /// </summary>
        public static Slot FromSerializable(this SlotData slot, GameField gameField)
        {
            Slot toReturn;

            if (!slot.IsLocked && slot.StoringItem.Level != 0)
            {
                toReturn = new Slot(gameField);
                toReturn.PutItem(slot.StoringItem.FromSerializable());
            }
            else
            {
                toReturn = new Slot(gameField);
                if (!slot.IsLocked)
                    toReturn.Unlock();
            }

            return toReturn;
        }

        /// <summary>
        /// Converts Item from JSON convertable format.
        /// </summary>
        public static Item FromSerializable(this ItemData item) => 
            new Item(item.Level);

        /// <summary>
        /// Converts Grid data from JSON convertable format.
        /// </summary>
        public static Slot[] FromSerializable(this GameFieldData serializableGameField, GameField gameField)
        {
            Slot[] toReturn = new Slot[serializableGameField.Grid.Length];

            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = serializableGameField.Grid[i].FromSerializable(gameField);
            }

            return toReturn;
        }
    }
}