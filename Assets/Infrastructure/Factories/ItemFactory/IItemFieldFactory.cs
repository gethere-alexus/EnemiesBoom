using Infrastructure.Services;
using Sources.ItemsBase.ItemFieldBase;

namespace Infrastructure.Factories.ItemFactory
{
    /// <summary>
    /// Creates item field, its control and extensions
    /// </summary>
    public interface IItemFieldFactory : IService
    {
        void CreateItemField();

        /// <summary>
        /// Creates operators for slots.
        /// </summary>
        void CreateFieldControl();

        /// <summary>
        /// Creates extensions for the field
        /// </summary>
        void CreateFieldExtensions();


        FieldControl FieldControl { get; }
    }
}