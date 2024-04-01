using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Item;
using Infrastructure.ProgressData.Slot;
using Sources.ItemBase;
using Sources.SlotBase;
using Sources.SlotsHolderBase;
using Unity.VisualScripting;

namespace Infrastructure.DataExtensions
{
    public static class JsonExtensions
    {
        public static SlotData ToSerializable(this Slot slot)
        {
            SlotData toReturn = new SlotData
            {
                StoringItem = slot.StoringItem?.ToSerializable(),
                IsLocked = slot.IsLocked
            };

            return toReturn;
        }

        public static FieldData ToSerializable(this Slot[] grid)
        {
            SlotData[] serializableField = new SlotData[grid.Length];

            for (int i = 0; i < serializableField.Length; i++)
            {
                serializableField[i] = grid[i].ToSerializable();
            }

            FieldData toReturn = new FieldData()
            {
                Grid = serializableField,
            };

            return toReturn;
        }

        public static ItemData ToSerializable(this Item item)
        {
            ItemData toReturn = new ItemData()
            {
                Level = item.Level,
            };

            return toReturn;
        }

        public static Slot FromSerializable(this SlotData slot, SlotsHolder slotsHolder)
        {
            Slot toReturn;
            
            if (!slot.IsLocked && slot.StoringItem.Level != 0)
                toReturn = new Slot(slotsHolder, slot.StoringItem.FromSerializable());
            else
                toReturn = new Slot(slotsHolder,slot.IsLocked);

            return toReturn;
        }

        public static Item FromSerializable(this ItemData item) => 
            new Item(item.Level);

        public static Slot[] FromSerializable(this FieldData serializableField, SlotsHolder slotsHolder)
        {
            Slot[] toReturn = new Slot[serializableField.Grid.Length];

            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = serializableField.Grid[i].FromSerializable(slotsHolder);
            }

            return toReturn;
        }
    }
}