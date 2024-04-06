namespace Infrastructure.Services.Factories.Field
{
    /// <summary>
    /// Creates game field, its control and extensions
    /// </summary>
    public interface IGameFieldFactory
    {
        
        /// <summary>
        /// Creates the slots where the arrows are being stored.
        /// </summary>
        void CreateField();

        /// <summary>
        /// Creates operators for slots.
        /// </summary>
        void CreateFieldControl();
        
        /// <summary>
        /// Creates extensions for the field
        /// </summary>
        void CreateFieldExtensions();
    }
}