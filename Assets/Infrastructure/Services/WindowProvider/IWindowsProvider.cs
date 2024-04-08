namespace Infrastructure.Services.WindowProvider
{
    public interface IWindowsProvider : IService
    {
        
        void OpenWindow(WindowType window);
        void CloseWindow(WindowType window);
    }
}