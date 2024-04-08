using UnityEngine;

namespace Infrastructure.Services.Factories.UI
{
    /// <summary>
    /// Builds UI
    /// </summary>
    public interface IUIFactory : IService
    {
        Canvas CreateUIRoot();
        void CreateBottomMenu();
        Canvas UIRoot { get; }
    }
}