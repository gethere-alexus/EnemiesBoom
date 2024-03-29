using Sources.ItemBase;

namespace Sources.SlotsHolderBase.Extensions.ItemsSort
{
    public static class ItemsComparer
    {
        public static bool CompareFromHighLevelToLow(Item a, Item b) => 
            a?.Level < b?.Level;
        public static bool CompareFromLowLevelToHigh(Item a, Item b) => 
            a?.Level > b?.Level;
    }
}