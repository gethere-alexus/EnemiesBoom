using System;
using Infrastructure.ProgressData.Item;

namespace Infrastructure.ProgressData.Slot
{
    [Serializable]
    public class SlotData
    {
        public bool IsLocked;
        public ItemData StoringItem;
    }
}