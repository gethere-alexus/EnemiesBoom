using Infrastructure.Services;

namespace Infrastructure.Factories.UI
{
    /// <summary>
    /// Creates ui menus
    /// </summary>
    public interface IUIMenuFactory : IService
    {
        void CreateBottomMenu();
    }
}