using UnityEngine;

namespace Infrastructure.Services.Factories.UI
{
    /// <summary>
    /// Builds UI
    /// </summary>
    public interface IUIFactory
    {
        Canvas CreateUIRoot();
        Canvas UIRoot { get; }
    }
}