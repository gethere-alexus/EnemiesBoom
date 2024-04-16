namespace Infrastructure.Services.Factories.UIFactory
{
    /// <summary>
    /// Creates ui menus
    /// </summary>
    public interface IUIMenuFactory : IService
    {
        void CreateBottomMenu();
    }
}