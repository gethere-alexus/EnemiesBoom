using System;
using Infrastructure.ProgressData.Item;

namespace Infrastructure.ProgressData
{
    [Serializable]
    public class ActiveHeroData
    {
        public int HeroID;
        public ItemData StoredItem;
    }
}