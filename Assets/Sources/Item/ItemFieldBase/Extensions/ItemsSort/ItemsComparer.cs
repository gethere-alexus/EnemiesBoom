namespace Sources.Item.ItemFieldBase.Extensions.ItemsSort
{
    public static class ItemsComparer
    {
        public static bool CompareFromHighLevelToLow(ItemBase.Item a, ItemBase.Item b) => 
            a?.Level < b?.Level;
        public static bool CompareFromLowLevelToHigh(ItemBase.Item a, ItemBase.Item b) => 
            a?.Level > b?.Level;
    }
}