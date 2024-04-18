using System;
using Infrastructure.Services;
using Infrastructure.Services.WindowProvider;
using Sources.Windows;

namespace Infrastructure.Factories.Windows
{
    /// <summary>
    /// Creates window
    /// </summary>
    public interface IWindowsFactory : IService
    {
        /// <summary>
        /// Instantiate window.
        /// </summary>
        /// <param name="window">Window to create</param>
        /// <param name="windowOrder">Canvas sorting order</param>
        /// <param name="onClosing">Action on window closing</param>
        /// <returns>Instantiated window</returns>
        WindowBase CreateWindow(WindowType window, int windowOrder, Action onClosing = null);

        /// <summary>
        /// Instantiates window with payload
        /// </summary>
        /// <param name="window">Window to create</param>
        /// <param name="windowOrder">Canvas sorting order</param>
        /// <param name="payload">Payload to send</param>
        /// <param name="onClosing">Action on window closing</param>
        /// <typeparam name="TPayload"></typeparam>
        /// <returns>Instantiated window</returns>
        WindowBase CreateWindow<TPayload>(WindowType window, int windowOrder, TPayload payload,
            Action onClosing = null);
    }
}