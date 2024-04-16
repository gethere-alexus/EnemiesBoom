using UnityEngine;

namespace Infrastructure.Services.Factories.UIFactory
{
    /// <summary>
    /// Creates and provide UI root
    /// </summary>
    public interface IUIRootFactory : IService
    {
        Canvas CreateUIRoot();
        Canvas UIRoot { get; }
    }
}