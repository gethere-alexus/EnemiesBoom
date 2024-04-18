using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories.UI
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