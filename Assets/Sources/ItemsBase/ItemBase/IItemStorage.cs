namespace Sources.ItemsBase.ItemBase
{
    public interface IItemStorage
    {
        void Store(Item item, out bool isSucceeded, IItemStorage previousStorage = null);
        void Store(Item item, IItemStorage previousStorage = null);
        void ClearStorage();
    }
}