using System;
using Infrastructure.ProgressData.Field.Slot;

namespace Infrastructure.ProgressData.Field
{
    [Serializable]
    public class GameFieldData : IProgressData
    {
        public SlotData[] Grid;
    }
}