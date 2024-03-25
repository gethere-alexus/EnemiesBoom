namespace Infrastructure.Services.Factories.UIFactory
{
    public interface IUIFactory
    {
        /// <summary>
        /// Creates the ui root, overwrites existing one(if there is)
        /// </summary>
        void CreateUIRoot();
        
        /// <summary>
        /// Creates the slots where the arrows are being stored.
        /// </summary>
        void CreateSlotsUI();

        /// <summary>
        /// Creates operating buttons for slots.
        /// </summary>
        void CreateSlotsControlUI();
    }
}