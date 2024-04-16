namespace Infrastructure.Services.WindowProvider
{
    /// <summary>
    /// Service is operating with the windows by WindowType, with/without payload.
    /// </summary>
    public interface IWindowsProvider : IService
    {
        /// <summary>
        /// Instantiates window.
        /// </summary>
        void OpenWindow(WindowType window);
        /// <summary>
        /// Instantiates window with payload.
        /// </summary>
        void OpenWindow<TPayload>(WindowType window, TPayload payload);
        
        /// <summary>
        /// Closes window if it was opened before.
        /// </summary>
        void CloseWindow(WindowType window);
    }
}